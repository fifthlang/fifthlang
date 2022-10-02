namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using System.Text;
using AST;
using AST.Visitors;
using IL;
using BinaryExpression = AST.BinaryExpression;
using ClassDefinition = AST.ClassDefinition;
using Expression = AST.Expression;
using FieldDefinition = AST.FieldDefinition;
using ParameterDeclaration = AST.ParameterDeclaration;
using PropertyDefinition = AST.PropertyDefinition;
using ReturnStatement = AST.ReturnStatement;
using UnaryExpression = AST.UnaryExpression;

public class CurrentStack<T> : Stack<T>
{
    public T Current => Peek();
}
public class ILGenerator : BaseAstVisitor
{
    public StringBuilder sb { get; set; } = new StringBuilder();
    private CurrentStack<IL.ProgramBuilder> ProgramBuilders = new();
    private CurrentStack<IL.AssemblyBuilder> AssemblyBuilders = new();
    private CurrentStack<IL.AssemblyReferenceBuilder> AssemblyRefBuilders = new();
    private CurrentStack<IL.ClassBuilder> ClassBuilders = new();
    private CurrentStack<IL.FieldBuilder> FieldBuilders = new();
    private CurrentStack<IL.PropertyBuilder> PropertyBuilders = new();
    private CurrentStack<IL.MethodBuilder> MethodBuilders = new();
    public override void EnterAssembly(Assembly ctx)
    {
        AssemblyBuilders.Push(IL.AssemblyBuilder.Create());
        AssemblyBuilders.Current.WithName(ctx.Name)
                        .WithVersion(ctx.Version)
                        .New();
    }

    public override void LeaveAssembly(Assembly ctx)
    {
        var ab = AssemblyBuilders.Pop();
        sb.AppendLine(ab.Build());
    }

    public override void EnterAssemblyRef(AssemblyRef ctx)
    {
        AssemblyRefBuilders.Push(IL.AssemblyReferenceBuilder.Create());
        AssemblyRefBuilders.Current.WithName(ctx.Name)
                           .WithVersion(new IL.Version(ctx.Version))
                           .WithPublicKeyToken(ctx.PublicKeyToken)
                           .New();
    }

    public override void LeaveAssemblyRef(AssemblyRef ctx)
    {
        var ab = AssemblyRefBuilders.Pop();
        sb.AppendLine(ab.Build());
    }

