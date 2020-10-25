grammar Fifth;

fifth:
    module_import*
    alias*
    statement*
    function_declaration* ;

alias:
    ALIAS iri AS packagename SEMICOLON;

block: OPENBRACE statement* CLOSEBRACE
;

explist
    : exp (COMMA exp)*
    ;

exp
    : exp LT exp               # ELT
    | exp GT exp               # EGT
    | exp LEQ exp               # ELEQ
    | exp GEQ exp               # EGEQ
    | exp AND exp               # EAnd
    | exp PLUS exp              # EAdd
    | exp MINUS exp             # ESub
    | exp TIMES exp             # EMul
    | exp DIVIDE exp            # EDiv
    | INT                       # EInt
    | FLOAT                     # EDouble
    | STRING                    # EString
    | var_name                  # EVarname
    | function_name OPENPAREN (exp (COMMA exp)*)? CLOSEPAREN  # EFuncCall
    | OPENPAREN exp CLOSEPAREN  # EFuncParen
    | NOT exp                   # ENegation
    | NEW type_initialiser      # ETypeCreate
    | statement                 #EStatement
;

formal_parameters:
    parameter_declaration (COMMA parameter_declaration)*
;

function_declaration:
    function_name
    function_args
    function_body
;

function_args:
    OPENPAREN
    formal_parameters?
    CLOSEPAREN
;

function_body:
    LAMBDASEP explist SEMICOLON
;

function_call
   : function_name OPENPAREN exp (COMMA exp)* CLOSEPAREN
   ;

function_name: IDENTIFIER ;

iri
   : IDENTIFIER COLON IDENTIFIER (QMARK iri_query)? (HASH IDENTIFIER)?
   ;
iri_query: iri_query_param ( AMP iri_query_param)* ;
iri_query_param:
    IDENTIFIER '=' IDENTIFIER;

module_import: USE module_name (COMMA module_name)* SEMICOLON ;

module_name: IDENTIFIER;

packagename: IDENTIFIER ;

parameter_declaration:
    parameter_type
    parameter_name
;

parameter_type: IDENTIFIER
;

parameter_name: IDENTIFIER
;

statement:
      type_name var_name (ASSIGN exp)?     # VarDeclStmt
    | var_name ASSIGN exp                  # AssignmentStmt
    | RETURN exp                           # ReturnStmt
    // | IF OPENPAREN exp CLOSEPAREN block # IfStmt // not sure this is valid when using statements as a kind of expression
    | IF OPENPAREN exp CLOSEPAREN block ELSE block   # IfElseStmt
    | WITH statement  block                # WithStmt
;

type_initialiser: type_name OPENBRACE type_property_init* CLOSEBRACE
;

type_name:  IDENTIFIER
;

type_property_init: var_name ASSIGN exp
;

var_name: IDENTIFIER
;

// RESERVED WORDS
ALIAS: 'alias';
AS: 'as';
ELSE: 'else';
IF: 'if';
NEW: 'new';
WITH: 'with';
RETURN: 'return';
USE: 'use';

// OPERATORS AND PUCTUATION
ASSIGN: '=';    // equal
CLOSEBRACE: '}';
CLOSEPAREN: ')';
COLON: ':';
COMMA: ',';
DIVIDE: '/';     // division
DOT: '.';
EQ: '==';    // equal
HASH: '#';
LAMBDASEP: '=>';
MINUS: '-';     // subtraction
OPENBRACE: '{';
OPENPAREN: '(';
PLUS: '+';     // addition
QMARK: '?';
TIMES: '*';     // multiplication
PERCENT: '%';     // modulo
POWER: '^' ;
NEQ: '!='    ;// not-equal
GT: '>'   ;  // greater-than
LT: '<'    ; // less-than
GEQ: '>='   ; // greater-or-equal
LEQ: '<='   ; // less-or-equal
AMP: '&';    // intersection
AND: '&&';    // intersection
OR: '||';     // union
NOT: '!';     // negation
SEMICOLON: ';' ;

// CHARACTER CLASSES ETC
IDENTIFIER: (LETTER|'_') (LETTER|DIGIT | '.')*;
fragment LETTER: [a-zA-Z];
STRING:'"' (~["])* '"'  | '\'' (~['])* '\'';
fragment EXP: [eE] [+\-]? INT;
fragment DIGIT: [0-9];
fragment HEXDIGIT: [0-9a-fA-F];
fragment POSITIVEDIGIT: [1-9];
INT: '0' | '-'? POSITIVEDIGIT DIGIT*;
FLOAT: '-'? INT ('.' DIGIT+ ) EXP?;
WS: [ \t\n\r]+ -> skip;
