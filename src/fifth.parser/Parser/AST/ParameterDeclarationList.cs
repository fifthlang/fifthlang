using System.Collections.Generic;

namespace fifth.parser.Parser.AST.Builders
{
    public class ParameterDeclarationList : FunctionDefinition
    {
        public List<ParameterDeclaration> ParameterDeclarations { get; set; }
    }
}