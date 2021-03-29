grammar FifthExpressions;
import FifthLexicalRules, FifthKeywords, FifthLists;

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

var_decl:
    type_name var_name
;

var_name: IDENTIFIER
;


