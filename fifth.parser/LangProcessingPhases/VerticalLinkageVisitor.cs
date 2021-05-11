// ReSharper disable InconsistentNaming

namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using AST;
    using AST.Visitors;
    using TypeSystem;

    public class VerticalLinkageVisitor : IAstVisitor
    {
        private readonly Stack<AstNode> parents = new();

        public void EnterClassDefinition(ClassDefinition ctx)
            => EnterNonTerminal(ctx);

        public void LeaveClassDefinition(ClassDefinition ctx)
            => LeaveNonTerminal(ctx);

        public void EnterPropertyDefinition(PropertyDefinition ctx)
            => EnterTerminal(ctx);

        public void LeavePropertyDefinition(PropertyDefinition ctx)
            => LeaveTerminal(ctx);

        public void EnterTypeCast(TypeCast ctx)
            => EnterNonTerminal(ctx);

        public void LeaveTypeCast(TypeCast ctx)
            => LeaveNonTerminal(ctx);

        public void EnterReturnStatement(ReturnStatement ctx)
            => EnterNonTerminal(ctx);

        public void LeaveReturnStatement(ReturnStatement ctx)
            => LeaveNonTerminal(ctx);

        public void EnterStatementList(StatementList ctx)
            => EnterNonTerminal(ctx);

        public void LeaveStatementList(StatementList ctx)
            => LeaveNonTerminal(ctx);

        public void EnterAbsoluteIri(AbsoluteIri ctx)
            => EnterTerminal(ctx);

        public void LeaveAbsoluteIri(AbsoluteIri ctx)
            => LeaveTerminal(ctx);

        public void EnterAliasDeclaration(AliasDeclaration ctx)
            => EnterNonTerminal(ctx);

        public void LeaveAliasDeclaration(AliasDeclaration ctx)
            => LeaveNonTerminal(ctx);

        public void EnterAssignmentStmt(AssignmentStmt ctx)
            => EnterNonTerminal(ctx);

        public void EnterBinaryExpression(BinaryExpression ctx)
            => EnterNonTerminal(ctx);

        public void EnterBlock(Block ctx)
            => EnterNonTerminal(ctx);


        public void EnterExpression(Expression ctx)
            => EnterNonTerminal(ctx); //??

        public void EnterExpressionList(ExpressionList ctx)
            => EnterNonTerminal(ctx);

        public void EnterFifthProgram(FifthProgram ctx)
            => EnterNonTerminal(ctx);

        public void EnterFloatValueExpression(FloatValueExpression ctx)
            => EnterTerminal(ctx);

        public void EnterFuncCallExpression(FuncCallExpression ctx)
            => EnterNonTerminal(ctx);

        public void EnterFunctionDefinition(FunctionDefinition ctx)
            => EnterNonTerminal(ctx);

        public void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
            => LeaveNonTerminal(ctx);

        public void EnterIdentifier(Identifier ctx)
            => EnterTerminal(ctx);

        public void EnterIdentifierExpression(IdentifierExpression ctx)
            => EnterNonTerminal(ctx);

        public void LeaveIfElseStatement(IfElseStatement ctx)
            => LeaveNonTerminal(ctx);

        public void EnterIntValueExpression(IntValueExpression ctx)
            => EnterTerminal(ctx);

        public void EnterModuleImport(ModuleImport ctx)
            => EnterTerminal(ctx);

        public void EnterParameterDeclaration(ParameterDeclaration ctx)
            => EnterTerminal(ctx);

        public void EnterParameterDeclarationList(ParameterDeclarationList ctx)
            => EnterNonTerminal(ctx);

        public void EnterStringValueExpression(StringValueExpression ctx)
            => EnterTerminal(ctx);

        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => EnterNonTerminal(ctx);

        public void EnterTypeInitialiser(TypeInitialiser ctx)
            => EnterNonTerminal(ctx);

        public void EnterUnaryExpression(UnaryExpression ctx)
            => EnterNonTerminal(ctx);

        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => EnterNonTerminal(ctx);

        public void EnterVariableReference(VariableReference ctx)
            => EnterTerminal(ctx);

        public void LeaveCompoundVariableReference(CompoundVariableReference ctx)
            => LeaveNonTerminal(ctx);

        public void EnterWhileExp(WhileExp ctx)
            => EnterNonTerminal(ctx);

        public void LeaveAssignmentStmt(AssignmentStmt ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveBinaryExpression(BinaryExpression ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveBlock(Block ctx)
            => LeaveNonTerminal(ctx);

        public void EnterBoolValueExpression(BoolValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveBoolValueExpression(BoolValueExpression ctx)
            => LeaveTerminal(ctx);

        public void LeaveExpression(Expression ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveExpressionList(ExpressionList ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveFifthProgram(FifthProgram ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveFloatValueExpression(FloatValueExpression ctx)
            => LeaveTerminal(ctx);

        public void LeaveFuncCallExpression(FuncCallExpression ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveFunctionDefinition(FunctionDefinition ctx)
            => LeaveNonTerminal(ctx);

        public void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
            => EnterNonTerminal(ctx); // TODO: Should I just ignore this?

        public void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
            => LeaveNonTerminal(ctx);

        public void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
            => EnterNonTerminal(ctx);

        public void LeaveIdentifier(Identifier ctx)
            => LeaveTerminal(ctx);

        public void LeaveIdentifierExpression(IdentifierExpression ctx)
            => LeaveNonTerminal(ctx);

        public void EnterIfElseStatement(IfElseStatement ctx)
            => EnterNonTerminal(ctx);

        public void LeaveIntValueExpression(IntValueExpression ctx)
            => LeaveTerminal(ctx);

        public void LeaveModuleImport(ModuleImport ctx)
            => LeaveTerminal(ctx);

        public void LeaveParameterDeclaration(ParameterDeclaration ctx)
            => LeaveTerminal(ctx);

        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveStringValueExpression(StringValueExpression ctx)
            => LeaveTerminal(ctx);

        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveTypeInitialiser(TypeInitialiser ctx)
            => LeaveNonTerminal(ctx);

        public void EnterDestructuringParamDecl(DestructuringParamDecl ctx)
            => EnterNonTerminal(ctx);

        public void LeaveDestructuringParamDecl(DestructuringParamDecl ctx)
            => LeaveNonTerminal(ctx);

        public void EnterPropertyBinding(PropertyBinding ctx)
            => EnterNonTerminal(ctx);

        public void LeavePropertyBinding(PropertyBinding ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveUnaryExpression(UnaryExpression ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveVariableReference(VariableReference ctx)
            => LeaveTerminal(ctx);

        public void EnterCompoundVariableReference(CompoundVariableReference ctx)
            => EnterNonTerminal(ctx);

        public void LeaveWhileExp(WhileExp ctx)
            => LeaveNonTerminal(ctx);

        public void EnterLongValueExpression(LongValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveLongValueExpression(LongValueExpression ctx)
            => LeaveTerminal(ctx);

        public void EnterShortValueExpression(ShortValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveShortValueExpression(ShortValueExpression ctx)
            => LeaveTerminal(ctx);

        public void EnterDoubleValueExpression(DoubleValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveDoubleValueExpression(DoubleValueExpression ctx)
            => LeaveTerminal(ctx);

        public void EnterDecimalValueExpression(DecimalValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveDecimalValueExpression(DecimalValueExpression ctx)
            => LeaveTerminal(ctx);

        public void EnterDateValueExpression(DateValueExpression ctx)
            => EnterTerminal(ctx);

        public void LeaveDateValueExpression(DateValueExpression ctx)
            => LeaveTerminal(ctx);

        public void EnterExpressionStatement(ExpressionStatement ctx)
            => EnterNonTerminal(ctx);

        public void LeaveExpressionStatement(ExpressionStatement ctx)
            => LeaveNonTerminal(ctx);

        public void EnterTypePropertyInit(TypePropertyInit ctx)
            => EnterNonTerminal(ctx);

        public void LeaveTypePropertyInit(TypePropertyInit ctx)
            => LeaveNonTerminal(ctx);

        public void LeaveNotExpression(UnaryExpression ctx)
            => LeaveNonTerminal(ctx);

        private void EnterNonTerminal(AstNode ctx)
        {
            ctx.ParentNode = parents.PeekOrDefault();
            parents.Push(ctx);
        }

        private void EnterTerminal(AstNode ctx)
            => ctx.ParentNode = parents.PeekOrDefault();

        private void LeaveNonTerminal(AstNode ctx)
            => parents.Pop();

        private void LeaveTerminal(AstNode ctx)
        {
        }
    }
}
