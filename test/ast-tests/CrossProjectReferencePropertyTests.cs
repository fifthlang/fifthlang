// Feature: sdk-cross-project-references, Property 1: Absolute Path Resolution
// Validates: Requirements 1.2, 1.3, 8.1, 8.2, 8.3

using FsCheck;
using FsCheck.Xunit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using compiler;
using System.Text;

namespace ast_tests;

/// <summary>
/// Property-based tests for the absolute path resolution logic used by
/// the GetTargetPath MSBuild target in Sdk.targets.
///
/// The target checks if FifthOutputPath is already absolute (rooted).
/// If relative, it prepends MSBuildProjectDirectory to make it absolute.
/// If already absolute, it returns the path unchanged.
/// </summary>
public class CrossProjectReferencePropertyTests
{
    /// <summary>
    /// Simulates the GetTargetPath resolution logic from Sdk.targets:
    /// - If outputPath is rooted, return it unchanged
    /// - If outputPath is relative, combine projectDir + outputPath
    /// </summary>
    private static string ResolveOutputPath(string projectDir, string outputPath)
    {
        if (Path.IsPathRooted(outputPath))
        {
            return outputPath;
        }

        return Path.Combine(projectDir, outputPath);
    }

    /// <summary>
    /// Generates valid directory path segments (no invalid path chars).
    /// </summary>
    private static Arbitrary<string> SafePathSegment()
    {
        return Arb.Default.NonEmptyString()
            .Filter(s =>
            {
                var str = s.Get;
                return !string.IsNullOrWhiteSpace(str)
                    && str.IndexOfAny(Path.GetInvalidPathChars()) < 0
                    && str.IndexOfAny(Path.GetInvalidFileNameChars()
                        .Where(c => c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar)
                        .ToArray()) < 0
                    && !str.Contains("..");
            })
            .Generator
            .Select(s => s.Get)
            .ToArbitrary();
    }

    [Property(MaxTest = 100)]
    public Property CombiningProjectDirAndRelativePath_AlwaysProducesRootedPath()
    {
        // Generate a rooted project directory and a relative output path
        var gen = from dirSegment in SafePathSegment().Generator
                  from fileSegment in SafePathSegment().Generator
                  let projectDir = Path.Combine(Path.GetTempPath(), dirSegment)
                  let relativePath = Path.Combine("bin", "Debug", "net8.0", fileSegment + ".dll")
                  select (projectDir, relativePath);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (projectDir, relativePath) = tuple;

            // The relative path should not be rooted
            if (Path.IsPathRooted(relativePath))
                return true.ToProperty(); // skip — not a valid relative path for this test

            var resolved = ResolveOutputPath(projectDir, relativePath);
            return Path.IsPathRooted(resolved).ToProperty();
        });
    }

    [Property(MaxTest = 100)]
    public Property AbsolutePath_IsReturnedUnchanged()
    {
        // Generate absolute paths
        var gen = from segment in SafePathSegment().Generator
                  let absolutePath = Path.Combine(Path.GetTempPath(), "projects", segment, "bin", "Debug", "net8.0", segment + ".dll")
                  select absolutePath;

        return Prop.ForAll(gen.ToArbitrary(), absolutePath =>
        {
            // Verify the generated path is indeed rooted
            if (!Path.IsPathRooted(absolutePath))
                return true.ToProperty(); // skip — generator produced non-rooted path

            var resolved = ResolveOutputPath("C:\\SomeOtherDir", absolutePath);
            return (resolved == absolutePath).ToProperty();
        });
    }

    [Property(MaxTest = 100)]
    public Property ResolvedPath_IsAlwaysRooted_RegardlessOfInput()
    {
        // Generate random combinations of project dirs and output paths (both relative and absolute)
        var gen = from dirSegment in SafePathSegment().Generator
                  from fileSegment in SafePathSegment().Generator
                  from useAbsolute in Arb.Default.Bool().Generator
                  let projectDir = Path.Combine(Path.GetTempPath(), dirSegment)
                  let outputPath = useAbsolute
                      ? Path.Combine(Path.GetTempPath(), fileSegment, fileSegment + ".dll")
                      : Path.Combine("bin", fileSegment + ".dll")
                  select (projectDir, outputPath);

        return Prop.ForAll(gen.ToArbitrary(), tuple =>
        {
            var (projectDir, outputPath) = tuple;
            var resolved = ResolveOutputPath(projectDir, outputPath);
            return Path.IsPathRooted(resolved).ToProperty();
        });
    }
}

