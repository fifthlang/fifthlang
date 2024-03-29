

namespace Fifth.AST.Visitors
{
    using System;
    using Symbols;
    using Fifth.AST;
    using TypeSystem;
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;
    using System.Collections.Generic;

    public interface IAstVisitor
    {
        public void EnterAssembly(Assembly ctx);
        public void LeaveAssembly(Assembly ctx);
        public void EnterAssemblyRef(AssemblyRef ctx);
        public void LeaveAssemblyRef(AssemblyRef ctx);
        public void EnterClassDefinition(ClassDefinition ctx);
        public void LeaveClassDefinition(ClassDefinition ctx);
        public void EnterPropertyDefinition(PropertyDefinition ctx);
        public void LeavePropertyDefinition(PropertyDefinition ctx);
        public void EnterTypeCast(TypeCast ctx);
        public void LeaveTypeCast(TypeCast ctx);
        public void EnterReturnStatement(ReturnStatement ctx);
        public void LeaveReturnStatement(ReturnStatement ctx);
        public void EnterStatementList(StatementList ctx);
        public void LeaveStatementList(StatementList ctx);
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
        public void EnterBoolValueExpression(BoolValueExpression ctx);
        public void LeaveBoolValueExpression(BoolValueExpression ctx);
        public void EnterShortValueExpression(ShortValueExpression ctx);
        public void LeaveShortValueExpression(ShortValueExpression ctx);
        public void EnterIntValueExpression(IntValueExpression ctx);
        public void LeaveIntValueExpression(IntValueExpression ctx);
        public void EnterLongValueExpression(LongValueExpression ctx);
        public void LeaveLongValueExpression(LongValueExpression ctx);
        public void EnterFloatValueExpression(FloatValueExpression ctx);
        public void LeaveFloatValueExpression(FloatValueExpression ctx);
        public void EnterDoubleValueExpression(DoubleValueExpression ctx);
        public void LeaveDoubleValueExpression(DoubleValueExpression ctx);
        public void EnterDecimalValueExpression(DecimalValueExpression ctx);
        public void LeaveDecimalValueExpression(DecimalValueExpression ctx);
        public void EnterStringValueExpression(StringValueExpression ctx);
        public void LeaveStringValueExpression(StringValueExpression ctx);
        public void EnterDateValueExpression(DateValueExpression ctx);
        public void LeaveDateValueExpression(DateValueExpression ctx);
        public void EnterExpressionList(ExpressionList ctx);
        public void LeaveExpressionList(ExpressionList ctx);
        public void EnterFifthProgram(FifthProgram ctx);
        public void LeaveFifthProgram(FifthProgram ctx);
        public void EnterFuncCallExpression(FuncCallExpression ctx);
        public void LeaveFuncCallExpression(FuncCallExpression ctx);
        public void EnterFunctionDefinition(FunctionDefinition ctx);
        public void LeaveFunctionDefinition(FunctionDefinition ctx);
        public void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
        public void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
        public void EnterIdentifier(Identifier ctx);
        public void LeaveIdentifier(Identifier ctx);
        public void EnterIdentifierExpression(IdentifierExpression ctx);
        public void LeaveIdentifierExpression(IdentifierExpression ctx);
        public void EnterIfElseStatement(IfElseStatement ctx);
        public void LeaveIfElseStatement(IfElseStatement ctx);
        public void EnterModuleImport(ModuleImport ctx);
        public void LeaveModuleImport(ModuleImport ctx);
        public void EnterDestructuringParamDecl(DestructuringParamDecl ctx);
        public void LeaveDestructuringParamDecl(DestructuringParamDecl ctx);
        public void EnterParameterDeclaration(ParameterDeclaration ctx);
        public void LeaveParameterDeclaration(ParameterDeclaration ctx);
        public void EnterParameterDeclarationList(ParameterDeclarationList ctx);
        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx);
        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void EnterTypeInitialiser(TypeInitialiser ctx);
        public void LeaveTypeInitialiser(TypeInitialiser ctx);
        public void EnterPropertyBinding(PropertyBinding ctx);
        public void LeavePropertyBinding(PropertyBinding ctx);
        public void EnterTypePropertyInit(TypePropertyInit ctx);
        public void LeaveTypePropertyInit(TypePropertyInit ctx);
        public void EnterUnaryExpression(UnaryExpression ctx);
        public void LeaveUnaryExpression(UnaryExpression ctx);
        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void EnterVariableReference(VariableReference ctx);
        public void LeaveVariableReference(VariableReference ctx);
        public void EnterCompoundVariableReference(CompoundVariableReference ctx);
        public void LeaveCompoundVariableReference(CompoundVariableReference ctx);
        public void EnterWhileExp(WhileExp ctx);
        public void LeaveWhileExp(WhileExp ctx);
        public void EnterExpressionStatement(ExpressionStatement ctx);
        public void LeaveExpressionStatement(ExpressionStatement ctx);
        public void EnterExpression(Expression ctx);
        public void LeaveExpression(Expression ctx);
    }

    public partial class BaseAstVisitor : IAstVisitor
    {
        public virtual void EnterAssembly(Assembly ctx){}
        public virtual void LeaveAssembly(Assembly ctx){}
        public virtual void EnterAssemblyRef(AssemblyRef ctx){}
        public virtual void LeaveAssemblyRef(AssemblyRef ctx){}
        public virtual void EnterClassDefinition(ClassDefinition ctx){}
        public virtual void LeaveClassDefinition(ClassDefinition ctx){}
        public virtual void EnterPropertyDefinition(PropertyDefinition ctx){}
        public virtual void LeavePropertyDefinition(PropertyDefinition ctx){}
        public virtual void EnterTypeCast(TypeCast ctx){}
        public virtual void LeaveTypeCast(TypeCast ctx){}
        public virtual void EnterReturnStatement(ReturnStatement ctx){}
        public virtual void LeaveReturnStatement(ReturnStatement ctx){}
        public virtual void EnterStatementList(StatementList ctx){}
        public virtual void LeaveStatementList(StatementList ctx){}
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
        public virtual void EnterBoolValueExpression(BoolValueExpression ctx){}
        public virtual void LeaveBoolValueExpression(BoolValueExpression ctx){}
        public virtual void EnterShortValueExpression(ShortValueExpression ctx){}
        public virtual void LeaveShortValueExpression(ShortValueExpression ctx){}
        public virtual void EnterIntValueExpression(IntValueExpression ctx){}
        public virtual void LeaveIntValueExpression(IntValueExpression ctx){}
        public virtual void EnterLongValueExpression(LongValueExpression ctx){}
        public virtual void LeaveLongValueExpression(LongValueExpression ctx){}
        public virtual void EnterFloatValueExpression(FloatValueExpression ctx){}
        public virtual void LeaveFloatValueExpression(FloatValueExpression ctx){}
        public virtual void EnterDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void LeaveDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void EnterDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void LeaveDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void EnterStringValueExpression(StringValueExpression ctx){}
        public virtual void LeaveStringValueExpression(StringValueExpression ctx){}
        public virtual void EnterDateValueExpression(DateValueExpression ctx){}
        public virtual void LeaveDateValueExpression(DateValueExpression ctx){}
        public virtual void EnterExpressionList(ExpressionList ctx){}
        public virtual void LeaveExpressionList(ExpressionList ctx){}
        public virtual void EnterFifthProgram(FifthProgram ctx){}
        public virtual void LeaveFifthProgram(FifthProgram ctx){}
        public virtual void EnterFuncCallExpression(FuncCallExpression ctx){}
        public virtual void LeaveFuncCallExpression(FuncCallExpression ctx){}
        public virtual void EnterFunctionDefinition(FunctionDefinition ctx){}
        public virtual void LeaveFunctionDefinition(FunctionDefinition ctx){}
        public virtual void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx){}
        public virtual void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx){}
        public virtual void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx){}
        public virtual void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx){}
        public virtual void EnterIdentifier(Identifier ctx){}
        public virtual void LeaveIdentifier(Identifier ctx){}
        public virtual void EnterIdentifierExpression(IdentifierExpression ctx){}
        public virtual void LeaveIdentifierExpression(IdentifierExpression ctx){}
        public virtual void EnterIfElseStatement(IfElseStatement ctx){}
        public virtual void LeaveIfElseStatement(IfElseStatement ctx){}
        public virtual void EnterModuleImport(ModuleImport ctx){}
        public virtual void LeaveModuleImport(ModuleImport ctx){}
        public virtual void EnterDestructuringParamDecl(DestructuringParamDecl ctx){}
        public virtual void LeaveDestructuringParamDecl(DestructuringParamDecl ctx){}
        public virtual void EnterParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void LeaveParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void EnterParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void LeaveParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void EnterTypeInitialiser(TypeInitialiser ctx){}
        public virtual void LeaveTypeInitialiser(TypeInitialiser ctx){}
        public virtual void EnterPropertyBinding(PropertyBinding ctx){}
        public virtual void LeavePropertyBinding(PropertyBinding ctx){}
        public virtual void EnterTypePropertyInit(TypePropertyInit ctx){}
        public virtual void LeaveTypePropertyInit(TypePropertyInit ctx){}
        public virtual void EnterUnaryExpression(UnaryExpression ctx){}
        public virtual void LeaveUnaryExpression(UnaryExpression ctx){}
        public virtual void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void EnterVariableReference(VariableReference ctx){}
        public virtual void LeaveVariableReference(VariableReference ctx){}
        public virtual void EnterCompoundVariableReference(CompoundVariableReference ctx){}
        public virtual void LeaveCompoundVariableReference(CompoundVariableReference ctx){}
        public virtual void EnterWhileExp(WhileExp ctx){}
        public virtual void LeaveWhileExp(WhileExp ctx){}
        public virtual void EnterExpressionStatement(ExpressionStatement ctx){}
        public virtual void LeaveExpressionStatement(ExpressionStatement ctx){}
        public virtual void EnterExpression(Expression ctx){}
        public virtual void LeaveExpression(Expression ctx){}
    }


    public interface IAstRecursiveDescentVisitor
    {
        public AstNode Visit(AstNode ctx);
        public Assembly VisitAssembly(Assembly ctx);
        public AssemblyRef VisitAssemblyRef(AssemblyRef ctx);
        public ClassDefinition VisitClassDefinition(ClassDefinition ctx);
        public PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx);
        public TypeCast VisitTypeCast(TypeCast ctx);
        public ReturnStatement VisitReturnStatement(ReturnStatement ctx);
        public StatementList VisitStatementList(StatementList ctx);
        public AbsoluteIri VisitAbsoluteIri(AbsoluteIri ctx);
        public AliasDeclaration VisitAliasDeclaration(AliasDeclaration ctx);
        public AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx);
        public BinaryExpression VisitBinaryExpression(BinaryExpression ctx);
        public Block VisitBlock(Block ctx);
        public BoolValueExpression VisitBoolValueExpression(BoolValueExpression ctx);
        public ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx);
        public IntValueExpression VisitIntValueExpression(IntValueExpression ctx);
        public LongValueExpression VisitLongValueExpression(LongValueExpression ctx);
        public FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx);
        public DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx);
        public DecimalValueExpression VisitDecimalValueExpression(DecimalValueExpression ctx);
        public StringValueExpression VisitStringValueExpression(StringValueExpression ctx);
        public DateValueExpression VisitDateValueExpression(DateValueExpression ctx);
        public ExpressionList VisitExpressionList(ExpressionList ctx);
        public FifthProgram VisitFifthProgram(FifthProgram ctx);
        public FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx);
        public FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx);
        public BuiltinFunctionDefinition VisitBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public OverloadedFunctionDefinition VisitOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
        public Identifier VisitIdentifier(Identifier ctx);
        public IdentifierExpression VisitIdentifierExpression(IdentifierExpression ctx);
        public IfElseStatement VisitIfElseStatement(IfElseStatement ctx);
        public ModuleImport VisitModuleImport(ModuleImport ctx);
        public DestructuringParamDecl VisitDestructuringParamDecl(DestructuringParamDecl ctx);
        public ParameterDeclaration VisitParameterDeclaration(ParameterDeclaration ctx);
        public ParameterDeclarationList VisitParameterDeclarationList(ParameterDeclarationList ctx);
        public TypeCreateInstExpression VisitTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public TypeInitialiser VisitTypeInitialiser(TypeInitialiser ctx);
        public PropertyBinding VisitPropertyBinding(PropertyBinding ctx);
        public TypePropertyInit VisitTypePropertyInit(TypePropertyInit ctx);
        public UnaryExpression VisitUnaryExpression(UnaryExpression ctx);
        public VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public VariableReference VisitVariableReference(VariableReference ctx);
        public CompoundVariableReference VisitCompoundVariableReference(CompoundVariableReference ctx);
        public WhileExp VisitWhileExp(WhileExp ctx);
        public ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx);
        public Expression VisitExpression(Expression ctx);
    }

    public class DefaultRecursiveDescentVisitor : IAstRecursiveDescentVisitor
    {
        public virtual AstNode Visit(AstNode ctx)
        => ctx switch
            {
                Assembly node => VisitAssembly(node),
                AssemblyRef node => VisitAssemblyRef(node),
                ClassDefinition node => VisitClassDefinition(node),
                PropertyDefinition node => VisitPropertyDefinition(node),
                TypeCast node => VisitTypeCast(node),
                ReturnStatement node => VisitReturnStatement(node),
                StatementList node => VisitStatementList(node),
                AbsoluteIri node => VisitAbsoluteIri(node),
                AliasDeclaration node => VisitAliasDeclaration(node),
                AssignmentStmt node => VisitAssignmentStmt(node),
                BinaryExpression node => VisitBinaryExpression(node),
                Block node => VisitBlock(node),
                BoolValueExpression node => VisitBoolValueExpression(node),
                ShortValueExpression node => VisitShortValueExpression(node),
                IntValueExpression node => VisitIntValueExpression(node),
                LongValueExpression node => VisitLongValueExpression(node),
                FloatValueExpression node => VisitFloatValueExpression(node),
                DoubleValueExpression node => VisitDoubleValueExpression(node),
                DecimalValueExpression node => VisitDecimalValueExpression(node),
                StringValueExpression node => VisitStringValueExpression(node),
                DateValueExpression node => VisitDateValueExpression(node),
                ExpressionList node => VisitExpressionList(node),
                FifthProgram node => VisitFifthProgram(node),
                FuncCallExpression node => VisitFuncCallExpression(node),
                FunctionDefinition node => VisitFunctionDefinition(node),
                BuiltinFunctionDefinition node => VisitBuiltinFunctionDefinition(node),
                OverloadedFunctionDefinition node => VisitOverloadedFunctionDefinition(node),
                Identifier node => VisitIdentifier(node),
                IdentifierExpression node => VisitIdentifierExpression(node),
                IfElseStatement node => VisitIfElseStatement(node),
                ModuleImport node => VisitModuleImport(node),
                DestructuringParamDecl node => VisitDestructuringParamDecl(node),
                ParameterDeclaration node => VisitParameterDeclaration(node),
                ParameterDeclarationList node => VisitParameterDeclarationList(node),
                TypeCreateInstExpression node => VisitTypeCreateInstExpression(node),
                TypeInitialiser node => VisitTypeInitialiser(node),
                PropertyBinding node => VisitPropertyBinding(node),
                TypePropertyInit node => VisitTypePropertyInit(node),
                UnaryExpression node => VisitUnaryExpression(node),
                VariableDeclarationStatement node => VisitVariableDeclarationStatement(node),
                VariableReference node => VisitVariableReference(node),
                CompoundVariableReference node => VisitCompoundVariableReference(node),
                WhileExp node => VisitWhileExp(node),
                ExpressionStatement node => VisitExpressionStatement(node),
                Expression node => VisitExpression(node),

                { } node => null,
            };

        public virtual Assembly VisitAssembly(Assembly ctx)
            => ctx;
        public virtual AssemblyRef VisitAssemblyRef(AssemblyRef ctx)
            => ctx;
        public virtual ClassDefinition VisitClassDefinition(ClassDefinition ctx)
            => ctx;
        public virtual PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
            => ctx;
        public virtual TypeCast VisitTypeCast(TypeCast ctx)
            => ctx;
        public virtual ReturnStatement VisitReturnStatement(ReturnStatement ctx)
            => ctx;
        public virtual StatementList VisitStatementList(StatementList ctx)
            => ctx;
        public virtual AbsoluteIri VisitAbsoluteIri(AbsoluteIri ctx)
            => ctx;
        public virtual AliasDeclaration VisitAliasDeclaration(AliasDeclaration ctx)
            => ctx;
        public virtual AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx)
            => ctx;
        public virtual BinaryExpression VisitBinaryExpression(BinaryExpression ctx)
            => ctx;
        public virtual Block VisitBlock(Block ctx)
            => ctx;
        public virtual BoolValueExpression VisitBoolValueExpression(BoolValueExpression ctx)
            => ctx;
        public virtual ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx)
            => ctx;
        public virtual IntValueExpression VisitIntValueExpression(IntValueExpression ctx)
            => ctx;
        public virtual LongValueExpression VisitLongValueExpression(LongValueExpression ctx)
            => ctx;
        public virtual FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx)
            => ctx;
        public virtual DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx)
            => ctx;
        public virtual DecimalValueExpression VisitDecimalValueExpression(DecimalValueExpression ctx)
            => ctx;
        public virtual StringValueExpression VisitStringValueExpression(StringValueExpression ctx)
            => ctx;
        public virtual DateValueExpression VisitDateValueExpression(DateValueExpression ctx)
            => ctx;
        public virtual ExpressionList VisitExpressionList(ExpressionList ctx)
            => ctx;
        public virtual FifthProgram VisitFifthProgram(FifthProgram ctx)
            => ctx;
        public virtual FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx)
            => ctx;
        public virtual FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx)
            => ctx;
        public virtual BuiltinFunctionDefinition VisitBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
            => ctx;
        public virtual OverloadedFunctionDefinition VisitOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
            => ctx;
        public virtual Identifier VisitIdentifier(Identifier ctx)
            => ctx;
        public virtual IdentifierExpression VisitIdentifierExpression(IdentifierExpression ctx)
            => ctx;
        public virtual IfElseStatement VisitIfElseStatement(IfElseStatement ctx)
            => ctx;
        public virtual ModuleImport VisitModuleImport(ModuleImport ctx)
            => ctx;
        public virtual DestructuringParamDecl VisitDestructuringParamDecl(DestructuringParamDecl ctx)
            => ctx;
        public virtual ParameterDeclaration VisitParameterDeclaration(ParameterDeclaration ctx)
            => ctx;
        public virtual ParameterDeclarationList VisitParameterDeclarationList(ParameterDeclarationList ctx)
            => ctx;
        public virtual TypeCreateInstExpression VisitTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => ctx;
        public virtual TypeInitialiser VisitTypeInitialiser(TypeInitialiser ctx)
            => ctx;
        public virtual PropertyBinding VisitPropertyBinding(PropertyBinding ctx)
            => ctx;
        public virtual TypePropertyInit VisitTypePropertyInit(TypePropertyInit ctx)
            => ctx;
        public virtual UnaryExpression VisitUnaryExpression(UnaryExpression ctx)
            => ctx;
        public virtual VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => ctx;
        public virtual VariableReference VisitVariableReference(VariableReference ctx)
            => ctx;
        public virtual CompoundVariableReference VisitCompoundVariableReference(CompoundVariableReference ctx)
            => ctx;
        public virtual WhileExp VisitWhileExp(WhileExp ctx)
            => ctx;
        public virtual ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx)
            => ctx;
        public virtual Expression VisitExpression(Expression ctx)
            => ctx;

    }

}

