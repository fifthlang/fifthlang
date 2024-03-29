grammar Fifth;

fifth
    : module_import* alias*
    ( functions+=function_declaration
    | classes += class_definition
    )*
    ;


function_call
    : function_name OPENPAREN exp (COMMA exp)* CLOSEPAREN
    ;

module_import
    : USE module_name (COMMA module_name)* SEMICOLON
    ;

module_name
    : IDENTIFIER
    ;

packagename
    : IDENTIFIER
    ;

// ========[TYPE DEFINITIONS]=========
class_definition
    : CLASS
      name=IDENTIFIER
      OPENBRACE
      ( functions += function_declaration
      | properties += property_declaration
      )*
      CLOSEBRACE
    ;

property_declaration
    : name=IDENTIFIER  COLON type=IDENTIFIER SEMICOLON
    ;

member_access
    : DOT IDENTIFIER
    ;

type_initialiser
    : typename=type_name OPENBRACE
        properties+=type_property_init
        (COMMA
            properties+=type_property_init
        )*
      CLOSEBRACE
    ;

type_name
    :  IDENTIFIER
    ;

// Ex: foo.bar = 5 * 23
type_property_init
    : var_name ASSIGN exp
    ;

// ========[FUNC DEFS]=========
// Ex: Foo(x:int, y:int):int { . . . }
function_declaration
    : name=function_name
      args=function_args
      COLON
      result_type=function_type
      body=function_body
    ;

// Ex: `x:int, y:float`
formal_parameters
    : parameter_declaration (COMMA parameter_declaration)*
    ;

// Ex: `( x:int, y:float )`
function_args
    : OPENPAREN formal_parameters? CLOSEPAREN
    ;

// Ex: `addr:Address`
// Ex: `p:Person{a:Age | a < 32}`
parameter_declaration
    : parameter_name COLON parameter_type variable_constraint?  # ParamDecl
    | type_destructuring_paramdecl                              # ParamDeclWithTypeDestructure
    ;

// Ex: `p:Person{a:Age | a < 32}`
type_destructuring_paramdecl
    : parameter_name
      COLON
      parameter_type
      OPENBRACE
        bindings+=property_binding
        ( COMMA bindings+=property_binding )*
      CLOSEBRACE
    ;

// Ex: `age : Age | age < 32`
property_binding
    : bound_variable_name=var_name
      COLON
      property_name=var_name
      variable_constraint?
    ;

variable_constraint
    : BAR constraint=exp
    ;

// Ex:  foo.bar.baz
parameter_type
    : identifier_chain
    ;

parameter_name
    : IDENTIFIER
    ;

function_body
    : block
    ;

function_name
    : identifier_chain
    ;

function_type
    : IDENTIFIER
    ;

// ========[STATEMENTS]=========
block
    : OPENBRACE (statement SEMICOLON)* CLOSEBRACE
    ;

statement
    : IF OPENPAREN condition=exp CLOSEPAREN ifpart=block (ELSE elsepart=block)? # SIfElse
    | WHILE OPENPAREN condition=exp CLOSEPAREN looppart=block                   # SWhile
    | WITH exp  block                                                           # SWith // this is not useful as is
    | decl=var_decl (ASSIGN exp)?                                               # SVarDecl
    | var_name ASSIGN exp                                                       # SAssignment
    | RETURN exp                                                                # SReturn
    | exp                                                                       # SBareExpression
    ;

var_decl
    :  var_name COLON ( type_name | list_type_signature )
    ;


// ========[EXPRESSIONS]=========
explist
    : exp (COMMA exp)*
    ;

exp
    : OPENPAREN type=type_name CLOSEPAREN subexp=exp                # ETypeCast
    | value=truth_value                                             # EBool
    | value=INT                                                     # EInt
    | value=FLOAT                                                   # EDouble
    | value=STRING                                                  # EString
    | value=truth_value                                             # EBoolean
    | value=list                                                    # EList
    | NOT operand=exp                                               # ELogicNegation
    | MINUS operand=exp                                             # EArithNegation
    | left=exp LT right=exp                                         # ELT
    | left=exp GT right=exp                                         # EGT
    | left=exp LEQ right=exp                                        # ELEQ
    | left=exp GEQ right=exp                                        # EGEQ
    | left=exp AND right=exp                                        # EAnd
    | left=exp PLUS right=exp                                       # EAdd
    | left=exp MINUS right=exp                                      # ESub
    | left=exp TIMES right=exp                                      # EMul
    | left=exp DIVIDE right=exp                                     # EDiv
    | var_name                                                      # EVarname
    | funcname=function_name OPENPAREN (args=explist)? CLOSEPAREN   # EFuncCall
    | OPENPAREN innerexp=exp CLOSEPAREN                             # EParen
    | NEW type_initialiser                                          # ETypeCreateInst
    ;

truth_value
    : value=TRUE | value=FALSE
    ;

// Ex: `foo.bar.baz`
identifier_chain
    : segments+=IDENTIFIER (DOT segments+=IDENTIFIER)*
    ;
var_name
    : identifier_chain
    ;


// ========[KNOWLEDGE GRAPHS]=========
alias
    : ALIAS name=packagename AS uri=absoluteIri SEMICOLON
    ;

iri
    : qNameIri | absoluteIri
    ;


qNameIri
    : prefix=IDENTIFIER? COLON fragname=IDENTIFIER
    ;

absoluteIri
    : iri_scheme=IDENTIFIER
      COLON DIVIDE DIVIDE
      iri_domain+=IDENTIFIER (DOT iri_domain+=IDENTIFIER)*
      (DIVIDE iri_segment+=IDENTIFIER)*
      DIVIDE?
      (HASH IDENTIFIER?)?
      // (QMARK iri_query_param (AMP iri_query_param)*)?
    ;

iri_query_param
    : name=IDENTIFIER ASSIGN val=IDENTIFIER
    ;



// ========[LISTS]=========

// int[] nums = [0,1,2,3,4,5,6,7,8,9],
// int[] evens = [x | x <- nums, x % 2 == 0],
// int[] odds  = [x | x <- nums, x % 2 == 1],
// int[] recombined = evens + odds,
// int[] fifths = [x*5 | x <- recombined],


list_type_signature
    : type_name OPENBRACK CLOSEBRACK
    ;

list
    : OPENBRACK body=list_body CLOSEBRACK
    ;

list_body
    : list_literal          #EListLiteral
    | list_comprehension    #EListComprehension
    ;

list_literal
    : explist
    ;

list_comprehension
    : varname=var_name BAR gen=list_comp_generator (COMMA constraints=list_comp_constraint)
    ;

list_comp_generator
    : varname=var_name GEN value=var_name
    ;

list_comp_constraint
    : exp // must be of type PrimitiveBoolean
    ;




// ========[RESERVED WORDS]=========
ALIAS: 'alias';
AS: 'as';
CLASS: 'class' ;
ELSE: 'else';
FALSE: 'false' ;
IF: 'if';
LIST: 'list' ;
NEW: 'new';
RETURN: 'return';
USE: 'use';
TRUE: 'true' ;
WHILE: 'while';
WITH: 'with';



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
UNDERSCORE: '_' ;

// CHARACTER CLASSES ETC
// IRI_PARAM_VALUE: (LETTER|DIGIT|URI_PUNCT)+;
IDENTIFIER: (LETTER|UNDERSCORE) (LETTER|DIGIT|UNDERSCORE)*;
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
