using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.parser.Parser.AST
{
    public class FunctionDefinition : AstNode
    {
        public List<Expression> Body { get; internal set; }
        public string Name { get; set; }
        public IFifthType ReturnType { get; internal set; }
    }
}