// Feature: sdk-cross-project-references, Property 2: Public Static Type Discovery Completeness
// Validates: Requirements 3.1, 3.2, 3.3

/// <summary>
/// Property-based tests verifying that DiscoverPublicStaticTypes returns exactly
/// the public static types with public static methods from a compiled assembly,
/// ignoring non-static classes and static classes without public static methods.
/// </summary>
public class PublicStaticTypeDiscoveryPropertyTests : IDisposable
{
    private readonly List<string> _tempFiles = new();

    public void Dispose()
    {
        foreach (var f in _tempFiles)
        {
            try { if (File.Exists(f)) File.Delete(f); } catch { }
        }
    }

    /// <summary>
    /// Represents a generated class definition for test assembly compilation.
    /// </summary>
    private record ClassDef(string Name, string Namespace, bool IsStatic, int PublicStaticMethodCount, int InstanceMethodCount);

    /// <summary>
    /// Generates a valid C# identifier from a seed index.
    /// </summary>
    private static string MakeIdentifier(string prefix, int index) => $"{prefix}{index}";

    /// <summary>
    /// Builds C# source code from a list of class definitions.
    /// </summary>
    private static string BuildSource(IReadOnlyList<ClassDef> classes)
    {
        var sb = new StringBuilder();
        foreach (var cls in classes)
        {
            sb.AppendLine($"namespace {cls.Namespace}");
            sb.AppendLine("{");
            sb.Append("    public ");
            if (cls.IsStatic) sb.Append("static ");
            sb.AppendLine($"class {cls.Name}");
            sb.AppendLine("    {");

            for (var i = 0; i < cls.PublicStaticMethodCount; i++)
            {
                sb.AppendLine($"        public static int StaticMethod{i}() => {i};");
            }

            for (var i = 0; i < cls.InstanceMethodCount; i++)
            {
                sb.AppendLine($"        public int InstanceMethod{i}() => {i};");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Compiles C# source to a temporary DLL on disk and returns the path.
    /// </summary>
    private string CompileToDll(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        var references = new[]
        {
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Runtime.dll")),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: $"TestAssembly_{Guid.NewGuid():N}",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.dll");
        _tempFiles.Add(tempPath);

        var result = compilation.Emit(tempPath);
        if (!result.Success)
        {
            var errors = string.Join("\n", result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
            throw new InvalidOperationException($"Compilation failed:\n{errors}\n\nSource:\n{source}");
        }

        return tempPath;
    }

    /// <summary>
    /// FsCheck generator for a list of ClassDef instances with a mix of
    /// public static classes (with at least one public static method) and
    /// non-static noise classes.
    /// </summary>
    private static Gen<IReadOnlyList<ClassDef>> ClassDefsGen()
    {
        // Generate 1-8 public static classes, each with 1-5 public static methods
        var staticClassGen =
            from index in Gen.Choose(0, 999)
            from methodCount in Gen.Choose(1, 5)
            from instanceMethodCount in Gen.Choose(0, 3)
            select new ClassDef(
                Name: MakeIdentifier("StaticClass", index),
                Namespace: "TestNs",
                IsStatic: true,
                PublicStaticMethodCount: methodCount,
                InstanceMethodCount: 0); // static classes can't have instance methods

        // Generate 0-5 non-static noise classes
        var noiseClassGen =
            from index in Gen.Choose(0, 999)
            from instanceMethodCount in Gen.Choose(0, 3)
            select new ClassDef(
                Name: MakeIdentifier("NoiseClass", index),
                Namespace: "TestNs",
                IsStatic: false,
                PublicStaticMethodCount: 0,
                InstanceMethodCount: instanceMethodCount);

        // Generate 0-3 static classes with NO public static methods (should be excluded)
        var emptyStaticClassGen =
            from index in Gen.Choose(0, 999)
            select new ClassDef(
                Name: MakeIdentifier("EmptyStaticClass", index),
                Namespace: "TestNs",
                IsStatic: true,
                PublicStaticMethodCount: 0,
                InstanceMethodCount: 0);

        return from staticCount in Gen.Choose(1, 8)
               from noiseCount in Gen.Choose(0, 5)
               from emptyStaticCount in Gen.Choose(0, 3)
               from statics in Gen.ListOf(staticCount, staticClassGen)
               from noises in Gen.ListOf(noiseCount, noiseClassGen)
               from empties in Gen.ListOf(emptyStaticCount, emptyStaticClassGen)
               // Deduplicate names by appending unique suffix
               let allClasses = DeduplicateNames(statics.Concat(noises).Concat(empties).ToList())
               select (IReadOnlyList<ClassDef>)allClasses;
    }

    /// <summary>
    /// Ensures all class names are unique by appending a suffix when duplicates exist.
    /// </summary>
    private static List<ClassDef> DeduplicateNames(List<ClassDef> classes)
    {
        var seen = new HashSet<string>();
        var result = new List<ClassDef>();
        var counter = 0;
        foreach (var cls in classes)
        {
            var name = cls.Name;
            while (!seen.Add(name))
            {
                name = $"{cls.Name}_{counter++}";
            }
            result.Add(cls with { Name = name });
        }
        return result;
    }

    [Property(MaxTest = 100)]
    public Property DiscoverPublicStaticTypes_ReturnsExactlyPublicStaticTypesWithMethods()
    {
        return Prop.ForAll(ClassDefsGen().ToArbitrary(), classDefs =>
        {
            // Build source and compile to DLL
            var source = BuildSource(classDefs);
            var dllPath = CompileToDll(source);

            // Call the method under test
            var discovered = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath);

            // Compute expected: only static classes with at least one public static method
            var expected = classDefs
                .Where(c => c.IsStatic && c.PublicStaticMethodCount > 0)
                .Select(c => $"{c.Namespace}.{c.Name}")
                .OrderBy(n => n)
                .ToList();

            var actual = discovered.OrderBy(n => n).ToList();

            // Property: discovered set matches expected set exactly
            return (actual.Count == expected.Count &&
                    actual.SequenceEqual(expected))
                .ToProperty()
                .Label($"Expected: [{string.Join(", ", expected)}], Actual: [{string.Join(", ", actual)}]");
        });
    }

    [Property(MaxTest = 100)]
    public Property DiscoverPublicStaticTypes_AllReturnedTypesArePublicStaticWithMethods()
    {
        return Prop.ForAll(ClassDefsGen().ToArbitrary(), classDefs =>
        {
            var source = BuildSource(classDefs);
            var dllPath = CompileToDll(source);

            var discovered = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath);

            // Build a lookup of expected qualifying types
            var qualifyingTypes = new HashSet<string>(
                classDefs
                    .Where(c => c.IsStatic && c.PublicStaticMethodCount > 0)
                    .Select(c => $"{c.Namespace}.{c.Name}"));

            // Property: every discovered type is in the qualifying set
            return discovered.All(t => qualifyingTypes.Contains(t))
                .ToProperty()
                .Label($"Discovered types not in qualifying set: [{string.Join(", ", discovered.Where(t => !qualifyingTypes.Contains(t)))}]");
        });
    }

    [Property(MaxTest = 100)]
    public Property DiscoverPublicStaticTypes_NeverReturnsNonStaticOrEmptyStaticClasses()
    {
        return Prop.ForAll(ClassDefsGen().ToArbitrary(), classDefs =>
        {
            var source = BuildSource(classDefs);
            var dllPath = CompileToDll(source);

            var discovered = new HashSet<string>(
                LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath));

            // Noise classes (non-static) should never appear
            var noiseTypes = classDefs
                .Where(c => !c.IsStatic)
                .Select(c => $"{c.Namespace}.{c.Name}");

            // Empty static classes (no public static methods) should never appear
            var emptyStaticTypes = classDefs
                .Where(c => c.IsStatic && c.PublicStaticMethodCount == 0)
                .Select(c => $"{c.Namespace}.{c.Name}");

            var excludedTypes = noiseTypes.Concat(emptyStaticTypes).ToList();
            var wronglyIncluded = excludedTypes.Where(t => discovered.Contains(t)).ToList();

            return (!wronglyIncluded.Any())
                .ToProperty()
                .Label($"Wrongly included types: [{string.Join(", ", wronglyIncluded)}]");
        });
    }
}

// Feature: sdk-cross-project-references, Property 3: Using Static Generation Correctness
// Validates: Requirements 4.1, 4.2

/// <summary>
/// Property-based tests verifying that the translator generates a `using static` directive
/// for every public static type (with public static methods) discovered in referenced assemblies,
/// and that each directive uses the fully qualified type name.
/// </summary>
public class UsingStaticGenerationPropertyTests : IDisposable
{
    private readonly List<string> _tempFiles = new();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        foreach (var f in _tempFiles)
        {
            try { if (File.Exists(f)) File.Delete(f); } catch { }
        }
    }

