# LAYOUT
DIR_PARSER := fifth.parser
DIR_GRAMMAR := $(DIR_PARSER)/grammar
DIR_TEST := fifth.test

# TOOLS
JVM := java
ANTLR := tools/antlr-4.8-complete.jar
ANTLR_ARGS := -Dlanguage=CSharp -visitor -listener -lib $(DIR_GRAMMAR)
CC := dotnet
CC_ARGS:=
# SOURCES
SRC_PARSER=$(wildcard $(DIR_PARSER)/**/*.cs)
SRC_TEST=$(wildcard $(DIR_TEST)/**/*.cs)

# ASSEMBLIES
BIN_PARSER=$(DIR_PARSER)/bin/Debug/net6.0/fifth.parser.dll
BIN_TEST=$(DIR_PARSER)/bin/Debug/net6.0/fifth.test.dll

.PHONY: all grammar test clean clean_grammar

all: clean grammar test

grammar: $(DIR_GRAMMAR)/FifthLexer.cs $(DIR_GRAMMAR)/FifthParser.cs

clean:
	$(CC) clean

$(DIR_GRAMMAR)/FifthLexer.cs $(DIR_GRAMMAR)/FifthParser.cs: $(DIR_GRAMMAR)/Fifth.g4
	$(JVM) -jar $(ANTLR) $(ANTLR_ARGS) $<

$(BIN_PARSER): $(SRC_PARSER) grammar
	$(CC) $(CC_ARGS) build

$(BIN_TEST): $(SRC_TEST) $(BIN_PARSER)

build: ast #$(BIN_TEST)
	$(CC) $(CC_ARGS) build ./fifthlang.sln

test: $(BIN_TEST)
	$(CC) test
ast:
	$(CC) $(CC_ARGS) build fifth.metamodel.metadata/fifth.metamodel.metadata.csproj
	$(MAKE) CC=$(CC) -C fifth.metamodel/AST ast
	$(MAKE) CC=$(CC) -C fifth.codegenerator/IL ast
