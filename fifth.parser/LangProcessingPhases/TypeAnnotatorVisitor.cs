namespace Fifth.Parser.LangProcessingPhases
{
    using Fifth.AST;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {
        public override void EnterBinaryExpression(BinaryExpression ctx)
        {
            base.EnterBinaryExpression(ctx);
        }

        public override void EnterExpression(Expression expression)
        {
            base.EnterExpression(expression);
        }

        public override void EnterExpressionList(ExpressionList ctx)
        {
            base.EnterExpressionList(ctx);
        }

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx["type"] = ctx.FifthType;

        public override void EnterFuncCallExpression(FuncCallExpression ctx)
        {
            base.EnterFuncCallExpression(ctx);
        }

        public override void EnterIdentifierExpression(IdentifierExpression identifierExpression)
        {
            base.EnterIdentifierExpression(identifierExpression);
        }

        public override void EnterIfElseStmt(IfElseStmt ctx)
        {
            base.EnterIfElseStmt(ctx);
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx["type"] = ctx.FifthType;

        public override void EnterNotExpression(UnaryExpression ctx)
        {
            base.EnterNotExpression(ctx);
        }

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx["type"] = ctx.FifthType;

        public override void EnterTypeInitialiser(TypeInitialiser ctx)
        {
            base.EnterTypeInitialiser(ctx);
        }

        public override void EnterUnaryExpression(UnaryExpression ctx)
        {
            base.EnterUnaryExpression(ctx);
        }

        public override void EnterVariableReference(VariableReference variableRef)
        {
            base.EnterVariableReference(variableRef);
        }

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
        {
            // 2. get the scope the var is declared in (should have been done by now)
            if (!ctx.Expression.HasAnnotation("type"))
            {
                throw new Fifth.TypeCheckingException("Unable to infer type of expression during assignment");
            }
            // 3. annotate the type of the symbol in the symtab
            // 4. annotate the type of the assignment expression
            ctx["type"] = ctx.Expression["type"];
        }
    }
}
