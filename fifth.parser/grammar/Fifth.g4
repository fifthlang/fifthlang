grammar Fifth;

fifth:
    module_import*
    alias*
    functions+=function_declaration* ;

alias:
    ALIAS LT absoluteIri GT AS packagename SEMICOLON;

block: OPENBRACE statement* CLOSEBRACE
;

explist
    : exp (COMMA exp)*
    ;

exp
    : left=exp LT right=exp      # ELT
    | left=exp GT right=exp      # EGT
    | left=exp LEQ right=exp     # ELEQ
    | left=exp GEQ right=exp     # EGEQ
    | left=exp AND right=exp     # EAnd
    | left=exp PLUS right=exp    # EAdd
    | left=exp MINUS right=exp   # ESub
    | left=exp TIMES right=exp   # EMul
    | left=exp DIVIDE right=exp  # EDiv
    | MINUS operand=exp          # EArithNegation
    | value=INT                  # EInt
    | value=FLOAT                # EDouble
    | value=STRING               # EString
    | var_name                   # EVarname
    | funcname=function_name OPENPAREN (args=explist)? CLOSEPAREN  # EFuncCall
    | OPENPAREN innerexp=exp CLOSEPAREN  # EParen
    | NOT operand=exp           # ELogicNegation
    | NEW type_initialiser      # ETypeCreateInst
    | statement                 # EStatement
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
   :
   qNameIri | absoluteIri
   ;


qNameIri: prefix=IDENTIFIER? COLON fragname=IDENTIFIER ;

absoluteIri :
    iri_scheme=IDENTIFIER
    COLON DIVIDE DIVIDE
    iri_domain+=IDENTIFIER (DOT iri_domain+=IDENTIFIER)*
    (DIVIDE iri_segment+=IDENTIFIER)*
    DIVIDE?
    (HASH IDENTIFIER?)?
    // (QMARK iri_query_param (AMP iri_query_param)*)?
;

iri_query_param:
    name=IDENTIFIER ASSIGN val=IDENTIFIER;

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
    | IF OPENPAREN condition=exp CLOSEPAREN ifpart=block ELSE elsepart=block   # IfElseStmt
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
// IRI_PARAM_VALUE: (LETTER|DIGIT|URI_PUNCT)+;
IDENTIFIER: (LETTER|'_') (LETTER|DIGIT | '.')*;
fragment LETTER: [a-zA-Z];
// fragment URI_PUNCT: [%\-_+];
STRING:'"' (~["])* '"'  | '\'' (~['])* '\'';
fragment EXP: [eE] [+\-]? INT;
fragment DIGIT: [0-9];
fragment HEXDIGIT: [0-9a-fA-F];
fragment POSITIVEDIGIT: [1-9];
INT: '0' | '-'? POSITIVEDIGIT DIGIT*;
FLOAT: '-'? INT ('.' DIGIT+ ) EXP?;
WS: [ \t\n\r]+ -> skip;