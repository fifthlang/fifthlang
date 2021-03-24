namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;
    using PrimitiveTypes;

    public class FifthProgram : ScopeAstNode
    {
        public IList<AliasDeclaration> Aliases { get; set; }
        public IList<FunctionDefinition> Functions { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFifthProgram(this);
            foreach (var a in Aliases)
            {
                a.Accept(visitor);
            }
            foreach (var f in Functions)
            {
                f.Accept(visitor);
            }
            visitor.LeaveFifthProgram(this);
        }

        public FifthProgram() : base(null, null, null)
        {
        }
    }
}
