grammar FifthLists;
import FifthLexicalRules, FifthKeywords;

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
