namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using System.Linq;
using AST;
using AST.Visitors;
using fifth.metamodel.metadata;
using fifth.metamodel.metadata.il;
using IL;
using TypeSystem;
using Block = AST.Block;
using ClassDefinition = AST.ClassDefinition;
using ExpressionStatement = AST.ExpressionStatement;
using FieldDefinition = AST.FieldDefinition;
using MemberAccessExpression = AST.MemberAccessExpression;
using ParameterDeclaration = AST.ParameterDeclaration;
using PropertyDefinition = AST.PropertyDefinition;
using ReturnStatement = AST.ReturnStatement;
using Statement = fifth.metamodel.metadata.il.Statement;
using VariableDeclarationStatement = AST.VariableDeclarationStatement;

/// <summary>
///     A visitor responsible for translating the AST into an IL-oriented AST specific to IL code generation.
/// </summary>
public class ILModelBuilder : DefaultRecursiveDescentVisitor
{
    // provide a place to place type refs generated from TypeIds (like a symbol table)
    private readonly Dictionary<TypeId, TypeReference> TypeLookups = new();

    private CurrentStack<AssemblyDeclarationBuilder> AssemblyBuilders = new();
    private CurrentStack<ModuleDeclarationBuilder> ModuleDeclarations = new();
    private CurrentStack<AssemblyReferenceBuilder> AssemblyRefBuilders = new();
    private CurrentStack<BlockBuilder> BlockBuilders = new();
    private CurrentStack<ClassDefinitionBuilder> ClassBuilders = new();
    private CurrentStack<IBuilder<fifth.metamodel.metadata.il.Expression>> ExpressionBuilders = new();
    private CurrentStack<FieldDefinitionBuilder> FieldBuilders = new();
    private CurrentStack<MethodDefinitionBuilder> MethodBuilders = new();
    private CurrentStack<MethodSignatureBuilder> MethodSignatureBuilders = new();
    private CurrentStack<PropertyDefinitionBuilder> PropertyBuilders = new();
    private CurrentStack<IBuilder<Statement>> StatementBuilders = new();
    public List<AssemblyDeclaration> CompletedAssemblies { get; set; } = new();


    private TypeReference GetTypeRef(TypeId t)
    {
        if (TypeLookups.TryGetValue(t, out var typeRef))
        {
            return typeRef;
        }

        var returnIType = t.Lookup();
        var result = TypeReferenceBuilder.Create()
                                         .WithModuleName(ModuleDeclarations.Current.Model.FileName)
                                         .WithNamespace(returnIType.Namespace)
                                         .WithName(returnIType.Name)
                                         .New();
        TypeLookups[t] = result;
        return result;
    }

    public override FifthProgram VisitFifthProgram(FifthProgram ctx)
    {
        string defaultAssemblyFileName = "out";

        AssemblyBuilders.Push(AssemblyDeclarationBuilder.Create());
        AssemblyBuilders.Current
            .WithVersion(new Version(1,0,0,0));
        ModuleDeclarations.Push(ModuleDeclarationBuilder.Create());

        ModuleDeclarations.Current.WithFileName(ctx.TargetAssemblyFileName ?? defaultAssemblyFileName);

        foreach (var @class in ctx.Classes)
        {
            Visit(@class);
        }

        ClassBuilders.Push(ClassDefinitionBuilder.Create().WithName("Program"));
        foreach (var function in ctx.Functions)
        {
            VisitFunctionDefinition(function as FunctionDefinition);
        }
        ModuleDeclarations.Current.AddingItemToClasses(ClassBuilders.Pop().New());

        var m = ModuleDeclarations.Pop().New();
        AssemblyBuilders.Current.WithPrimeModule(m);
        return ctx;
    }

    public override Assembly VisitAssembly(Assembly ctx)
    {
        VisitFifthProgram(ctx.Program);
        CompletedAssemblies.Add(AssemblyBuilders.Pop().New());
        return ctx;
    }

    public override ClassDefinition VisitClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Push(ClassDefinitionBuilder.Create());
        ClassBuilders.Current
                     .WithName(ctx.Name)
                     .WithVisibility(MemberAccessability.Public);
        foreach (var f in ctx.Fields)
        {
            Visit(f);
        }

