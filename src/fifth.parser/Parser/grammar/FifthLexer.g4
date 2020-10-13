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
URICONSTANT: LT [a-zA-Z0-9\-:/?&]+ GT ;
IDENTIFIER: IDSTART IDPART*;
IDSTART: LETTER | '_';
IDPART: IDSTART | DIGIT;
TIMEINTERVAL: NAT [SMHD];
LETTER: [a-zA-Z];
DIGIT: [0-9];
HEXDIGIT: [0-9a-fA-F];
POSITIVEDIGIT: [1-9];
NAT: POSITIVEDIGIT DIGIT*;
STRING:'"' (~["])* '"'  | '\'' (~['])* '\'';
FLOAT: '-'? INT ('.' DIGIT+ )? EXP?;
INT: '0' | POSITIVEDIGIT DIGIT*;
EXP: [eE] [+\-]? INT;
WS: [ \t\n\r]+ -> skip;

/////////////////////

VARIABLE: VALID_ID_START VALID_ID_CHAR*;
fragment VALID_ID_START: ('a' .. 'z') | ('A' .. 'Z') | '_';
fragment VALID_ID_CHAR: VALID_ID_START | ('0' .. '9');
ScientificNumber: NUMBER ((E1 | E2) SIGN? NUMBER)?;

fragment E1: 'E';
fragment E2: 'e';
fragment SIGN: ('+' | '-');
fragment NUMBER: ('0' .. '9') + ('.' ('0' .. '9') +)?;



/*
 * IRI Lexer characters and tokens from antlr grammar v4
 */
UCSCHAR: '\u00A0' .. '\uD7FF'| '\uF900' .. '\uFDCF'| '\uFDF0' .. '\uFFEF'| '\u{10000}' .. '\u{1FFFD}'| '\u{20000}' .. '\u{2FFFD}'| '\u{30000}' .. '\u{3FFFD}'| '\u{40000}' .. '\u{4FFFD}'| '\u{50000}' .. '\u{5FFFD}'| '\u{60000}' .. '\u{6FFFD}'| '\u{70000}' .. '\u{7FFFD}'| '\u{80000}' .. '\u{8FFFD}'| '\u{90000}' .. '\u{9FFFD}'| '\u{A0000}' .. '\u{AFFFD}'| '\u{B0000}' .. '\u{BFFFD}'| '\u{C0000}' .. '\u{CFFFD}'| '\u{D0000}' .. '\u{DFFFD}'| '\u{E1000}' .. '\u{EFFFD}';

/// iprivate       = %xE000-F8FF / %xF0000-FFFFD / %x100000-10FFFD

IPRIVATE: '\uE000' .. '\uF8FF'| '\u{F0000}' .. '\u{FFFFD}'| '\u{100000}' .. '\u{10FFFD}';
D0: '0';
D1: '1';
D2: '2';
D3: '3';
D4: '4';
D5: '5';
D6: '6';
D7: '7';
D8: '8';
D9: '9';
A: [aA];
B: [bB];
C: [cC];
D: [dD];
E: [eE];
F: [fF];
G: [gG];
H: [hH];
I: [iI];
J: [jJ];
K: [kK];
L: [lL];
M: [mM];
N: [nN];
O: [oO];
P: [pP];
Q: [qQ];
R: [rR];
S: [sS];
T: [tT];
U: [uU];
V: [vV];
W: [wW];
X: [xX];
Y: [yY];
Z: [zZ];
COL2: '::';
COL: ':';

HYPHEN: '-';
TILDE: '~';
USCORE: '_';
EXCL: '!';
DOLLAR: '$';
AMP: '&';
SQUOTE: '\'';
OPAREN: '(';
CPAREN: ')';
STAR: '*';

SCOL: ';';
EQUALS: '=';
FSLASH2: '//';
FSLASH: '/';
QMARK: '?';
HASH: '#';
OBRACK: '[';
CBRACK: ']';
AT: '@';