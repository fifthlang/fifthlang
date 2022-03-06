
namespace Fifth.AST.Visitors;
using Fifth.AST;
using Fifth.AST.Builders;

public interface IAstMutatorVisitor<TContext>
{
    Assembly ProcessAssembly(Assembly node, TContext ctx);
    AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx);
    ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx);
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
    BuiltinFunctionDefinition ProcessBuiltinFunctionDefinition(BuiltinFunctionDefinition node, TContext ctx);
    OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx);
    Identifier ProcessIdentifier(Identifier node, TContext ctx);
    IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx);
    IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx);
    ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx);
    ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx);
    ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx);
    TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx);
    TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx);
    DestructuringParamDecl ProcessDestructuringParamDecl(DestructuringParamDecl node, TContext ctx);
    PropertyBinding ProcessPropertyBinding(PropertyBinding node, TContext ctx);
    TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx);
    UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx);
    VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx);
    VariableReference ProcessVariableReference(VariableReference node, TContext ctx);
    CompoundVariableReference ProcessCompoundVariableReference(CompoundVariableReference node, TContext ctx);
    WhileExp ProcessWhileExp(WhileExp node, TContext ctx);
    ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx);
    Expression ProcessExpression(Expression node, TContext ctx);
}

public partial class NullMutatorVisitor<TContext> : IAstMutatorVisitor<TContext>
{
    public Assembly ProcessAssembly(Assembly node, TContext ctx)=>node;
    public AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx)=>node;
    public ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx)=>node;
    public PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)=>node;
    public TypeCast ProcessTypeCast(TypeCast node, TContext ctx)=>node;
    public ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx)=>node;
    public StatementList ProcessStatementList(StatementList node, TContext ctx)=>node;
    public AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)=>node;
    public AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)=>node;
    public AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)=>node;
    public BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx)=>node;
    public Block ProcessBlock(Block node, TContext ctx)=>node;
    public BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)=>node;
    public ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx)=>node;
    public IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx)=>node;
    public LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx)=>node;
    public FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)=>node;
    public DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)=>node;
    public DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)=>node;
    public StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx)=>node;
    public DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx)=>node;
    public ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx)=>node;
    public FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx)=>node;
    public FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)=>node;
    public FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)=>node;
    public BuiltinFunctionDefinition ProcessBuiltinFunctionDefinition(BuiltinFunctionDefinition node, TContext ctx)=>node;
    public OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)=>node;
    public Identifier ProcessIdentifier(Identifier node, TContext ctx)=>node;
    public IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)=>node;
    public IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx)=>node;
    public ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx)=>node;
    public ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)=>node;
    public ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)=>node;
    public TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)=>node;
    public TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)=>node;
    public DestructuringParamDecl ProcessDestructuringParamDecl(DestructuringParamDecl node, TContext ctx)=>node;
    public PropertyBinding ProcessPropertyBinding(PropertyBinding node, TContext ctx)=>node;
    public TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)=>node;
    public UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx)=>node;
    public VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)=>node;
    public VariableReference ProcessVariableReference(VariableReference node, TContext ctx)=>node;
    public CompoundVariableReference ProcessCompoundVariableReference(CompoundVariableReference node, TContext ctx)=>node;
    public WhileExp ProcessWhileExp(WhileExp node, TContext ctx)=>node;
    public ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx)=>node;
    public Expression ProcessExpression(Expression node, TContext ctx)=>node;

}

