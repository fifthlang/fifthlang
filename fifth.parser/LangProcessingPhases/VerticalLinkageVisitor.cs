namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class VerticalLinkageVisitor : IAstVisitor
    {
        public Stack<AstNode> Parents = new Stack<AstNode>();

        private void EnterNonTerminal(AstNode ctx)
        {
            ctx.ParentNode = Parents.PeekOrDefault();
            Parents.Push(ctx);
        }

        private void EnterTerminal(AstNode ctx)
        {
            ctx.ParentNode = Parents.PeekOrDefault();
        }

        private void LeaveNonTerminal(AstNode ctx)
        {
            Parents.Pop();
        }

        private void LeaveTerminal(AstNode ctx)
        { }

        public void EnterAbsoluteUri(AbsoluteIri ctx) => EnterTerminal(ctx);

        public void LeaveAbsoluteUri(AbsoluteIri ctx) => LeaveNonTerminal(ctx);

        public void EnterAlias(AliasDeclaration ctx) => EnterNonTerminal(ctx);

        public void EnterAssignmentStmt(AssignmentStmt ctx) => EnterNonTerminal(ctx);

        public void EnterBinaryExpression(BinaryExpression ctx) => EnterNonTerminal(ctx);

        public void EnterBlock(Block ctx) => EnterNonTerminal(ctx);

        public void EnterBooleanExpression(BooleanExpression ctx) => EnterTerminal(ctx);

        public void EnterExpression(Expression ctx) => EnterNonTerminal(ctx);//??

        public void EnterExpressionList(ExpressionList ctx) => EnterNonTerminal(ctx);

        public void EnterFifthProgram(FifthProgram ctx) => EnterNonTerminal(ctx);

        public void EnterFloatValueExpression(FloatValueExpression ctx) => EnterTerminal(ctx);

        public void EnterFuncCallExpression(FuncCallExpression ctx) => EnterNonTerminal(ctx);

        public void EnterFunctionDefinition(FunctionDefinition ctx) => EnterNonTerminal(ctx);

        public void EnterIdentifier(Identifier ctx) => EnterTerminal(ctx);

        public void EnterIdentifierExpression(IdentifierExpression ctx) => EnterNonTerminal(ctx);

        public void EnterIfElseExp(IfElseExp ctx) => EnterNonTerminal(ctx);

        public void EnterIntValueExpression(IntValueExpression ctx) => EnterTerminal(ctx);

        public void EnterModuleImport(ModuleImport ctx) => EnterTerminal(ctx);

        public void EnterNotExpression(UnaryExpression ctx) => EnterNonTerminal(ctx);

        public void EnterParameterDeclaration(ParameterDeclaration ctx) => EnterTerminal(ctx);

        public void EnterParameterDeclarationList(ParameterDeclarationList ctx) =>
            EnterNonTerminal(ctx);

        public void EnterStringValueExpression(StringValueExpression ctx) => EnterTerminal(ctx);

        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx) => EnterNonTerminal(ctx);

        public void EnterTypeInitialiser(TypeInitialiser ctx) => EnterNonTerminal(ctx);

        public void EnterUnaryExpression(UnaryExpression ctx) => EnterNonTerminal(ctx);

        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx) => EnterNonTerminal(ctx);

        public void EnterVariableReference(VariableReference ctx) => EnterTerminal(ctx);

        public void EnterWhileExp(WhileExp ctx) => EnterNonTerminal(ctx);

        public void LeaveAlias(AliasDeclaration ctx) => LeaveNonTerminal(ctx);

        public void LeaveAssignmentStmt(AssignmentStmt ctx) => LeaveNonTerminal(ctx);

        public void LeaveBinaryExpression(BinaryExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveBlock(Block ctx) => LeaveNonTerminal(ctx);

        public void LeaveBooleanExpression(BooleanExpression ctx) => LeaveTerminal(ctx);

        public void LeaveExpression(Expression ctx) => LeaveNonTerminal(ctx);

        public void LeaveExpressionList(ExpressionList ctx) => LeaveNonTerminal(ctx);

        public void LeaveFifthProgram(FifthProgram ctx) => LeaveNonTerminal(ctx);

        public void LeaveFloatValueExpression(FloatValueExpression ctx) => LeaveTerminal(ctx);

        public void LeaveFuncCallExpression(FuncCallExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveFunctionDefinition(FunctionDefinition ctx) => LeaveNonTerminal(ctx);

        public void LeaveIdentifier(Identifier ctx) => LeaveTerminal(ctx);

        public void LeaveIdentifierExpression(IdentifierExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveIfElseExp(IfElseExp ctx) => LeaveNonTerminal(ctx);

        public void LeaveIntValueExpression(IntValueExpression ctx) => LeaveTerminal(ctx);

        public void LeaveModuleImport(ModuleImport ctx) => LeaveTerminal(ctx);

        public void LeaveNotExpression(UnaryExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveParameterDeclaration(ParameterDeclaration ctx) => LeaveTerminal(ctx);

        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx) => LeaveNonTerminal(ctx);

        public void LeaveStringValueExpression(StringValueExpression ctx) => LeaveTerminal(ctx);

        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveTypeInitialiser(TypeInitialiser ctx) => LeaveNonTerminal(ctx);

        public void LeaveUnaryExpression(UnaryExpression ctx) => LeaveNonTerminal(ctx);

        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx) => LeaveNonTerminal(ctx);

        public void LeaveVariableReference(VariableReference ctx) => LeaveTerminal(ctx);

        public void LeaveWhileExp(WhileExp ctx) => LeaveNonTerminal(ctx);
    }
}
