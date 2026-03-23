using FluentAssertions;

namespace fifth_sdk_tests;

/// <summary>
/// Property-based validation tests for the .NET 10 migration.
/// These tests scan all project files in the solution to verify migration invariants.
/// </summary>
public class Net10MigrationPropertyTests
{
    // Feature: dotnet10-migration, Property 1: Sole net10.0 target with no net8.0 remnants
    /// <summary>
    /// Validates: Requirements 2.1, 2.2, 2.3, 2.4
    /// For every .csproj in the solution, the file must contain exactly one
    /// &lt;TargetFramework&gt;net10.0&lt;/TargetFramework&gt; and zero occurrences of "net8.0".
    /// </summary>
    [Fact]
    public void AllProjectFiles_TargetOnlyNet10_WithNoNet8References()
    {
        var repoRoot = FindRepoRoot();
        var csprojFiles = Directory.GetFiles(repoRoot, "*.csproj", SearchOption.AllDirectories);

        csprojFiles.Should().NotBeEmpty("the solution should contain .csproj files");

        foreach (var csproj in csprojFiles)
        {
            var content = File.ReadAllText(csproj);
            var relativePath = Path.GetRelativePath(repoRoot, csproj);

            var targetFrameworkCount = CountOccurrences(content, "<TargetFramework>net10.0</TargetFramework>");
            targetFrameworkCount.Should().Be(1,
                because: $"{relativePath} should contain exactly one <TargetFramework>net10.0</TargetFramework>");

            content.Should().NotContain("net8.0",
                because: $"{relativePath} should have zero references to net8.0");
        }
    }

    // Feature: dotnet10-migration, Property 2: EnableNet10 infrastructure fully removed
    /// <summary>
    /// Validates: Requirements 3.1, 3.2, 3.3
    /// For every .csproj and Directory.Build.props file in the repository,
    /// the file must contain zero occurrences of "EnableNet10".
    /// </summary>
    [Fact]
    public void AllProjectAndBuildFiles_ContainNoEnableNet10References()
    {
        var repoRoot = FindRepoRoot();

        var csprojFiles = Directory.GetFiles(repoRoot, "*.csproj", SearchOption.AllDirectories);
        var buildPropsFiles = Directory.GetFiles(repoRoot, "Directory.Build.props", SearchOption.AllDirectories);

        var allFiles = csprojFiles.Concat(buildPropsFiles).ToArray();
        allFiles.Should().NotBeEmpty("the repository should contain .csproj or Directory.Build.props files");

        foreach (var file in allFiles)
        {
            var content = File.ReadAllText(file);
            var relativePath = Path.GetRelativePath(repoRoot, file);

            content.Should().NotContain("EnableNet10",
                because: $"{relativePath} should have zero references to EnableNet10");
        }
    }

    // Feature: dotnet10-migration, Property 3: Conditional compilation directives removed
    /// <summary>
    /// Validates: Requirements 4.1, 4.3, 4.4
    /// For every .cs file in the repository (excluding obj/, bin/, and this test file),
    /// the file must contain zero preprocessor directives for NET10_0_OR_GREATER.
    /// </summary>
    [Fact]
    public void AllCSharpFiles_ContainNoNet10ConditionalCompilation()
    {
        const string directive = "#if NET10_0_OR_GREATER";
        var repoRoot = FindRepoRoot();
        var thisFile = Path.GetFullPath(Path.Combine(repoRoot, "test", "fifth-sdk-tests", "Net10MigrationPropertyTests.cs"));

        var csFiles = Directory.GetFiles(repoRoot, "*.cs", SearchOption.AllDirectories)
            .Where(f =>
            {
                if (string.Equals(Path.GetFullPath(f), thisFile, StringComparison.OrdinalIgnoreCase))
                    return false;
                var relativePath = Path.GetRelativePath(repoRoot, f);
                var parts = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                return !parts.Any(p => p.Equals("obj", StringComparison.OrdinalIgnoreCase)
                                    || p.Equals("bin", StringComparison.OrdinalIgnoreCase));
            })
            .ToArray();

        csFiles.Should().NotBeEmpty("the repository should contain .cs files");

        foreach (var csFile in csFiles)
        {
            var content = File.ReadAllText(csFile);
            var relativePath = Path.GetRelativePath(repoRoot, csFile);

            content.Should().NotContain(directive,
                because: $"{relativePath} should have zero conditional compilation directives for NET10_0_OR_GREATER");
        }
    }

    // Feature: dotnet10-migration, Property 4: No framework-conditional ItemGroups
    /// <summary>
    /// Validates: Requirements 5.1, 5.2, 5.3
    /// For every .csproj in the solution, the file must contain zero &lt;ItemGroup&gt;
    /// elements with a Condition attribute that references $(TargetFramework).
    /// </summary>
    [Fact]
    public void AllProjectFiles_ContainNoFrameworkConditionalItemGroups()
    {
        var repoRoot = FindRepoRoot();
        var csprojFiles = Directory.GetFiles(repoRoot, "*.csproj", SearchOption.AllDirectories);

        csprojFiles.Should().NotBeEmpty("the solution should contain .csproj files");

        var violations = new List<string>();

        foreach (var csproj in csprojFiles)
        {
            var content = File.ReadAllText(csproj);
            var relativePath = Path.GetRelativePath(repoRoot, csproj);

            // Match <ItemGroup with a Condition attribute referencing $(TargetFramework)
            if (System.Text.RegularExpressions.Regex.IsMatch(
                content,
                @"<ItemGroup\s[^>]*Condition\s*=\s*""[^""]*\$\(TargetFramework\)[^""]*""",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                violations.Add(relativePath);
            }
        }

        violations.Should().BeEmpty(
            because: "no .csproj file should have <ItemGroup> elements conditioned on $(TargetFramework)");
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private static string FindRepoRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "fifthlang.sln")))
                return directory.FullName;
            directory = directory.Parent;
        }
        throw new DirectoryNotFoundException("Repository root not found.");
    }

    private static int CountOccurrences(string text, string pattern)
    {
        var count = 0;
        var index = 0;
        while ((index = text.IndexOf(pattern, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += pattern.Length;
        }
        return count;
    }
}
