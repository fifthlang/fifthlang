

namespace Fifth.AST.Visitors
{
    using Symbols;
    using TypeSystem;

    public interface IAstVisitor
    {
        public void EnterAbsoluteIri(AbsoluteIri ctx);
        public void LeaveAbsoluteIri(AbsoluteIri ctx);
        public void EnterAliasDeclaration(AliasDeclaration ctx);
        public void LeaveAliasDeclaration(AliasDeclaration ctx);
        public void EnterAssignmentStmt(AssignmentStmt ctx);
        public void LeaveAssignmentStmt(AssignmentStmt ctx);
        public void EnterBinaryExpression(BinaryExpression ctx);
        public void LeaveBinaryExpression(BinaryExpression ctx);
        public void EnterBlock(Block ctx);
        public void LeaveBlock(Block ctx);
        public void EnterBooleanExpression(BooleanExpression ctx);
        public void LeaveBooleanExpression(BooleanExpression ctx);
        public void EnterDateValueExpression(DateValueExpression ctx);
        public void LeaveDateValueExpression(DateValueExpression ctx);
        public void EnterDecimalValueExpression(DecimalValueExpression ctx);
        public void LeaveDecimalValueExpression(DecimalValueExpression ctx);
        public void EnterDoubleValueExpression(DoubleValueExpression ctx);
        public void LeaveDoubleValueExpression(DoubleValueExpression ctx);
        public void EnterExpression(Expression ctx);
        public void LeaveExpression(Expression ctx);
        public void EnterExpressionList(ExpressionList ctx);
        public void LeaveExpressionList(ExpressionList ctx);
        public void EnterExpressionStatement(ExpressionStatement ctx);
        public void LeaveExpressionStatement(ExpressionStatement ctx);
        public void EnterFifthProgram(FifthProgram ctx);
        public void LeaveFifthProgram(FifthProgram ctx);
        public void EnterFloatValueExpression(FloatValueExpression ctx);
        public void LeaveFloatValueExpression(FloatValueExpression ctx);
        public void EnterFuncCallExpression(FuncCallExpression ctx);
        public void LeaveFuncCallExpression(FuncCallExpression ctx);
        public void EnterFunctionDefinition(FunctionDefinition ctx);
        public void LeaveFunctionDefinition(FunctionDefinition ctx);
        public void EnterIdentifier(Identifier ctx);
        public void LeaveIdentifier(Identifier ctx);
        public void EnterIdentifierExpression(IdentifierExpression ctx);
        public void LeaveIdentifierExpression(IdentifierExpression ctx);
        public void EnterIfElseStatement(IfElseStatement ctx);
        public void LeaveIfElseStatement(IfElseStatement ctx);
        public void EnterIntValueExpression(IntValueExpression ctx);
        public void LeaveIntValueExpression(IntValueExpression ctx);
        public void EnterLongValueExpression(LongValueExpression ctx);
        public void LeaveLongValueExpression(LongValueExpression ctx);
        public void EnterModuleImport(ModuleImport ctx);
        public void LeaveModuleImport(ModuleImport ctx);
        public void EnterParameterDeclaration(ParameterDeclaration ctx);
        public void LeaveParameterDeclaration(ParameterDeclaration ctx);
        public void EnterParameterDeclarationList(ParameterDeclarationList ctx);
        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx);
        public void EnterReturnStatement(ReturnStatement ctx);
        public void LeaveReturnStatement(ReturnStatement ctx);
        public void EnterShortValueExpression(ShortValueExpression ctx);
        public void LeaveShortValueExpression(ShortValueExpression ctx);
        public void EnterStatementList(StatementList ctx);
        public void LeaveStatementList(StatementList ctx);
        public void EnterStringValueExpression(StringValueExpression ctx);
        public void LeaveStringValueExpression(StringValueExpression ctx);
        public void EnterTypeCast(TypeCast ctx);
        public void LeaveTypeCast(TypeCast ctx);
        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void EnterTypeInitialiser(TypeInitialiser ctx);
        public void LeaveTypeInitialiser(TypeInitialiser ctx);
        public void EnterUnaryExpression(UnaryExpression ctx);
        public void LeaveUnaryExpression(UnaryExpression ctx);
        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void EnterVariableReference(VariableReference ctx);
        public void LeaveVariableReference(VariableReference ctx);
        public void EnterWhileExp(WhileExp ctx);
        public void LeaveWhileExp(WhileExp ctx);
    }

    public partial class BaseAstVisitor : IAstVisitor
    {
        public virtual void EnterAbsoluteIri(AbsoluteIri ctx){}
        public virtual void LeaveAbsoluteIri(AbsoluteIri ctx){}
        public virtual void EnterAliasDeclaration(AliasDeclaration ctx){}
        public virtual void LeaveAliasDeclaration(AliasDeclaration ctx){}
        public virtual void EnterAssignmentStmt(AssignmentStmt ctx){}
        public virtual void LeaveAssignmentStmt(AssignmentStmt ctx){}
        public virtual void EnterBinaryExpression(BinaryExpression ctx){}
        public virtual void LeaveBinaryExpression(BinaryExpression ctx){}
        public virtual void EnterBlock(Block ctx){}
        public virtual void LeaveBlock(Block ctx){}
        public virtual void EnterBooleanExpression(BooleanExpression ctx){}
        public virtual void LeaveBooleanExpression(BooleanExpression ctx){}
        public virtual void EnterDateValueExpression(DateValueExpression ctx){}
        public virtual void LeaveDateValueExpression(DateValueExpression ctx){}
        public virtual void EnterDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void LeaveDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void EnterDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void LeaveDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void EnterExpression(Expression ctx){}
        public virtual void LeaveExpression(Expression ctx){}
        public virtual void EnterExpressionList(ExpressionList ctx){}
        public virtual void LeaveExpressionList(ExpressionList ctx){}
        public virtual void EnterExpressionStatement(ExpressionStatement ctx){}
        public virtual void LeaveExpressionStatement(ExpressionStatement ctx){}
        public virtual void EnterFifthProgram(FifthProgram ctx){}
        public virtual void LeaveFifthProgram(FifthProgram ctx){}
        public virtual void EnterFloatValueExpression(FloatValueExpression ctx){}
        public virtual void LeaveFloatValueExpression(FloatValueExpression ctx){}
        public virtual void EnterFuncCallExpression(FuncCallExpression ctx){}
        public virtual void LeaveFuncCallExpression(FuncCallExpression ctx){}
        public virtual void EnterFunctionDefinition(FunctionDefinition ctx){}
        public virtual void LeaveFunctionDefinition(FunctionDefinition ctx){}
        public virtual void EnterIdentifier(Identifier ctx){}
        public virtual void LeaveIdentifier(Identifier ctx){}
        public virtual void EnterIdentifierExpression(IdentifierExpression ctx){}
        public virtual void LeaveIdentifierExpression(IdentifierExpression ctx){}
        public virtual void EnterIfElseStatement(IfElseStatement ctx){}
        public virtual void LeaveIfElseStatement(IfElseStatement ctx){}
        public virtual void EnterIntValueExpression(IntValueExpression ctx){}
        public virtual void LeaveIntValueExpression(IntValueExpression ctx){}
        public virtual void EnterLongValueExpression(LongValueExpression ctx){}
        public virtual void LeaveLongValueExpression(LongValueExpression ctx){}
        public virtual void EnterModuleImport(ModuleImport ctx){}
        public virtual void LeaveModuleImport(ModuleImport ctx){}
        public virtual void EnterParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void LeaveParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void EnterParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void LeaveParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void EnterReturnStatement(ReturnStatement ctx){}
        public virtual void LeaveReturnStatement(ReturnStatement ctx){}
        public virtual void EnterShortValueExpression(ShortValueExpression ctx){}
        public virtual void LeaveShortValueExpression(ShortValueExpression ctx){}
        public virtual void EnterStatementList(StatementList ctx){}
        public virtual void LeaveStatementList(StatementList ctx){}
        public virtual void EnterStringValueExpression(StringValueExpression ctx){}
        public virtual void LeaveStringValueExpression(StringValueExpression ctx){}
        public virtual void EnterTypeCast(TypeCast ctx){}
        public virtual void LeaveTypeCast(TypeCast ctx){}
        public virtual void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void EnterTypeInitialiser(TypeInitialiser ctx){}
        public virtual void LeaveTypeInitialiser(TypeInitialiser ctx){}
        public virtual void EnterUnaryExpression(UnaryExpression ctx){}
        public virtual void LeaveUnaryExpression(UnaryExpression ctx){}
        public virtual void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void EnterVariableReference(VariableReference ctx){}
        public virtual void LeaveVariableReference(VariableReference ctx){}
        public virtual void EnterWhileExp(WhileExp ctx){}
        public virtual void LeaveWhileExp(WhileExp ctx){}
    }
}

