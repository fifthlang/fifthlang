using System.Collections.Generic;

namespace fifth.Parser.AST
{
    public class FunctionInvocationExpression : Expression
    {
        public Expression Body { get; set; }
        private IEnumerable<ParameterDeclaration> Parameters { get; set; }
    }
}