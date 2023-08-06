
namespace Fifth.AST.Visitors;
using Fifth.AST;
using Fifth.AST.Builders;

public interface IAstMutatorVisitor<TContext>
{
    Assembly ProcessAssembly(Assembly node, TContext ctx);
    AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx);
    ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx);
    FieldDefinition ProcessFieldDefinition(FieldDefinition node, TContext ctx);
    PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx);
    TypeCast ProcessTypeCast(TypeCast node, TContext ctx);
    ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx);
    StatementList ProcessStatementList(StatementList node, TContext ctx);
    AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx);
    AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx);
    AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx);
    BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx);
    Block ProcessBlock(Block node, TContext ctx);
    BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx);
    ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx);
    IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx);
    LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx);
    FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx);
    DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx);
    DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx);
    StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx);
    DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx);
    ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx);
    FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx);
    FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx);
    FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx);
    OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx);
    Identifier ProcessIdentifier(Identifier node, TContext ctx);
    IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx);
    IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx);
    ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx);
    ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx);
    ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx);
    DestructuringDeclaration ProcessDestructuringDeclaration(DestructuringDeclaration node, TContext ctx);
    DestructuringBinding ProcessDestructuringBinding(DestructuringBinding node, TContext ctx);
    TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx);
    TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx);
    TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx);
    UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx);
    VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx);
    VariableReference ProcessVariableReference(VariableReference node, TContext ctx);
    MemberAccessExpression ProcessMemberAccessExpression(MemberAccessExpression node, TContext ctx);
    WhileExp ProcessWhileExp(WhileExp node, TContext ctx);
    ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx);
    Expression ProcessExpression(Expression node, TContext ctx);
}

public partial class NullMutatorVisitor<TContext> : IAstMutatorVisitor<TContext>
{
    public virtual Assembly ProcessAssembly(Assembly node, TContext ctx)=>node;
    public virtual AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx)=>node;
    public virtual ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx)=>node;
    public virtual FieldDefinition ProcessFieldDefinition(FieldDefinition node, TContext ctx)=>node;
    public virtual PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)=>node;
    public virtual TypeCast ProcessTypeCast(TypeCast node, TContext ctx)=>node;
    public virtual ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx)=>node;
    public virtual StatementList ProcessStatementList(StatementList node, TContext ctx)=>node;
    public virtual AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)=>node;
    public virtual AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)=>node;
    public virtual AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)=>node;
    public virtual BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx)=>node;
    public virtual Block ProcessBlock(Block node, TContext ctx)=>node;
    public virtual BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)=>node;
    public virtual ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx)=>node;
    public virtual IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx)=>node;
    public virtual LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx)=>node;
    public virtual FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)=>node;
    public virtual DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)=>node;
    public virtual DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)=>node;
    public virtual StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx)=>node;
    public virtual DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx)=>node;
    public virtual ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx)=>node;
    public virtual FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx)=>node;
    public virtual FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)=>node;
    public virtual FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)=>node;
    public virtual OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)=>node;
    public virtual Identifier ProcessIdentifier(Identifier node, TContext ctx)=>node;
    public virtual IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)=>node;
    public virtual IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx)=>node;
    public virtual ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx)=>node;
    public virtual ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)=>node;
    public virtual ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)=>node;
    public virtual DestructuringDeclaration ProcessDestructuringDeclaration(DestructuringDeclaration node, TContext ctx)=>node;
    public virtual DestructuringBinding ProcessDestructuringBinding(DestructuringBinding node, TContext ctx)=>node;
    public virtual TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)=>node;
    public virtual TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)=>node;
    public virtual TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)=>node;
    public virtual UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx)=>node;
    public virtual VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)=>node;
    public virtual VariableReference ProcessVariableReference(VariableReference node, TContext ctx)=>node;
    public virtual MemberAccessExpression ProcessMemberAccessExpression(MemberAccessExpression node, TContext ctx)=>node;
    public virtual WhileExp ProcessWhileExp(WhileExp node, TContext ctx)=>node;
    public virtual ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx)=>node;
    public virtual Expression ProcessExpression(Expression node, TContext ctx)=>node;

}