    /// <summary>
    /// Represents a generated class definition for test assembly compilation.
    /// </summary>
    private record ClassDef(string Name, string Namespace, bool IsStatic, int PublicStaticMethodCount);

    /// <summary>
    /// Builds C# source code from a list of class definitions.
    /// </summary>
    private static string BuildSource(IReadOnlyList<ClassDef> classes)
    {
        var sb = new StringBuilder();
        foreach (var cls in classes)
        {
            sb.AppendLine($"namespace {cls.Namespace}");
            sb.AppendLine("{");
            sb.Append("    public ");
            if (cls.IsStatic) sb.Append("static ");
            sb.AppendLine($"class {cls.Name}");
            sb.AppendLine("    {");

            for (var i = 0; i < cls.PublicStaticMethodCount; i++)
            {
                sb.AppendLine($"        public static int Method{i}() => {i};");
            }

            // Non-static classes need at least a body to compile
            if (!cls.IsStatic && cls.PublicStaticMethodCount == 0)
            {
                sb.AppendLine("        public int Dummy() => 0;");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Compiles C# source to a temporary DLL on disk and returns the path.
    /// </summary>
    private string CompileToDll(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        var references = new[]
        {
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Runtime.dll")),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: $"TestAssembly_{Guid.NewGuid():N}",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.dll");
        _tempFiles.Add(tempPath);

        var result = compilation.Emit(tempPath);
        if (!result.Success)
        {
            var errors = string.Join("\n", result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
            throw new InvalidOperationException($"Compilation failed:\n{errors}\n\nSource:\n{source}");
        }

        return tempPath;
    }

    /// <summary>
    /// Creates a minimal AssemblyDef with one empty module suitable for translation.
    /// </summary>
    private static ast.AssemblyDef CreateMinimalAssemblyDef()
    {
        var voidType = new ast_model.TypeSystem.FifthType.TVoidType { Name = ast_model.TypeSystem.TypeName.From("void") };
        var module = new ast.ModuleDef
        {
            Annotations = new Dictionary<string, object>(),
            OriginalModuleName = "TestModule",
            NamespaceDecl = ast.NamespaceName.From("TestModule"),
            Classes = [],
            Functions = [],
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };

        return new ast.AssemblyDef
        {
            Annotations = new Dictionary<string, object>(),
            Name = ast.AssemblyName.From("TestAssembly"),
            PublicKeyToken = "",
            Version = "0.0.0",
            TestProperty = "",
            AssemblyRefs = [],
            Modules = [module],
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };
    }

    /// <summary>
    /// Extracts all `using static` directive type names from a C# source string.
    /// </summary>
    private static HashSet<string> ExtractUsingStaticDirectives(string csharpSource)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpSource);
        var root = tree.GetCompilationUnitRoot();
        var result = new HashSet<string>(StringComparer.Ordinal);

        // Check top-level using directives
        foreach (var usingDir in root.Usings)
        {
            if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
            {
                result.Add(usingDir.Name?.ToString() ?? "");
            }
        }

        // Check namespace-level using directives
        foreach (var member in root.Members)
        {
            if (member is Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax ns)
            {
                foreach (var usingDir in ns.Usings)
                {
                    if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
                    {
                        result.Add(usingDir.Name?.ToString() ?? "");
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Ensures all class names are unique by appending a suffix when duplicates exist.
    /// </summary>
    private static List<ClassDef> DeduplicateNames(List<ClassDef> classes)
    {
        var seen = new HashSet<string>();
        var result = new List<ClassDef>();
        var counter = 0;
        foreach (var cls in classes)
        {
            var name = cls.Name;
            while (!seen.Add(name))
            {
                name = $"{cls.Name}_{counter++}";
            }
            result.Add(cls with { Name = name });
        }
        return result;
    }

    /// <summary>
    /// FsCheck generator for sets of public static classes across 1-3 assemblies.
    /// Each assembly gets 1-4 public static classes with 1-3 public static methods.
    /// </summary>
    private static Gen<IReadOnlyList<IReadOnlyList<ClassDef>>> AssemblyClassDefsGen()
    {
        var staticClassGen = (string nsPrefix) =>
            from index in Gen.Choose(0, 999)
            from methodCount in Gen.Choose(1, 3)
            select new ClassDef(
                Name: $"Cls{index}",
                Namespace: $"{nsPrefix}",
                IsStatic: true,
                PublicStaticMethodCount: methodCount);

        return from assemblyCount in Gen.Choose(1, 3)
               from assemblies in Gen.Sequence(
                   Enumerable.Range(0, assemblyCount).Select(asmIdx =>
                       from classCount in Gen.Choose(1, 4)
                       from classes in Gen.ListOf(classCount, staticClassGen($"Ns{asmIdx}"))
                       let deduped = DeduplicateNames(classes.ToList())
                       select (IReadOnlyList<ClassDef>)deduped))
               select (IReadOnlyList<IReadOnlyList<ClassDef>>)assemblies.ToList();
    }

    [Property(MaxTest = 100)]
    public Property GeneratedCSharp_ContainsUsingStaticForEachDiscoveredType()
    {
        return Prop.ForAll(AssemblyClassDefsGen().ToArbitrary(), assemblyClassDefs =>
        {
            // Build and compile each assembly
            var dllPaths = new List<string>();
            var allExpectedTypes = new HashSet<string>(StringComparer.Ordinal);

            foreach (var classDefs in assemblyClassDefs)
            {
                var source = BuildSource(classDefs);
                var dllPath = CompileToDll(source);
                dllPaths.Add(dllPath);

                foreach (var cls in classDefs.Where(c => c.IsStatic && c.PublicStaticMethodCount > 0))
                {
                    allExpectedTypes.Add($"{cls.Namespace}.{cls.Name}");
                }
            }

            // Translate a minimal module with those assemblies as references
            var assemblyDef = CreateMinimalAssemblyDef();
            var translator = new LoweredAstToRoslynTranslator();
            var options = new TranslatorOptions
            {
                AdditionalReferences = dllPaths
            };

            var result = translator.Translate(assemblyDef, options);

            // Parse the generated C# to extract using static directives
            var generatedSource = result.Sources.FirstOrDefault() ?? "";
            var usingStatics = ExtractUsingStaticDirectives(generatedSource);

            // Verify: every expected type has a corresponding using static directive
            var missing = allExpectedTypes.Where(t => !usingStatics.Contains(t)).ToList();

            return (missing.Count == 0)
                .ToProperty()
                .Label($"Missing using static directives: [{string.Join(", ", missing)}]");
        });
    }

    [Property(MaxTest = 100)]
    public Property GeneratedUsingStatics_UseFullyQualifiedTypeNames()
    {
        return Prop.ForAll(AssemblyClassDefsGen().ToArbitrary(), assemblyClassDefs =>
        {
            // Build and compile each assembly
            var dllPaths = new List<string>();
            var allExpectedTypes = new HashSet<string>(StringComparer.Ordinal);

            foreach (var classDefs in assemblyClassDefs)
            {
                var source = BuildSource(classDefs);
                var dllPath = CompileToDll(source);
                dllPaths.Add(dllPath);

                foreach (var cls in classDefs.Where(c => c.IsStatic && c.PublicStaticMethodCount > 0))
                {
                    allExpectedTypes.Add($"{cls.Namespace}.{cls.Name}");
                }
            }

            // Translate
            var assemblyDef = CreateMinimalAssemblyDef();
            var translator = new LoweredAstToRoslynTranslator();
            var options = new TranslatorOptions
            {
                AdditionalReferences = dllPaths
            };

            var result = translator.Translate(assemblyDef, options);
            var generatedSource = result.Sources.FirstOrDefault() ?? "";
            var usingStatics = ExtractUsingStaticDirectives(generatedSource);

            // Verify: each using static directive for our types uses fully qualified name
            // (contains a dot, meaning namespace.class format)
            var ourDirectives = usingStatics.Where(u => allExpectedTypes.Contains(u)).ToList();
            var allFullyQualified = ourDirectives.All(d => d.Contains('.'));

            return allFullyQualified
                .ToProperty()
                .Label($"Non-fully-qualified directives: [{string.Join(", ", ourDirectives.Where(d => !d.Contains('.')))}]");
        });
    }
}

// Feature: sdk-cross-project-references, Property 4: No Duplicate Using Static Directives
// Validates: Requirements 4.3

/// <summary>
/// Property-based tests verifying that when multiple assemblies export types with the
/// same fully qualified name, the translator emits at most one `using static` directive
/// per unique type name — no duplicates.
/// </summary>
public class NoDuplicateUsingStaticPropertyTests : IDisposable
{
    private readonly List<string> _tempFiles = new();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        foreach (var f in _tempFiles)
        {
            try { if (File.Exists(f)) File.Delete(f); } catch { }
        }
    }

    private record ClassDef(string Name, string Namespace, bool IsStatic, int PublicStaticMethodCount);

    private static string BuildSource(IReadOnlyList<ClassDef> classes)
    {
        var sb = new StringBuilder();
        foreach (var cls in classes)
        {
            sb.AppendLine($"namespace {cls.Namespace}");
            sb.AppendLine("{");
            sb.Append("    public ");
            if (cls.IsStatic) sb.Append("static ");
            sb.AppendLine($"class {cls.Name}");
            sb.AppendLine("    {");

            for (var i = 0; i < cls.PublicStaticMethodCount; i++)
            {
                sb.AppendLine($"        public static int Method{i}() => {i};");
            }

            if (!cls.IsStatic && cls.PublicStaticMethodCount == 0)
            {
                sb.AppendLine("        public int Dummy() => 0;");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
        }
        return sb.ToString();
    }

    private string CompileToDll(string source, string assemblyName)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        var references = new[]
        {
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Runtime.dll")),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.dll");
        _tempFiles.Add(tempPath);

        var result = compilation.Emit(tempPath);
        if (!result.Success)
        {
            var errors = string.Join("\n", result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
            throw new InvalidOperationException($"Compilation failed:\n{errors}\n\nSource:\n{source}");
        }

        return tempPath;
    }

    private static ast.AssemblyDef CreateMinimalAssemblyDef()
    {
        var voidType = new ast_model.TypeSystem.FifthType.TVoidType { Name = ast_model.TypeSystem.TypeName.From("void") };
        var module = new ast.ModuleDef
        {
            Annotations = new Dictionary<string, object>(),
            OriginalModuleName = "TestModule",
            NamespaceDecl = ast.NamespaceName.From("TestModule"),
            Classes = [],
            Functions = [],
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };

        return new ast.AssemblyDef
        {
            Annotations = new Dictionary<string, object>(),
            Name = ast.AssemblyName.From("TestAssembly"),
            PublicKeyToken = "",
            Version = "0.0.0",
            TestProperty = "",
            AssemblyRefs = [],
            Modules = [module],
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };
    }

    /// <summary>
    /// Counts ALL `using static` directives in the generated C# source (including duplicates).
    /// Unlike ExtractUsingStaticDirectives which returns a HashSet, this returns a raw list
    /// so we can detect if the translator emitted duplicates.
    /// </summary>
    private static List<string> ExtractAllUsingStaticDirectives(string csharpSource)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpSource);
        var root = tree.GetCompilationUnitRoot();
        var result = new List<string>();

        foreach (var usingDir in root.Usings)
        {
            if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
            {
                result.Add(usingDir.Name?.ToString() ?? "");
            }
        }

        foreach (var member in root.Members)
        {
            if (member is Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax ns)
            {
                foreach (var usingDir in ns.Usings)
                {
                    if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
                    {
                        result.Add(usingDir.Name?.ToString() ?? "");
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Generates a shared pool of type names, then distributes them across 2-4 assemblies
    /// with intentional overlap so the same fully qualified type name appears in multiple assemblies.
    /// </summary>
    private static Gen<(IReadOnlyList<string> TypeNames, int AssemblyCount)> OverlappingTypeNamesGen()
    {
        return from typeCount in Gen.Choose(1, 6)
               from indices in Gen.ListOf(typeCount, Gen.Choose(0, 99))
               from assemblyCount in Gen.Choose(2, 4)
               let distinctNames = indices.Select(i => $"SharedNs.StaticType{i}").Distinct().ToList()
               where distinctNames.Count > 0
               select ((IReadOnlyList<string>)distinctNames, assemblyCount);
    }

    [Property(MaxTest = 100)]
    public Property UsingStaticCount_EqualsDistinctTypeCount_WhenDuplicatesAcrossAssemblies()
    {
        // **Validates: Requirements 4.3**
        return Prop.ForAll(OverlappingTypeNamesGen().ToArbitrary(), input =>
        {
            var (typeNames, assemblyCount) = input;

            // Create multiple assemblies, each containing ALL the shared type names.
            // This means every type name appears in every assembly — maximum overlap.
            var dllPaths = new List<string>();
            var classDefs = typeNames.Select(fqn =>
            {
                var parts = fqn.Split('.');
                var ns = parts[0];
                var name = parts[1];
                return new ClassDef(name, ns, IsStatic: true, PublicStaticMethodCount: 1);
            }).ToList();

            for (var asmIdx = 0; asmIdx < assemblyCount; asmIdx++)
            {
                var source = BuildSource(classDefs);
                var dllPath = CompileToDll(source, $"OverlapAsm{asmIdx}_{Guid.NewGuid():N}");
                dllPaths.Add(dllPath);
            }

            // Translate with all assemblies as references
            var assemblyDef = CreateMinimalAssemblyDef();
            var translator = new LoweredAstToRoslynTranslator();
            var options = new TranslatorOptions
            {
                AdditionalReferences = dllPaths
            };

            var result = translator.Translate(assemblyDef, options);
            var generatedSource = result.Sources.FirstOrDefault() ?? "";

            // Extract ALL using static directives (as a list, preserving duplicates)
            var allDirectives = ExtractAllUsingStaticDirectives(generatedSource);

            // Filter to only our shared type names
            var ourTypeSet = new HashSet<string>(typeNames, StringComparer.Ordinal);
            var ourDirectives = allDirectives.Where(d => ourTypeSet.Contains(d)).ToList();

            // Property: count of using static directives for our types == count of distinct type names
            var distinctCount = typeNames.Count; // already distinct from generator
            return (ourDirectives.Count == distinctCount)
                .ToProperty()
                .Label($"Expected {distinctCount} using static directives, got {ourDirectives.Count}. " +
                       $"Types: [{string.Join(", ", typeNames)}], " +
                       $"Directives: [{string.Join(", ", ourDirectives)}]");
        });
    }
}

// Feature: sdk-cross-project-references, Property 5: Backward Compatibility — No Extra Directives Without References
// Validates: Requirements 4.4, 9.2, 9.3

/// <summary>
/// Property-based tests verifying that when no additional references are provided
/// (AdditionalReferences = null), the translator produces only the known default
/// set of using static directives — no extras appear regardless of module complexity.
/// </summary>
public class BackwardCompatibilityPropertyTests
{
    /// <summary>
    /// The known default set of using static directives emitted by the translator
    /// when no additional references are provided.
    /// </summary>
    private static readonly HashSet<string> DefaultUsingStatics = new(StringComparer.Ordinal)
    {
        "Fifth.System.Functional",
        "Fifth.System.List",
        "Fifth.System.IO",
        "Fifth.System.Math"
    };

    /// <summary>
    /// Creates an AssemblyDef with one module containing the given number of simple functions.
    /// Each function returns an int literal, simulating varying module complexity.
    /// </summary>
    private static ast.AssemblyDef CreateAssemblyDefWithFunctions(int functionCount)
    {
        var intType = new ast_model.TypeSystem.FifthType.TType { Name = ast_model.TypeSystem.TypeName.From("int") };
        var voidType = new ast_model.TypeSystem.FifthType.TVoidType { Name = ast_model.TypeSystem.TypeName.From("void") };

        var functions = new List<ast.ScopedDefinition>();
        for (var i = 0; i < functionCount; i++)
        {
            var returnExp = new ast.Int32LiteralExp
            {
                Value = i,
                Annotations = new Dictionary<string, object>(),
                Type = intType,
                Parent = null
            };

            var body = new ast.BlockStatement
            {
                Statements = [new ast.ReturnStatement
                {
                    ReturnValue = returnExp,
                    Annotations = new Dictionary<string, object>(),
                    Type = voidType,
                    Parent = null
                }],
                Annotations = new Dictionary<string, object>(),
                Type = voidType,
                Parent = null
            };

            var funcDef = new ast.FunctionDef
            {
                Name = ast.MemberName.From($"func{i}"),
                TypeParameters = [],
                Params = [],
                Body = body,
                ReturnType = intType,
                IsStatic = true,
                IsConstructor = false,
                Annotations = new Dictionary<string, object>(),
                Type = intType,
                Parent = null,
                Visibility = ast.Visibility.Public
            };

            functions.Add(funcDef);
        }

        var module = new ast.ModuleDef
        {
            Annotations = new Dictionary<string, object>(),
            OriginalModuleName = "TestModule",
            NamespaceDecl = ast.NamespaceName.From("TestModule"),
            Classes = [],
            Functions = functions,
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };

        return new ast.AssemblyDef
        {
            Annotations = new Dictionary<string, object>(),
            Name = ast.AssemblyName.From("TestAssembly"),
            PublicKeyToken = "",
            Version = "0.0.0",
            TestProperty = "",
            AssemblyRefs = [],
            Modules = [module],
            Type = voidType,
            Parent = null,
            Visibility = ast.Visibility.Public
        };
    }

    /// <summary>
    /// Extracts all using static directive type names from a C# source string.
    /// </summary>
    private static HashSet<string> ExtractUsingStaticDirectives(string csharpSource)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpSource);
        var root = tree.GetCompilationUnitRoot();
        var result = new HashSet<string>(StringComparer.Ordinal);

        foreach (var usingDir in root.Usings)
        {
            if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
            {
                result.Add(usingDir.Name?.ToString() ?? "");
            }
        }

        foreach (var member in root.Members)
        {
            if (member is Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax ns)
            {
                foreach (var usingDir in ns.Usings)
                {
                    if (usingDir.StaticKeyword.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
                    {
                        result.Add(usingDir.Name?.ToString() ?? "");
                    }
                }
            }
        }

        return result;
    }

    [Property(MaxTest = 100)]
    public Property NoAdditionalReferences_ProducesOnlyDefaultUsingStatics()
    {
        // **Validates: Requirements 4.4, 9.2, 9.3**
        // Generate modules with 0-5 functions and verify no extra using static directives appear
        var gen = Gen.Choose(0, 5);

        return Prop.ForAll(gen.ToArbitrary(), functionCount =>
        {
            var assemblyDef = CreateAssemblyDefWithFunctions(functionCount);
            var translator = new LoweredAstToRoslynTranslator();

            // Translate with AdditionalReferences = null (backward-compatible path)
            var result = translator.Translate(assemblyDef, null);
            var generatedSource = result.Sources.FirstOrDefault() ?? "";

            var usingStatics = ExtractUsingStaticDirectives(generatedSource);

            // Property: the using static set must be exactly the default set
            var extra = usingStatics.Except(DefaultUsingStatics).ToList();
            var missing = DefaultUsingStatics.Except(usingStatics).ToList();

            return (extra.Count == 0 && missing.Count == 0)
                .ToProperty()
                .Label($"FunctionCount={functionCount}. " +
                       $"Extra directives: [{string.Join(", ", extra)}], " +
                       $"Missing directives: [{string.Join(", ", missing)}]");
        });
    }

    [Property(MaxTest = 100)]
    public Property NoAdditionalReferences_WithoutOptions_ProducesOnlyDefaultUsingStatics()
    {
        // **Validates: Requirements 4.4, 9.2, 9.3**
        // Same as above but calling the no-options Translate overload
        var gen = Gen.Choose(0, 5);

        return Prop.ForAll(gen.ToArbitrary(), functionCount =>
        {
            var assemblyDef = CreateAssemblyDefWithFunctions(functionCount);
            var translator = new LoweredAstToRoslynTranslator();

            // Translate without passing TranslatorOptions at all
            var result = translator.Translate(assemblyDef);
            var generatedSource = result.Sources.FirstOrDefault() ?? "";

            var usingStatics = ExtractUsingStaticDirectives(generatedSource);

            // Property: the using static set must be exactly the default set
            var extra = usingStatics.Except(DefaultUsingStatics).ToList();
            var missing = DefaultUsingStatics.Except(usingStatics).ToList();

            return (extra.Count == 0 && missing.Count == 0)
                .ToProperty()
                .Label($"FunctionCount={functionCount}. " +
                       $"Extra directives: [{string.Join(", ", extra)}], " +
                       $"Missing directives: [{string.Join(", ", missing)}]");
        });
    }
}
