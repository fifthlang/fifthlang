# Metafunction.DereferenceVariable

Dereferencing a variable specifically means getting the value bound to a variable name within the current or outer scopes.  It is different from the use of a variable on the lhs of an assignment operation.  The LHS of an assignment is resolved directly by the `\Assign` metafunction.

Stack effect

    [id, \DereferenceVariable] => [value]

Environment Effect

    {id -> value} => {id -> value}

Knowledge Graph Effect

    () => ()

Prerequisites:

1. The identifier MUST exist in the environment prior to dispatching the `\Assign`
2. The identifier MUST be bound to a value in the environment (must be initialised)