public partial class DefaultMutatorVisitor<TContext> : IAstMutatorVisitor<TContext>
{
    public AstNode Process(AstNode x, TContext ctx)
    {
        if (x == null) return x;
        var result = x switch
        {
            Assembly node => ProcessAssembly(node, ctx),
            AssemblyRef node => ProcessAssemblyRef(node, ctx),
            ClassDefinition node => ProcessClassDefinition(node, ctx),
            FieldDefinition node => ProcessFieldDefinition(node, ctx),
            PropertyDefinition node => ProcessPropertyDefinition(node, ctx),
            TypeCast node => ProcessTypeCast(node, ctx),
            ReturnStatement node => ProcessReturnStatement(node, ctx),
            StatementList node => ProcessStatementList(node, ctx),
            AbsoluteIri node => ProcessAbsoluteIri(node, ctx),
            AliasDeclaration node => ProcessAliasDeclaration(node, ctx),
            AssignmentStmt node => ProcessAssignmentStmt(node, ctx),
            BinaryExpression node => ProcessBinaryExpression(node, ctx),
            Block node => ProcessBlock(node, ctx),
            BoolValueExpression node => ProcessBoolValueExpression(node, ctx),
            ShortValueExpression node => ProcessShortValueExpression(node, ctx),
            IntValueExpression node => ProcessIntValueExpression(node, ctx),
            LongValueExpression node => ProcessLongValueExpression(node, ctx),
            FloatValueExpression node => ProcessFloatValueExpression(node, ctx),
            DoubleValueExpression node => ProcessDoubleValueExpression(node, ctx),
            DecimalValueExpression node => ProcessDecimalValueExpression(node, ctx),
            StringValueExpression node => ProcessStringValueExpression(node, ctx),
            DateValueExpression node => ProcessDateValueExpression(node, ctx),
            ExpressionList node => ProcessExpressionList(node, ctx),
            FifthProgram node => ProcessFifthProgram(node, ctx),
            FuncCallExpression node => ProcessFuncCallExpression(node, ctx),
            FunctionDefinition node => ProcessFunctionDefinition(node, ctx),
            OverloadedFunctionDefinition node => ProcessOverloadedFunctionDefinition(node, ctx),
            Identifier node => ProcessIdentifier(node, ctx),
            IdentifierExpression node => ProcessIdentifierExpression(node, ctx),
            IfElseStatement node => ProcessIfElseStatement(node, ctx),
            ModuleImport node => ProcessModuleImport(node, ctx),
            ParameterDeclarationList node => ProcessParameterDeclarationList(node, ctx),
            ParameterDeclaration node => ProcessParameterDeclaration(node, ctx),
            DestructuringDeclaration node => ProcessDestructuringDeclaration(node, ctx),
            DestructuringBinding node => ProcessDestructuringBinding(node, ctx),
            TypeCreateInstExpression node => ProcessTypeCreateInstExpression(node, ctx),
            TypeInitialiser node => ProcessTypeInitialiser(node, ctx),
            TypePropertyInit node => ProcessTypePropertyInit(node, ctx),
            UnaryExpression node => ProcessUnaryExpression(node, ctx),
            VariableDeclarationStatement node => ProcessVariableDeclarationStatement(node, ctx),
            VariableReference node => ProcessVariableReference(node, ctx),
            MemberAccessExpression node => ProcessMemberAccessExpression(node, ctx),
            WhileExp node => ProcessWhileExp(node, ctx),
            ExpressionStatement node => ProcessExpressionStatement(node, ctx),
            Expression node => ProcessExpression(node, ctx),

            { } node => node,
        };
        // in case result is totally new, copy in the metadata, so we don't lose any that's been generated previously
        x.CopyMetadataInto(result);

        if (x is ITypedAstNode tn && result is ITypedAstNode rtn)
        {
            rtn.TypeId = tn.TypeId;
        }

        return result;
    }


