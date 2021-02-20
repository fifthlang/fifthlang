# Alias

An alias is an operation that 1) imports all of the symbols from an ontology file, and 2) prefixes all of those symbols with the package name in the symbol table.

Things it could do:

1. download from the IRI
2. Cache any ontology definitions locally, in memory
3. Store any instance data in a named KG
4. Create symtab entries for all types in the ontology
5. Define properties for each of them
6. Create 5th ADT definitions for the ontology types

So the applicable metafunctions would be:

1. create a knowledge graph (KG)
2. Download ontology definition into KG
3. Encapsulate Ontology types (with 5th ADTs)
4. Import names into scope, using prefixes
