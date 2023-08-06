namespace Fifth.CodeGeneration.LangProcessingPhases;

#region

using System.Collections.Generic;
using AST;
using AST.Visitors;
using fifth.metamodel.metadata;
using IL;
using Version = fifth.metamodel.metadata.il.Version;

#endregion

/// <summary>
///     A visitor responsible for translating the AST into an IL-oriented AST specific to IL code generation.
/// </summary>
// ReSharper disable once InconsistentNaming
public class ILModelBuilder : DefaultRecursiveDescentVisitor
{
    // provide a place to place type refs generated from TypeIds (like a symbol table)
    private readonly Dictionary<TypeId, TypeReference> typeLookups = new();

    private readonly CurrentStack<AssemblyDeclarationBuilder> assemblyBuilders = new();
    private CurrentStack<AssemblyReferenceBuilder> assemblyRefBuilders = new();
    private readonly CurrentStack<BlockBuilder> blockBuilders = new();
    private readonly CurrentStack<ClassDefinitionBuilder> classBuilders = new();
    private CurrentStack<IBuilder<Expression>> expressionBuilders = new();
    private readonly CurrentStack<FieldDefinitionBuilder> fieldBuilders = new();
    private readonly CurrentStack<MethodDefinitionBuilder> methodBuilders = new();
    private readonly CurrentStack<MethodSignatureBuilder> methodSignatureBuilders = new();
    private readonly CurrentStack<ModuleDeclarationBuilder> moduleDeclarations = new();
    private readonly CurrentStack<PropertyDefinitionBuilder> propertyBuilders = new();
    private CurrentStack<IBuilder<Statement>> statementBuilders = new();
    public List<AssemblyDeclaration> CompletedAssemblies { get; set; } = new();
    public Stack<fifth.metamodel.metadata.il.Expression> CompletedExpressions  { get; set; } = new();

    private TypeReference GetTypeRef(TypeId t)
    {
        if (typeLookups.TryGetValue(t, out var typeRef))
        {
            return typeRef;
        }

        var returnIType = t.Lookup();
        var result = TypeReferenceBuilder.Create()
                                         .WithModuleName(moduleDeclarations.Current.Model.FileName)
                                         .WithNamespace(returnIType.Namespace)
                                         .WithName(returnIType.Name)
                                         .New();
        typeLookups[t] = result;
        return result;
    }

    public override FifthProgram VisitFifthProgram(FifthProgram ctx)
    {
        var defaultAssemblyFileName = "out";

        assemblyBuilders.Push(AssemblyDeclarationBuilder.Create());
        assemblyBuilders.Current
                        .WithVersion(new Version(1, 0, 0, 0));
        moduleDeclarations.Push(ModuleDeclarationBuilder.Create());

        moduleDeclarations.Current.WithFileName(ctx.TargetAssemblyFileName ?? defaultAssemblyFileName);

        foreach (var @class in ctx.Classes)
        {
            Visit(@class);
        }

        classBuilders.Push(ClassDefinitionBuilder.Create().WithName("Program"));
        foreach (var function in ctx.Functions)
        {
            VisitFunctionDefinition(function as FunctionDefinition);
        }

        moduleDeclarations.Current.AddingItemToClasses(classBuilders.Pop().New());

        var m = moduleDeclarations.Pop().New();
        assemblyBuilders.Current.WithPrimeModule(m);
        return ctx;
    }

    public override Assembly VisitAssembly(Assembly ctx)
    {
        assemblyBuilders.Push(AssemblyDeclarationBuilder.Create());
        foreach (var assemblyRef in ctx.References)
        {
            VisitAssemblyRef(assemblyRef);
        }
        VisitFifthProgram(ctx.Program);
        CompletedAssemblies.Add(assemblyBuilders.Pop().New());
        return ctx;
    }

