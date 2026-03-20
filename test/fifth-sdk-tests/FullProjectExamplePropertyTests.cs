using System.Xml.Linq;
using FluentAssertions;

namespace fifth_sdk_tests;

/// <summary>
/// Feature: full-project-example
/// Property 1: Project files declare required MSBuild properties
/// Validates: Requirements 2.2, 3.2, 5.4
/// </summary>
public class FullProjectExamplePropertyTests
{
    private static readonly string RepoRoot = FindRepoRoot();
    private static readonly string SampleRoot = Path.Combine(RepoRoot, "samples", "FullProjectExample");

    public static IEnumerable<object[]> ProjectFiles()
    {
        yield return new object[]
        {
            Path.Combine(SampleRoot, "src", "CoreLib", "CoreLib.5thproj"),
            "Library"
        };
        yield return new object[]
        {
            Path.Combine(SampleRoot, "src", "MathLib", "MathLib.5thproj"),
            "Library"
        };
        yield return new object[]
        {
            Path.Combine(SampleRoot, "src", "App", "App.5thproj"),
            "Exe"
        };
    }

    /// <summary>
    /// Property 1: Project files declare required MSBuild properties
    /// For any .5thproj file in the sample, it SHALL declare TargetFramework as net8.0,
    /// FifthCompilerCommand as the .NET tool command name, and OutputType matching its role.
    /// **Validates: Requirements 3.2, 5.4**
    /// </summary>
    [Theory]
    [MemberData(nameof(ProjectFiles))]
    public void ProjectFile_DeclareRequiredMSBuildProperties(string projectPath, string expectedOutputType)
    {
        File.Exists(projectPath).Should().BeTrue($"project file should exist at {projectPath}");

        var doc = XDocument.Load(projectPath);
        var ns = doc.Root!.GetDefaultNamespace();
        var propertyGroup = doc.Root.Element(ns + "PropertyGroup");
        propertyGroup.Should().NotBeNull("project file should contain a PropertyGroup");

        var targetFramework = propertyGroup!.Element(ns + "TargetFramework")?.Value;
        targetFramework.Should().Be("net8.0", "TargetFramework must be net8.0");

        var compilerCommand = propertyGroup.Element(ns + "FifthCompilerCommand")?.Value;
        compilerCommand.Should().Be("fifthc", "FifthCompilerCommand must be fifthc");

        var outputType = propertyGroup.Element(ns + "OutputType")?.Value;
        outputType.Should().Be(expectedOutputType, $"OutputType must be {expectedOutputType}");
    }

    private static string FindRepoRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "fifthlang.sln")))
            {
                return directory.FullName;
            }
            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Repository root not found.");
    }
}