public partial class DefaultMutatorVisitor<TContext> : IAstMutatorVisitor<TContext>
{
    public AstNode Process(AstNode x, TContext ctx)
    {
        return x switch
        {
            Assembly node => ProcessAssembly(node, ctx),
            AssemblyRef node => ProcessAssemblyRef(node, ctx),
            ClassDefinition node => ProcessClassDefinition(node, ctx),
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
            BuiltinFunctionDefinition node => ProcessBuiltinFunctionDefinition(node, ctx),
            OverloadedFunctionDefinition node => ProcessOverloadedFunctionDefinition(node, ctx),
            Identifier node => ProcessIdentifier(node, ctx),
            IdentifierExpression node => ProcessIdentifierExpression(node, ctx),
            IfElseStatement node => ProcessIfElseStatement(node, ctx),
            ModuleImport node => ProcessModuleImport(node, ctx),
            ParameterDeclaration node => ProcessParameterDeclaration(node, ctx),
            ParameterDeclarationList node => ProcessParameterDeclarationList(node, ctx),
            TypeCreateInstExpression node => ProcessTypeCreateInstExpression(node, ctx),
            TypeInitialiser node => ProcessTypeInitialiser(node, ctx),
            DestructuringParamDecl node => ProcessDestructuringParamDecl(node, ctx),
            PropertyBinding node => ProcessPropertyBinding(node, ctx),
            TypePropertyInit node => ProcessTypePropertyInit(node, ctx),
            UnaryExpression node => ProcessUnaryExpression(node, ctx),
            VariableDeclarationStatement node => ProcessVariableDeclarationStatement(node, ctx),
            VariableReference node => ProcessVariableReference(node, ctx),
            CompoundVariableReference node => ProcessCompoundVariableReference(node, ctx),
            WhileExp node => ProcessWhileExp(node, ctx),
            ExpressionStatement node => ProcessExpressionStatement(node, ctx),
            Expression node => ProcessExpression(node, ctx),

            { } node => node,
        };
    }







    public Assembly ProcessAssembly(Assembly node, TContext ctx)
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


