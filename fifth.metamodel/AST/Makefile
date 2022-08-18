T4=dotnet.exe t4
T4_ARGS=-I=. -P=../../fifth.metamodel.metadata/bin/Debug/net6.0

SRC_GEN=ast.nodes.cs ast.typesystem.cs ast.mutator-visitors.cs ast.visitors.cs ast.builders.cs

%.cs: %.tt %.ttinclude ast.metamodel.ttinclude
	$(T4) $(T4_ARGS) --out=$@ $<

ast: $(SRC_GEN)