    public override AssemblyRef VisitAssemblyRef(AssemblyRef ctx)
    {
        assemblyRefBuilders.Push(AssemblyReferenceBuilder.Create());
        assemblyRefBuilders.Current.WithName(ctx.Name)
                           .WithPublicKeyToken(ctx.PublicKeyToken)
                           .WithVersion(new Version(ctx.Version));
        assemblyBuilders.Current.AddingItemToAssemblyReferences(assemblyRefBuilders.Pop().New());
        return ctx;
    }

    public override ClassDefinition VisitClassDefinition(ClassDefinition ctx)
    {
        classBuilders.Push(ClassDefinitionBuilder.Create());
        classBuilders.Current
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

        moduleDeclarations.Current.AddingItemToClasses(classBuilders.Pop().New());
        return ctx;
    }

    public override FieldDefinition VisitFieldDefinition(FieldDefinition ctx)
    {
        fieldBuilders.Push(FieldDefinitionBuilder.Create());
        fieldBuilders.Current.WithName(ctx.Name)
                     .WithTheType(GetTypeRef(ctx.TypeId))
                     .WithVisibility(MemberAccessability.Public);
        classBuilders.Current.AddingItemToFields(fieldBuilders.Pop().New());
        return ctx;
    }

    public override PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
    {
        propertyBuilders.Push(PropertyDefinitionBuilder.Create());
        propertyBuilders.Current.WithName(ctx.Name)
                        .WithParentClass(classBuilders.Current.Model)
                        .WithTypeName(ctx.TypeName)
                        .WithVisibility(MemberAccessability.Public);
        classBuilders.Current.AddingItemToProperties(propertyBuilders.Pop().New());
        return ctx;
    }

