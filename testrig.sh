#!/usr/bin/env bash
#
#-- Antlr4 path (here, to Mac Homebrew tree)
CP=~/bin/antlr-4.8-complete.jar
#
#-- Append Grammar path (here, to generated classes in Maven target)
CP="${CP}:/Users/aabs/dev/by-technology/dotnet/fifthlang/src/fifth.parser/Parser/grammar/.antlr"
#
#-- Call TestRig with command line parameters
#-- E.g. ./gtest.sh com.rogersalumni.calculator.g4.Calculator start -gui <expression.txt
java -cp ${CP} org.antlr.v4.gui.TestRig "$@"
