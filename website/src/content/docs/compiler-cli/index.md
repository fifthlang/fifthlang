---
title: "Compiler CLI Reference"
description: "Command-line interface reference for the Fifth compiler"
category: "compiler-cli"
order: 1
---

The Fifth compiler (`fifth`) provides a command-line interface for compiling, running, and linting Fifth source files.

> This section is under construction. For installation instructions, see the [Installation Guide](/docs/getting-started/installation/).

## Basic Usage

```bash
# Compile a Fifth source file
fifth --source hello.5th --output hello.exe

# Compile and run
fifth --command run --source hello.5th --output hello.exe --args "--name Ada"

# Lint source files
fifth --command lint --source src/
```

## Commands (Coming Soon)

- **build** — Compile Fifth source files to .NET assemblies
- **run** — Compile and execute a Fifth program
- **lint** — Check source files for common issues

## Options

- `--source <path>` — Path to the Fifth source file or directory
- `--output <path>` — Path for the output executable
- `--command <cmd>` — Command to execute (build, run, lint)
- `--args <args>` — Arguments to pass to the compiled program (with `run`)

## Contributing

If you'd like to help expand this reference, check out the [Get Involved](/get-involved/) page for ways to contribute.
