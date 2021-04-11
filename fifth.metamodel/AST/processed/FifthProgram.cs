namespace Fifth.AST.Deprecated
{
    using System.Collections.Generic;
    using Symbols;
    using Visitors;

    public class FifthProgram : ScopeAstNode
    {
        public FifthProgram(Symbols.IScope _)
        {
        }

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
    }
}