    public virtual Assembly ProcessAssembly(Assembly node, TContext ctx)
    {


    var builder = AssemblyBuilder.CreateAssembly();

        builder.WithName(node.Name);
            builder.WithPublicKeyToken(node.PublicKeyToken);
            builder.WithVersion(node.Version);
            builder.WithProgram((FifthProgram)Process(node.Program, ctx));
            foreach(var x in node.References){
            builder.AddingItemToReferences((AssemblyRef)Process((AssemblyRef)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx)
    {


    var builder = AssemblyRefBuilder.CreateAssemblyRef();

        builder.WithName(node.Name);
            builder.WithPublicKeyToken(node.PublicKeyToken);
            builder.WithVersion(node.Version);
    
        return builder.Build();
    }

    public virtual ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx)
    {


    var builder = ClassDefinitionBuilder.CreateClassDefinition();

        builder.WithName(node.Name);
            builder.WithNamespace(node.Namespace);
            foreach(var x in node.Fields){
            builder.AddingItemToFields((FieldDefinition)Process((FieldDefinition)x, ctx));
        }
            foreach(var x in node.Properties){
            builder.AddingItemToProperties((PropertyDefinition)Process((PropertyDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual FieldDefinition ProcessFieldDefinition(FieldDefinition node, TContext ctx)
    {


    var builder = FieldDefinitionBuilder.CreateFieldDefinition();

        builder.WithBackingFieldFor(node.BackingFieldFor);
            builder.WithName(node.Name);
            builder.WithTypeName(node.TypeName);
    
        return builder.Build();
    }

    public virtual PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)
    {


    var builder = PropertyDefinitionBuilder.CreatePropertyDefinition();

        builder.WithBackingField(node.BackingField);
            builder.WithGetAccessor((FunctionDefinition?)Process(node.GetAccessor, ctx));
            builder.WithSetAccessor((FunctionDefinition?)Process(node.SetAccessor, ctx));
            builder.WithName(node.Name);
            builder.WithTypeName(node.TypeName);
    
        return builder.Build();
    }

    public virtual TypeCast ProcessTypeCast(TypeCast node, TContext ctx)
    {


    var builder = TypeCastBuilder.CreateTypeCast();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build();
    }

    public virtual ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx)
    {


    var builder = ReturnStatementBuilder.CreateReturnStatement();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build();
    }

    public virtual StatementList ProcessStatementList(StatementList node, TContext ctx)
    {


    var builder = StatementListBuilder.CreateStatementList();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)
    {


    var builder = AbsoluteIriBuilder.CreateAbsoluteIri();

        builder.WithUri(node.Uri);
    
        return builder.Build();
    }

    public virtual AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)
    {


    var builder = AliasDeclarationBuilder.CreateAliasDeclaration();

        builder.WithIRI((AbsoluteIri)Process(node.IRI, ctx));
            builder.WithName(node.Name);
    
        return builder.Build();
    }

    public virtual AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)
    {


    var builder = AssignmentStmtBuilder.CreateAssignmentStmt();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithVariableRef((BaseVarReference)Process(node.VariableRef, ctx));
    
        return builder.Build();
    }

    public virtual BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx)
    {


    var builder = BinaryExpressionBuilder.CreateBinaryExpression();

        builder.WithLeft((Expression)Process(node.Left, ctx));
            builder.WithOp(node.Op);
            builder.WithRight((Expression)Process(node.Right, ctx));
    
        return builder.Build();
    }

    public virtual Block ProcessBlock(Block node, TContext ctx)
    {


    var builder = BlockBuilder.CreateBlock();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)
    {


    var builder = BoolValueExpressionBuilder.CreateBoolValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx)
    {


    var builder = ShortValueExpressionBuilder.CreateShortValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx)
    {


    var builder = IntValueExpressionBuilder.CreateIntValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx)
    {


    var builder = LongValueExpressionBuilder.CreateLongValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)
    {


    var builder = FloatValueExpressionBuilder.CreateFloatValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)
    {


    var builder = DoubleValueExpressionBuilder.CreateDoubleValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)
    {


    var builder = DecimalValueExpressionBuilder.CreateDecimalValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx)
    {


    var builder = StringValueExpressionBuilder.CreateStringValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx)
    {


    var builder = DateValueExpressionBuilder.CreateDateValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }

    public virtual ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx)
    {


    var builder = ExpressionListBuilder.CreateExpressionList();

        foreach(var x in node.Expressions){
            builder.AddingItemToExpressions((Expression)Process((Expression)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx)
    {


    var builder = FifthProgramBuilder.CreateFifthProgram();

        foreach(var x in node.Aliases){
            builder.AddingItemToAliases((AliasDeclaration)Process((AliasDeclaration)x, ctx));
        }
            foreach(var x in node.Classes){
            builder.AddingItemToClasses((ClassDefinition)Process((ClassDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)
    {


    var builder = FuncCallExpressionBuilder.CreateFuncCallExpression();

        builder.WithActualParameters((ExpressionList)Process(node.ActualParameters, ctx));
            builder.WithName(node.Name);
    
        return builder.Build();
    }

    public virtual FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)
    {


    var builder = FunctionDefinitionBuilder.CreateFunctionDefinition();

        builder.WithParameterDeclarations((ParameterDeclarationList)Process(node.ParameterDeclarations, ctx));
            builder.WithBody((Block?)Process(node.Body, ctx));
            builder.WithTypename(node.Typename);
            builder.WithName(node.Name);
            builder.WithIsEntryPoint(node.IsEntryPoint);
            builder.WithIsInstanceFunction(node.IsInstanceFunction);
            builder.WithFunctionKind(node.FunctionKind);
            builder.WithReturnType(node.ReturnType);
    
        return builder.Build();
    }

    public virtual OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)
    {


    var builder = OverloadedFunctionDefinitionBuilder.CreateOverloadedFunctionDefinition();

        foreach(var x in node.OverloadClauses){
            builder.AddingItemToOverloadClauses((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
            builder.WithSignature(node.Signature);
    
        return builder.Build();
    }

    public virtual Identifier ProcessIdentifier(Identifier node, TContext ctx)
    {


    var builder = IdentifierBuilder.CreateIdentifier();

        builder.WithValue(node.Value);
    
        return builder.Build();
    }

    public virtual IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)
    {


    var builder = IdentifierExpressionBuilder.CreateIdentifierExpression();

        builder.WithIdentifier((Identifier)Process(node.Identifier, ctx));
    
        return builder.Build();
    }

    public virtual IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx)
    {


    var builder = IfElseStatementBuilder.CreateIfElseStatement();

        builder.WithIfBlock((Block)Process(node.IfBlock, ctx));
            builder.WithElseBlock((Block)Process(node.ElseBlock, ctx));
            builder.WithCondition((Expression)Process(node.Condition, ctx));
    
        return builder.Build();
    }

    public virtual ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx)
    {


    var builder = ModuleImportBuilder.CreateModuleImport();

        builder.WithModuleName(node.ModuleName);
    
        return builder.Build();
    }

    public virtual ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)
    {


    var builder = ParameterDeclarationListBuilder.CreateParameterDeclarationList();

        foreach(var x in node.ParameterDeclarations){
            builder.AddingItemToParameterDeclarations((IParameterListItem)Process((ParameterDeclaration)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)
    {


    var builder = ParameterDeclarationBuilder.CreateParameterDeclaration();

        builder.WithParameterName((Identifier)Process(node.ParameterName, ctx));
            builder.WithTypeName(node.TypeName);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
            builder.WithDestructuringDecl((DestructuringDeclaration)Process(node.DestructuringDecl, ctx));
    
        return builder.Build();
    }

    public virtual DestructuringDeclaration ProcessDestructuringDeclaration(DestructuringDeclaration node, TContext ctx)
    {


    var builder = DestructuringDeclarationBuilder.CreateDestructuringDeclaration();

        foreach(var x in node.Bindings){
            builder.AddingItemToBindings((DestructuringBinding)Process((DestructuringBinding)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual DestructuringBinding ProcessDestructuringBinding(DestructuringBinding node, TContext ctx)
    {


    var builder = DestructuringBindingBuilder.CreateDestructuringBinding();

        builder.WithVarname(node.Varname);
            builder.WithPropname(node.Propname);
            builder.WithPropDecl(node.PropDecl);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
            builder.WithDestructuringDecl((DestructuringDeclaration)Process(node.DestructuringDecl, ctx));
    
        return builder.Build();
    }

    public virtual TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)
    {


    var builder = TypeCreateInstExpressionBuilder.CreateTypeCreateInstExpression();


        return builder.Build();
    }

    public virtual TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)
    {


    var builder = TypeInitialiserBuilder.CreateTypeInitialiser();

        builder.WithTypeName(node.TypeName);
            foreach(var x in node.PropertyInitialisers){
            builder.AddingItemToPropertyInitialisers((TypePropertyInit)Process((TypePropertyInit)x, ctx));
        }
    
        return builder.Build();
    }

    public virtual TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)
    {


    var builder = TypePropertyInitBuilder.CreateTypePropertyInit();

        builder.WithName(node.Name);
            builder.WithValue((Expression)Process(node.Value, ctx));
    
        return builder.Build();
    }

    public virtual UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx)
    {


    var builder = UnaryExpressionBuilder.CreateUnaryExpression();

        builder.WithOperand((Expression)Process(node.Operand, ctx));
            builder.WithOp(node.Op);
    
        return builder.Build();
    }

    public virtual VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)
    {


    var builder = VariableDeclarationStatementBuilder.CreateVariableDeclarationStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithName(node.Name);
            builder.WithUnresolvedTypeName(node.UnresolvedTypeName);
    
        return builder.Build();
    }

    public virtual VariableReference ProcessVariableReference(VariableReference node, TContext ctx)
    {


    var builder = VariableReferenceBuilder.CreateVariableReference();

        builder.WithName(node.Name);
    
        return builder.Build();
    }

    public virtual MemberAccessExpression ProcessMemberAccessExpression(MemberAccessExpression node, TContext ctx)
    {


    var builder = MemberAccessExpressionBuilder.CreateMemberAccessExpression();

        builder.WithLHS((Expression)Process(node.LHS, ctx));
            builder.WithRHS((Expression)Process(node.RHS, ctx));
    
        return builder.Build();
    }

    public virtual WhileExp ProcessWhileExp(WhileExp node, TContext ctx)
    {


    var builder = WhileExpBuilder.CreateWhileExp();

        builder.WithCondition((Expression)Process(node.Condition, ctx));
            builder.WithLoopBlock(node.LoopBlock);
    
        return builder.Build();
    }

    public virtual ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx)
    {


    var builder = ExpressionStatementBuilder.CreateExpressionStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
    
        return builder.Build();
    }

    public virtual Expression ProcessExpression(Expression node, TContext ctx)
    {


    var builder = ExpressionBuilder.CreateExpression();


        return builder.Build();
    }

}


public interface IAstMutatorVisitor2<TContext>
{
    AstNode ProcessAssembly(Assembly node, TContext ctx);
    AstNode ProcessAssemblyRef(AssemblyRef node, TContext ctx);
    AstNode ProcessClassDefinition(ClassDefinition node, TContext ctx);
    AstNode ProcessFieldDefinition(FieldDefinition node, TContext ctx);
    AstNode ProcessPropertyDefinition(PropertyDefinition node, TContext ctx);
    AstNode ProcessTypeCast(TypeCast node, TContext ctx);
    AstNode ProcessReturnStatement(ReturnStatement node, TContext ctx);
    AstNode ProcessStatementList(StatementList node, TContext ctx);
    AstNode ProcessAbsoluteIri(AbsoluteIri node, TContext ctx);
    AstNode ProcessAliasDeclaration(AliasDeclaration node, TContext ctx);
    AstNode ProcessAssignmentStmt(AssignmentStmt node, TContext ctx);
    AstNode ProcessBinaryExpression(BinaryExpression node, TContext ctx);
    AstNode ProcessBlock(Block node, TContext ctx);
    AstNode ProcessBoolValueExpression(BoolValueExpression node, TContext ctx);
    AstNode ProcessShortValueExpression(ShortValueExpression node, TContext ctx);
    AstNode ProcessIntValueExpression(IntValueExpression node, TContext ctx);
    AstNode ProcessLongValueExpression(LongValueExpression node, TContext ctx);
    AstNode ProcessFloatValueExpression(FloatValueExpression node, TContext ctx);
    AstNode ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx);
    AstNode ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx);
    AstNode ProcessStringValueExpression(StringValueExpression node, TContext ctx);
    AstNode ProcessDateValueExpression(DateValueExpression node, TContext ctx);
    AstNode ProcessExpressionList(ExpressionList node, TContext ctx);
    AstNode ProcessFifthProgram(FifthProgram node, TContext ctx);
    AstNode ProcessFuncCallExpression(FuncCallExpression node, TContext ctx);
    AstNode ProcessFunctionDefinition(FunctionDefinition node, TContext ctx);
    AstNode ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx);
    AstNode ProcessIdentifier(Identifier node, TContext ctx);
    AstNode ProcessIdentifierExpression(IdentifierExpression node, TContext ctx);
    AstNode ProcessIfElseStatement(IfElseStatement node, TContext ctx);
    AstNode ProcessModuleImport(ModuleImport node, TContext ctx);
    AstNode ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx);
    AstNode ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx);
    AstNode ProcessDestructuringDeclaration(DestructuringDeclaration node, TContext ctx);
    AstNode ProcessDestructuringBinding(DestructuringBinding node, TContext ctx);
    AstNode ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx);
    AstNode ProcessTypeInitialiser(TypeInitialiser node, TContext ctx);
    AstNode ProcessTypePropertyInit(TypePropertyInit node, TContext ctx);
    AstNode ProcessUnaryExpression(UnaryExpression node, TContext ctx);
    AstNode ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx);
    AstNode ProcessVariableReference(VariableReference node, TContext ctx);
    AstNode ProcessMemberAccessExpression(MemberAccessExpression node, TContext ctx);
    AstNode ProcessWhileExp(WhileExp node, TContext ctx);
    AstNode ProcessExpressionStatement(ExpressionStatement node, TContext ctx);
    AstNode ProcessExpression(Expression node, TContext ctx);
}