    public override FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx)
    {
        methodBuilders.Push(MethodDefinitionBuilder.Create());
        methodSignatureBuilders.Push(MethodSignatureBuilder.Create());
        if (ctx.IsInstanceFunction && ctx.ParentNode is ClassDefinition)
        {
            methodSignatureBuilders.Current.WithCallingConvention(MethodCallingConvention.Instance);
        }
        else
        {
            methodSignatureBuilders.Current.WithCallingConvention(MethodCallingConvention.Default);
        }

        if (ctx.ReturnType is not null)
        {
            methodSignatureBuilders.Current.WithReturnTypeSignature(GetTypeRef(ctx.ReturnType))
                                   .WithNumberOfParameters(
                                       (ushort)ctx.ParameterDeclarations.ParameterDeclarations.Count);
        }

        methodBuilders.Current
                      .WithHeader(MethodHeaderBuilder.Create()
                                                     .WithFunctionKind(ctx.FunctionKind)
                                                     .WithIsEntrypoint(ctx.IsEntryPoint)
                                                     .New());

        VisitParameterDeclarationList(ctx.ParameterDeclarations);
        methodBuilders.Current
                      .WithParentClass(classBuilders.Current.Model)
                      .WithSignature(methodSignatureBuilders.Pop().New())
                      .WithName(ctx.Name)
                      .WithVisibility(MemberAccessability.Public);

        if (ctx.Body != null)
        {
            VisitBlock(ctx.Body);
            methodBuilders.Current.WithImpl(
                MethodImplBuilder.Create()
                                 .WithBody(blockBuilders.Pop().New())
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

        var mb = methodBuilders.Pop().New();
        classBuilders.Current.AddingItemToMethods(mb);
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
        methodSignatureBuilders.Current.AddingItemToParameterSignatures(pd);
        return ctx;
    }

    public override Block VisitBlock(Block ctx)
    {
        blockBuilders.Push(BlockBuilder.Create());
        foreach (var s in ctx.Statements)
        {
            switch (s)
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
                case ExpressionStatement expstmt:
                    VisitExpressionStatement(expstmt);
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
        blockBuilders.Current.AddingItemToStatements(
            ExpressionStatementBuilder.Create()
                                      .WithExpression(ExpressionILBuilder.Generate(ctx.Expression))
                                      .New());
        return ctx;
    }

    public override ReturnStatement VisitReturnStatement(ReturnStatement ctx)
    {
        blockBuilders.Current.AddingItemToStatements(
            ReturnStatementBuilder.Create()
                                  .WithExp(ExpressionILBuilder.Generate(ctx.SubExpression))
                                  .New());
        return ctx;
    }

    public override AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx)
    {
        string vr;
        int? ord = null;
        switch (ctx.VariableRef)
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

        blockBuilders.Current.AddingItemToStatements(builder.New());

        return ctx;
    }

    public override IfElseStatement VisitIfElseStatement(IfElseStatement ctx)
    {
        var builder = IfStatementBuilder.Create()
                                        .WithConditional(ExpressionILBuilder.Generate(ctx.Condition))
            ;
        Visit(ctx.IfBlock);
        builder.WithIfBlock(blockBuilders.Pop().New());
        if (ctx.ElseBlock.Statements != null && ctx.ElseBlock.Statements.Count != 0)
        {
            Visit(ctx.ElseBlock);
            builder.WithElseBlock(blockBuilders.Pop().New());
        }

        blockBuilders.Current.AddingItemToStatements(builder.New());
        return ctx;
    }

    public override VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx)
    {
        var b = VariableDeclarationStatementBuilder.Create()
                                                   .WithName(ctx.Name)
                                                   .WithTypeName(ctx.TypeName)
            ;
        if (ctx.Expression != null)
        {
            b.WithInitialisationExpression(ExpressionILBuilder.Generate(ctx.Expression));
        }

        blockBuilders.Current.AddingItemToStatements(b.New());
        return ctx;
    }

    public override WhileExp VisitWhileExp(WhileExp ctx)
    {
        var b = WhileStatementBuilder.Create();
        b.WithConditional(ExpressionILBuilder.Generate(ctx.Condition));
        Visit(ctx.LoopBlock);
        b.WithLoopBlock(blockBuilders.Pop().New());
        blockBuilders.Current.AddingItemToStatements(b.New());
        return ctx;
    }

    #endregion

    #region Expression Handling

    public override BinaryExpression VisitBinaryExpression(BinaryExpression ctx)
    {
        CompletedExpressions.Push(ExpressionILBuilder.Generate(ctx));
        return ctx;
    }

    public override UnaryExpression VisitUnaryExpression(UnaryExpression ctx)
    {
        CompletedExpressions.Push(ExpressionILBuilder.Generate(ctx));
        return ctx;
    }

    public override StringValueExpression VisitStringValueExpression(StringValueExpression ctx)
    {
        CompletedExpressions.Push(new StringLiteral(ctx.TheValue));
        return base.VisitStringValueExpression(ctx);
    }

    public override BoolValueExpression VisitBoolValueExpression(BoolValueExpression ctx)
    {
        CompletedExpressions.Push(new BoolLiteral(ctx.TheValue));
        return base.VisitBoolValueExpression(ctx);
    }

    public override ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx)
    {
        CompletedExpressions.Push(new ShortLiteral(ctx.TheValue));
        return base.VisitShortValueExpression(ctx);
    }

    public override IntValueExpression VisitIntValueExpression(IntValueExpression ctx)
    {
        CompletedExpressions.Push(new IntLiteral(ctx.TheValue));
        return base.VisitIntValueExpression(ctx);
    }

    public override LongValueExpression VisitLongValueExpression(LongValueExpression ctx)
    {
        CompletedExpressions.Push(new LongLiteral(ctx.TheValue));
        return base.VisitLongValueExpression(ctx);
    }

    public override FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx)
    {
        CompletedExpressions.Push(new FloatLiteral(ctx.TheValue));
        return base.VisitFloatValueExpression(ctx);
    }

    public override DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx)
    {
        CompletedExpressions.Push(new DoubleLiteral(ctx.TheValue));
        return base.VisitDoubleValueExpression(ctx);
    }

    public override DecimalValueExpression VisitDecimalValueExpression(DecimalValueExpression ctx)
    {
        CompletedExpressions.Push(new DecimalLiteral(ctx.TheValue));
        return base.VisitDecimalValueExpression(ctx);
    }

    #endregion
}
