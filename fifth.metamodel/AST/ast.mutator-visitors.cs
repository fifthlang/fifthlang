

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


    public Assembly ProcessAssembly(Assembly node, TContext ctx)
    {
        var result = AssemblyBuilder.CreateAssembly()
            .WithName(node.Name)
            .WithPublicKeyToken(node.PublicKeyToken)
            .WithVersion(node.Version)
            .WithProgram(node.Program)
            .WithReferences(node.References)
        .Build();
        return result;
    }


    public AssemblyRef ProcessAssemblyRef(AssemblyRef node, TContext ctx)
    {
        var result = AssemblyRefBuilder.CreateAssemblyRef()
            .WithName(node.Name)
            .WithPublicKeyToken(node.PublicKeyToken)
            .WithVersion(node.Version)
        .Build();
        return result;
    }


    public ClassDefinition ProcessClassDefinition(ClassDefinition node, TContext ctx)
    {
        var result = ClassDefinitionBuilder.CreateClassDefinition()
            .WithName(node.Name)
            .WithProperties(node.Properties)
            .WithFunctions(node.Functions)
        .Build();
        return result;
    }


    public PropertyDefinition ProcessPropertyDefinition(PropertyDefinition node, TContext ctx)
    {
        var result = PropertyDefinitionBuilder.CreatePropertyDefinition()
            .WithName(node.Name)
            .WithTypeName(node.TypeName)
        .Build();
        return result;
    }


    public TypeCast ProcessTypeCast(TypeCast node, TContext ctx)
    {
        var result = TypeCastBuilder.CreateTypeCast()
            .WithSubExpression(node.SubExpression)
            .WithTargetTid(node.TargetTid)
        .Build();
        return result;
    }


    public ReturnStatement ProcessReturnStatement(ReturnStatement node, TContext ctx)
    {
        var result = ReturnStatementBuilder.CreateReturnStatement()
            .WithSubExpression(node.SubExpression)
            .WithTargetTid(node.TargetTid)
        .Build();
        return result;
    }


    public StatementList ProcessStatementList(StatementList node, TContext ctx)
    {
        var result = StatementListBuilder.CreateStatementList()
            .WithStatements(node.Statements)
        .Build();
        return result;
    }


    public AbsoluteIri ProcessAbsoluteIri(AbsoluteIri node, TContext ctx)
    {
        var result = AbsoluteIriBuilder.CreateAbsoluteIri()
            .WithUri(node.Uri)
        .Build();
        return result;
    }


    public AliasDeclaration ProcessAliasDeclaration(AliasDeclaration node, TContext ctx)
    {
        var result = AliasDeclarationBuilder.CreateAliasDeclaration()
            .WithIRI(node.IRI)
            .WithName(node.Name)
        .Build();
        return result;
    }


    public AssignmentStmt ProcessAssignmentStmt(AssignmentStmt node, TContext ctx)
    {
        var result = AssignmentStmtBuilder.CreateAssignmentStmt()
            .WithExpression(node.Expression)
            .WithVariableRef(node.VariableRef)
        .Build();
        return result;
    }


    public BinaryExpression ProcessBinaryExpression(BinaryExpression node, TContext ctx)
    {
        var result = BinaryExpressionBuilder.CreateBinaryExpression()
            .WithLeft(node.Left)
            .WithOp(node.Op)
            .WithRight(node.Right)
        .Build();
        return result;
    }


    public Block ProcessBlock(Block node, TContext ctx)
    {
        var result = BlockBuilder.CreateBlock()
            .WithStatements(node.Statements)
        .Build();
        return result;
    }


    public BoolValueExpression ProcessBoolValueExpression(BoolValueExpression node, TContext ctx)
    {
        var result = BoolValueExpressionBuilder.CreateBoolValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public ShortValueExpression ProcessShortValueExpression(ShortValueExpression node, TContext ctx)
    {
        var result = ShortValueExpressionBuilder.CreateShortValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public IntValueExpression ProcessIntValueExpression(IntValueExpression node, TContext ctx)
    {
        var result = IntValueExpressionBuilder.CreateIntValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public LongValueExpression ProcessLongValueExpression(LongValueExpression node, TContext ctx)
    {
        var result = LongValueExpressionBuilder.CreateLongValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public FloatValueExpression ProcessFloatValueExpression(FloatValueExpression node, TContext ctx)
    {
        var result = FloatValueExpressionBuilder.CreateFloatValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public DoubleValueExpression ProcessDoubleValueExpression(DoubleValueExpression node, TContext ctx)
    {
        var result = DoubleValueExpressionBuilder.CreateDoubleValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public DecimalValueExpression ProcessDecimalValueExpression(DecimalValueExpression node, TContext ctx)
    {
        var result = DecimalValueExpressionBuilder.CreateDecimalValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public StringValueExpression ProcessStringValueExpression(StringValueExpression node, TContext ctx)
    {
        var result = StringValueExpressionBuilder.CreateStringValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public DateValueExpression ProcessDateValueExpression(DateValueExpression node, TContext ctx)
    {
        var result = DateValueExpressionBuilder.CreateDateValueExpression()
            .WithTheValue(node.TheValue)
        .Build();
        return result;
    }


    public ExpressionList ProcessExpressionList(ExpressionList node, TContext ctx)
    {
        var result = ExpressionListBuilder.CreateExpressionList()
            .WithExpressions(node.Expressions)
        .Build();
        return result;
    }


    public FifthProgram ProcessFifthProgram(FifthProgram node, TContext ctx)
    {
        var result = FifthProgramBuilder.CreateFifthProgram()
            .WithAliases(node.Aliases)
            .WithClasses(node.Classes)
            .WithFunctions(node.Functions)
        .Build();
        return result;
    }


    public FuncCallExpression ProcessFuncCallExpression(FuncCallExpression node, TContext ctx)
    {
        var result = FuncCallExpressionBuilder.CreateFuncCallExpression()
            .WithActualParameters(node.ActualParameters)
            .WithName(node.Name)
        .Build();
        return result;
    }


    public FunctionDefinition ProcessFunctionDefinition(FunctionDefinition node, TContext ctx)
    {
        var result = FunctionDefinitionBuilder.CreateFunctionDefinition()
            .WithParameterDeclarations(node.ParameterDeclarations)
            .WithBody(node.Body)
            .WithTypename(node.Typename)
            .WithName(node.Name)
            .WithIsEntryPoint(node.IsEntryPoint)
            .WithReturnType(node.ReturnType)
        .Build();
        return result;
    }


    public BuiltinFunctionDefinition ProcessBuiltinFunctionDefinition(BuiltinFunctionDefinition node, TContext ctx)
    {
        var result = BuiltinFunctionDefinitionBuilder.CreateBuiltinFunctionDefinition()
        .Build();
        return result;
    }


    public OverloadedFunctionDefinition ProcessOverloadedFunctionDefinition(OverloadedFunctionDefinition node, TContext ctx)
    {
        var result = OverloadedFunctionDefinitionBuilder.CreateOverloadedFunctionDefinition()
            .WithOverloadClauses(node.OverloadClauses)
            .WithSignature(node.Signature)
        .Build();
        return result;
    }


    public Identifier ProcessIdentifier(Identifier node, TContext ctx)
    {
        var result = IdentifierBuilder.CreateIdentifier()
            .WithValue(node.Value)
        .Build();
        return result;
    }


    public IdentifierExpression ProcessIdentifierExpression(IdentifierExpression node, TContext ctx)
    {
        var result = IdentifierExpressionBuilder.CreateIdentifierExpression()
            .WithIdentifier(node.Identifier)
        .Build();
        return result;
    }


    public IfElseStatement ProcessIfElseStatement(IfElseStatement node, TContext ctx)
    {
        var result = IfElseStatementBuilder.CreateIfElseStatement()
            .WithIfBlock(node.IfBlock)
            .WithElseBlock(node.ElseBlock)
            .WithCondition(node.Condition)
        .Build();
        return result;
    }


    public ModuleImport ProcessModuleImport(ModuleImport node, TContext ctx)
    {
        var result = ModuleImportBuilder.CreateModuleImport()
            .WithModuleName(node.ModuleName)
        .Build();
        return result;
    }


    public ParameterDeclaration ProcessParameterDeclaration(ParameterDeclaration node, TContext ctx)
    {
        var result = ParameterDeclarationBuilder.CreateParameterDeclaration()
            .WithParameterName(node.ParameterName)
            .WithTypeName(node.TypeName)
            .WithConstraint(node.Constraint)
        .Build();
        return result;
    }


    public ParameterDeclarationList ProcessParameterDeclarationList(ParameterDeclarationList node, TContext ctx)
    {
        var result = ParameterDeclarationListBuilder.CreateParameterDeclarationList()
            .WithParameterDeclarations(node.ParameterDeclarations)
        .Build();
        return result;
    }


    public TypeCreateInstExpression ProcessTypeCreateInstExpression(TypeCreateInstExpression node, TContext ctx)
    {
        var result = TypeCreateInstExpressionBuilder.CreateTypeCreateInstExpression()
        .Build();
        return result;
    }


    public TypeInitialiser ProcessTypeInitialiser(TypeInitialiser node, TContext ctx)
    {
        var result = TypeInitialiserBuilder.CreateTypeInitialiser()
            .WithTypeName(node.TypeName)
            .WithPropertyInitialisers(node.PropertyInitialisers)
        .Build();
        return result;
    }


    public DestructuringParamDecl ProcessDestructuringParamDecl(DestructuringParamDecl node, TContext ctx)
    {
        var result = DestructuringParamDeclBuilder.CreateDestructuringParamDecl()
            .WithTypeName(node.TypeName)
            .WithParameterName(node.ParameterName)
            .WithPropertyBindings(node.PropertyBindings)
        .Build();
        return result;
    }


    public PropertyBinding ProcessPropertyBinding(PropertyBinding node, TContext ctx)
    {
        var result = PropertyBindingBuilder.CreatePropertyBinding()
            .WithBoundPropertyName(node.BoundPropertyName)
            .WithBoundVariableName(node.BoundVariableName)
            .WithConstraint(node.Constraint)
        .Build();
        return result;
    }


    public TypePropertyInit ProcessTypePropertyInit(TypePropertyInit node, TContext ctx)
    {
        var result = TypePropertyInitBuilder.CreateTypePropertyInit()
            .WithName(node.Name)
            .WithValue(node.Value)
        .Build();
        return result;
    }


    public UnaryExpression ProcessUnaryExpression(UnaryExpression node, TContext ctx)
    {
        var result = UnaryExpressionBuilder.CreateUnaryExpression()
            .WithOperand(node.Operand)
            .WithOp(node.Op)
        .Build();
        return result;
    }


    public VariableDeclarationStatement ProcessVariableDeclarationStatement(VariableDeclarationStatement node, TContext ctx)
    {
        var result = VariableDeclarationStatementBuilder.CreateVariableDeclarationStatement()
            .WithExpression(node.Expression)
            .WithName(node.Name)
        .Build();
        return result;
    }


    public VariableReference ProcessVariableReference(VariableReference node, TContext ctx)
    {
        var result = VariableReferenceBuilder.CreateVariableReference()
            .WithName(node.Name)
        .Build();
        return result;
    }


    public CompoundVariableReference ProcessCompoundVariableReference(CompoundVariableReference node, TContext ctx)
    {
        var result = CompoundVariableReferenceBuilder.CreateCompoundVariableReference()
            .WithComponentReferences(node.ComponentReferences)
        .Build();
        return result;
    }


    public WhileExp ProcessWhileExp(WhileExp node, TContext ctx)
    {
        var result = WhileExpBuilder.CreateWhileExp()
            .WithCondition(node.Condition)
            .WithLoopBlock(node.LoopBlock)
        .Build();
        return result;
    }


    public ExpressionStatement ProcessExpressionStatement(ExpressionStatement node, TContext ctx)
    {
        var result = ExpressionStatementBuilder.CreateExpressionStatement()
            .WithExpression(node.Expression)
        .Build();
        return result;
    }


    public Expression ProcessExpression(Expression node, TContext ctx)
    {
        var result = ExpressionBuilder.CreateExpression()
        .Build();
        return result;
    }

}

