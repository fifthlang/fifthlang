// Unit tests for cross-project reference translator functionality
// Validates: Requirements 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4, 9.2, 9.3

using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using compiler;
using System.Text;

namespace ast_tests;

/// <summary>
/// Unit tests for DiscoverPublicStaticTypes — verifies specific examples and edge cases
/// for the reflection-based type discovery in the Roslyn translator.
/// </summary>
public class DiscoverPublicStaticTypesTests : IDisposable
{
    private readonly List<string> _tempFiles = new();

    public void Dispose()
    {
        foreach (var f in _tempFiles)
        {
            try { if (File.Exists(f)) File.Delete(f); } catch { }
        }
    }

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
            assemblyName: $"TestAsm_{Guid.NewGuid():N}",
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

    [Fact]
    public void OnePublicStaticClass_ReturnsThatClassName()
    {
        // Validates: Requirements 3.1, 3.2, 3.3
        var source = @"
namespace MyLib
{
    public static class Helpers
    {
        public static int Add(int a, int b) => a + b;
    }
}";
        var dllPath = CompileToDll(source);

        var result = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath);

        result.Should().ContainSingle()
            .Which.Should().Be("MyLib.Helpers");
    }

    [Fact]
    public void NoPublicStaticClasses_ReturnsEmptyList()
    {
        // Validates: Requirements 3.2, 3.3
        var source = @"
namespace MyLib
{
    public class RegularClass
    {
        public int GetValue() => 42;
    }
}";
        var dllPath = CompileToDll(source);

        var result = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath);

        result.Should().BeEmpty();
    }

    [Fact]
    public void MixOfStaticAndNonStatic_ReturnsOnlyStaticOnes()
    {
        // Validates: Requirements 3.1, 3.2, 3.3
        var source = @"
namespace MixLib
{
    public static class StaticHelper
    {
        public static int Square(int x) => x * x;
    }

    public class InstanceClass
    {
        public int Value { get; set; }
    }

    public static class AnotherStatic
    {
        public static string Greet() => ""hello"";
    }

    internal class InternalClass
    {
        public int Foo() => 1;
    }
}";
        var dllPath = CompileToDll(source);

        var result = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(dllPath);

        result.Should().HaveCount(2);
        result.Should().Contain("MixLib.StaticHelper");
        result.Should().Contain("MixLib.AnotherStatic");
        result.Should().NotContain("MixLib.InstanceClass");
        result.Should().NotContain("MixLib.InternalClass");
    }

    [Fact]
    public void NonExistentPath_ReturnsEmptyList()
    {
        // Validates: Requirement 3.4
        var fakePath = Path.Combine(Path.GetTempPath(), $"nonexistent_{Guid.NewGuid():N}.dll");

        var result = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(fakePath);

        result.Should().BeEmpty();
    }

    [Fact]
    public void InvalidDll_ReturnsEmptyList()
    {
        // Validates: Requirement 3.4
        var tempPath = Path.Combine(Path.GetTempPath(), $"garbage_{Guid.NewGuid():N}.dll");
        _tempFiles.Add(tempPath);
        File.WriteAllBytes(tempPath, new byte[] { 0xDE, 0xAD, 0xBE, 0xEF, 0x00, 0x01, 0x02, 0x03 });

        var result = LoweredAstToRoslynTranslator.DiscoverPublicStaticTypes(tempPath);

        result.Should().BeEmpty();
    }
}


/// <summary>
/// Unit tests for BuildSyntaxTreeFromModule using directive generation —
/// verifies that the translator correctly generates using static directives
/// when additional references are provided.
/// </summary>
public class TranslatorUsingDirectiveTests : IDisposable
{
    private readonly List<string> _tempFiles = new();

    public void Dispose()
    {
        foreach (var f in _tempFiles)
        {
            try { if (File.Exists(f)) File.Delete(f); } catch { }
        }
    }