public partial class DefaultMutatorVisitor2<TContext> : IAstMutatorVisitor2<TContext>
{
    public AstNode Process(AstNode x, TContext ctx)
    {
        if (x == null) return x;
        var result = x switch
        {
            Assembly node => ProcessAssembly(node, ctx),
            AssemblyRef node => ProcessAssemblyRef(node, ctx),
            ClassDefinition node => ProcessClassDefinition(node, ctx),
            FieldDefinition node => ProcessFieldDefinition(node, ctx),
            PropertyDefinition node => ProcessPropertyDefinition(node, ctx),
            TypeCast node => ProcessTypeCast(node, ctx),
            ReturnStatement node => ProcessReturnStatement(node, ctx),
            StatementList node => ProcessStatementList(node, ctx),
            AbsoluteIri node => ProcessAbsoluteIri(node, ctx),
            AliasDeclaration node => ProcessAliasDeclaration(node, ctx),
            AssignmentStmt node => ProcessAssignmentStmt(node, ctx),
            BinaryExpression node => ProcessBinaryExpression(node, ctx),
            Block node => ProcessBlock(node, ctx),
            BoolValueExpression node => ProcessBoolValueExpression(node, ctx),
            ShortValueExpression node => ProcessShortValueExpression(node, ctx),
            IntValueExpression node => ProcessIntValueExpression(node, ctx),
            LongValueExpression node => ProcessLongValueExpression(node, ctx),
            FloatValueExpression node => ProcessFloatValueExpression(node, ctx),
            DoubleValueExpression node => ProcessDoubleValueExpression(node, ctx),
            DecimalValueExpression node => ProcessDecimalValueExpression(node, ctx),
            StringValueExpression node => ProcessStringValueExpression(node, ctx),
            DateValueExpression node => ProcessDateValueExpression(node, ctx),
            ExpressionList node => ProcessExpressionList(node, ctx),
            FifthProgram node => ProcessFifthProgram(node, ctx),
            FuncCallExpression node => ProcessFuncCallExpression(node, ctx),
            FunctionDefinition node => ProcessFunctionDefinition(node, ctx),
            OverloadedFunctionDefinition node => ProcessOverloadedFunctionDefinition(node, ctx),
            Identifier node => ProcessIdentifier(node, ctx),
            IdentifierExpression node => ProcessIdentifierExpression(node, ctx),
            IfElseStatement node => ProcessIfElseStatement(node, ctx),
            ModuleImport node => ProcessModuleImport(node, ctx),
            ParameterDeclarationList node => ProcessParameterDeclarationList(node, ctx),
            ParameterDeclaration node => ProcessParameterDeclaration(node, ctx),
            DestructuringDeclaration node => ProcessDestructuringDeclaration(node, ctx),
            DestructuringBinding node => ProcessDestructuringBinding(node, ctx),
            TypeCreateInstExpression node => ProcessTypeCreateInstExpression(node, ctx),
            TypeInitialiser node => ProcessTypeInitialiser(node, ctx),
            TypePropertyInit node => ProcessTypePropertyInit(node, ctx),
            UnaryExpression node => ProcessUnaryExpression(node, ctx),
            VariableDeclarationStatement node => ProcessVariableDeclarationStatement(node, ctx),
            VariableReference node => ProcessVariableReference(node, ctx),
            MemberAccessExpression node => ProcessMemberAccessExpression(node, ctx),
            WhileExp node => ProcessWhileExp(node, ctx),
            ExpressionStatement node => ProcessExpressionStatement(node, ctx),
            Expression node => ProcessExpression(node, ctx),

            { } node => node,
        };
        // in case result is totally new, copy in the metadata, so we don't lose any that's been generated previously
        x.CopyMetadataInto(result);

        if (x is ITypedAstNode tn && result is ITypedAstNode rtn)
        {
            rtn.TypeId = tn.TypeId;
        }

        return result;
    }


