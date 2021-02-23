namespace Fifth.Parser.LangProcessingPhases
{
    using Fifth.AST;
    using Fifth.PrimitiveTypes;

    public class TypeAnnotatorVisitor : BaseAstVisitor
    {

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => ctx["type"] = PrimitiveFloat.Default;

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => ctx["type"] = PrimitiveInteger.Default;

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => ctx["type"] = PrimitiveString.Default;

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

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            if (TypeHelpers.TryInferOperationResultType(ctx.Op, ctx.Left["type"] as IFifthType, ctx.Right["type"] as IFifthType, out var resulttype))
            {
                ctx["type"] = resulttype;
            }
        }

    }
}
