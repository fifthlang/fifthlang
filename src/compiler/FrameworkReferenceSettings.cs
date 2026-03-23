namespace compiler;

/// <summary>
/// Centralised constants and helpers for framework assembly references used during Roslyn
/// compilation and runtime-config generation.
///
/// Extracting these from <see cref="Compiler"/> makes it straightforward to update when
/// targeting different .NET frameworks or adding new required assemblies, and is the fix
/// for issue #155 ("Assembly prefixes should be configurable").
/// </summary>
public static class FrameworkReferenceSettings
{
    /// <summary>Default target-framework moniker used when none is specified.</summary>
    public const string DefaultTargetFramework = "net10.0";

    /// <summary>Name of the .NET Core shared framework written into runtimeconfig.json.</summary>
    public const string DefaultFrameworkName = "Microsoft.NETCore.App";

    /// <summary>Framework version string written into runtimeconfig.json when the TFM is <see cref="DefaultTargetFramework"/>.</summary>
    public const string DefaultFrameworkVersion = "10.0.0";

    /// <summary>
    /// The netstandard façade assembly that is loaded to provide broad BCL API surface.
    /// Loading this is best-effort; the load is silently skipped when the assembly is not
    /// available in the current runtime.
    /// </summary>
    public const string NetStandardFacadeAssembly = "netstandard";

    /// <summary>
    /// DLL file names (not paths) that are treated as required runtime dependencies for
    /// Fifth programs.  These are copied to the output directory and added as Roslyn metadata
    /// references when the package-lib directory is not available.
    ///
    /// Update this list when Fifth gains new runtime dependencies or targets a framework
    /// that requires different assemblies.
    /// </summary>
    public static readonly IReadOnlyList<string> DefaultRuntimeDependencyNames =
    [
        "Fifth.System.dll",
        "dotNetRdf.dll",
        "dotNetRdf.Client.dll",
        "VDS.Common.dll",
    ];

    /// <summary>
    /// Derives a three-part framework version string from a TFM such as
    /// <c>"net8.0"</c> → <c>"8.0.0"</c> or <c>"net9.0"</c> → <c>"9.0.0"</c>.
    /// Returns <see cref="DefaultFrameworkVersion"/> when <paramref name="targetFramework"/>
    /// is <see langword="null"/>, empty, or cannot be parsed.
    /// </summary>
    /// <param name="targetFramework">A target-framework moniker, e.g. <c>"net8.0"</c>.</param>
    public static string GetFrameworkVersion(string? targetFramework)
    {
        if (string.IsNullOrWhiteSpace(targetFramework))
        {
            return DefaultFrameworkVersion;
        }

        // Strip the leading "net" prefix (handles "net8.0", "net9.0", "net10.0", etc.)
        var versionPart = targetFramework.Trim();
        if (versionPart.StartsWith("net", StringComparison.OrdinalIgnoreCase))
        {
            versionPart = versionPart.Substring(3);
        }

        // Normalise to a three-part version (e.g. "8.0" → "8.0.0")
        if (Version.TryParse(versionPart, out var version))
        {
            var build = Math.Max(0, version.Build);
            return $"{version.Major}.{version.Minor}.{build}";
        }

        return DefaultFrameworkVersion;
    }
}