namespace Fifth.TypeSystem
{
    using AST;
    using Symbols;

    public interface ITypeChecker
    {
        public IType Infer(IScope scope, AbsoluteIri node);
        public IType Infer(IScope scope, AliasDeclaration node);
        public IType Infer(IScope scope, AssignmentStmt node);
        public IType Infer(IScope scope, BinaryExpression node);
        public IType Infer(IScope scope, Block node);
        public IType Infer(IScope scope, BooleanExpression node);
        public IType Infer(IScope scope, DateValueExpression node);
        public IType Infer(IScope scope, DecimalValueExpression node);
        public IType Infer(IScope scope, DoubleValueExpression node);
        public IType Infer(IScope scope, Expression node);
        public IType Infer(IScope scope, ExpressionList node);
        public IType Infer(IScope scope, ExpressionStatement node);
        public IType Infer(IScope scope, FifthProgram node);
        public IType Infer(IScope scope, FloatValueExpression node);
        public IType Infer(IScope scope, FuncCallExpression node);
        public IType Infer(IScope scope, FunctionDefinition node);
        public IType Infer(IScope scope, Identifier node);
        public IType Infer(IScope scope, IdentifierExpression node);
        public IType Infer(IScope scope, IfElseStatement node);
        public IType Infer(IScope scope, IntValueExpression node);
        public IType Infer(IScope scope, LongValueExpression node);
        public IType Infer(IScope scope, ModuleImport node);
        public IType Infer(IScope scope, ParameterDeclaration node);
        public IType Infer(IScope scope, ParameterDeclarationList node);
        public IType Infer(IScope scope, ReturnStatement node);
        public IType Infer(IScope scope, ShortValueExpression node);
        public IType Infer(IScope scope, StatementList node);
        public IType Infer(IScope scope, StringValueExpression node);
        public IType Infer(IScope scope, TypeCast node);
        public IType Infer(IScope scope, TypeCreateInstExpression node);
        public IType Infer(IScope scope, TypeInitialiser node);
        public IType Infer(IScope scope, UnaryExpression node);
        public IType Infer(IScope scope, VariableDeclarationStatement node);
        public IType Infer(IScope scope, VariableReference node);
        public IType Infer(IScope scope, WhileExp node);
    }

