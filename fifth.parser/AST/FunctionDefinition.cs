namespace Fifth.AST
{
    using System.Linq;
    using Fifth.Parser.LangProcessingPhases;

    public class FunctionDefinition : AstNode
    {
        public ExpressionList Body { get; set; }
        public string Name { get; set; }
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public IFifthType ReturnType { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFunctionDefinition(this);
            visitor.Accept(ParameterDeclarations);
            visitor.Accept(Body);
            visitor.LeaveFunctionDefinition(this);
        }
    }
}
