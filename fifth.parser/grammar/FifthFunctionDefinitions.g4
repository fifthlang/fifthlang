grammar FifthFunctionDefinitions;
import FifthLexicalRules, FifthKeywords;

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
