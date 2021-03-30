namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using Parser.LangProcessingPhases;
    using PrimitiveTypes;

    public class DesugaringVisitor : BaseAstVisitor
    {
        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            if (ctx.Name == "main")
            {
                // if this is the entry point, then create a discard variable

                var newExpressions = new List<Expression>()
                {
                    new VariableDeclarationStatement(new Identifier("__discard__"), new StringValueExpression("__discard__", PrimitiveString.Default.TypeId),
                        PrimitiveString.Default.TypeId) {ParentNode = ctx.Body}
                };
                newExpressions.AddRange(ctx.Body.Expressions);
                ctx.Body.Expressions = newExpressions;
            }
        }

        public override void LeaveExpressionList(ExpressionList ctx)
        {
            for (int i = 0; i < ctx.Expressions.Count - 1; i++)
            {
                if (ctx.Expressions[i] is FuncCallExpression fce)
                {
                    var x = new AssignmentStmt(ctx, fce.FifthType);
                    x.VariableRef = new VariableReference(ctx, null)
                        {
                            Name = "__discard__",
                            ParentNode = fce.ParentNode,
                        };
                    x.Expression = fce;
                    fce.ParentNode = x;
                    ctx.Expressions[i] = x;
                }
            }
        }
    }
}
