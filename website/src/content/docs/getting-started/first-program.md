---
title: "Your First Program"
description: "Write, compile, and run your first Fifth program"
category: "getting-started"
order: 2
---

This guide walks you through writing a simple Fifth program, compiling it, and running it. By the end, you'll have a working program and understand the basic structure of Fifth code.

## Prerequisites

Make sure you have the Fifth compiler installed. If not, follow the [Installation Guide](/docs/getting-started/installation) first.

Verify your installation:

```bash
fifth --version
```

## Create Your First Program

Create a new file called `hello.5th` with the following content:

```fifth
// hello.5th — Your first Fifth program

main(): int {
    x: int;
    x = 42;
    return 0;
}
```

Every Fifth program needs a `main` function as its entry point. The `main` function returns an `int` — by convention, `0` indicates success.

## Understanding the Code

Let's break down what's happening:

- `main(): int` — declares a function named `main` that takes no parameters and returns an `int`
- `x: int;` — declares a variable `x` of type `int` using Fifth's colon syntax
- `x = 42;` — assigns the value `42` to `x`
- `return 0;` — returns `0` to indicate the program completed successfully

## Compile and Run

Compile your program with the Fifth compiler:

```bash
fifth hello.5th
```

Then run the compiled output:

```bash
dotnet hello.dll
```

## Adding Functions

Fifth supports defining functions outside of `main`. Create a file called `math.5th`:

```fifth
// math.5th — Working with functions

add(a: int, b: int): int {
    return a + b;
}

square(x: int): int {
    return x * x;
}

main(): int {
    sum: int;
    sum = add(3, 4);

    sq: int;
    sq = square(sum);

    return 0;
}
```

Functions are declared with the syntax `name(params): returnType { body }`. Parameters use the same colon syntax as variable declarations: `name: type`.

## Working with Different Types

Fifth supports several built-in types. Here's a quick taste:

```fifth
main(): int {
    // Integer types
    count: int;
    count = 100;

    // Floating-point
    pi: float;
    pi = 3.14159;

    // Booleans
    active: bool;
    active = true;

    // Strings
    greeting: string;
    greeting = "Hello, Fifth!";

    return 0;
}
```

## A Glimpse of Knowledge Graphs

One of Fifth's unique features is native support for knowledge graphs. Here's a preview:

```fifth
alias ex as <http://example.org/>;
alias foaf as <http://xmlns.com/foaf/0.1/>;

main(): int {
    g : graph in <ex:> = KG.CreateGraph();
    g += <ex:alice, foaf:name, "Alice">;
    g += <ex:alice, foaf:age, 30>;
    g += <ex:alice, ex:knows, ex:bob>;

    return 0;
}
```

This creates an RDF knowledge graph with triples describing a person named Alice. Knowledge graphs are a first-class feature in Fifth — no external libraries needed.

## Next Steps

- [Project Setup](/docs/getting-started/project-setup) — Set up a multi-project Fifth solution with the SDK
- [Learn Fifth in Y Minutes](/tutorials/learn-fifth-in-y-minutes) — A rapid tour of all Fifth syntax and features
