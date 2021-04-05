namespace Fifth.AST.Visitors
{
    public interface IAstVisitor
    {
        // void Accept(AstNode astNode);

        void EnterReturnStatement(ReturnStatement ctx);

        void LeaveReturnStatement(ReturnStatement ctx);

        void EnterStatementList(StatementList ctx);

        void LeaveStatementList(StatementList ctx);

        void EnterAbsoluteUri(AbsoluteIri absoluteIri);

        void EnterAlias(AliasDeclaration ctx);

        void EnterAssignmentStmt(AssignmentStmt ctx);

        void EnterBinaryExpression(BinaryExpression ctx);

        void EnterBlock(Block ctx);

        void EnterBooleanExpression(BooleanExpression ctx);

        void EnterExpression(Expression expression);

        void EnterExpressionList(ExpressionList ctx);

        void EnterFifthProgram(FifthProgram ctx);

        void EnterFloatValueExpression(FloatValueExpression ctx);

        void EnterFuncCallExpression(FuncCallExpression ctx);

        void EnterFunctionDefinition(FunctionDefinition ctx);

        void EnterIdentifier(Identifier identifier);

        void EnterIdentifierExpression(IdentifierExpression identifierExpression);

        void EnterIfElseExp(IfElseStatement ctx);

        void EnterIntValueExpression(IntValueExpression ctx);

        void EnterModuleImport(ModuleImport ctx);

        void EnterNotExpression(UnaryExpression ctx);

        void EnterParameterDeclaration(ParameterDeclaration ctx);

        void EnterParameterDeclarationList(ParameterDeclarationList parameterDeclarationList);

        void EnterStringValueExpression(StringValueExpression ctx);

        void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx);

        void EnterTypeInitialiser(TypeInitialiser ctx);

        void EnterUnaryExpression(UnaryExpression ctx);

        void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx);

        void EnterVariableReference(VariableReference variableRef);

        void EnterWhileExp(WhileExp ctx);

        void LeaveAbsoluteUri(AbsoluteIri absoluteIri);

        void LeaveAlias(AliasDeclaration ctx);

        void LeaveAssignmentStmt(AssignmentStmt ctx);

        void LeaveBinaryExpression(BinaryExpression ctx);

        void LeaveBlock(Block ctx);

        void LeaveBooleanExpression(BooleanExpression ctx);

        void LeaveExpression(Expression expression);

        void LeaveExpressionList(ExpressionList ctx);

        void LeaveFifthProgram(FifthProgram ctx);

        void LeaveFloatValueExpression(FloatValueExpression ctx);

        void LeaveFuncCallExpression(FuncCallExpression ctx);

        void LeaveFunctionDefinition(FunctionDefinition ctx);

        void LeaveIdentifier(Identifier identifier);

        void LeaveIdentifierExpression(IdentifierExpression identifierExpression);

        void LeaveIfElseExp(IfElseStatement ctx);

        void LeaveIntValueExpression(IntValueExpression ctx);

        void LeaveModuleImport(ModuleImport ctx);

        void LeaveNotExpression(UnaryExpression ctx);

        void LeaveParameterDeclaration(ParameterDeclaration ctx);

        void LeaveParameterDeclarationList(ParameterDeclarationList parameterDeclarationList);

        void LeaveStringValueExpression(StringValueExpression ctx);

        void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx);

        void LeaveTypeInitialiser(TypeInitialiser ctx);

        void LeaveUnaryExpression(UnaryExpression ctx);

        void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx);

        void LeaveVariableReference(VariableReference variableRef);

        void LeaveWhileExp(WhileExp ctx);
        void EnterLongValueExpression(LongValueExpression ctx);
        void LeaveLongValueExpression(LongValueExpression ctx);
        void EnterShortValueExpression(ShortValueExpression cts);
        void LeaveShortValueExpression(ShortValueExpression ctx);
        void EnterDoubleValueExpression(DoubleValueExpression ctx);
        void LeaveDoubleValueExpression(DoubleValueExpression ctx);
        void EnterDecimalValueExpression(DecimalValueExpression ctx);
        void LeaveDecimalValueExpression(DecimalValueExpression ctx);
        void EnterDateValueExpression(DateValueExpression ctx);
        void LeaveDateValueExpression(DateValueExpression ctx);
    }
}
