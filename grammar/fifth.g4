   grammar fifth;

fifth:
    module_import*
    function_declaration*
;

function_declaration:
    function_name 
    function_args
    function_body
        Semicolon
; 

function_args:
    OpenParen
    formal_parameters?
    CloseParen
; 

function_body:
    LambdaSep
    expression_list
;

function_name: Identifier ;

expression_list:
                   expression
                   (Comma expression)*
               ;

equation
   : expression relop expression
   ;

expression
   : multiplying_expression ((Plus | Minus) multiplying_expression)*
   ;

multiplying_expression
   : pow_expression ((Times | Divide) pow_expression)*
   ;

pow_expression
   : signed_atom (Power signed_atom)*
   ;

relop
   : EQ
   | GT
   | LT
   ;

signed_atom
   : Plus signed_atom
   | Minus signed_atom
   | function_call
   | atom
   ;

function_call
   : function_name OpenParen expression (Comma expression)* CloseParen
   ;

variable: Identifier ;
atom
   : scientific
   | variable
   | OpenParen expression CloseParen
   ;

scientific
   : ScientificNumber
   ;


formal_parameters:
    parameter_declaration
    (Comma parameter_declaration)*
;

parameter_declaration:
    Identifier
    Identifier
;

parameter_type: Identifier;

parameter_name: Identifier;

module_import: Use Identifier Semicolon ;

OpenParen: '(';
CloseParen: ')';
Comma: ',';
LambdaSep: '=>';
Use: 'use';
Plus: '+';     // addition
Minus: '-';     // subtraction
Times: '*';     // multiplication
Divide: '/';     // division
Percent: '%';     // modulo
Power: '^' ;
EQ: '==';    // equal
NEQ: '!='    ;// not-equal
GT: '>'   ;  // greater-than
LT: '<'    ; // less-than
GEQ: '>='   ; // greater-or-equal
LEQ: '<='   ; // less-or-equal

And: '&&';    // intersection
Or: '||';     // union

Semicolon: ';' ;

Identifier: IdStart IdPart*;
IdStart: Letter | '_';
IdPart: IdStart | Digit;
TimeInterval: Nat [smhd];
Letter: [a-zA-Z];
Digit: [0-9];
PositiveDigit: [1-9];
Nat: PositiveDigit Digit*;
String:'"' (~["])* '"' 
     | '\'' (~['])* '\'';
Float: '-'? Int ('.' Digit+ )? Exp?;
Int: '0' | [1-9] [0-9]*;
Exp: [Ee] [+\-]? Int;
Ws: [ \t\n\r] + -> skip;

/////////////////////

VARIABLE
   : VALID_ID_START VALID_ID_CHAR*
   ;


fragment VALID_ID_START
   : ('a' .. 'z') | ('A' .. 'Z') | '_'
   ;


fragment VALID_ID_CHAR
   : VALID_ID_START | ('0' .. '9')
   ;


ScientificNumber
   : NUMBER ((E1 | E2) SIGN? NUMBER)?
   ;

fragment E1
   : 'E'
   ;


fragment E2
   : 'e'
   ;
fragment SIGN
   : ('+' | '-')
   ;
fragment NUMBER
   : ('0' .. '9') + ('.' ('0' .. '9') +)?
   ;
