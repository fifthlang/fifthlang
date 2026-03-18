using compiler;
using FluentAssertions;

namespace fifth_sdk_tests;

/// <summary>
/// Tests that verify the Fifth SDK MSBuild target passes <c>--target-framework</c>
/// to the compiler and that <see cref="FrameworkReferenceSettings"/> constants are
/// used end-to-end (issue #155 – assembly prefixes should be configurable).
/// </summary>
public class TargetFrameworkPropagationTests
{
    // ── FrameworkReferenceSettings constants reachable from this project ──

    [Fact]
    public void FrameworkReferenceSettings_DefaultTargetFramework_IsNet8()
    {
        FrameworkReferenceSettings.DefaultTargetFramework.Should().Be("net8.0");
    }

    [Fact]
    public void FrameworkReferenceSettings_DefaultRuntimeDependencies_AreNonEmpty()
    {
        FrameworkReferenceSettings.DefaultRuntimeDependencyNames.Should().NotBeEmpty();
    }

    [Fact]
    public void FrameworkReferenceSettings_GetFrameworkVersion_Net8()
    {
        FrameworkReferenceSettings.GetFrameworkVersion("net8.0").Should().Be("8.0.0");
    }

    [Fact]
    public void FrameworkReferenceSettings_GetFrameworkVersion_Net9()
    {
        FrameworkReferenceSettings.GetFrameworkVersion("net9.0").Should().Be("9.0.0");
    }

    // ── MSBuild Sdk.targets passes --target-framework on the command line ─

    [Fact]
    public void SdkTargets_FifthCompileCommand_ContainsTargetFrameworkFlag()
    {
        var repoRoot = FindRepoRoot();
        var sdkPath = Path.Combine(repoRoot, "src", "Fifth.Sdk", "Sdk");
        var targetsPath = Path.Combine(sdkPath, "Sdk.targets");

        var targetsContent = File.ReadAllText(targetsPath);

        // Verify that Sdk.targets forwards --target-framework to the compiler (issue #155).
        // The TargetFramework MSBuild property must be threaded through to the compiler so
        // that runtimeconfig.json and assembly reference resolution use the correct framework.
        targetsContent.Should().Contain("--target-framework",
            because: "Sdk.targets must forward --target-framework to the compiler (issue #155)");
        targetsContent.Should().Contain("$(TargetFramework)",
            because: "Sdk.targets must substitute the TargetFramework MSBuild property value");
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
}
