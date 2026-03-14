---
inclusion: manual
---

# Fifth Language Syntax Reference

## Basic Syntax

```fifth
class Person {
    Name: string;
    Height: float;
}

main() => myprint(5 + 6);
myprint(int x) => std.print(x);
```

## Variable Declarations

Use `name: type = value` form. Never use `var name =` (C#/JS-style) or `type name =` (type-first).

```fifth
x: int = 42;
g: graph = KG.CreateGraph();
```

## Function Definitions

```fifth
// Expression body
add(int a, int b) => a + b;

// Block body
greet(string name) {
    std.print("Hello " + name);
}
```

## Parameter Constraints (Guards)

Use the parameter constraint form with block bodies:

```fifth
// Correct: parameter constraint form
myprint(int x | x == 0) { std.print(x); }

// INVALID: legacy 'when' shorthand
// myprint(int x) when x == 0 => std.print(x);
```

## Knowledge Graph Constructs

```fifth
// Store declarations
myStore: store = sparql_store(<http://example.org/store>);
store default = sparql_store(<http://example.org/default>);

// Graph operations
g: graph = KG.CreateGraph();
// Add triples with += operator
```

## TriG and SPARQL Literals

- TriG literals: `<{...}>`
- SPARQL literals: `?<...>`
- Literal types in object position: strings, booleans, chars, all signed/unsigned integers, float, double, decimal

## Sample Files

- Test code samples: `test/ast-tests/CodeSamples/*.5th`
- Parser test samples: `src/parser/grammar/test_samples/*.5th`
- Documentation examples: `docs/Getting-Started/`
