namespace Fifth.Runtime.LangProcessingPhases
{
    using System.Runtime.CompilerServices;
    using AST;
    using Parser.LangProcessingPhases;


    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public IRuntimeStack Stack { get; set; }
        public IStackEmitter Emit { get; set; }

        #region Builders

        private ExpressionStackEmitter _varDeclBldr;
        #endregion

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

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
        {
            // need to create scopes for params and body block, and execute within those scopes
        }

        public override void LeaveIfElseStmt(IfElseStmt ctx)
        {
            // need to create scopes for ifblock and else block, and execute the blocks in those scopes
        }

        public override void LeaveNotExpression(UnaryExpression ctx)
            => Emit.UnaryFunction(Stack, (bool b) => !b);

        public override void LeaveStringValueExpression(StringValueExpression ctx)
            => Emit.Value(Stack, ctx.Value);

        public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => Emit.MetaFunction(Stack, MetaFunction.DeclareVariable);

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
            _varDeclBldr = new ExpressionStackEmitter(ctx);
        }


        /// <summary>
        ///     Bind what was resolved in the Enter phase, to whatever was emitted as part of the expression
        /// </summary>
        public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx) =>
            _varDeclBldr.Emit(Emit, Stack);

        public override void LeaveVariableReference(VariableReference variableRef) =>
            Emit.MetaFunction(Stack, MetaFunction.DereferenceVariable);
    }

    internal interface ISpecialFormEmitter
    {
        void Emit(IStackEmitter emitter, IRuntimeStack stack);
    }
    internal class ExpressionStackEmitter : ISpecialFormEmitter
    {
        private readonly Expression expression;

        public ExpressionStackEmitter(Expression expression)
        {
            this.expression = expression;
        }
        public void EmitBooleanExpression(BooleanExpression be, IStackEmitter emitter, IRuntimeStack stack)
        {
            emitter.Value(stack, be.Value);
        }
        public void EmitIntValueExpression(IntValueExpression ie, IStackEmitter emitter, IRuntimeStack stack)
        {
            emitter.Value(stack, ie.Value);
        }
        public void EmitVariableDeclarationExpression(VariableDeclarationStatement vde, IStackEmitter emitter, IRuntimeStack stack)
        {
            new ExpressionStackEmitter(vde.Expression).Emit(emitter, stack);
            emitter.VariableReference(stack, vde.Name.Value);
            emitter.MetaFunction(stack, MetaFunction.DereferenceVariable);
            emitter.MetaFunction(stack, MetaFunction.Assign);
        }
        public void EmitBinaryExpression(BinaryExpression be, IStackEmitter emitter, IRuntimeStack stack)
        {
            var lhsEmitter = new ExpressionStackEmitter(be.Left);
            var rhsEmitter = new ExpressionStackEmitter(be.Right);
            rhsEmitter.Emit(emitter, stack);
            lhsEmitter.Emit(emitter, stack);
            emitter.Operator(stack, be);
        }
        public void Emit(IStackEmitter emitter, IRuntimeStack stack)
        {
            switch (expression)
            {
                case BinaryExpression be:
                    EmitBinaryExpression(be, emitter, stack);
                    break;
                case BooleanExpression boole:
                    EmitBooleanExpression(boole, emitter, stack);
                    break;
                case IntValueExpression ie:
                    EmitIntValueExpression(ie, emitter, stack);
                    break;
                case VariableDeclarationStatement vde:
                    EmitVariableDeclarationExpression(vde, emitter, stack);
                    break;
            }
        }
    }
    internal class VariableDeclarationStackEmitter : ISpecialFormEmitter
    {
        public VariableDeclarationStatement VarDeclAstNode { get; }

        public VariableDeclarationStackEmitter(VariableDeclarationStatement varDeclAstNode)
        {
            VarDeclAstNode = varDeclAstNode;
        }

        public void Emit(IStackEmitter emitter, IRuntimeStack stack)
        {
        }
    }
}
