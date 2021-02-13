namespace Fifth.Runtime.LangProcessingPhases
{
    using AST;
    using Parser.LangProcessingPhases;

    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public IRuntimeStack Stack { get; set; }
        public IStackEmitter Emit { get; set; }


        /// <summary>
        ///     Reverse the expression list, to ensure code is generated into the stack in the right order for consumption
        /// </summary>
        /// <param name="ctx">the expression list to visit</param>
        public override void EnterExpressionList(ExpressionList ctx)
            => ctx.Expressions.Reverse();

        /// <summary>
        ///     reverse the reversal of the expression list performed in the enter function
        /// </summary>
        /// <param name="ctx"></param>
        public override void LeaveExpressionList(ExpressionList ctx)
            => ctx.Expressions.Reverse();

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

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
            // by the time this gets called:
            // the expression on the rhs (if there is one?) will have been emitted
            // we need to supply a name for the variable and then the meta instruction to bind that name to the value
            Emit.Value(Stack, ctx.TypeName);
            Emit.Value(Stack, ctx.Name.Value);
            Emit.MetaFunction(Stack, MetaFunction.DeclareVariable);

            // TODO We need a way to ensure that whatever needs to be bound to the variable
            // reference created in the line above, gets generated before we emit the
            // BindVariable meta below. We do that by doing all this in the Enter part, and leaving the tail end
            // stuff in the Leave part below.  
        }


        /// <summary>
        ///     Bind what was resolved in the Enter phase, to whatever was emitted as part of the expression
        /// </summary>
        public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx) =>
            Emit.MetaFunction(Stack, MetaFunction.BindVariable);

        public override void LeaveVariableReference(VariableReference variableRef) =>
            Emit.MetaFunction(Stack, MetaFunction.DereferenceVariable);
    }
}
