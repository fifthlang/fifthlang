parser grammar FifthParser;

options { tokenVocab=FifthLexer; }

fifth:
    module_import*
    alias*
    statement*
    function_declaration* ;

alias:
    ALIAS iri AS packagename SEMICOLON;

atom
   : scientific
   | var_name
   | STRING
   | OPENPAREN expression CLOSEPAREN
   ;


block: OPENBRACE statement* CLOSEBRACE
;

equation
   : expression relop expression
   ;

expression
   : multiplying_expression ((PLUS | MINUS) multiplying_expression)*
   ;

formal_parameters:
    parameter_declaration (COMMA parameter_declaration)*
;

function_declaration:
    function_name
    function_args
    function_body
;

function_args:
    OPENPAREN
    formal_parameters?
    CLOSEPAREN
;

function_body:
    block
;

function_call
   : q_function_name OPENPAREN expression (COMMA expression)* CLOSEPAREN
   ;

function_name: IDENTIFIER ;

iri
   : scheme COL ihier_part (QMARK iquery)? (HASH ifragment)?
   ;

module_import: USE module_name (COMMA module_name)* SEMICOLON ;

module_name: IDENTIFIER;

multiplying_expression
   : pow_expression ((TIMES | DIVIDE) pow_expression)*
   ;

packagename: IDENTIFIER ;

pow_expression
   : signed_atom (POWER signed_atom)*
   ;

parameter_declaration:
    q_type_name
    var_name
;

parameter_type: IDENTIFIER
;

parameter_name: IDENTIFIER
;

q_function_name: function_name (DOT function_name)* ;

qvarname: var_name (DOT var_name)*
;
q_type_name: type_name (DOT type_name)*
;

relop
   : EQ
   | GT
   | LT
   ;


scientific
   : ScientificNumber
   ;

signed_atom
   : PLUS signed_atom
   | MINUS signed_atom
   | function_call
   | atom
   ;

statement: qvarname ASSIGN expression SEMICOLON
            | RETURN expression  SEMICOLON
            | IF OPENPAREN expression CLOSEPAREN block
            | IF OPENPAREN expression CLOSEPAREN block ELSE block
            | WITH statement  SEMICOLON
            | expression  SEMICOLON
;

type_initialiser: type_name OPENBRACE type_property_init* CLOSEBRACE
;

type_name:  IDENTIFIER
;

type_property_init: var_name ASSIGN expression
;

var_name: IDENTIFIER
;


/*
 *  IRI Grammar (from ANTLR Grammars v4 repo
 */

ihier_part
   : FSLASH2 iauthority ipath_abempty
   | ipath_absolute
   | ipath_rootless
   | ipath_empty
   ;

/// IRI-reference  = IRI / irelative-ref
iri_reference
   : iri
   | irelative_ref
   ;

/// absolute-IRI   = scheme ":" ihier-part [ "?" iquery ]
absolute_iri
   : scheme COL ihier_part (QMARK iquery)?
   ;

/// irelative-ref  = irelative-part [ "?" iquery ] [ "#" ifragment ]
irelative_ref
   : irelative_part (QMARK iquery)? (HASH ifragment)?
   ;

/// irelative-part = "//" iauthority ipath-abempty///                     / ipath-absolute///                     / ipath-noscheme///                     / ipath-empty
irelative_part
   : FSLASH2 iauthority ipath_abempty
   | ipath_absolute
   | ipath_noscheme
   | ipath_empty
   ;

/// iauthority     = [ iuserinfo "@" ] ihost [ ":" port ]
iauthority
   : (iuserinfo AT)? ihost (COL port)?
   ;

/// iuserinfo      = *( iunreserved / pct-encoded / sub-delims / ":" )
iuserinfo
   : (iunreserved | pct_encoded | sub_delims | COL)*
   ;

/// ihost          = IP-literal / IPv4address / ireg-name
ihost
   : ip_literal
   | ip_v4_address
   | ireg_name
   ;

/// ireg-name      = *( iunreserved / pct-encoded / sub-delims )
ireg_name
   : (iunreserved | pct_encoded | sub_delims)*
   ;

/// ipath          = ipath-abempty   ; begins with "/" or is empty///                / ipath-absolute  ; begins with "/" but not "//"///                / ipath-noscheme  ; begins with a non-COL segment///                / ipath-rootless  ; begins with a segment///                / ipath-empty     ; zero characters
ipath
   : ipath_abempty
   | ipath_absolute
   | ipath_noscheme
   | ipath_rootless
   | ipath_empty
   ;

/// ipath-abempty  = *( "/" isegment )
ipath_abempty
   : (FSLASH isegment)*
   ;

/// ipath-absolute = "/" [ isegment-nz *( "/" isegment ) ]
ipath_absolute
   : FSLASH (isegment_nz (FSLASH isegment)*)?
   ;

/// ipath-noscheme = isegment-nz-nc *( "/" isegment )
ipath_noscheme
   : isegment_nz_nc (FSLASH isegment)*
   ;

/// ipath-rootless = isegment-nz *( "/" isegment )
ipath_rootless
   : isegment_nz (FSLASH isegment)*
   ;

/// ipath-empty    = 0<ipchar>
ipath_empty
   :/* nothing */
   ;

/// isegment       = *ipchar
isegment
   : ipchar*
   ;

