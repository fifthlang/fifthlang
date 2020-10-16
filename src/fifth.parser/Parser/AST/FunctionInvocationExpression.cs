using System.Collections.Generic;

namespace fifth.parser.Parser.AST
{
    public class FunctionInvocationExpression : Expression
    {
        public Expression Body { get; set; }
        private IEnumerable<ParameterDeclaration> Parameters { get; set; }
    }
}