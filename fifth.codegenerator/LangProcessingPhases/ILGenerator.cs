namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Linq;
using System.Text;
using AST;
using AST.Builders;
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
using fifth.metamodel.metadata.il;
using PrimitiveTypes;
using TypeSystem;
using TypeSystem.PrimitiveTypes;
using BlockBuilder = IL.BlockBuilder;
using Statement = fifth.metamodel.metadata.il.Statement;
using CILMM = fifth.metamodel.metadata.il;

public class ILGenerator : BaseAstVisitor
{
    private CurrentStack<AssemblyDeclarationBuilder> AssemblyBuilders = new();
    private CurrentStack<AssemblyReferenceBuilder> AssemblyRefBuilders = new();
    private CurrentStack<BlockBuilder> BlockBuilders = new();
    private CurrentStack<IL.ClassDefinitionBuilder> ClassBuilders = new();
    private CurrentStack<IBuilder<CILMM.Expression>> ExpressionBuilders = new();
    private CurrentStack<IL.FieldDefinitionBuilder> FieldBuilders = new();
    private CurrentStack<MethodDefinitionBuilder> MethodBuilders = new();
    private CurrentStack<ProgramDefinitionBuilder> ProgramBuilders = new();
    private CurrentStack<IL.PropertyDefinitionBuilder> PropertyBuilders = new();
    private CurrentStack<IBuilder<Statement>> StatementBuilders = new();
    public StringBuilder sb { get; set; } = new();

    public override void EnterAssembly(Assembly ctx)
    {
        AssemblyBuilders.Push(AssemblyDeclarationBuilder.Create());
        AssemblyBuilders.Current.WithName(ctx.Name)
                        .WithVersion(new Version(ctx.Version))
                        .New();
    }

    public override void LeaveAssembly(Assembly ctx) => Emit(AssemblyBuilders.Pop());

    public override void EnterAssemblyRef(AssemblyRef ctx)
    {
        AssemblyRefBuilders.Push(AssemblyReferenceBuilder.Create());
        AssemblyRefBuilders.Current.WithName(ctx.Name)
                           .WithVersion(new Version(ctx.Version))
                           .WithPublicKeyToken(ctx.PublicKeyToken)
                           .New();
    }

