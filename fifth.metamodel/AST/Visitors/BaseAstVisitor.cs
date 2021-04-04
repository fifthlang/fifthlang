namespace Fifth.AST.Visitors
{
    using AST;

    public abstract class BaseAstVisitor : IAstVisitor
    {
        public virtual void EnterAbsoluteUri(AbsoluteIri absoluteIri)
        {
        }

        public virtual void EnterAlias(AliasDeclaration ctx)
        {
        }

        public virtual void EnterAssignmentStmt(AssignmentStmt ctx)
        {
        }

        public virtual void EnterBinaryExpression(BinaryExpression ctx)
        {
        }

        public virtual void EnterBooleanExpression(BooleanExpression ctx)
        {
        }

        public virtual void LeaveBooleanExpression(BooleanExpression ctx)
        {
        }

        public virtual void EnterBlock(Block ctx)
        {
        }

        public virtual void EnterExpression(Expression expression)
        {
        }

        public virtual void EnterExpressionList(ExpressionList ctx)
        {
        }

        public virtual void EnterFifthProgram(FifthProgram ctx)
        {
        }

        public virtual void EnterFloatValueExpression(FloatValueExpression ctx)
        {
        }

        public virtual void EnterFuncCallExpression(FuncCallExpression ctx)
        {
        }

        public virtual void EnterFunctionDefinition(FunctionDefinition ctx)
        {
        }

        public virtual void EnterIdentifier(Identifier identifier)
        {
        }

        public virtual void EnterIdentifierExpression(IdentifierExpression identifierExpression)
        {
        }

        public virtual void EnterIfElseExp(IfElseExp ctx)
        {
        }

        public virtual void EnterIntValueExpression(IntValueExpression ctx)
        {
        }

        public virtual void EnterModuleImport(ModuleImport ctx)
        {
        }

        public virtual void EnterNotExpression(UnaryExpression ctx)
        {
        }

        public virtual void EnterParameterDeclaration(ParameterDeclaration ctx)
        {
        }

        public virtual void EnterParameterDeclarationList(ParameterDeclarationList parameterDeclarationList)
        {
        }

        public virtual void EnterStringValueExpression(StringValueExpression ctx)
        {
        }

        public virtual void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx)
        {
        }

        public virtual void EnterTypeInitialiser(TypeInitialiser ctx)
        {
        }

        public virtual void EnterUnaryExpression(UnaryExpression ctx)
        {
        }

        public virtual void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
        }

        public virtual void EnterVariableReference(VariableReference variableRef)
        {
        }

        public virtual void LeaveAbsoluteUri(AbsoluteIri absoluteIri)
        {
        }

        public virtual void LeaveAlias(AliasDeclaration ctx)
        {
        }

        public virtual void LeaveAssignmentStmt(AssignmentStmt ctx)
        {
        }

        public virtual void LeaveBinaryExpression(BinaryExpression ctx)
        {
        }

        public virtual void LeaveBlock(Block ctx)
        {
        }

        public virtual void LeaveExpression(Expression expression)
        {
        }

        public virtual void LeaveExpressionList(ExpressionList ctx)
        {
        }

        public virtual void LeaveFifthProgram(FifthProgram ctx)
        {
        }

        public virtual void LeaveFloatValueExpression(FloatValueExpression ctx)
        {
        }

        public virtual void LeaveFuncCallExpression(FuncCallExpression ctx)
        {
        }

        public virtual void LeaveFunctionDefinition(FunctionDefinition ctx)
        {
        }

        public virtual void LeaveIdentifier(Identifier identifier)
        {
        }

        public virtual void LeaveIdentifierExpression(IdentifierExpression identifierExpression)
        {
        }

        public virtual void LeaveIfElseExp(IfElseExp ctx)
        {
        }

        public virtual void LeaveIntValueExpression(IntValueExpression ctx)
        {
        }

        public virtual void LeaveModuleImport(ModuleImport ctx)
        {
        }

        public virtual void LeaveNotExpression(UnaryExpression ctx)
        {
        }

        public virtual void LeaveParameterDeclaration(ParameterDeclaration ctx)
        {
        }

        public virtual void LeaveParameterDeclarationList(ParameterDeclarationList parameterDeclarationList)
        {
        }

        public virtual void LeaveStringValueExpression(StringValueExpression ctx)
        {
        }

        public virtual void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
        {
        }

        public virtual void LeaveTypeInitialiser(TypeInitialiser ctx)
        {
        }

        public virtual void LeaveUnaryExpression(UnaryExpression ctx)
        {
        }

        public virtual void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
        }

        public virtual void LeaveVariableReference(VariableReference variableRef)
        {
        }

        public void EnterWhileExp(WhileExp ctx)
        {
        }

        public void LeaveWhileExp(WhileExp ctx)
        {
        }
    }
}
