parser grammar FifthParser;

options { tokenVocab=FifthLexer; }

exp: exp AND exp 
    | exp  PLUS exp 
    | exp  MINUS exp 
    | exp TIMES exp 
    | exp DIVIDE exp 
    | INT
    | FLOAT 
    | STRING 
    | qvarname 
    | function_name OPENPAREN exp? CLOSEPAREN 
    | qvarname OPENPAREN exp? CLOSEPAREN 
    | OPENPAREN exp CLOSEPAREN 
    | NOT exp 
    | NEW type_initialiser 
;

fifth:
    module_import* 
    alias* 
    statement* 
    function_declaration* ;

alias:
    ALIAS URICONSTANT AS packagename SEMICOLON;



exp_list:
    exp
    (COMMA exp)*
;

formal_parameters:
    parameter_declaration
    (COMMA parameter_declaration)*
;

function_declaration:
    function_name 
    function_args
    function_body
        SEMICOLON
; 

function_args:
    OPENPAREN
    formal_parameters?
    CLOSEPAREN
; 

function_body:
    LAMBDASEP
    exp_list
;

function_call
   : function_name OPENPAREN exp (COMMA exp)* CLOSEPAREN
   ;

function_name: IDENTIFIER ;

packagename: IDENTIFIER ;

module_import: USE IDENTIFIER SEMICOLON ;

parameter_declaration:
    IDENTIFIER
    IDENTIFIER
;
block: OPENBRACE statement* CLOSEBRACE 
;

parameter_type: IDENTIFIER
;

parameter_name: IDENTIFIER
;

qvarname: var_name (DOT var_name)
;

statement: qvarname ASSIGN exp 
            | RETURN exp 
            | IF OPENPAREN exp CLOSEPAREN block 
            | IF OPENPAREN exp CLOSEPAREN block ELSE block 
            | WITH statement 
            | exp 
;

type_initialiser: type_name OPENBRACE type_property_init* CLOSEBRACE 
;

type_name:  qvarname 
;

type_property_init: var_name ASSIGN exp 
;

var_name: IDENTIFIER
;

variable: IDENTIFIER 
;