    public override void EnterClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Push(IL.ClassBuilder.Create());
        ClassBuilders.Current.WithName(ctx.Name) ;
    }

    public override void LeaveClassDefinition(ClassDefinition ctx)
    {
        var ab = ClassBuilders.Pop();
        ProgramBuilders.Current.WithClass(ab.New());
    }

    public override void EnterFieldDefinition(FieldDefinition ctx)
    {
        FieldBuilders.Push(IL.FieldBuilder.Create());
        FieldBuilders.Current.WithName(ctx.Name)
                     .WithType(ctx.TypeName);
    }

    public override void LeaveFieldDefinition(FieldDefinition ctx)
    {
        var x = FieldBuilders.Pop();
        sb.AppendLine(x.Build());
    }

    public override void EnterPropertyDefinition(PropertyDefinition ctx)
    {
        PropertyBuilders.Push(IL.PropertyBuilder.Create());
        PropertyBuilders.Current.WithName(ctx.Name)
                     .WithType(ctx.TypeName);
    }

    public override void LeavePropertyDefinition(PropertyDefinition ctx)
    {
        var x = PropertyBuilders.Pop();
        sb.AppendLine(x.Build());
    }

    public override void EnterTypeCast(TypeCast ctx)
    {
        base.EnterTypeCast(ctx);
    }

    public override void LeaveTypeCast(TypeCast ctx)
    {
        base.LeaveTypeCast(ctx);
    }

    public override void EnterReturnStatement(ReturnStatement ctx)
    {
        base.EnterReturnStatement(ctx);
    }

    public override void LeaveReturnStatement(ReturnStatement ctx)
    {
        base.LeaveReturnStatement(ctx);
    }

    public override void EnterStatementList(StatementList ctx)
    {
        base.EnterStatementList(ctx);
    }

    public override void LeaveStatementList(StatementList ctx)
    {
        base.LeaveStatementList(ctx);
    }

    public override void EnterAbsoluteIri(AbsoluteIri ctx)
    {
        base.EnterAbsoluteIri(ctx);
    }

    public override void LeaveAbsoluteIri(AbsoluteIri ctx)
    {
        base.LeaveAbsoluteIri(ctx);
    }

    public override void EnterAliasDeclaration(AliasDeclaration ctx)
    {
        base.EnterAliasDeclaration(ctx);
    }

    public override void LeaveAliasDeclaration(AliasDeclaration ctx)
    {
        base.LeaveAliasDeclaration(ctx);
    }

    public override void EnterAssignmentStmt(AssignmentStmt ctx)
    {
        base.EnterAssignmentStmt(ctx);
    }

    public override void LeaveAssignmentStmt(AssignmentStmt ctx)
    {
        base.LeaveAssignmentStmt(ctx);
    }

    public override void EnterBinaryExpression(BinaryExpression ctx)
    {
        base.EnterBinaryExpression(ctx);
    }

    public override void LeaveBinaryExpression(BinaryExpression ctx)
    {
        base.LeaveBinaryExpression(ctx);
    }

    public override void EnterBlock(Block ctx)
    {
        base.EnterBlock(ctx);
    }

    public override void LeaveBlock(Block ctx)
    {
        base.LeaveBlock(ctx);
    }

    public override void EnterBoolValueExpression(BoolValueExpression ctx)
    {
        base.EnterBoolValueExpression(ctx);
    }

    public override void LeaveBoolValueExpression(BoolValueExpression ctx)
    {
        base.LeaveBoolValueExpression(ctx);
    }

    public override void EnterShortValueExpression(ShortValueExpression ctx)
    {
        base.EnterShortValueExpression(ctx);
    }

    public override void LeaveShortValueExpression(ShortValueExpression ctx)
    {
        base.LeaveShortValueExpression(ctx);
    }

    public override void EnterIntValueExpression(IntValueExpression ctx)
    {
        base.EnterIntValueExpression(ctx);
    }

    public override void LeaveIntValueExpression(IntValueExpression ctx)
    {
        base.LeaveIntValueExpression(ctx);
    }

    public override void EnterLongValueExpression(LongValueExpression ctx)
    {
        base.EnterLongValueExpression(ctx);
    }

    public override void LeaveLongValueExpression(LongValueExpression ctx)
    {
        base.LeaveLongValueExpression(ctx);
    }

    public override void EnterFloatValueExpression(FloatValueExpression ctx)
    {
        base.EnterFloatValueExpression(ctx);
    }

    public override void LeaveFloatValueExpression(FloatValueExpression ctx)
    {
        base.LeaveFloatValueExpression(ctx);
    }

    public override void EnterDoubleValueExpression(DoubleValueExpression ctx)
    {
        base.EnterDoubleValueExpression(ctx);
    }

    public override void LeaveDoubleValueExpression(DoubleValueExpression ctx)
    {
        base.LeaveDoubleValueExpression(ctx);
    }

    public override void EnterDecimalValueExpression(DecimalValueExpression ctx)
    {
        base.EnterDecimalValueExpression(ctx);
    }

    public override void LeaveDecimalValueExpression(DecimalValueExpression ctx)
    {
        base.LeaveDecimalValueExpression(ctx);
    }

    public override void EnterStringValueExpression(StringValueExpression ctx)
    {
        base.EnterStringValueExpression(ctx);
    }

    public override void LeaveStringValueExpression(StringValueExpression ctx)
    {
        base.LeaveStringValueExpression(ctx);
    }

    public override void EnterDateValueExpression(DateValueExpression ctx)
    {
        base.EnterDateValueExpression(ctx);
    }

    public override void LeaveDateValueExpression(DateValueExpression ctx)
    {
        base.LeaveDateValueExpression(ctx);
    }

    public override void EnterExpressionList(ExpressionList ctx)
    {
        base.EnterExpressionList(ctx);
    }

    public override void LeaveExpressionList(ExpressionList ctx)
    {
        base.LeaveExpressionList(ctx);
    }

    public override void EnterFifthProgram(FifthProgram ctx)
    {
        ProgramBuilders.Push(ProgramBuilder.Create());
        ProgramBuilders.Current.WithAsmFileName(ctx.TargetAssemblyFileName);
    }

    public override void LeaveFifthProgram(FifthProgram ctx)
    {
        var sb = new StringBuilder();
        var x = ProgramBuilders.Pop();
        sb.AppendLine(x.Build());
    }

    public override void EnterFuncCallExpression(FuncCallExpression ctx)
    {
        base.EnterFuncCallExpression(ctx);
    }

    public override void LeaveFuncCallExpression(FuncCallExpression ctx)
    {
        base.LeaveFuncCallExpression(ctx);
    }

    public override void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
    {
        base.EnterBuiltinFunctionDefinition(ctx);
    }

    public override void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
    {
        base.LeaveBuiltinFunctionDefinition(ctx);
    }

    public override void EnterFunctionDefinition(FunctionDefinition ctx)
    {
        base.EnterFunctionDefinition(ctx);
    }

    public override void LeaveFunctionDefinition(FunctionDefinition ctx)
    {
        base.LeaveFunctionDefinition(ctx);
    }

    public override void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
    {
        base.EnterOverloadedFunctionDefinition(ctx);
    }

    public override void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
    {
        base.LeaveOverloadedFunctionDefinition(ctx);
    }

    public override void EnterIdentifier(Identifier ctx)
    {
        base.EnterIdentifier(ctx);
    }

    public override void LeaveIdentifier(Identifier ctx)
    {
        base.LeaveIdentifier(ctx);
    }

    public override void EnterIdentifierExpression(IdentifierExpression ctx)
    {
        base.EnterIdentifierExpression(ctx);
    }

    public override void LeaveIdentifierExpression(IdentifierExpression ctx)
    {
        base.LeaveIdentifierExpression(ctx);
    }

    public override void EnterIfElseStatement(IfElseStatement ctx)
    {
        base.EnterIfElseStatement(ctx);
    }

    public override void LeaveIfElseStatement(IfElseStatement ctx)
    {
        base.LeaveIfElseStatement(ctx);
    }

    public override void EnterModuleImport(ModuleImport ctx)
    {
        base.EnterModuleImport(ctx);
    }

    public override void LeaveModuleImport(ModuleImport ctx)
    {
        base.LeaveModuleImport(ctx);
    }

    public override void EnterParameterDeclarationList(ParameterDeclarationList ctx)
    {
        base.EnterParameterDeclarationList(ctx);
    }

    public override void LeaveParameterDeclarationList(ParameterDeclarationList ctx)
    {
        base.LeaveParameterDeclarationList(ctx);
    }

    public override void EnterParameterDeclaration(ParameterDeclaration ctx)
    {
        base.EnterParameterDeclaration(ctx);
    }

    public override void LeaveParameterDeclaration(ParameterDeclaration ctx)
    {
        base.LeaveParameterDeclaration(ctx);
    }

    public override void EnterDestructuringDeclaration(DestructuringDeclaration ctx)
    {
        base.EnterDestructuringDeclaration(ctx);
    }

    public override void LeaveDestructuringDeclaration(DestructuringDeclaration ctx)
    {
        base.LeaveDestructuringDeclaration(ctx);
    }

    public override void EnterDestructuringBinding(DestructuringBinding ctx)
    {
        base.EnterDestructuringBinding(ctx);
    }

    public override void LeaveDestructuringBinding(DestructuringBinding ctx)
    {
        base.LeaveDestructuringBinding(ctx);
    }

    public override void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx)
    {
        base.EnterTypeCreateInstExpression(ctx);
    }

    public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
    {
        base.LeaveTypeCreateInstExpression(ctx);
    }

    public override void EnterTypeInitialiser(TypeInitialiser ctx)
    {
        base.EnterTypeInitialiser(ctx);
    }

    public override void LeaveTypeInitialiser(TypeInitialiser ctx)
    {
        base.LeaveTypeInitialiser(ctx);
    }

    public override void EnterTypePropertyInit(TypePropertyInit ctx)
    {
        base.EnterTypePropertyInit(ctx);
    }

    public override void LeaveTypePropertyInit(TypePropertyInit ctx)
    {
        base.LeaveTypePropertyInit(ctx);
    }

    public override void EnterUnaryExpression(UnaryExpression ctx)
    {
        base.EnterUnaryExpression(ctx);
    }

    public override void LeaveUnaryExpression(UnaryExpression ctx)
    {
        base.LeaveUnaryExpression(ctx);
    }

    public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx)
    {
        base.EnterVariableDeclarationStatement(ctx);
    }

    public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx)
    {
        base.LeaveVariableDeclarationStatement(ctx);
    }

    public override void EnterVariableReference(VariableReference ctx)
    {
        base.EnterVariableReference(ctx);
    }

    public override void LeaveVariableReference(VariableReference ctx)
    {
        base.LeaveVariableReference(ctx);
    }

    public override void EnterCompoundVariableReference(CompoundVariableReference ctx)
    {
        base.EnterCompoundVariableReference(ctx);
    }

    public override void LeaveCompoundVariableReference(CompoundVariableReference ctx)
    {
        base.LeaveCompoundVariableReference(ctx);
    }

    public override void EnterWhileExp(WhileExp ctx)
    {
        base.EnterWhileExp(ctx);
    }

    public override void LeaveWhileExp(WhileExp ctx)
    {
        base.LeaveWhileExp(ctx);
    }

    public override void EnterExpressionStatement(ExpressionStatement ctx)
    {
        base.EnterExpressionStatement(ctx);
    }

    public override void LeaveExpressionStatement(ExpressionStatement ctx)
    {
        base.LeaveExpressionStatement(ctx);
    }

    public override void EnterExpression(Expression ctx)
    {
        base.EnterExpression(ctx);
    }

    public override void LeaveExpression(Expression ctx)
    {
        base.LeaveExpression(ctx);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