    public partial class FunctionalTypeChecker
    {

        public IType Infer(AstNode exp)
        {
            var scope = exp.NearestScope();
            return exp switch
            {
                TypeCast node => Infer(scope, node),
                ReturnStatement node => Infer(scope, node),
                StatementList node => Infer(scope, node),
                AbsoluteIri node => Infer(scope, node),
                AliasDeclaration node => Infer(scope, node),
                AssignmentStmt node => Infer(scope, node),
                BinaryExpression node => Infer(scope, node),
                Block node => Infer(scope, node),
                BooleanExpression node => Infer(scope, node),
                ExpressionList node => Infer(scope, node),
                FifthProgram node => Infer(scope, node),
                FuncCallExpression node => Infer(scope, node),
                FunctionDefinition node => Infer(scope, node),
                Identifier node => Infer(scope, node),
                IdentifierExpression node => Infer(scope, node),
                IfElseStatement node => Infer(scope, node),
                ModuleImport node => Infer(scope, node),
                ParameterDeclaration node => Infer(scope, node),
                ParameterDeclarationList node => Infer(scope, node),
                TypeCreateInstExpression node => Infer(scope, node),
                TypeInitialiser node => Infer(scope, node),
                UnaryExpression node => Infer(scope, node),
                VariableDeclarationStatement node => Infer(scope, node),
                VariableReference node => Infer(scope, node),
                WhileExp node => Infer(scope, node),
                ExpressionStatement node => Infer(scope, node),
                StringValueExpression node => Infer(scope, node),
                ShortValueExpression node => Infer(scope, node),
                IntValueExpression node => Infer(scope, node),
                LongValueExpression node => Infer(scope, node),
                FloatValueExpression node => Infer(scope, node),
                DoubleValueExpression node => Infer(scope, node),
                DecimalValueExpression node => Infer(scope, node),
                DateValueExpression node => Infer(scope, node),
                Expression node => Infer(scope, node),

                { } node => Infer(scope, node),
            };
        }


    }
}

