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

        private ExpressionListStackEmitter elsemitter;
        #endregion

        /// <summary>
        ///     Reverse the expression list, to ensure code is generated into the stack in the right order for consumption
        /// </summary>
        /// <param name="ctx">the expression list to visit</param>
        public override void EnterExpressionList(ExpressionList ctx)
            => elsemitter = new ExpressionListStackEmitter(ctx);

        /// <summary>
        ///     reverse the reversal of the expression list performed in the enter function
        /// </summary>
        /// <param name="ctx"></param>
        public override void LeaveExpressionList(ExpressionList ctx)
            => elsemitter.Emit(Emit, Stack);

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

        public override void LeaveVariableReference(VariableReference variableRef) =>
            Emit.MetaFunction(Stack, MetaFunction.DereferenceVariable);
    }

    public interface ISpecialFormEmitter
    {
        void Emit(IStackEmitter emitter, IRuntimeStack stack);
    }

    public class ExpressionStackEmitter : ISpecialFormEmitter
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
            // see /docs/semantic/metafunctions/Metafunction.DeclareVariable.md for semantics

            if (vde.Expression == null)
            {
                // just a bare decl: `int x`
                // format:     [typename, id, \DeclareVariable] => []
                emitter.Value(stack, vde.TypeName);
                emitter.Value(stack, vde.Name.Value);
                emitter.MetaFunction(stack, MetaFunction.DeclareVariable);
            }
            else
            {
                // a compound decl: `int x = expression`
                // format:     [<expression>, id, \Assign, typename, id, \DeclareVariable] => []

                // assign part
                new ExpressionStackEmitter(vde.Expression).Emit(emitter, stack);
                emitter.Value(stack, vde.Name.Value);
                emitter.MetaFunction(stack, MetaFunction.BindVariable);
                // decl part
                emitter.Value(stack, vde.TypeName);
                emitter.Value(stack, vde.Name.Value);
                emitter.MetaFunction(stack, MetaFunction.DeclareVariable);
            }

        }
        public void EmitIdentifierExpression(IdentifierExpression ie, IStackEmitter emitter, IRuntimeStack stack)
        {
            emitter.Value(stack, ie.Identifier.Value);
            emitter.MetaFunction(stack, MetaFunction.DereferenceVariable);
        }
        public void EmitFloatValueExpression(FloatValueExpression fe, IStackEmitter emitter, IRuntimeStack stack)
        {
            emitter.Value(stack, fe.Value);
        }
        public void EmitBinaryExpression(BinaryExpression be, IStackEmitter emitter, IRuntimeStack stack)
        {
            var lhsEmitter = new ExpressionStackEmitter(be.Left);
            var rhsEmitter = new ExpressionStackEmitter(be.Right);
            lhsEmitter.Emit(emitter, stack);
            rhsEmitter.Emit(emitter, stack);
            emitter.Operator(stack, be);
        }
        public void Emit(IStackEmitter emitter, IRuntimeStack stack)
        {
            switch (expression)
            {
                case IdentifierExpression ie:
                    EmitIdentifierExpression(ie, emitter, stack);
                    break;
                case BinaryExpression be:
                    EmitBinaryExpression(be, emitter, stack);
                    break;
                case BooleanExpression boole:
                    EmitBooleanExpression(boole, emitter, stack);
                    break;
                case IntValueExpression ie:
                    EmitIntValueExpression(ie, emitter, stack);
                    break;
                case FloatValueExpression fe:
                    EmitFloatValueExpression(fe, emitter, stack);
                    break;
                case VariableDeclarationStatement vde:
                    EmitVariableDeclarationExpression(vde, emitter, stack);
                    break;
            }
        }
    }

    public class ExpressionListStackEmitter : ISpecialFormEmitter
    {
        private ExpressionList expressionList;
        public ExpressionListStackEmitter(ExpressionList el)
        {
            expressionList = el;
        }
        public void Emit(IStackEmitter emitter, IRuntimeStack stack)
        {
            expressionList.Expressions.Reverse();
            foreach (var e in expressionList.Expressions)
            {
                var ese = new ExpressionStackEmitter(e);
                ese.Emit(emitter, stack);
            }
        }
    }
}
