namespace Fifth.Runtime.LangProcessingPhases
{
    using System;
    using Fifth;
    using Fifth.AST;
    using Fifth.Parser.LangProcessingPhases;
    using Fifth.PrimitiveTypes;

    public class StackGeneratorVisitor : BaseAstVisitor
    {
        public FuncStack Stack { get; set; }

        /// <summary>
        /// Adds the wrapped function to the stack
        /// </summary>
        /// <param name="fs">
        /// A variable length param array of wrapped functions to be pushed onto the stack
        /// </param>
        public void Emit(params FuncWrapper[] fs)
        {
            foreach (var f in fs)
            {
                Stack.Push(f);
            }
        }

        /// <summary>
        /// Adds a meta function to the stack
        /// </summary>
        /// <param name="metafunc">The metafunction to be pushed</param>
        public void EmitMeta(Func<IDispatcher, IDispatcher> metafunc)
        {
            var wrapped = Fun.Wrap(metafunc);
            wrapped.IsMetaFunction = true;
            Emit(wrapped);
        }

        public override void EnterAbsoluteUri(AbsoluteIri absoluteIri) => Emit(absoluteIri.Uri.AsFun());

        public override void EnterAlias(AliasDeclaration ctx)
        {
        }

        public override void EnterBinaryExpression(BinaryExpression ctx)
        {
        }

        public override void EnterBlock(Block ctx)
        {
        }

        public override void EnterExpression(Expression expression)
        {
        }

        public override void EnterExpressionList(ExpressionList ctx)
        {
        }

        public override void EnterFifthProgram(FifthProgram ctx)
        {
        }

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
        {
        }

        public override void EnterFuncCallExpression(FuncCallExpression ctx)
        {
        }

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
        }

        public override void EnterIdentifier(Identifier identifier)
        {
        }

        public override void EnterIdentifierExpression(IdentifierExpression identifierExpression)
        {
        }

        public override void EnterIfElseStmt(IfElseStmt ctx)
        {
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
        {
        }

        public override void EnterModuleImport(ModuleImport ctx)
        {
        }

        public override void EnterNotExpression(UnaryExpression ctx)
        {
        }

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
        {
        }

        public override void EnterParameterDeclarationList(ParameterDeclarationList parameterDeclarationList)
        {
        }

        public override void EnterStringValueExpression(StringValueExpression ctx)
        {
        }

        public override void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx)
        {
        }

        public override void EnterTypeInitialiser(TypeInitialiser ctx)
        {
        }

        public override void EnterUnaryExpression(UnaryExpression ctx)
        {
        }

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
        }

        public override void EnterVariableReference(VariableReference variableRef)
        {
        }

        public override void LeaveAbsoluteUri(AbsoluteIri absoluteIri)
        {
        }

        public override void LeaveAlias(AliasDeclaration ctx)
        {
        }

        public override void LeaveAssignmentStmt(AssignmentStmt ctx) => EmitMeta(MetaFunction.Assign);

        public override void LeaveBinaryExpression(BinaryExpression ctx) => EmitOperator(ctx);

        public override void LeaveBlock(Block ctx)
        {
        }

        public override void LeaveExpression(Expression expression)
        {
        }

        public override void LeaveExpressionList(ExpressionList ctx)
        {
        }

        public override void LeaveFifthProgram(FifthProgram ctx)
        {
        }

        public override void LeaveFloatValueExpression(FloatValueExpression ctx) => Emit(ctx.Value.AsFun());

        public override void LeaveFuncCallExpression(FuncCallExpression ctx)
        {
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
        {
            // need to create scopes for params and body block, and execute within those scopes
        }

        public override void LeaveIdentifier(Identifier identifier)
        {
            Emit(identifier.Value.AsFun());
            EmitMeta(MetaFunction.DereferenceVariable);
        }

        public override void LeaveIdentifierExpression(IdentifierExpression identifierExpression)
        {
            Emit(identifierExpression.Identifier.Value.AsFun());
            EmitMeta(MetaFunction.DereferenceVariable);
        }

        public override void LeaveIfElseStmt(IfElseStmt ctx)
        {
            // need to create scopes for ifblock and else block, and execute the blocks in those scopes
        }

        public override void LeaveIntValueExpression(IntValueExpression ctx) => Emit(ctx.Value.AsFun());

        public override void LeaveModuleImport(ModuleImport ctx)
        {
        }

        public override void LeaveNotExpression(UnaryExpression ctx) => Emit(Fun.Wrap((bool b) => !b));

        public override void LeaveParameterDeclaration(ParameterDeclaration ctx)
        {
        }

        public override void LeaveParameterDeclarationList(ParameterDeclarationList parameterDeclarationList)
        {
        }

        public override void LeaveStringValueExpression(StringValueExpression ctx) => Emit(ctx.Value.AsFun());

        public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx) => EmitMeta(MetaFunction.CreateVariable);

        public override void LeaveTypeInitialiser(TypeInitialiser ctx)
        {
        }

        public override void LeaveUnaryExpression(UnaryExpression ctx)
        {
        }

        public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx) => EmitMeta(MetaFunction.CreateVariable);

        public override void LeaveVariableReference(VariableReference variableRef) => EmitMeta(MetaFunction.DereferenceVariable);

        private void EmitOperator(BinaryExpression ctx)
        {
            var lhsType = LookupType(ctx.Left);
            var rhsType = LookupType(ctx.Right);

            if (!TypeHelpers.TryGetOperatorByNameAndTypes(ctx.Op, lhsType, rhsType, out var operatorFunction))
            {
                throw new TypeCheckingException($"operator {ctx.Op} not supported between {lhsType.Name} and {rhsType.Name}.");
            }
            Emit(operatorFunction);
        }

        private Type LookupType(Expression e) => e["type"] as Type;
    }
}
