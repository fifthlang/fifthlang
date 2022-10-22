T4=dotnet t4
T4_ARGS=-I=. -P=../../fifth.metamodel.metadata/bin/Debug/net6.0

SRC_GEN= il.builders.cs

%.cs: %.tt %.ttinclude il.metamodel.ttinclude
	$(T4) $(T4_ARGS) --out=$@ $<

ast: $(SRC_GEN)
