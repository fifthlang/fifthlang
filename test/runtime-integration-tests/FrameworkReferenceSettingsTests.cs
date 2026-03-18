using compiler;
using FluentAssertions;

namespace runtime_integration_tests;

/// <summary>
/// Unit tests for <see cref="FrameworkReferenceSettings"/>.
/// Verifies that the centralised constants and helpers produce the expected values,
/// which is the observable fix for issue #155 ("Assembly prefixes should be configurable").
/// </summary>
public class FrameworkReferenceSettingsTests
{
    // ── Constants ──────────────────────────────────────────────────────────

    [Fact]
    public void DefaultTargetFramework_IsNet8()
    {
        FrameworkReferenceSettings.DefaultTargetFramework.Should().Be("net8.0");
    }

    [Fact]
    public void DefaultFrameworkName_IsMicrosoftNETCoreApp()
    {
        FrameworkReferenceSettings.DefaultFrameworkName.Should().Be("Microsoft.NETCore.App");
    }

    [Fact]
    public void DefaultFrameworkVersion_Is800()
    {
        FrameworkReferenceSettings.DefaultFrameworkVersion.Should().Be("8.0.0");
    }

    [Fact]
    public void NetStandardFacadeAssembly_IsNetstandard()
    {
        FrameworkReferenceSettings.NetStandardFacadeAssembly.Should().Be("netstandard");
    }

    [Fact]
    public void DefaultRuntimeDependencyNames_ContainsFifthSystemDll()
    {
        FrameworkReferenceSettings.DefaultRuntimeDependencyNames
            .Should().Contain("Fifth.System.dll");
    }

    [Fact]
    public void DefaultRuntimeDependencyNames_ContainsDotNetRdfAssemblies()
    {
        FrameworkReferenceSettings.DefaultRuntimeDependencyNames
            .Should().Contain("dotNetRdf.dll")
            .And.Contain("dotNetRdf.Client.dll")
            .And.Contain("VDS.Common.dll");
    }

    // ── GetFrameworkVersion ────────────────────────────────────────────────

    [Theory]
    [InlineData("net8.0", "8.0.0")]
    [InlineData("net9.0", "9.0.0")]
    [InlineData("net10.0", "10.0.0")]
    [InlineData("NET8.0", "8.0.0")]   // case-insensitive
    [InlineData("NET9.0", "9.0.0")]
    public void GetFrameworkVersion_ParsesStandardTfmsCorrectly(string tfm, string expected)
    {
        FrameworkReferenceSettings.GetFrameworkVersion(tfm).Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetFrameworkVersion_ReturnsDefaultWhenTfmIsNullOrWhiteSpace(string? tfm)
    {
        FrameworkReferenceSettings.GetFrameworkVersion(tfm).Should().Be(
            FrameworkReferenceSettings.DefaultFrameworkVersion);
    }

    [Theory]
    [InlineData("notaframework")]
    [InlineData("netstandard2.0")]   // not a simple net<M.m> moniker
    public void GetFrameworkVersion_ReturnsDefaultForUnrecognisedTfm(string tfm)
    {
        // netstandard2.0 strips "net" → "standard2.0" which doesn't parse as a Version,
        // so the fallback is returned.
        FrameworkReferenceSettings.GetFrameworkVersion(tfm).Should().Be(
            FrameworkReferenceSettings.DefaultFrameworkVersion);
    }

    // ── CompilerOptions integration ────────────────────────────────────────

    [Fact]
    public void CompilerOptions_DefaultTargetFramework_MatchesFrameworkReferenceSettings()
    {
        var options = new CompilerOptions();
        options.TargetFramework.Should().Be(FrameworkReferenceSettings.DefaultTargetFramework);
    }

    [Fact]
    public void CompilerOptions_AcceptsCustomTargetFramework()
    {
        var options = new CompilerOptions(TargetFramework: "net9.0");
        options.TargetFramework.Should().Be("net9.0");
    }
}
