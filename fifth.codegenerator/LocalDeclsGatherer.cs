using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth.CodeGeneration
{
    using AST;
    using AST.Visitors;

    public class LocalDeclsGatherer : BaseAstVisitor
    {
        public List<VariableDeclarationStatement> Decls { get; } = new();

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => Decls.Add(ctx);
    }
}
