---
title: "[DEPRECATED] Announcing Graph Assertion Blocks in Fifth"
date: 2025-06-16
author: "Andrew Matthews"
summary: "Graph Assertion Blocks have been removed from Fifth. This post is kept for historical reference only. The language now uses RDF/SPARQL literals and operators instead."
draft: false
---

> **Deprecated — This feature has been removed from Fifth.**
> Graph Assertion Blocks (`<{ ... }>`) were superseded by dedicated RDF triple literals, TriG block literals, and SPARQL literal expressions, which provide cleaner and more explicit semantics. The content below is preserved for historical reference only. Do not use `<{ ... }>` syntax in new Fifth programs.

Today we're excited to announce Graph Assertion Blocks — a new, first-class construct in Fifth that lets your ordinary code produce knowledge graph facts with zero boilerplate. Wherever you can use a regular block, you can now write a Graph Assertion Block delimited by `<{` and `}>`. While the block runs, mutations to assertable objects are accumulated as assertions; on completion, you either get a graph value you can assign or persist, or, when used as a standalone statement, those assertions are written to the default knowledge graph.

**TL;DR:**
- New syntax: `<{ ... }>` works as both a statement and an expression.
- As a statement: persists to the default graph on successful completion.
- As an expression: yields a graph value; persist it explicitly with `store += graphValue` or assign it to a graph variable.
- Nested blocks, deterministic scoping, set semantics, and clear exception behavior.

## What's New (Historical)

- **Graph Assertion Block construct**: `<{ ... }>`
  - Behaves like a normal block for variables, control flow, and object state.
  - Additionally records assertions from mutations to assertable objects executed within the block.
- **Dual role** (statement + expression)
  - Statement-form: commits to the default graph if the block completes without an unhandled exception.
  - Expression-form: produces a graph value; nothing is persisted until you do it explicitly.
- **Explicit graph targeting**
  - Assign the result to a graph l-value (in-memory) or persist to a store via `storeVar += graphValue`.
  - Named graph scoping via `in <iri-or-alias>` is supported on graph variables.
- **Nesting and composition**
  - Inner blocks merge their assertions into the enclosing block's assertion set; commit happens at the outer boundary unless you explicitly persist earlier.
- **Set semantics + open world**
  - Identical triples deduplicate; multiple distinct values for the same predicate are allowed by design.
- **Robust error model**
  - On an unhandled exception: transactional l-value commits do not happen. Any explicit persist already performed to the default store isn't rolled back.
  - Missing default graph/store yields a clear 'Unknown Default Graph or Store' error.

## Syntax Overview

Statement-form (auto-persist to default graph on success):

```fifth
<{
   // Your code here; assertions come from assertable object mutations
}>;
```

Expression-form (produce a graph value; no auto-persist):

```fifth
g: graph = <{ /* compute facts */ }>;
store default = sparql_store(<http://example.org/store>);
default += g;                         // explicit persist
```

Graph variable with named graph scope:

```fifth
alias x as <http://example.org/people#>;

store default = sparql_store(<http://example.org/store>);

ericKnowledge : graph in <x:people> = <{
   d: datetime = new datetime(1926, 5, 14);
   eric.dob = d;
   eric.age = calculate_age(d);
}>;

default += ericKnowledge;  // persist to the store; scoped to <x:people>
```


## Semantics at a Glance

- **Execution model**: Runs like a regular block. Only statements actually executed contribute assertions. Works in loops/conditionals — only the executed paths assert.
- **Persistence model**: Statement-form auto-persists to default on success. Expression-form has no auto-persist; persist with `storeVar += graphValue`. Assignment to a graph variable is transactional at the commit boundary.
- **Nesting**: Inner assertions merge into the enclosing block's assertion set; the outer boundary determines commit unless you explicitly persist earlier.
- **Exceptions**: Unhandled exception means transactional l-values do not commit; explicit default store writes already performed aren't rolled back.
- **Set semantics**: Identical triples deduplicate; contradictory facts are allowed (open world).
- **Aliases**: Prefix/alias resolution inside the block follows the normal Fifth rules and the in-scope declarations.

## Design Principles Reflected

- **Code as facts**: write ordinary imperative code; get graph assertions for free.
- **Explicit persistence**: expression-form never auto-writes; you stay in control.
- **Predictable scope**: only executed statements assert; nesting composes naturally.
- **Open world**: multiple values are allowed; identical triples deduplicate.

## What's Next

- Ergonomics: additional sugar for common assertion patterns.
- Tooling: richer diagnostics and editor hints for assertion-producing code.
- Performance: further tuning for large assertion sets and batch persistence.

If you build something with Graph Assertion Blocks, we'd love to hear about it. Share examples, questions, and feedback in the [GitHub Discussions](https://github.com/aabs/fifthlang/discussions) — it helps us refine the feature and prioritize what comes next.
