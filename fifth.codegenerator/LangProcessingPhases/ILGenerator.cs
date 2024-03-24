namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using System.Text;
using AST;
using AST.Visitors;
using fifth.metamodel.metadata;
using IL;
using CILMM = fifth.metamodel.metadata.il;

[Obsolete]
public class ILGenerator : BaseAstVisitor
{
    private readonly CurrentStack<AssemblyDeclarationBuilder> assemblyBuilders = new();

    private readonly CurrentStack<AssemblyReferenceBuilder> AssemblyRefBuilders = new();
    private readonly CurrentStack<BlockBuilder> BlockBuilders = new();
    private readonly CurrentStack<ClassDefinitionBuilder> ClassBuilders = new();
    private readonly CurrentStack<IBuilder<CILMM.Expression>> ExpressionBuilders = new();
    private readonly CurrentStack<FieldDefinitionBuilder> FieldBuilders = new();
    private readonly CurrentStack<MethodDefinitionBuilder> MethodBuilders = new();
    private readonly CurrentStack<ModuleDeclarationBuilder> ModuleDeclarationBuilders = new();
    private readonly CurrentStack<PropertyDefinitionBuilder> PropertyBuilders = new();
    private readonly CurrentStack<IBuilder<CILMM.Statement>> StatementBuilders = new();
    public StringBuilder sb { get; set; } = new();

    public override void EnterAssembly(Assembly ctx)
    {
        assemblyBuilders.Push(AssemblyDeclarationBuilder.Create());
        assemblyBuilders.Current.WithName(ctx.Name)
                        .WithVersion(new CILMM.Version(ctx.Version))
                        .New();
    }

    public override void LeaveAssembly(Assembly ctx) { } //=> Emit(AssemblyBuilders.Pop());

    public override void EnterAssemblyRef(AssemblyRef ctx)
    {
        AssemblyRefBuilders.Push(AssemblyReferenceBuilder.Create());
        AssemblyRefBuilders.Current.WithName(ctx.Name)
                           .WithVersion(new CILMM.Version(ctx.Version))
                           .WithPublicKeyToken(ctx.PublicKeyToken)
                           .New();
    }

    public override void LeaveAssemblyRef(AssemblyRef ctx) { } // => Emit(AssemblyRefBuilders.Pop());

    public override void EnterClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Push(ClassDefinitionBuilder.Create());
        ClassBuilders.Current.WithName(ctx.Name);
    }

    public override void LeaveClassDefinition(ClassDefinition ctx)
    {
        var ab = ClassBuilders.Pop();
        ModuleDeclarationBuilders.Current.AddingItemToClasses(ab.New());
    }

    public override void EnterFieldDefinition(FieldDefinition ctx)
    {
        FieldBuilders.Push(FieldDefinitionBuilder.Create());
        FieldBuilders.Current.WithName(ctx.Name)
                     .WithTheType(GetTypeRef(ctx.TypeId))
                     .WithVisibility(MemberAccessability.Public);
    }

    public override void LeaveFieldDefinition(FieldDefinition ctx)
    {
        ClassBuilders.Current.AddingItemToFields(FieldBuilders.Pop().New());
    }

    public override void EnterPropertyDefinition(PropertyDefinition ctx)
    {
        PropertyBuilders.Push(PropertyDefinitionBuilder.Create());
        PropertyBuilders.Current.WithName(ctx.Name)
                        .WithTypeName(ctx.TypeName)
                        .WithVisibility(MemberAccessability.Public);
    }

    public override void LeavePropertyDefinition(PropertyDefinition ctx)
    {
        ClassBuilders.Current.AddingItemToProperties(PropertyBuilders.Pop().New());
    }

    public override void EnterTypeCast(TypeCast ctx)
    {
        ExpressionBuilders.Push(ExpressionBuilderFactory.Create<TypeCastExpression>());
    }

    public override void LeaveTypeCast(TypeCast ctx)
    {
        var tc = ExpressionBuilders.Pop().New();
        if (!ExpressionBuilders.Empty)
        {
            // if there is an expression being built. Add to it?
        }
    }

    public override void EnterReturnStatement(ReturnStatement ctx)
    {
        StatementBuilders.Push(StatementBuilderFactory.Create<CILMM.ReturnStatement>());
    }

    public override void LeaveReturnStatement(ReturnStatement ctx)
    {
        BlockBuilders.Current.AddingItemToStatements(StatementBuilders.Pop().New());
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
        BlockBuilders.Push(BlockBuilder.Create());
    }

    public override void LeaveBlock(Block ctx)
    {
        // this is going to be tricky - where should the block end up?
        // assuming a body for now, but what about loop blocks or if/else statements
        MethodBuilders.Current.WithImpl(
            MethodImplBuilder.Create()
                             .WithBody(BlockBuilders.Pop().New())
                             .WithImplementationFlags(ImplementationFlag.internalCall)
                             .WithIsManaged(true)
                             .New()
        );
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
        ModuleDeclarationBuilders.Push(ModuleDeclarationBuilder.Create());
        ModuleDeclarationBuilders.Current.WithFileName(ctx.TargetAssemblyFileName);
    }

    public override void LeaveFifthProgram(FifthProgram ctx)
    {
        var sb = new StringBuilder();
        var x = ModuleDeclarationBuilders.Pop();
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
    private readonly Dictionary<TypeId, TypeReference> TypeLookups = new();

    private TypeReference GetTypeRef(TypeId t)
    {
        if (TypeLookups.TryGetValue(t, out var typeRef))
        {
            return typeRef;
        }

        var returnIType = t.Lookup();
        var result = TypeReferenceBuilder.Create()
                                         .WithModuleName(ModuleDeclarationBuilders.Current.Model.FileName)
                                         //.WithNamespace(returnIType.Namespace)
                                         .WithName(returnIType.Name)
                                         .New();
        TypeLookups[t] = result;
        return result;
    }

    public override void EnterFunctionDefinition(FunctionDefinition ctx)
    {
        var sigbldr = MethodSignatureBuilder.Create();
        if (ctx.ParentNode is ClassDefinition)
        {
            sigbldr.WithCallingConvention(MethodCallingConvention.Instance);
        }
        else
        {
            sigbldr.WithCallingConvention(MethodCallingConvention.Default);
        }

        sigbldr.WithReturnTypeSignature(GetTypeRef(ctx.ReturnType.Value))
               .WithNumberOfParameters((ushort)ctx.ParameterDeclarations.ParameterDeclarations.Count);
        foreach (var pd in ctx.ParameterDeclarations.ParameterDeclarations)
        {
        }
        MethodBuilders.Push(MethodDefinitionBuilder.Create());
        MethodBuilders.Current
                      .WithSignature(sigbldr.New());
    }

    public override void LeaveFunctionDefinition(FunctionDefinition ctx)
    {
        var mb = MethodBuilders.Pop().New();
        if (ctx.ParentNode is FifthProgram p)
        {
            ModuleDeclarationBuilders.Current.AddingItemToFunctions(mb);
        }
        else
        {
            ClassBuilders.Current.AddingItemToMethods(mb);
        }
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