    public AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx)
    {


    var builder = AssemblyRefBuilder.CreateAssemblyRef();

        builder.WithName(node.Name);
            builder.WithPublicKeyToken(node.PublicKeyToken);
            builder.WithVersion(node.Version);
    
        return builder.Build();
    }


    public ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx)
    {


    var builder = ClassDefinitionBuilder.CreateClassDefinition();

        builder.WithName(node.Name);
            foreach(var x in node.Properties){
            builder.AddingItemToProperties((PropertyDefinition)Process((PropertyDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((FunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build();
    }


    public PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)
    {


    var builder = PropertyDefinitionBuilder.CreatePropertyDefinition();

        builder.WithName(node.Name);
            builder.WithTypeName(node.TypeName);
    
        return builder.Build();
    }


    public TypeCast ProcessTypeCast(TypeCast node, TContext ctx)
    {


    var builder = TypeCastBuilder.CreateTypeCast();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build();
    }


    public ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx)
    {


    var builder = ReturnStatementBuilder.CreateReturnStatement();

        builder.WithSubExpression((Expression)Process(node.SubExpression, ctx));
            builder.WithTargetTid(node.TargetTid);
    
        return builder.Build();
    }


    public StatementList ProcessStatementList(StatementList node, TContext ctx)
    {


    var builder = StatementListBuilder.CreateStatementList();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build();
    }


    public AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)
    {


    var builder = AbsoluteIriBuilder.CreateAbsoluteIri();

        builder.WithUri(node.Uri);
    
        return builder.Build();
    }


    public AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)
    {


    var builder = AliasDeclarationBuilder.CreateAliasDeclaration();

        builder.WithIRI((AbsoluteIri)Process(node.IRI, ctx));
            builder.WithName(node.Name);
    
        return builder.Build();
    }


    public AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)
    {


    var builder = AssignmentStmtBuilder.CreateAssignmentStmt();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithVariableRef((BaseVarReference)Process(node.VariableRef, ctx));
    
        return builder.Build();
    }


    public BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx)
    {


    var builder = BinaryExpressionBuilder.CreateBinaryExpression();

        builder.WithLeft((Expression)Process(node.Left, ctx));
            builder.WithOp(node.Op);
            builder.WithRight((Expression)Process(node.Right, ctx));
    
        return builder.Build();
    }


    public Block ProcessBlock(Block node, TContext ctx)
    {


    var builder = BlockBuilder.CreateBlock();

        foreach(var x in node.Statements){
            builder.AddingItemToStatements((Statement)Process((Statement)x, ctx));
        }
    
        return builder.Build();
    }


    public BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)
    {


    var builder = BoolValueExpressionBuilder.CreateBoolValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx)
    {


    var builder = ShortValueExpressionBuilder.CreateShortValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx)
    {


    var builder = IntValueExpressionBuilder.CreateIntValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx)
    {


    var builder = LongValueExpressionBuilder.CreateLongValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)
    {


    var builder = FloatValueExpressionBuilder.CreateFloatValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)
    {


    var builder = DoubleValueExpressionBuilder.CreateDoubleValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)
    {


    var builder = DecimalValueExpressionBuilder.CreateDecimalValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx)
    {


    var builder = StringValueExpressionBuilder.CreateStringValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx)
    {


    var builder = DateValueExpressionBuilder.CreateDateValueExpression();

        builder.WithTheValue(node.TheValue);
    
        return builder.Build();
    }


    public ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx)
    {


    var builder = ExpressionListBuilder.CreateExpressionList();

        foreach(var x in node.Expressions){
            builder.AddingItemToExpressions((Expression)Process((Expression)x, ctx));
        }
    
        return builder.Build();
    }


    public FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx)
    {


    var builder = FifthProgramBuilder.CreateFifthProgram();

        foreach(var x in node.Aliases){
            builder.AddingItemToAliases((AliasDeclaration)Process((AliasDeclaration)x, ctx));
        }
            foreach(var x in node.Classes){
            builder.AddingItemToClasses((ClassDefinition)Process((ClassDefinition)x, ctx));
        }
            foreach(var x in node.Functions){
            builder.AddingItemToFunctions((FunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
    
        return builder.Build();
    }


    public FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)
    {


    var builder = FuncCallExpressionBuilder.CreateFuncCallExpression();

        builder.WithActualParameters((ExpressionList)Process(node.ActualParameters, ctx));
            builder.WithName(node.Name);
    
        return builder.Build();
    }


    public FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)
    {


    var builder = FunctionDefinitionBuilder.CreateFunctionDefinition();

        builder.WithParameterDeclarations((ParameterDeclarationList)Process(node.ParameterDeclarations, ctx));
            builder.WithBody((Block)Process(node.Body, ctx));
            builder.WithTypename(node.Typename);
            builder.WithName(node.Name);
            builder.WithIsEntryPoint(node.IsEntryPoint);
            builder.WithReturnType(node.ReturnType);
    
        return builder.Build();
    }


    public BuiltinFunctionDefinition ProcessBuiltinFunctionDefinition(BuiltinFunctionDefinition node, TContext ctx)
    {


    var builder = BuiltinFunctionDefinitionBuilder.CreateBuiltinFunctionDefinition();


        return builder.Build();
    }


    public OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)
    {


    var builder = OverloadedFunctionDefinitionBuilder.CreateOverloadedFunctionDefinition();

        foreach(var x in node.OverloadClauses){
            builder.AddingItemToOverloadClauses((FunctionDefinition)Process((FunctionDefinition)x, ctx));
        }
            builder.WithSignature(node.Signature);
    
        return builder.Build();
    }


    public Identifier ProcessIdentifier(Identifier node, TContext ctx)
    {


    var builder = IdentifierBuilder.CreateIdentifier();

        builder.WithValue(node.Value);
    
        return builder.Build();
    }


    public IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)
    {


    var builder = IdentifierExpressionBuilder.CreateIdentifierExpression();

        builder.WithIdentifier((Identifier)Process(node.Identifier, ctx));
    
        return builder.Build();
    }


    public IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx)
    {


    var builder = IfElseStatementBuilder.CreateIfElseStatement();

        builder.WithIfBlock((Block)Process(node.IfBlock, ctx));
            builder.WithElseBlock((Block)Process(node.ElseBlock, ctx));
            builder.WithCondition((Expression)Process(node.Condition, ctx));
    
        return builder.Build();
    }


    public ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx)
    {


    var builder = ModuleImportBuilder.CreateModuleImport();

        builder.WithModuleName(node.ModuleName);
    
        return builder.Build();
    }


    public ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)
    {


    var builder = ParameterDeclarationBuilder.CreateParameterDeclaration();

        builder.WithParameterName((Identifier)Process(node.ParameterName, ctx));
            builder.WithTypeName(node.TypeName);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
    
        return builder.Build();
    }


    public ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)
    {


    var builder = ParameterDeclarationListBuilder.CreateParameterDeclarationList();

        foreach(var x in node.ParameterDeclarations){
            builder.AddingItemToParameterDeclarations(x);
        }
    
        return builder.Build();
    }


    public TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)
    {


    var builder = TypeCreateInstExpressionBuilder.CreateTypeCreateInstExpression();


        return builder.Build();
    }


    public TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)
    {


    var builder = TypeInitialiserBuilder.CreateTypeInitialiser();

        builder.WithTypeName(node.TypeName);
            foreach(var x in node.PropertyInitialisers){
            builder.AddingItemToPropertyInitialisers((TypePropertyInit)Process((TypePropertyInit)x, ctx));
        }
    
        return builder.Build();
    }


    public DestructuringParamDecl ProcessDestructuringParamDecl(DestructuringParamDecl node, TContext ctx)
    {


    var builder = DestructuringParamDeclBuilder.CreateDestructuringParamDecl();

        builder.WithTypeName(node.TypeName);
            builder.WithParameterName(node.ParameterName);
            foreach(var x in node.PropertyBindings){
            builder.AddingItemToPropertyBindings((PropertyBinding)Process((PropertyBinding)x, ctx));
        }
    
        return builder.Build();
    }


    public PropertyBinding ProcessPropertyBinding(PropertyBinding node, TContext ctx)
    {


    var builder = PropertyBindingBuilder.CreatePropertyBinding();

        builder.WithBoundPropertyName(node.BoundPropertyName);
            builder.WithBoundVariableName(node.BoundVariableName);
            builder.WithConstraint((Expression)Process(node.Constraint, ctx));
    
        return builder.Build();
    }


    public TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)
    {


    var builder = TypePropertyInitBuilder.CreateTypePropertyInit();

        builder.WithName(node.Name);
            builder.WithValue((Expression)Process(node.Value, ctx));
    
        return builder.Build();
    }


    public UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx)
    {


    var builder = UnaryExpressionBuilder.CreateUnaryExpression();

        builder.WithOperand((Expression)Process(node.Operand, ctx));
            builder.WithOp(node.Op);
    
        return builder.Build();
    }


    public VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)
    {


    var builder = VariableDeclarationStatementBuilder.CreateVariableDeclarationStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
            builder.WithName((Identifier)Process(node.Name, ctx));
    
        return builder.Build();
    }


    public VariableReference ProcessVariableReference(VariableReference node, TContext ctx)
    {


    var builder = VariableReferenceBuilder.CreateVariableReference();

        builder.WithName(node.Name);
    
        return builder.Build();
    }


    public CompoundVariableReference ProcessCompoundVariableReference(CompoundVariableReference node, TContext ctx)
    {


    var builder = CompoundVariableReferenceBuilder.CreateCompoundVariableReference();

        foreach(var x in node.ComponentReferences){
            builder.AddingItemToComponentReferences((VariableReference)Process((VariableReference)x, ctx));
        }
    
        return builder.Build();
    }


    public WhileExp ProcessWhileExp(WhileExp node, TContext ctx)
    {


    var builder = WhileExpBuilder.CreateWhileExp();

        builder.WithCondition((Expression)Process(node.Condition, ctx));
            builder.WithLoopBlock(node.LoopBlock);
    
        return builder.Build();
    }


    public ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx)
    {


    var builder = ExpressionStatementBuilder.CreateExpressionStatement();

        builder.WithExpression((Expression)Process(node.Expression, ctx));
    
        return builder.Build();
    }


    public Expression ProcessExpression(Expression node, TContext ctx)
    {


    var builder = ExpressionBuilder.CreateExpression();


        return builder.Build();
    }

}

