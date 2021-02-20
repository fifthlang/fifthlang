# Metafunction.Assign

Performs an assignment within the stack frame

Stack effect

    [<expression>, id, \Assign] => []

Environment Effect

    {id -> ?} => {id -> value}

Knowledge Graph Effect

    () => ()

This builtin stack function has the effect of taking an identifier from the top of the
stack, plus the result of evaluating an expression, and creates/updates an entry in the
environment with the value bound to the identifier.

Prerequisites:

1. The identifier MUST exist in the environment prior to dispatching the `\Assign`
