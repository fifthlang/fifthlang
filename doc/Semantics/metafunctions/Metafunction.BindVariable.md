# Metafunction.BindVariable

Binding a variable specifically means setting the value bound to a variable name within the current or outer scopes.  It is specifically the use of a variable on the LHS of an assignment operation.  The RHS of an assignment is resolved directly by the `\DereferenceVariable` metafunction.

Stack effect

    [value, id, \BindVariable] => []

Environment Effect

    {id -> ?} => {id -> value}

Knowledge Graph Effect

    () => ()

Prerequisites:

1. The identifier MUST exist in the environment prior to dispatching the `\BindVariable`
2. The identifier MUST be bound to a value in the environment (must be initialised)
