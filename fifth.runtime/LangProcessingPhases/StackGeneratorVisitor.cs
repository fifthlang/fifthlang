namespace Fifth.Runtime.LangProcessingPhases
{
    using AST;
    using Parser.LangProcessingPhases;

    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public IRuntimeStack Stack { get; set; }
        public IStackEmitter Emit { get; set; }


        public override void EnterExpressionList(ExpressionList ctx) => ctx.Expressions.Reverse();

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
            => Emit.MetaFunction(Stack, MetaFunction.Assign);

        public override void LeaveBinaryExpression(BinaryExpression ctx)
            => Emit.Operator(Stack, ctx);

        public override void LeaveFloatValueExpression(FloatValueExpression ctx)
            => Emit.Value(Stack, ctx.Value);


        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
        {
            // need to create scopes for params and body block, and execute within those scopes
        }

        public override void LeaveIdentifier(Identifier identifier)
            => Emit.Value(Stack, identifier.Value);

        public override void LeaveIdentifierExpression(IdentifierExpression identifierExpression)
        {
            Emit.Value(Stack, identifierExpression.Identifier.Value);
            Emit.MetaFunction(Stack, MetaFunction.DereferenceVariable);
        }

        public override void LeaveIfElseStmt(IfElseStmt ctx)
        {
            // need to create scopes for ifblock and else block, and execute the blocks in those scopes
        }

        public override void LeaveIntValueExpression(IntValueExpression ctx)
            => Emit.Value(Stack, ctx.Value);

        public override void LeaveNotExpression(UnaryExpression ctx)
            => Emit.UnaryFunction(Stack, (bool b) => !b);

        public override void LeaveStringValueExpression(StringValueExpression ctx)
            => Emit.Value(Stack, ctx.Value);

        public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => Emit.MetaFunction(Stack, MetaFunction.DeclareVariable);

        public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
            // by the time this gets called:
            // the expression on the rhs (if there is one?) will have been emitted
            // we need to supply a name for the variable and then the meta instruction to bind that name to the value
            Emit.Value(Stack, ctx.Name.Value);
            Emit.MetaFunction(Stack, MetaFunction.DeclareVariable);
            Emit.MetaFunction(Stack, MetaFunction.BindVariable);
        }

        public override void LeaveVariableReference(VariableReference variableRef) =>
            Emit.MetaFunction(Stack, MetaFunction.DereferenceVariable);
    }
}
