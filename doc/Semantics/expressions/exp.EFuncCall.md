# exp.EFuncCall

Application of a function to a list of parameters, yielding a new value on the stack

Stack effect

    [<expression>, <expression>, id, \Apply] => [value]

Environment Effect

    {id -> <func def>} => {}

Knowledge Graph Effect

    () => ()

This builtin metafunction is responsible for invoking a function to get a value, and 
then placing that result value (if there is one) back on the stack.

This is of particular importance, since it is the means whereby the system, supports 
extension, new scopes etc.  In the process of performing this operation, the system 
will:

- spawn a new scope, for execution of the function
- resolve each of the parameters to the function
- make initial assignments for each of the function params in the environment
- execute the function definition
- copy the result of the function invocation back onto the calling stack
- scrap the new scope and clean up

Prerequisites:

1. The identifier MUST exist in the environment prior to dispatching the `\Apply`, and 
   it must be a reference to a function definition.
