# Semantics

This folder contains descriptions of the semantics of each operation available as part of the language.

There are three places where it is possible for an operation to hace side-effects:

- In the environment - by creating, setting or modifying the binding for a variable in the scope.
- On the stack - the ongoing dispatching of an operation has the effect of consuming or contributing elements on the stack of the scope
- In the knowledge base - values on the stack, or entities in the environment can be promoted into the knowledge base for the scope.

## Notation

**Stack Effect**: `[] => []`

    [state prior to dispatching operation] => [State after dispatch]

**Environment Effect**: `{id -> ?, id2 -> ?} => {id -> value, id2 -> value2}`

where `id` is a string with the name of the variable, `?` means whatever value, and `value` typically means whatever concrete value the variable was bound to.

**Knowledge Graph Effect**: `() => (<s,p,o>)`



The individual elements are represented as growing towards the right.  That means the bottom of the stack is on the left of the elements in the stack.

For example, if we had an empty stack, and we performed two pushes like so:

    push(5), push(6)

You would expect `5` as the first pushed element to be at the bottom of the stack at the end:

    [] => [5, 6]

Popping the stack would of course return `6` first and then `5`.

The reason for belaboring this point is to illustrate the fact that the code generation for operations needs to be conscious of the reverse order of emitting elements onto the stack to allow consumption in the order required.  We need to start by thinking of the order of how we want to consume elements from the stack, and work backwards from that to understand what order they need to be pushed onto the stack.

## Grammatical Components

The elements whose semantics we are documenting, primarily need to come from the language grammar.  But the language runtime makes use of 'meta-functions'.  Meta-functions are ones that, when dispatched, act on the environment itself.  This means that we can use meta-functions to perform actions like declaring variables, creating new scopes, or managing knowledge bases.

I'll start by documenting the metafunctions, which can then form a language used to describe what code gets generated for the top-level grammatical elements.

- Metafunction.Assign (redundant)
- Metafunction.DeclareVariable
- Metafunction.BindVariable
- Metafunction.DereferenceVariable

- fifth
- alias
- block
- explist
- exp.ELT
- exp.EGT
- exp.ELEQ
- exp.EGEQ
- exp.EAnd
- exp.EAdd
- exp.ESub
- exp.EMul
- exp.EDiv
- exp.EArithNegation
- exp.EBool
- exp.EInt
- exp.EDouble
- exp.EString
- exp.WithStmt
- exp.VarDeclStmt
- exp.AssignmentStmt
- exp.EVarname
- exp.EFuncCall
- exp.EParen
- exp.ELogicNegation
- exp.ETypeCreateInst
- exp.IfElseStmt
- formal_parameters
- function_declaration
- function_args
- function_body
- function_call
- function_name
- iri
- qNameIri
- absoluteIri
- iri_query_param
- module_import
- module_name
- packagename
- parameter_declaration
- parameter_type
- parameter_name
- type_initialiser
- type_name
- type_property_init
- var_name
