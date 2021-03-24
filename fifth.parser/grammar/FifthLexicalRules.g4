lexer grammar FifthLexicalRules;

// OPERATORS AND PUCTUATION
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