/// isegment-nz    = 1*ipchar
isegment_nz
   : ipchar +
   ;

/// isegment-nz-nc = 1*( iunreserved / pct-encoded / sub-delims / "@" )///                ; non-zero-length segment without any COL ":"
isegment_nz_nc
   : (iunreserved | pct_encoded | sub_delims | AT) +
   ;

/// ipchar         = iunreserved / pct-encoded / sub-delims / ":" / "@"
ipchar
   : iunreserved
   | pct_encoded
   | sub_delims
   | (COL | AT)
   ;

/// iquery         = *( ipchar / iprivate / "/" / "?" )
iquery
   : (ipchar | (IPRIVATE | FSLASH | QMARK))*
   ;

/// ifragment      = *( ipchar / "/" / "?" )
ifragment
   : (ipchar | (FSLASH | QMARK))*
   ;

/// iunreserved    = ALPHA / DIGIT / "-" / "." / "_" / "~" / ucschar
iunreserved
   : alpha
   | digit
   | (MINUS | DOT | USCORE | TILDE | UCSCHAR)
   ;

/// scheme         = ALPHA *( ALPHA / DIGIT / "+" / "-" / "." )
scheme
   : alpha (alpha | digit | (PLUS | MINUS | DOT))*
   ;

/// port           = *DIGIT
port
   : digit*
   ;

/// IP-literal     = "[" ( IPv6address / IPvFuture  ) "]"
ip_literal
   : OBRACK (ip_v6_address | ip_v_future) CBRACK
   ;

/// IPvFuture      = "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
ip_v_future
   : V hexdig + DOT (unreserved | sub_delims | COL) +
   ;

/// IPv6address    =                            6( h16 ":" ) ls32///                /                       "::" 5( h16 ":" ) ls32///                / [               h16 ] "::" 4( h16 ":" ) ls32///                / [ *1( h16 ":" ) h16 ] "::" 3( h16 ":" ) ls32///                / [ *2( h16 ":" ) h16 ] "::" 2( h16 ":" ) ls32///                / [ *3( h16 ":" ) h16 ] "::"    h16 ":"   ls32///                / [ *4( h16 ":" ) h16 ] "::"              ls32///                / [ *5( h16 ":" ) h16 ] "::"              h16///                / [ *6( h16 ":" ) h16 ] "::"
ip_v6_address
   : h16 COL h16 COL h16 COL h16 COL h16 COL h16 COL ls32
   | COL2 h16 COL h16 COL h16 COL h16 COL h16 COL ls32
   | h16? COL2 h16 COL h16 COL h16 COL h16 COL ls32
   | ((h16 COL)? h16)? COL2 h16 COL h16 COL h16 COL ls32
   | (((h16 COL)? h16 COL)? h16)? COL2 h16 COL h16 COL ls32
   | ((((h16 COL)? h16 COL)? h16 COL)? h16)? COL2 h16 COL ls32
   | (((((h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16)? COL2 ls32
   | ((((((h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16)? COL2 h16
   | (((((((h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16 COL)? h16)? COL2
   ;

/// h16            = 1*4HEXDIG
h16
   : hexdig hexdig hexdig hexdig
   | hexdig hexdig hexdig
   | hexdig hexdig
   | hexdig
   ;

/// ls32           = ( h16 ":" h16 ) / IPv4address
ls32
   : h16 COL h16
   | ip_v4_address
   ;

/// IPv4address    = dec-octet "." dec-octet "." dec-octet "." dec-octet
ip_v4_address
   : dec_octet DOT dec_octet DOT dec_octet DOT dec_octet
   ;

/// dec-octet      = DIGIT                 ; 0-9///                / %x31-39 DIGIT         ; 10-99///                / "1" 2DIGIT            ; 100-199///                / "2" %x30-34 DIGIT     ; 200-249///                / "25" %x30-35          ; 250-255
dec_octet
   : digit
   | non_zero_digit digit
   | D1 digit digit
   | D2 (D0 | D1 | D2 | D3 | D4) digit
   | D2 D5 (D0 | D1 | D2 | D3 | D4 | D5)
   ;

/// pct-encoded    = "%" HEXDIG HEXDIG
pct_encoded
   : PERCENT hexdig hexdig
   ;

/// unreserved     = ALPHA / DIGIT / "-" / "." / "_" / "~"
unreserved
   : alpha
   | digit
   | (MINUS | DOT | USCORE | TILDE)
   ;

/// reserved       = gen-delims / sub-delims
reserved
   : gen_delims
   | sub_delims
   ;

/// gen-delims     = ":" / "/" / "?" / "#" / "[" / "]" / "@"
gen_delims
   : COL
   | FSLASH
   | QMARK
   | HASH
   | OBRACK
   | CBRACK
   | AT
   ;

/// sub-delims     = "!" / "$" / "&" / "'" / "(" / ")"///                / "*" / "+" / "," / ";" / "="
sub_delims
   : NOT
   | DOLLAR
   | AMP
   | SQUOT
   | OPAREN
   | CPAREN
   | TIMES
   | PLUS
   | COMMA
   | SEMICOLON
   | ASSIGN
   ;

alpha
   : LETTER
   ;

hexdig
   : HEXDIGIT
   ;

digit
   : DIGIT
   ;

non_zero_digit
   : POSITIVEDIGIT
   ;

