grammar FifthKnowledgeGraphs;
import FifthLexicalRules, FifthKeywords;

alias:
    'alias'
    name=packagename
    'as'
    uri=absoluteIri
    SEMICOLON
;

iri:
   qNameIri | absoluteIri
   ;


qNameIri: prefix=IDENTIFIER? COLON fragname=IDENTIFIER ;

absoluteIri :
    iri_scheme=IDENTIFIER
    COLON DIVIDE DIVIDE
    iri_domain+=IDENTIFIER (DOT iri_domain+=IDENTIFIER)*
    (DIVIDE iri_segment+=IDENTIFIER)*
    DIVIDE?
    (HASH IDENTIFIER?)?
    // (QMARK iri_query_param (AMP iri_query_param)*)?
;

iri_query_param:
    name=IDENTIFIER ASSIGN val=IDENTIFIER;

