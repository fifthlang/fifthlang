---
description: fifth-language-syntax
inclusion: always
---
## Syntax
- SYN-001 [MANDATORY]: Use canonical Fifth syntax in examples. To comply, use forms such as:

```fifth
class Person {
    Name: string;
    Height: float;
}

main(): int
{
  myprint(5 + 6);
  return 0;  
}
myprint(int x): int
{
  std.print(x);
}
```
- SYN-002: Declare variables in `name: type = value` form. To comply, use declarations such as:

```fifth
x: int = 42;
g: graph = KG.CreateGraph();
```
## Functions
- SYN-003: Always define functions with block bodies. To comply, use forms such as:

```fifth
greet(string name) {
    std.print("Hello " + name);
}
```
## Guards
- SYN-004: Write guards as parameter constraints, not `when` clauses. To comply, write guards like:

```fifth
myprint(int x | x == 0) { std.print(x); }
```
## Knowledge Graph
- SYN-005: Use canonical store and graph forms for knowledge-graph code. To comply, use forms such as:

```fifth
myStore: store = sparql_store(<http://example.org/store>);
g: graph = KG.CreateGraph();
```
- SYN-006: Use canonical literal syntax for TriG and SPARQL values. To comply, use `<{...}>` for TriG, `?<...>` for SPARQL, and supported scalar types only for object literals.
## Reference
- SYN-007: Use repository sample paths as canonical syntax references. To comply, prefer `test/ast-tests/CodeSamples/*.5th`, `src/parser/grammar/test_samples/*.5th`, and `docs/Getting-Started/`.
