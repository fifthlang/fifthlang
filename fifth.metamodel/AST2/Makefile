T4=dotnet t4
T4_ARGS=-I=. -P=../../fifth.metamodel.metadata/bin/Debug/net8.0

SRC_GEN=ast.builders.cs

%.cs: %.tt %.ttinclude ast.metamodel.ttinclude
	$(T4) $(T4_ARGS) --out=$@ $<

ast2: $(SRC_GEN)