    private string CompileToDll(string source, string? assemblyName = null)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        var references = new[]
        {
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Combine(runtimeDir, "System.Runtime.dll")),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: assemblyName ?? $"TestAsm_{Guid.NewGuid():N}",
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
        var voidType = new ast_model.TypeSystem.FifthType.TVoidType
        {
            Name = ast_model.TypeSystem.TypeName.From("void")
        };
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
    /// Extracts all using static directive type names from generated C# source.
    /// </summary>
    private static HashSet<string> ExtractUsingStaticDirectives(string csharpSource)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpSource);
        var root = tree.GetCompilationUnitRoot();
        var result = new HashSet<string>(StringComparer.Ordinal);

        foreach (var usingDir in root.Usings)
        {
            if (usingDir.StaticKeyword.IsKind(SyntaxKind.StaticKeyword))
            {
                result.Add(usingDir.Name?.ToString() ?? "");
            }
        }

        foreach (var member in root.Members)
        {
            if (member is NamespaceDeclarationSyntax ns)
            {
                foreach (var usingDir in ns.Usings)
                {
                    if (usingDir.StaticKeyword.IsKind(SyntaxKind.StaticKeyword))
                    {
                        result.Add(usingDir.Name?.ToString() ?? "");
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Extracts all using static directives as a list (preserving duplicates).
    /// </summary>
    private static List<string> ExtractAllUsingStaticDirectives(string csharpSource)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpSource);
        var root = tree.GetCompilationUnitRoot();
        var result = new List<string>();

        foreach (var usingDir in root.Usings)
        {
            if (usingDir.StaticKeyword.IsKind(SyntaxKind.StaticKeyword))
            {
                result.Add(usingDir.Name?.ToString() ?? "");
            }
        }

        foreach (var member in root.Members)
        {
            if (member is NamespaceDeclarationSyntax ns)
            {
                foreach (var usingDir in ns.Usings)
                {
                    if (usingDir.StaticKeyword.IsKind(SyntaxKind.StaticKeyword))
                    {
                        result.Add(usingDir.Name?.ToString() ?? "");
                    }
                }
            }
        }

        return result;
    }

    private static readonly HashSet<string> DefaultUsingStatics = new(StringComparer.Ordinal)
    {
        "Fifth.System.Functional",
        "Fifth.System.List",
        "Fifth.System.IO",
        "Fifth.System.Math"
    };

    [Fact]
    public void NoAdditionalReferences_DefaultDirectivesOnly()
    {
        // Validates: Requirements 4.4, 9.2, 9.3
        var assemblyDef = CreateMinimalAssemblyDef();
        var translator = new LoweredAstToRoslynTranslator();

        var result = translator.Translate(assemblyDef, null);
        var source = result.Sources.First();
        var usingStatics = ExtractUsingStaticDirectives(source);

        usingStatics.Should().BeEquivalentTo(DefaultUsingStatics);
    }

    [Fact]
    public void OneReferenceWithOnePublicStaticType_AddsOneUsingStatic()
    {
        // Validates: Requirements 4.1, 4.2
        var refSource = @"
namespace CoreLib
{
    public static class Program
    {
        public static int Square(int x) => x * x;
    }
}";
        var dllPath = CompileToDll(refSource);

        var assemblyDef = CreateMinimalAssemblyDef();
        var translator = new LoweredAstToRoslynTranslator();
        var options = new TranslatorOptions
        {
            AdditionalReferences = new List<string> { dllPath }
        };

        var result = translator.Translate(assemblyDef, options);
        var source = result.Sources.First();
        var usingStatics = ExtractUsingStaticDirectives(source);

        usingStatics.Should().Contain("CoreLib.Program");
        // Should still have all defaults
        foreach (var defaultUsing in DefaultUsingStatics)
        {
            usingStatics.Should().Contain(defaultUsing);
        }
    }

    [Fact]
    public void MultipleReferences_AddsUsingStaticForEach()
    {
        // Validates: Requirements 4.1, 4.2
        var refSource1 = @"
namespace LibA
{
    public static class HelperA
    {
        public static int Add(int a, int b) => a + b;
    }
}";
        var refSource2 = @"
namespace LibB
{
    public static class HelperB
    {
        public static int Multiply(int a, int b) => a * b;
    }
}";
        var dll1 = CompileToDll(refSource1);
        var dll2 = CompileToDll(refSource2);

        var assemblyDef = CreateMinimalAssemblyDef();
        var translator = new LoweredAstToRoslynTranslator();
        var options = new TranslatorOptions
        {
            AdditionalReferences = new List<string> { dll1, dll2 }
        };

        var result = translator.Translate(assemblyDef, options);
        var source = result.Sources.First();
        var usingStatics = ExtractUsingStaticDirectives(source);

        usingStatics.Should().Contain("LibA.HelperA");
        usingStatics.Should().Contain("LibB.HelperB");
    }

    [Fact]
    public void DuplicateTypeAcrossReferences_OnlyOneEmitted()
    {
        // Validates: Requirement 4.3
        var sharedSource = @"
namespace SharedNs
{
    public static class SharedHelper
    {
        public static int DoWork() => 1;
    }
}";
        // Compile the same source into two separate assemblies
        var dll1 = CompileToDll(sharedSource, "SharedAsm1");
        var dll2 = CompileToDll(sharedSource, "SharedAsm2");

        var assemblyDef = CreateMinimalAssemblyDef();
        var translator = new LoweredAstToRoslynTranslator();
        var options = new TranslatorOptions
        {
            AdditionalReferences = new List<string> { dll1, dll2 }
        };

        var result = translator.Translate(assemblyDef, options);
        var source = result.Sources.First();

        // Use the list-based extractor to detect duplicates
        var allDirectives = ExtractAllUsingStaticDirectives(source);
        var sharedDirectives = allDirectives.Where(d => d == "SharedNs.SharedHelper").ToList();

        sharedDirectives.Should().HaveCount(1,
            "duplicate type across assemblies should produce only one using static directive");
    }
}
