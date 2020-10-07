lexer grammar FifthLexer;

channels { COMMENTS_CHANNEL, DIRECTIVE }

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
COMMA: ',';
DIVIDE: '/';     // division
DOT: '.';
EQ: '==';    // equal
LAMBDASEP: '=>';
MINUS: '-';     // subtraction
OPENBRACE: '{';
OPENPAREN: '(';
PLUS: '+';     // addition
TIMES: '*';     // multiplication
PERCENT: '%';     // modulo
POWER: '^' ;
NEQ: '!='    ;// not-equal
GT: '>'   ;  // greater-than
LT: '<'    ; // less-than
GEQ: '>='   ; // greater-or-equal
LEQ: '<='   ; // less-or-equal
AND: '&&';    // intersection
OR: '||';     // union
NOT: '!';     // negation
SEMICOLON: ';' ;

// CHARACTER CLASSES ETC
URICONSTANT: LT STRING GT ;
IDENTIFIER: IDSTART IDPART*;
IDSTART: LETTER | '_';
IDPART: IDSTART | DIGIT;
TIMEINTERVAL: NAT [SMHD];
LETTER: [a-zA-Z];
DIGIT: [0-9];
POSITIVEDIGIT: [1-9];
NAT: POSITIVEDIGIT DIGIT*;
STRING:'"' (~["])* '"' 
     | '\'' (~['])* '\'';
FLOAT: '-'? INT ('.' DIGIT+ )? EXP?;
INT: '0' | [1-9] [0-9]*;
EXP: [eE] [+\-]? INT;
WS: [ \t\n\r] + -> skip;

/////////////////////

VARIABLE
   : VALID_ID_START VALID_ID_CHAR*
   ;


fragment VALID_ID_START
   : ('a' .. 'z') | ('A' .. 'Z') | '_'
   ;


fragment VALID_ID_CHAR
   : VALID_ID_START | ('0' .. '9')
   ;


ScientificNumber
   : NUMBER ((E1 | E2) SIGN? NUMBER)?
   ;

fragment E1
   : 'E'
   ;


fragment E2
   : 'e'
   ;
fragment SIGN
   : ('+' | '-')
   ;
fragment NUMBER
   : ('0' .. '9') + ('.' ('0' .. '9') +)?
   ;
