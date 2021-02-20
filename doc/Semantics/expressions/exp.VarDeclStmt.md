# exp.VarDeclStmt

A variable declaration statement is a compound operation that declares a variable, and then binds it to a value in a single operation.  Since the operation is a compound of declare and bind, the code generated is the combination of the two.


Stack effect

    [value, id, \BindVariable, typename, id, \DeclareVariable] => []

Environment Effect

    {} => {id -> value}

Knowledge Graph Effect

    () => ()

Prerequisites:

1. The identifier MUST NOT exist in the environment prior to dispatching the var decl.
