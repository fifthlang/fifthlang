grammar FifthTypeDefinition;
import FifthLexicalRules, FifthKeywords;

type_initialiser: type_name OPENBRACE type_property_init* CLOSEBRACE
;

type_name:  IDENTIFIER
;

type_property_init: var_name ASSIGN exp
;

