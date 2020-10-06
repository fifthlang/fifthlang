# LAYOUT
DIR_GRAMMAR := grammar
DIR_PARSER := src/fifth.parser
DIR_TEST := test/fifth.test
DIR_ANTLR_GEN := $(DIR_PARSER)/Parser/gen

# TOOLS
JVM := java
ANTLR := ~/bin/antlr-4.8-complete.jar
ANTLR_ARGS := -Dlanguage=CSharp -o $(DIR_ANTLR_GEN)
CC := dotnet
CC_ARGS:=
NET_RUNTIME:=netstandard2.0

# SOURCES
SRC_ANTLR_GEN=$(wildcard $(DIR_ANTLR_GEN)/grammar/*.cs)
SRC_PARSER=$(wildcard $(DIR_PARSER)/**/*.cs)
SRC_TEST=$(wildcard $(DIR_TEST)/**/*.cs)

# ASSEMBLIES
BIN_PARSER=$(DIR_PARSER)/bin/Debug/$(NET_RUNTIME)/fifth.parser.dll
BIN_TEST=$(DIR_PARSER)/bin/Debug/netcoreapp3.1/fifth.test.dll

.PHONY: grammar

$(ANTLR_GEN):
	mkdir -p $@

grammar: $(SRC_ANTLR_GEN)

$(DIR_ANTLR_GEN)/grammar/fifthParser.cs: $(ANTLR_GEN) $(DIR_GRAMMAR)/fifth.g4
	$(JVM) -jar $(ANTLR) $(ANTLR_ARGS) $(DIR_GRAMMAR)/fifth.g4

$(BIN_PARSER): $(SRC_PARSER)
	$(CC) $(CC_ARGS) build

$(BIN_TEST): $(SRC_TEST) $(BIN_PARSER)

test: $(BIN_TEST)
	$(CC) test