        foreach (var p in ctx.Properties)
        {
            Visit(p);
        }

        foreach (var fn in ctx.Functions)
        {
            Visit(fn as FunctionDefinition);
        }

        ModuleDeclarations.Current.AddingItemToClasses(ClassBuilders.Pop().New());
        return ctx;
    }

    public override FieldDefinition VisitFieldDefinition(FieldDefinition ctx)
    {
        FieldBuilders.Push(FieldDefinitionBuilder.Create());
        FieldBuilders.Current.WithName(ctx.Name)
                     .WithTheType(GetTypeRef(ctx.TypeId))
                     .WithVisibility(MemberAccessability.Public);
        ClassBuilders.Current.AddingItemToFields(FieldBuilders.Pop().New());
        return ctx;
    }

    public override PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
    {
        PropertyBuilders.Push(PropertyDefinitionBuilder.Create());
        PropertyBuilders.Current.WithName(ctx.Name)
                        .WithParentClass(ClassBuilders.Current.Model)
                        .WithTypeName(ctx.TypeName)
                        .WithVisibility(MemberAccessability.Public);
        ClassBuilders.Current.AddingItemToProperties(PropertyBuilders.Pop().New());
        return ctx;
    }

    public override FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx)
    {
        MethodBuilders.Push(MethodDefinitionBuilder.Create());
        MethodSignatureBuilders.Push(MethodSignatureBuilder.Create());
        if (ctx.IsInstanceFunction && ctx.ParentNode is ClassDefinition)
        {
            MethodSignatureBuilders.Current.WithCallingConvention(MethodCallingConvention.Instance);
        }
        else
        {
            MethodSignatureBuilders.Current.WithCallingConvention(MethodCallingConvention.Default);
        }

        if (ctx.ReturnType is not null)
        {
            MethodSignatureBuilders.Current.WithReturnTypeSignature(GetTypeRef(ctx.ReturnType))
                   .WithNumberOfParameters((ushort)ctx.ParameterDeclarations.ParameterDeclarations.Count);
        }

        MethodBuilders.Current
                      .WithHeader(MethodHeaderBuilder.Create()
                                                     .WithFunctionKind(ctx.FunctionKind)
                                                     .WithIsEntrypoint(ctx.IsEntryPoint)
                                                     .New());

        VisitParameterDeclarationList(ctx.ParameterDeclarations);
        MethodBuilders.Current
                      .WithParentClass(ClassBuilders.Current.Model)
                      .WithSignature(MethodSignatureBuilders.Pop().New())
                      .WithName(ctx.Name)
                      .WithVisibility(MemberAccessability.Public);

        if (ctx.Body != null)
        {
            VisitBlock(ctx.Body);
            MethodBuilders.Current.WithImpl(
                MethodImplBuilder.Create()
                                 .WithBody(BlockBuilders.Pop().New())
                                 .WithImplementationFlags(ImplementationFlag.internalCall)
                                 .WithIsManaged(true)
                                 .New()
                );
        }

        if (ctx.FunctionKind == FunctionKind.Getter)
        {
            // should the IL generator need to know what the prop was?  It just needs to generate its statements.
//            MethodBuilders.Current.WithAssociatedProperty(ctx.)
        }
        var mb = MethodBuilders.Pop().New();
        ClassBuilders.Current.AddingItemToMethods(mb);
        return ctx;
    }

    public override ParameterDeclarationList VisitParameterDeclarationList(ParameterDeclarationList ctx)
    {
        foreach (var p in ctx.ParameterDeclarations)
        {
            VisitParameterDeclaration(p as ParameterDeclaration);
        }

        return ctx;
    }

    public override ParameterDeclaration VisitParameterDeclaration(ParameterDeclaration ctx)
    {
        var pd = ParameterSignatureBuilder.Create()
                                            .WithName(ctx.ParameterName.Value)
                                            .WithTypeReference(GetTypeRef(ctx.TypeId))
                                            .WithIsUDTType(ctx.TypeId.Lookup() is UserDefinedType)
                                            .New();
        // we ignore the constraints and destructurings, because by this point we should have
        // desugared them into regular functions
        MethodSignatureBuilders.Current.AddingItemToParameterSignatures(pd);
        return ctx;
    }

    public override Block VisitBlock(Block ctx)
    {
        BlockBuilders.Push(BlockBuilder.Create());
        foreach (var s in ctx.Statements)
        {
            switch(s)
            {
                case VariableDeclarationStatement vds:
                    VisitVariableDeclarationStatement(vds);
                    break;
                case AssignmentStmt ass:
                    VisitAssignmentStmt(ass);
                    break;
                case ReturnStatement rs:
                    VisitReturnStatement(rs);
                    break;
                case IfElseStatement stmt:
                    VisitIfElseStatement(stmt);
                    break;
                case WhileExp wst:
                    VisitWhileExp(wst);
                    break;
                case AST.ExpressionStatement expstmt:
                    VisitExpressionStatement(expstmt);
                    break;
                default:
                    break;
            }
            //Visit(s);
        }

        // don't insert into recipient (they must do that since only the caller knows the context
        // of this block (i.e. it could be a method body, or a while loop body or an ifelse block))
        return ctx;
    }

    #region Statement handling

    public override ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx)
    {
        BlockBuilders.Current.AddingItemToStatements(
        ExpressionStatementBuilder.Create()
                                  .WithExpression(ExpressionILBuilder.Generate(ctx.Expression))
                                  .New());
        return ctx;
    }

    public override ReturnStatement VisitReturnStatement(ReturnStatement ctx)
    {
        BlockBuilders.Current.AddingItemToStatements(
            ReturnStatementBuilder.Create()
                                  .WithExp(ExpressionILBuilder.Generate(ctx.SubExpression))
                                  .New());
        return ctx;
    }

    public override AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx)
    {
        string vr;
        int? ord = null;
        switch ((AST.Expression)ctx.VariableRef)
        {
            case VariableReference ivr:
                var ilvr = ExpressionILBuilder.Generate(ivr) as VariableReferenceExpression;
                vr = ivr.Name;
                ord = ilvr.Ordinal;
                break;

            // case CompoundVariableReference cvr:
            //     vr = cvr.ComponentReferences.Select(cr => cr.Name).Join(s => s, ".");
            //     break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ctx.VariableRef));
        }

        var builder = VariableAssignmentStatementBuilder.Create()
                                                        .WithLHS(vr)
                                                        .WithRHS(ExpressionILBuilder.Generate(ctx.Expression))
            ;
        if (ord.HasValue)
        {
            builder.WithOrdinal(ord.Value);
        }

        BlockBuilders.Current.AddingItemToStatements(builder.New());

        return ctx;
    }

    public override IfElseStatement VisitIfElseStatement(IfElseStatement ctx)
    {
        var builder = IfStatementBuilder.Create()
                                        .WithConditional(ExpressionILBuilder.Generate(ctx.Condition))
            ;
        Visit(ctx.IfBlock);
        builder.WithIfBlock(BlockBuilders.Pop().New());
        if (ctx.ElseBlock.Statements != null && ctx.ElseBlock.Statements.Count != 0)
        {
            Visit(ctx.ElseBlock);
            builder.WithElseBlock(BlockBuilders.Pop().New());
        }

        BlockBuilders.Current.AddingItemToStatements(builder.New());
        return ctx;
    }

    public override AST.VariableDeclarationStatement VisitVariableDeclarationStatement(AST.VariableDeclarationStatement ctx)
    {
        var b = VariableDeclarationStatementBuilder.Create()
                                                   .WithName(ctx.Name)
                                                   .WithTypeName(ctx.TypeName)
            ;
        if (ctx.Expression != null)
        {
            b.WithInitialisationExpression(ExpressionILBuilder.Generate(ctx.Expression));
        }

        BlockBuilders.Current.AddingItemToStatements(b.New());
        return ctx;
    }

    public override WhileExp VisitWhileExp(WhileExp ctx)
    {
        var b = WhileStatementBuilder.Create();
        b.WithConditional(ExpressionILBuilder.Generate(ctx.Condition));
        Visit(ctx.LoopBlock);
        b.WithLoopBlock(BlockBuilders.Pop().New());
        BlockBuilders.Current.AddingItemToStatements(b.New());
        return ctx;
    }

    #endregion
}