    public override void LeaveAssemblyRef(AssemblyRef ctx) => Emit(AssemblyRefBuilders.Pop());
    public override void EnterClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Push(IL.ClassDefinitionBuilder.Create());
        ClassBuilders.Current.WithName(ctx.Name);
    }

    public override void LeaveClassDefinition(ClassDefinition ctx)
    {
        var ab = ClassBuilders.Pop();
        ProgramBuilders.Current.AddingItemToClasses(ab.New());
    }

    public override void EnterFieldDefinition(FieldDefinition ctx)
    {
        FieldBuilders.Push(IL.FieldDefinitionBuilder.Create());
        FieldBuilders.Current.WithName(ctx.Name)
                     .WithTypeName(ctx.TypeName)
                     .WithVisibility(ILVisibility.Public);
    }

    public override void LeaveFieldDefinition(FieldDefinition ctx) =>
        ClassBuilders.Current.AddingItemToFields(FieldBuilders.Pop().New());

    public override void EnterPropertyDefinition(PropertyDefinition ctx)
    {
        PropertyBuilders.Push(IL.PropertyDefinitionBuilder.Create());
        PropertyBuilders.Current.WithName(ctx.Name)
                        .WithTypeName(ctx.TypeName)
                        .WithVisibility(ILVisibility.Public);
    }

    public override void LeavePropertyDefinition(PropertyDefinition ctx) =>
        ClassBuilders.Current.AddingItemToProperties(PropertyBuilders.Pop().New());

    public override void EnterTypeCast(TypeCast ctx) =>
        ExpressionBuilders.Push(ExpressionBuilderFactory.Create<TypeCastExpression>());

    public override void LeaveTypeCast(TypeCast ctx)
    {
        var tc = ExpressionBuilders.Pop().New();
        if (!ExpressionBuilders.Empty)
        {
            // if there is an expression being built. Add to it?
        }
        else
        {
        }
    }

    public override void EnterReturnStatement(ReturnStatement ctx) =>
        StatementBuilders.Push(StatementBuilderFactory.Create<CILMM.ReturnStatement>());

    public override void LeaveReturnStatement(ReturnStatement ctx) =>
        BlockBuilders.Current.AddingItemToStatements(StatementBuilders.Pop().New());

    public override void EnterStatementList(StatementList ctx) => base.EnterStatementList(ctx);

    public override void LeaveStatementList(StatementList ctx) => base.LeaveStatementList(ctx);

    public override void EnterAbsoluteIri(AbsoluteIri ctx) => base.EnterAbsoluteIri(ctx);

    public override void LeaveAbsoluteIri(AbsoluteIri ctx) => base.LeaveAbsoluteIri(ctx);

    public override void EnterAliasDeclaration(AliasDeclaration ctx) => base.EnterAliasDeclaration(ctx);

    public override void LeaveAliasDeclaration(AliasDeclaration ctx) => base.LeaveAliasDeclaration(ctx);

    public override void EnterAssignmentStmt(AssignmentStmt ctx) => base.EnterAssignmentStmt(ctx);

    public override void LeaveAssignmentStmt(AssignmentStmt ctx) => base.LeaveAssignmentStmt(ctx);

    public override void EnterBinaryExpression(BinaryExpression ctx) => base.EnterBinaryExpression(ctx);

    public override void LeaveBinaryExpression(BinaryExpression ctx) => base.LeaveBinaryExpression(ctx);

    public override void EnterBlock(AST.Block ctx) => BlockBuilders.Push(BlockBuilder.Create());

    public override void LeaveBlock(AST.Block ctx) =>
        // this is going to be tricky - where should the block end up?
        // assuming a body for now, but what about loop blocks or if/else statements
        MethodBuilders.Current.WithBody(BlockBuilders.Pop().New());

    public override void EnterBoolValueExpression(BoolValueExpression ctx) => base.EnterBoolValueExpression(ctx);

    public override void LeaveBoolValueExpression(BoolValueExpression ctx) => base.LeaveBoolValueExpression(ctx);

    public override void EnterShortValueExpression(ShortValueExpression ctx) => base.EnterShortValueExpression(ctx);

    public override void LeaveShortValueExpression(ShortValueExpression ctx) => base.LeaveShortValueExpression(ctx);

    public override void EnterIntValueExpression(IntValueExpression ctx) => base.EnterIntValueExpression(ctx);

    public override void LeaveIntValueExpression(IntValueExpression ctx) => base.LeaveIntValueExpression(ctx);

    public override void EnterLongValueExpression(LongValueExpression ctx) => base.EnterLongValueExpression(ctx);

    public override void LeaveLongValueExpression(LongValueExpression ctx) => base.LeaveLongValueExpression(ctx);

    public override void EnterFloatValueExpression(FloatValueExpression ctx) => base.EnterFloatValueExpression(ctx);

    public override void LeaveFloatValueExpression(FloatValueExpression ctx) => base.LeaveFloatValueExpression(ctx);

    public override void EnterDoubleValueExpression(DoubleValueExpression ctx) => base.EnterDoubleValueExpression(ctx);

    public override void LeaveDoubleValueExpression(DoubleValueExpression ctx) => base.LeaveDoubleValueExpression(ctx);

    public override void EnterDecimalValueExpression(DecimalValueExpression ctx) =>
        base.EnterDecimalValueExpression(ctx);

    public override void LeaveDecimalValueExpression(DecimalValueExpression ctx) =>
        base.LeaveDecimalValueExpression(ctx);

    public override void EnterStringValueExpression(StringValueExpression ctx) => base.EnterStringValueExpression(ctx);

    public override void LeaveStringValueExpression(StringValueExpression ctx) => base.LeaveStringValueExpression(ctx);

    public override void EnterDateValueExpression(DateValueExpression ctx) => base.EnterDateValueExpression(ctx);

    public override void LeaveDateValueExpression(DateValueExpression ctx) => base.LeaveDateValueExpression(ctx);

    public override void EnterExpressionList(ExpressionList ctx) => base.EnterExpressionList(ctx);

    public override void LeaveExpressionList(ExpressionList ctx) => base.LeaveExpressionList(ctx);

    public override void EnterFifthProgram(FifthProgram ctx)
    {
        ProgramBuilders.Push(ProgramDefinitionBuilder.Create());
        ProgramBuilders.Current.WithTargetAsmFileName(ctx.TargetAssemblyFileName);
    }

    public override void LeaveFifthProgram(FifthProgram ctx)
    {
        var sb = new StringBuilder();
        var x = ProgramBuilders.Pop();
        sb.AppendLine(x.Build());
    }

    public override void EnterFuncCallExpression(FuncCallExpression ctx) => base.EnterFuncCallExpression(ctx);

    public override void LeaveFuncCallExpression(FuncCallExpression ctx) => base.LeaveFuncCallExpression(ctx);


    public override void EnterFunctionDefinition(FunctionDefinition ctx)
    {
        MethodBuilders.Push(MethodDefinitionBuilder.Create());
        MethodBuilders.Current
                      .WithName(ctx.Name)
                      .WithVisibility(ILVisibility.Public)
                      .WithReturnType(ctx.Typename);
    }

    public override void LeaveFunctionDefinition(FunctionDefinition ctx)
    {
        var mb = MethodBuilders.Pop().New();
        if (ctx.ParentNode is FifthProgram p)
        {
            ProgramBuilders.Current.AddingItemToFunctions(mb);
        }
        else
        {
            ClassBuilders.Current.AddingItemToMethods(mb);
        }
    }

    public override void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx) =>
        base.EnterOverloadedFunctionDefinition(ctx);

    public override void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx) =>
        base.LeaveOverloadedFunctionDefinition(ctx);

    public override void EnterIdentifier(Identifier ctx) => base.EnterIdentifier(ctx);

    public override void LeaveIdentifier(Identifier ctx) => base.LeaveIdentifier(ctx);

    public override void EnterIdentifierExpression(IdentifierExpression ctx) => base.EnterIdentifierExpression(ctx);

    public override void LeaveIdentifierExpression(IdentifierExpression ctx) => base.LeaveIdentifierExpression(ctx);

    public override void EnterIfElseStatement(IfElseStatement ctx) => base.EnterIfElseStatement(ctx);

    public override void LeaveIfElseStatement(IfElseStatement ctx) => base.LeaveIfElseStatement(ctx);

    public override void EnterModuleImport(ModuleImport ctx) => base.EnterModuleImport(ctx);

    public override void LeaveModuleImport(ModuleImport ctx) => base.LeaveModuleImport(ctx);

    public override void EnterParameterDeclarationList(ParameterDeclarationList ctx) =>
        base.EnterParameterDeclarationList(ctx);

    public override void LeaveParameterDeclarationList(ParameterDeclarationList ctx) =>
        base.LeaveParameterDeclarationList(ctx);

    public override void EnterParameterDeclaration(ParameterDeclaration ctx) => base.EnterParameterDeclaration(ctx);

    public override void LeaveParameterDeclaration(ParameterDeclaration ctx) => base.LeaveParameterDeclaration(ctx);

    public override void EnterDestructuringDeclaration(DestructuringDeclaration ctx) =>
        base.EnterDestructuringDeclaration(ctx);

    public override void LeaveDestructuringDeclaration(DestructuringDeclaration ctx) =>
        base.LeaveDestructuringDeclaration(ctx);

    public override void EnterDestructuringBinding(DestructuringBinding ctx) => base.EnterDestructuringBinding(ctx);

    public override void LeaveDestructuringBinding(DestructuringBinding ctx) => base.LeaveDestructuringBinding(ctx);

    public override void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx) =>
        base.EnterTypeCreateInstExpression(ctx);

    public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx) =>
        base.LeaveTypeCreateInstExpression(ctx);

    public override void EnterTypeInitialiser(TypeInitialiser ctx) => base.EnterTypeInitialiser(ctx);

    public override void LeaveTypeInitialiser(TypeInitialiser ctx) => base.LeaveTypeInitialiser(ctx);

    public override void EnterTypePropertyInit(TypePropertyInit ctx) => base.EnterTypePropertyInit(ctx);

    public override void LeaveTypePropertyInit(TypePropertyInit ctx) => base.LeaveTypePropertyInit(ctx);

    public override void EnterUnaryExpression(UnaryExpression ctx) => base.EnterUnaryExpression(ctx);

    public override void LeaveUnaryExpression(UnaryExpression ctx) => base.LeaveUnaryExpression(ctx);

    public override void EnterVariableDeclarationStatement(AST.VariableDeclarationStatement ctx) =>
        base.EnterVariableDeclarationStatement(ctx);

    public override void LeaveVariableDeclarationStatement(AST.VariableDeclarationStatement ctx) =>
        base.LeaveVariableDeclarationStatement(ctx);

    public override void EnterVariableReference(VariableReference ctx) => base.EnterVariableReference(ctx);

    public override void LeaveVariableReference(VariableReference ctx) => base.LeaveVariableReference(ctx);


    public override void EnterWhileExp(WhileExp ctx) => base.EnterWhileExp(ctx);

    public override void LeaveWhileExp(WhileExp ctx) => base.LeaveWhileExp(ctx);

    public override void EnterExpressionStatement(AST.ExpressionStatement ctx) => base.EnterExpressionStatement(ctx);

    public override void LeaveExpressionStatement(AST.ExpressionStatement ctx) => base.LeaveExpressionStatement(ctx);

    public override void EnterExpression(Expression ctx) => base.EnterExpression(ctx);

    public override void LeaveExpression(Expression ctx) => base.LeaveExpression(ctx);

    public override bool Equals(object obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => base.ToString();
}