    public virtual AstNode ProcessAssembly(Assembly node, TContext ctx)
    {


    var builder = AssemblyBuilder.CreateAssembly();

        builder.WithName(node.Name);
            builder.WithPublicKeyToken(node.PublicKeyToken);
            builder.WithVersion(node.Version);
            builder.WithProgram((FifthProgram)Process(node.Program, ctx));
            foreach(var x in node.References){
            builder.AddingItemToReferences((AssemblyRef)Process((AssemblyRef)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessAssemblyRef(AssemblyRef node, TContext ctx)
    {


    var builder = AssemblyRefBuilder.CreateAssemblyRef();

        builder.WithName(node.Name);
            builder.WithPublicKeyToken(node.PublicKeyToken);
            builder.WithVersion(node.Version);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessClassDefinition(ClassDefinition node, TContext ctx)
    {


    var builder = ClassDefinitionBuilder.CreateClassDefinition();

        builder.WithName(node.Name);
            builder.WithNamespace(node.Namespace);
            foreach(var x in node.Fields){
            builder.AddingItemToFields((FieldDefinition)Process((FieldDefinition)x, ctx));
        }
            foreach(var x in node.Properties){
            builder.AddingItemToProperties((PropertyDefinition)Process((PropertyDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessFieldDefinition(FieldDefinition node, TContext ctx)
    {


    var builder = FieldDefinitionBuilder.CreateFieldDefinition();

        builder.WithBackingFieldFor(node.BackingFieldFor);
            builder.WithName(node.Name);
            builder.WithTypeName(node.TypeName);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)
    {


    var builder = PropertyDefinitionBuilder.CreatePropertyDefinition();

        builder.WithBackingField(node.BackingField);
            builder.WithGetAccessor((FunctionDefinition?)Process(node.GetAccessor, ctx));
            builder.WithSetAccessor((FunctionDefinition?)Process(node.SetAccessor, ctx));
            builder.WithName(node.Name);
            builder.WithTypeName(node.TypeName);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessTypeCast(TypeCast node, TContext ctx)
    {


    var builder = TypeCastBuilder.CreateTypeCast();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessReturnStatement(ReturnStatement node, TContext ctx)
    {


    var builder = ReturnStatementBuilder.CreateReturnStatement();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessStatementList(StatementList node, TContext ctx)
    {


    var builder = StatementListBuilder.CreateStatementList();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)
    {


    var builder = AbsoluteIriBuilder.CreateAbsoluteIri();

        builder.WithUri(node.Uri);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)
    {


    var builder = AliasDeclarationBuilder.CreateAliasDeclaration();

        builder.WithIRI((AbsoluteIri)Process(node.IRI, ctx));
            builder.WithName(node.Name);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)
    {


    var builder = AssignmentStmtBuilder.CreateAssignmentStmt();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithVariableRef((BaseVarReference)Process(node.VariableRef, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessBinaryExpression(BinaryExpression node, TContext ctx)
    {


    var builder = BinaryExpressionBuilder.CreateBinaryExpression();

        builder.WithLeft((Expression)Process(node.Left, ctx));
            builder.WithOp(node.Op);
            builder.WithRight((Expression)Process(node.Right, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessBlock(Block node, TContext ctx)
    {


    var builder = BlockBuilder.CreateBlock();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)
    {


    var builder = BoolValueExpressionBuilder.CreateBoolValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessShortValueExpression(ShortValueExpression node, TContext ctx)
    {


    var builder = ShortValueExpressionBuilder.CreateShortValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessIntValueExpression(IntValueExpression node, TContext ctx)
    {


    var builder = IntValueExpressionBuilder.CreateIntValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessLongValueExpression(LongValueExpression node, TContext ctx)
    {


    var builder = LongValueExpressionBuilder.CreateLongValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)
    {


    var builder = FloatValueExpressionBuilder.CreateFloatValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)
    {


    var builder = DoubleValueExpressionBuilder.CreateDoubleValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)
    {


    var builder = DecimalValueExpressionBuilder.CreateDecimalValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessStringValueExpression(StringValueExpression node, TContext ctx)
    {


    var builder = StringValueExpressionBuilder.CreateStringValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessDateValueExpression(DateValueExpression node, TContext ctx)
    {


    var builder = DateValueExpressionBuilder.CreateDateValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessExpressionList(ExpressionList node, TContext ctx)
    {


    var builder = ExpressionListBuilder.CreateExpressionList();

        foreach(var x in node.Expressions){
            builder.AddingItemToExpressions((Expression)Process((Expression)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessFifthProgram(FifthProgram node, TContext ctx)
    {


    var builder = FifthProgramBuilder.CreateFifthProgram();

        foreach(var x in node.Aliases){
            builder.AddingItemToAliases((AliasDeclaration)Process((AliasDeclaration)x, ctx));
        }
            foreach(var x in node.Classes){
            builder.AddingItemToClasses((ClassDefinition)Process((ClassDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)
    {


    var builder = FuncCallExpressionBuilder.CreateFuncCallExpression();

        builder.WithActualParameters((ExpressionList)Process(node.ActualParameters, ctx));
            builder.WithName(node.Name);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)
    {


    var builder = FunctionDefinitionBuilder.CreateFunctionDefinition();

        builder.WithParameterDeclarations((ParameterDeclarationList)Process(node.ParameterDeclarations, ctx));
            builder.WithBody((Block?)Process(node.Body, ctx));
            builder.WithTypename(node.Typename);
            builder.WithName(node.Name);
            builder.WithIsEntryPoint(node.IsEntryPoint);
            builder.WithIsInstanceFunction(node.IsInstanceFunction);
            builder.WithFunctionKind(node.FunctionKind);
            builder.WithReturnType(node.ReturnType);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)
    {


    var builder = OverloadedFunctionDefinitionBuilder.CreateOverloadedFunctionDefinition();

        foreach(var x in node.OverloadClauses){
            builder.AddingItemToOverloadClauses((IFunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
            builder.WithSignature(node.Signature);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessIdentifier(Identifier node, TContext ctx)
    {


    var builder = IdentifierBuilder.CreateIdentifier();

        builder.WithValue(node.Value);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)
    {


    var builder = IdentifierExpressionBuilder.CreateIdentifierExpression();

        builder.WithIdentifier((Identifier)Process(node.Identifier, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessIfElseStatement(IfElseStatement node, TContext ctx)
    {


    var builder = IfElseStatementBuilder.CreateIfElseStatement();

        builder.WithIfBlock((Block)Process(node.IfBlock, ctx));
            builder.WithElseBlock((Block)Process(node.ElseBlock, ctx));
            builder.WithCondition((Expression)Process(node.Condition, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessModuleImport(ModuleImport node, TContext ctx)
    {


    var builder = ModuleImportBuilder.CreateModuleImport();

        builder.WithModuleName(node.ModuleName);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)
    {


    var builder = ParameterDeclarationListBuilder.CreateParameterDeclarationList();

        foreach(var x in node.ParameterDeclarations){
            builder.AddingItemToParameterDeclarations((IParameterListItem)Process((ParameterDeclaration)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)
    {


    var builder = ParameterDeclarationBuilder.CreateParameterDeclaration();

        builder.WithParameterName((Identifier)Process(node.ParameterName, ctx));
            builder.WithTypeName(node.TypeName);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
            builder.WithDestructuringDecl((DestructuringDeclaration)Process(node.DestructuringDecl, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessDestructuringDeclaration(DestructuringDeclaration node, TContext ctx)
    {


    var builder = DestructuringDeclarationBuilder.CreateDestructuringDeclaration();

        foreach(var x in node.Bindings){
            builder.AddingItemToBindings((DestructuringBinding)Process((DestructuringBinding)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessDestructuringBinding(DestructuringBinding node, TContext ctx)
    {


    var builder = DestructuringBindingBuilder.CreateDestructuringBinding();

        builder.WithVarname(node.Varname);
            builder.WithPropname(node.Propname);
            builder.WithPropDecl(node.PropDecl);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
            builder.WithDestructuringDecl((DestructuringDeclaration)Process(node.DestructuringDecl, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)
    {


    var builder = TypeCreateInstExpressionBuilder.CreateTypeCreateInstExpression();


        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)
    {


    var builder = TypeInitialiserBuilder.CreateTypeInitialiser();

        builder.WithTypeName(node.TypeName);
            foreach(var x in node.PropertyInitialisers){
            builder.AddingItemToPropertyInitialisers((TypePropertyInit)Process((TypePropertyInit)x, ctx));
        }
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)
    {


    var builder = TypePropertyInitBuilder.CreateTypePropertyInit();

        builder.WithName(node.Name);
            builder.WithValue((Expression)Process(node.Value, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessUnaryExpression(UnaryExpression node, TContext ctx)
    {


    var builder = UnaryExpressionBuilder.CreateUnaryExpression();

        builder.WithOperand((Expression)Process(node.Operand, ctx));
            builder.WithOp(node.Op);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)
    {


    var builder = VariableDeclarationStatementBuilder.CreateVariableDeclarationStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithName(node.Name);
            builder.WithUnresolvedTypeName(node.UnresolvedTypeName);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessVariableReference(VariableReference node, TContext ctx)
    {


    var builder = VariableReferenceBuilder.CreateVariableReference();

        builder.WithName(node.Name);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessMemberAccessExpression(MemberAccessExpression node, TContext ctx)
    {


    var builder = MemberAccessExpressionBuilder.CreateMemberAccessExpression();

        builder.WithLHS((Expression)Process(node.LHS, ctx));
            builder.WithRHS((Expression)Process(node.RHS, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessWhileExp(WhileExp node, TContext ctx)
    {


    var builder = WhileExpBuilder.CreateWhileExp();

        builder.WithCondition((Expression)Process(node.Condition, ctx));
            builder.WithLoopBlock(node.LoopBlock);
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessExpressionStatement(ExpressionStatement node, TContext ctx)
    {


    var builder = ExpressionStatementBuilder.CreateExpressionStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
    
        return builder.Build() as AstNode;
    }

    public virtual AstNode ProcessExpression(Expression node, TContext ctx)
    {


    var builder = ExpressionBuilder.CreateExpression();


        return builder.Build() as AstNode;
    }

}
