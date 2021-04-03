grammar Fifth;

fifth:
    module_import*
    alias*
    functions+=function_declaration*
;

block:
    OPENBRACE explist CLOSEBRACE
;


function_call:
    function_name OPENPAREN exp (COMMA exp)* CLOSEPAREN
   ;


module_import: USE module_name (COMMA module_name)* SEMICOLON ;

module_name: IDENTIFIER;

packagename: IDENTIFIER ;

// ========[TYPE DEFINITIONS]=========

type_initialiser: type_name OPENBRACE type_property_init* CLOSEBRACE
;

type_name:  IDENTIFIER
;

type_property_init: var_name ASSIGN exp
;


// ========[KNOWLEDGE GRAPHS]=========

alias:
    'alias'
    name=packagename
    'as'
    uri=absoluteIri
    SEMICOLON
;

iri:
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



// ========[FUNC DEFS]=========

formal_parameters:
    parameter_declaration (COMMA parameter_declaration)*
;

function_declaration:
    result_type=function_type
    name=function_name
    args=function_args
    body=function_body
;

function_args:
    OPENPAREN
    formal_parameters?
    CLOSEPAREN
;

parameter_declaration:
    parameter_type
    parameter_name
;

parameter_type: IDENTIFIER
;

parameter_name: IDENTIFIER
;

function_body:
    LAMBDASEP explist SEMICOLON
;

function_name:
    IDENTIFIER
;

function_type:
    IDENTIFIER
;

// ========[LISTS]=========

// int[] nums = [0,1,2,3,4,5,6,7,8,9],
// int[] evens = [x | x <- nums, x % 2 == 0],
// int[] odds  = [x | x <- nums, x % 2 == 1],
// int[] recombined = evens + odds,
// int[] fifths = [x*5 | x <- recombined],


list_type_signature :
    type_name OPENBRACK CLOSEBRACK
;

list :
    OPENBRACK body=list_body CLOSEBRACK
;

list_body:
      list_literal          #EListLiteral
    | list_comprehension    #EListComprehension
;

list_literal:
    explist
;

list_comprehension:
    varname=var_name BAR gen=list_comp_generator (COMMA constraints=list_comp_constraint)
;

list_comp_generator:
    varname=var_name GEN value=var_name
;

list_comp_constraint:
    exp // must be of type PrimitiveBoolean
;

// ========[EXPRESSIONS]=========

explist
    : exp (COMMA exp)*
    ;

exp :
      left=exp LT right=exp                                                     # ELT
    | left=exp GT right=exp                                                     # EGT
    | left=exp LEQ right=exp                                                    # ELEQ
    | left=exp GEQ right=exp                                                    # EGEQ
    | left=exp AND right=exp                                                    # EAnd
    | left=exp PLUS right=exp                                                   # EAdd
    | left=exp MINUS right=exp                                                  # ESub
    | left=exp TIMES right=exp                                                  # EMul
    | left=exp DIVIDE right=exp                                                 # EDiv
    | MINUS operand=exp                                                         # EArithNegation
    | boolean                                                                   # EBool
    | value=INT                                                                 # EInt
    | value=FLOAT                                                               # EDouble
    | value=STRING                                                              # EString
    | WITH exp  block                                                           # WithStmt // this is not useful as is
    | decl=var_decl (ASSIGN exp)?                                               # VarDeclStmt
    | var_name ASSIGN exp                                                       # AssignmentStmt
    | var_name                                                                  # EVarname
    | funcname=function_name OPENPAREN (args=explist)? CLOSEPAREN               # EFuncCall
    | OPENPAREN innerexp=exp CLOSEPAREN                                         # EParen
    | NOT operand=exp                                                           # ELogicNegation
    | NEW type_initialiser                                                      # ETypeCreateInst
    | IF OPENPAREN condition=exp CLOSEPAREN ifpart=block ELSE elsepart=block    # IfElseStmt
    | WHILE OPENPAREN condition=exp CLOSEPAREN looppart=block                   # EWhile
    | value=list                                                                # EList
;

boolean: value=TRUE | value=FALSE ;

var_decl:
    (
      type_name
    | list_type_signature
    )
    var_name
;

var_name: IDENTIFIER
;





// ========[RESERVED WORDS]=========
ALIAS: 'alias';
AS: 'as';
ELSE: 'else';
IF: 'if';
WHILE: 'while';
NEW: 'new';
WITH: 'with';
RETURN: 'return';
USE: 'use';
TRUE: 'true' ;
FALSE: 'false' ;
LIST: 'list' ;



// ========[OPERATORS AND PUCTUATION]=========
AMP: '&';    // intersection
AND: '&&';    // intersection
ASSIGN: '=';    // equal
BAR: '|';     // union
CLOSEBRACE: '}';
CLOSEBRACK: ']';
CLOSEPAREN: ')';
COLON: ':';
COMMA: ',';
DIVIDE: '/';     // division
DOT: '.';
EQ: '==';    // equal
GEN: '<-' ; // list comprehension generator
GEQ: '>='   ; // greater-or-equal
GT: '>'   ;  // greater-than
HASH: '#';
LAMBDASEP: '=>';
LEQ: '<='   ; // less-or-equal
LT: '<'    ; // less-than
MINUS: '-';     // subtraction
NEQ: '!='    ;// not-equal
NOT: '!';     // negation
OPENBRACE: '{';
OPENBRACK: '[';
OPENPAREN: '(';
OR: '||';     // union
PERCENT: '%';     // modulo
PLUS: '+';     // addition
POWER: '^' ;
QMARK: '?';
SEMICOLON: ';' ;
TIMES: '*';     // multiplication

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
