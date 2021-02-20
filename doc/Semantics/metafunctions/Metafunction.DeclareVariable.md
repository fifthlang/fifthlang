# Metafunction.DeclareVariable

Declaring a variable means creating an entry in the environment for the scope.  It does NOT mean binding a value to that variable in the environment.

Stack effect

    [typename, id, \DeclareVariable] => []

Environment Effect

    {} => {id -> ?}

Knowledge Graph Effect

    () => ()

Prerequisites:

1. The identifier MUST NOT exist in the environment prior to dispatching the `\Assign`
