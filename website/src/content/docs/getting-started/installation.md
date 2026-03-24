---
title: "Installation Guide"
description: "Install the Fifth compiler and SDK using the .NET toolchain"
category: "getting-started"
order: 1
---

Fifth integrates with the .NET toolchain. You install the compiler as a .NET tool and the SDK resolves automatically from NuGet when you build. No platform-specific downloads needed.

## Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)

Verify with:

```bash
dotnet --version  # Should show 10.0.x
```

## Install the Compiler

Install the Fifth compiler as a global .NET tool:

```bash
dotnet tool install --global Fifth.Compiler.Tool --version 0.9.0
```

Verify it works:

```bash
fifthc --version
```

### Using a Local Tool Manifest (recommended for teams)

A local tool manifest pins the compiler version per-project so everyone on the team uses the same version.

Create `.config/dotnet-tools.json` in your project root:

```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "fifth.compiler.tool": {
      "version": "0.9.0",
      "commands": ["fifthc"]
    }
  }
}
```

Then restore:

```bash
dotnet tool restore
```

## Set Up the SDK

The Fifth.Sdk is an MSBuild SDK that lets you build `.5thproj` files with `dotnet build`. Pin the version in a `global.json` at your project or solution root:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.9.0"
  }
}
```

MSBuild pulls the SDK package from NuGet automatically on first build. No manual download required.

## Create a Project

Create a `.5thproj` file:

```xml
<Project Sdk="Fifth.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <FifthCompilerCommand>fifthc</FifthCompilerCommand>
  </PropertyGroup>
</Project>
```

Add a source file, e.g. `main.5th`:

```
main(): void {
    std.print("Hello from Fifth!");
}
```

Build and run:

```bash
dotnet build
dotnet run
```

## Updating

To update the compiler tool:

```bash
# Global install
dotnet tool update --global Fifth.Compiler.Tool

# Or local manifest
dotnet tool update fifth.compiler.tool
```

To update the SDK, bump the version in `global.json`:

```json
{
  "msbuild-sdks": {
    "Fifth.Sdk": "0.10.0"
  }
}
```

Keep the compiler tool and SDK versions in sync. The next `dotnet build` will pull the new SDK automatically.

## Build from Source

If you prefer to build the compiler from source:

```bash
git clone https://github.com/aabs/fifthlang.git
cd fifthlang
dotnet restore fifthlang.sln
dotnet build fifthlang.sln
```

Requires Java 17+ (for ANTLR grammar compilation). The build takes 1-2 minutes on first run.

## Next Steps

- [Full Project Setup](/docs/getting-started/project-setup) — Multi-project solutions with ProjectReference and SLNX
