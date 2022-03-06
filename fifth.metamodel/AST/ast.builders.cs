
namespace Fifth.AST.Builders;

using System;
using Symbols;
using Visitors;
using TypeSystem;
using PrimitiveTypes;
using TypeSystem.PrimitiveTypes;
using System.Collections.Generic;

public interface INodeBuilder{}

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AssemblyBuilder : INodeBuilder
    {
        private AssemblyBuilder()
        {
        _References = new List<AssemblyRef>();
        }

        public static AssemblyBuilder CreateAssembly() => new ();
        public Assembly Build()
          => new (_Name, _PublicKeyToken, _Version, _Program, _References);

        private string _Name;
        public AssemblyBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _PublicKeyToken;
        public AssemblyBuilder WithPublicKeyToken(string value){
            _PublicKeyToken = value;
            return this;
        }

        private string _Version;
        public AssemblyBuilder WithVersion(string value){
            _Version = value;
            return this;
        }

        private FifthProgram _Program;
        public AssemblyBuilder WithProgram(FifthProgram value){
            _Program = value;
            return this;
        }

        private List<AssemblyRef> _References;
        public AssemblyBuilder WithReferences(List<AssemblyRef> value){
            _References = value;
            return this;
        }

        public AssemblyBuilder AddingItemToReferences(AssemblyRef value){
            _References.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AssemblyRefBuilder : INodeBuilder
    {
        private AssemblyRefBuilder()
        {
        }

        public static AssemblyRefBuilder CreateAssemblyRef() => new ();
        public AssemblyRef Build()
          => new (_Name, _PublicKeyToken, _Version);

        private string _Name;
        public AssemblyRefBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _PublicKeyToken;
        public AssemblyRefBuilder WithPublicKeyToken(string value){
            _PublicKeyToken = value;
            return this;
        }

        private string _Version;
        public AssemblyRefBuilder WithVersion(string value){
            _Version = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ClassDefinitionBuilder : INodeBuilder
    {
        private ClassDefinitionBuilder()
        {
        _Properties = new List<PropertyDefinition>();
        _Functions = new List<IFunctionDefinition>();
        }

        public static ClassDefinitionBuilder CreateClassDefinition() => new ();
        public ClassDefinition Build()
          => new (_Name, _Properties, _Functions);

        private string _Name;
        public ClassDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private List<PropertyDefinition> _Properties;
        public ClassDefinitionBuilder WithProperties(List<PropertyDefinition> value){
            _Properties = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToProperties(PropertyDefinition value){
            _Properties.Add(value);
            return this;
        }
        private List<IFunctionDefinition> _Functions;
        public ClassDefinitionBuilder WithFunctions(List<IFunctionDefinition> value){
            _Functions = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToFunctions(IFunctionDefinition value){
            _Functions.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class PropertyDefinitionBuilder : INodeBuilder
    {
        private PropertyDefinitionBuilder()
        {
        }

        public static PropertyDefinitionBuilder CreatePropertyDefinition() => new ();
        public PropertyDefinition Build()
          => new (_Name, _TypeName);

        private string _Name;
        public PropertyDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _TypeName;
        public PropertyDefinitionBuilder WithTypeName(string value){
            _TypeName = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypeCastBuilder : INodeBuilder
    {
        private TypeCastBuilder()
        {
        }

        public static TypeCastBuilder CreateTypeCast() => new ();
        public TypeCast Build()
          => new (_SubExpression, _TargetTid);

        private Expression _SubExpression;
        public TypeCastBuilder WithSubExpression(Expression value){
            _SubExpression = value;
            return this;
        }

        private TypeId _TargetTid;
        public TypeCastBuilder WithTargetTid(TypeId value){
            _TargetTid = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ReturnStatementBuilder : INodeBuilder
    {
        private ReturnStatementBuilder()
        {
        }

        public static ReturnStatementBuilder CreateReturnStatement() => new ();
        public ReturnStatement Build()
          => new (_SubExpression, _TargetTid);

        private Expression _SubExpression;
        public ReturnStatementBuilder WithSubExpression(Expression value){
            _SubExpression = value;
            return this;
        }

        private TypeId _TargetTid;
        public ReturnStatementBuilder WithTargetTid(TypeId value){
            _TargetTid = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class StatementListBuilder : INodeBuilder
    {
        private StatementListBuilder()
        {
        _Statements = new List<Statement>();
        }

        public static StatementListBuilder CreateStatementList() => new ();
        public StatementList Build()
          => new (_Statements);

        private List<Statement> _Statements;
        public StatementListBuilder WithStatements(List<Statement> value){
            _Statements = value;
            return this;
        }

        public StatementListBuilder AddingItemToStatements(Statement value){
            _Statements.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AbsoluteIriBuilder : INodeBuilder
    {
        private AbsoluteIriBuilder()
        {
        }

        public static AbsoluteIriBuilder CreateAbsoluteIri() => new ();
        public AbsoluteIri Build()
          => new (_Uri);

        private string _Uri;
        public AbsoluteIriBuilder WithUri(string value){
            _Uri = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AliasDeclarationBuilder : INodeBuilder
    {
        private AliasDeclarationBuilder()
        {
        }

        public static AliasDeclarationBuilder CreateAliasDeclaration() => new ();
        public AliasDeclaration Build()
          => new (_IRI, _Name);

        private AbsoluteIri _IRI;
        public AliasDeclarationBuilder WithIRI(AbsoluteIri value){
            _IRI = value;
            return this;
        }

        private string _Name;
        public AliasDeclarationBuilder WithName(string value){
            _Name = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AssignmentStmtBuilder : INodeBuilder
    {
        private AssignmentStmtBuilder()
        {
        }

        public static AssignmentStmtBuilder CreateAssignmentStmt() => new ();
        public AssignmentStmt Build()
          => new (_Expression, _VariableRef);

        private Expression _Expression;
        public AssignmentStmtBuilder WithExpression(Expression value){
            _Expression = value;
            return this;
        }

        private BaseVarReference _VariableRef;
        public AssignmentStmtBuilder WithVariableRef(BaseVarReference value){
            _VariableRef = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class BinaryExpressionBuilder : INodeBuilder
    {
        private BinaryExpressionBuilder()
        {
        }

        public static BinaryExpressionBuilder CreateBinaryExpression() => new ();
        public BinaryExpression Build()
          => new (_Left, _Op, _Right);

        private Expression _Left;
        public BinaryExpressionBuilder WithLeft(Expression value){
            _Left = value;
            return this;
        }

        private Operator? _Op;
        public BinaryExpressionBuilder WithOp(Operator? value){
            _Op = value;
            return this;
        }

        private Expression _Right;
        public BinaryExpressionBuilder WithRight(Expression value){
            _Right = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class BlockBuilder : INodeBuilder
    {
        private BlockBuilder()
        {
        _Statements = new List<Statement>();
        }

        public static BlockBuilder CreateBlock() => new ();
        public Block Build()
          => new (_Statements);

        private List<Statement> _Statements;
        public BlockBuilder WithStatements(List<Statement> value){
            _Statements = value;
            return this;
        }

        public BlockBuilder AddingItemToStatements(Statement value){
            _Statements.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class BoolValueExpressionBuilder : INodeBuilder
    {
        private BoolValueExpressionBuilder()
        {
        }

        public static BoolValueExpressionBuilder CreateBoolValueExpression() => new ();
        public BoolValueExpression Build()
          => new (_TheValue);

        private bool _TheValue;
        public BoolValueExpressionBuilder WithTheValue(bool value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ShortValueExpressionBuilder : INodeBuilder
    {
        private ShortValueExpressionBuilder()
        {
        }

        public static ShortValueExpressionBuilder CreateShortValueExpression() => new ();
        public ShortValueExpression Build()
          => new (_TheValue);

        private short _TheValue;
        public ShortValueExpressionBuilder WithTheValue(short value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IntValueExpressionBuilder : INodeBuilder
    {
        private IntValueExpressionBuilder()
        {
        }

        public static IntValueExpressionBuilder CreateIntValueExpression() => new ();
        public IntValueExpression Build()
          => new (_TheValue);

        private int _TheValue;
        public IntValueExpressionBuilder WithTheValue(int value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class LongValueExpressionBuilder : INodeBuilder
    {
        private LongValueExpressionBuilder()
        {
        }

        public static LongValueExpressionBuilder CreateLongValueExpression() => new ();
        public LongValueExpression Build()
          => new (_TheValue);

        private long _TheValue;
        public LongValueExpressionBuilder WithTheValue(long value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class FloatValueExpressionBuilder : INodeBuilder
    {
        private FloatValueExpressionBuilder()
        {
        }

        public static FloatValueExpressionBuilder CreateFloatValueExpression() => new ();
        public FloatValueExpression Build()
          => new (_TheValue);

        private float _TheValue;
        public FloatValueExpressionBuilder WithTheValue(float value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DoubleValueExpressionBuilder : INodeBuilder
    {
        private DoubleValueExpressionBuilder()
        {
        }

        public static DoubleValueExpressionBuilder CreateDoubleValueExpression() => new ();
        public DoubleValueExpression Build()
          => new (_TheValue);

        private double _TheValue;
        public DoubleValueExpressionBuilder WithTheValue(double value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DecimalValueExpressionBuilder : INodeBuilder
    {
        private DecimalValueExpressionBuilder()
        {
        }

        public static DecimalValueExpressionBuilder CreateDecimalValueExpression() => new ();
        public DecimalValueExpression Build()
          => new (_TheValue);

        private decimal _TheValue;
        public DecimalValueExpressionBuilder WithTheValue(decimal value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class StringValueExpressionBuilder : INodeBuilder
    {
        private StringValueExpressionBuilder()
        {
        }

        public static StringValueExpressionBuilder CreateStringValueExpression() => new ();
        public StringValueExpression Build()
          => new (_TheValue);

        private string _TheValue;
        public StringValueExpressionBuilder WithTheValue(string value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DateValueExpressionBuilder : INodeBuilder
    {
        private DateValueExpressionBuilder()
        {
        }

        public static DateValueExpressionBuilder CreateDateValueExpression() => new ();
        public DateValueExpression Build()
          => new (_TheValue);

        private DateTimeOffset _TheValue;
        public DateValueExpressionBuilder WithTheValue(DateTimeOffset value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ExpressionListBuilder : INodeBuilder
    {
        private ExpressionListBuilder()
        {
        _Expressions = new List<Expression>();
        }

        public static ExpressionListBuilder CreateExpressionList() => new ();
        public ExpressionList Build()
          => new (_Expressions);

        private List<Expression> _Expressions;
        public ExpressionListBuilder WithExpressions(List<Expression> value){
            _Expressions = value;
            return this;
        }

        public ExpressionListBuilder AddingItemToExpressions(Expression value){
            _Expressions.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class FifthProgramBuilder : INodeBuilder
    {
        private FifthProgramBuilder()
        {
        _Aliases = new List<AliasDeclaration>();
        _Classes = new List<ClassDefinition>();
        _Functions = new List<IFunctionDefinition>();
        }

        public static FifthProgramBuilder CreateFifthProgram() => new ();
        public FifthProgram Build()
          => new (_Aliases, _Classes, _Functions);

        private List<AliasDeclaration> _Aliases;
        public FifthProgramBuilder WithAliases(List<AliasDeclaration> value){
            _Aliases = value;
            return this;
        }

        public FifthProgramBuilder AddingItemToAliases(AliasDeclaration value){
            _Aliases.Add(value);
            return this;
        }
        private List<ClassDefinition> _Classes;
        public FifthProgramBuilder WithClasses(List<ClassDefinition> value){
            _Classes = value;
            return this;
        }

        public FifthProgramBuilder AddingItemToClasses(ClassDefinition value){
            _Classes.Add(value);
            return this;
        }
        private List<IFunctionDefinition> _Functions;
        public FifthProgramBuilder WithFunctions(List<IFunctionDefinition> value){
            _Functions = value;
            return this;
        }

        public FifthProgramBuilder AddingItemToFunctions(IFunctionDefinition value){
            _Functions.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class FuncCallExpressionBuilder : INodeBuilder
    {
        private FuncCallExpressionBuilder()
        {
        }

        public static FuncCallExpressionBuilder CreateFuncCallExpression() => new ();
        public FuncCallExpression Build()
          => new (_ActualParameters, _Name);

        private ExpressionList _ActualParameters;
        public FuncCallExpressionBuilder WithActualParameters(ExpressionList value){
            _ActualParameters = value;
            return this;
        }

        private string _Name;
        public FuncCallExpressionBuilder WithName(string value){
            _Name = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class FunctionDefinitionBuilder : INodeBuilder
    {
        private FunctionDefinitionBuilder()
        {
        }

        public static FunctionDefinitionBuilder CreateFunctionDefinition() => new ();
        public FunctionDefinition Build()
          => new (_ParameterDeclarations, _Body, _Typename, _Name, _IsEntryPoint, _ReturnType);

        private ParameterDeclarationList _ParameterDeclarations;
        public FunctionDefinitionBuilder WithParameterDeclarations(ParameterDeclarationList value){
            _ParameterDeclarations = value;
            return this;
        }

        private Block _Body;
        public FunctionDefinitionBuilder WithBody(Block value){
            _Body = value;
            return this;
        }

        private string _Typename;
        public FunctionDefinitionBuilder WithTypename(string value){
            _Typename = value;
            return this;
        }

        private string _Name;
        public FunctionDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private bool _IsEntryPoint;
        public FunctionDefinitionBuilder WithIsEntryPoint(bool value){
            _IsEntryPoint = value;
            return this;
        }

        private TypeId _ReturnType;
        public FunctionDefinitionBuilder WithReturnType(TypeId value){
            _ReturnType = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class BuiltinFunctionDefinitionBuilder : INodeBuilder
    {
        private BuiltinFunctionDefinitionBuilder()
        {
        }

        public static BuiltinFunctionDefinitionBuilder CreateBuiltinFunctionDefinition() => new ();
        public BuiltinFunctionDefinition Build()
          => new ();

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class OverloadedFunctionDefinitionBuilder : INodeBuilder
    {
        private OverloadedFunctionDefinitionBuilder()
        {
        _OverloadClauses = new List<IFunctionDefinition>();
        }

        public static OverloadedFunctionDefinitionBuilder CreateOverloadedFunctionDefinition() => new ();
        public OverloadedFunctionDefinition Build()
          => new (_OverloadClauses, _Signature);

        private List<IFunctionDefinition> _OverloadClauses;
        public OverloadedFunctionDefinitionBuilder WithOverloadClauses(List<IFunctionDefinition> value){
            _OverloadClauses = value;
            return this;
        }

        public OverloadedFunctionDefinitionBuilder AddingItemToOverloadClauses(IFunctionDefinition value){
            _OverloadClauses.Add(value);
            return this;
        }
        private IFunctionSignature _Signature;
        public OverloadedFunctionDefinitionBuilder WithSignature(IFunctionSignature value){
            _Signature = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IdentifierBuilder : INodeBuilder
    {
        private IdentifierBuilder()
        {
        }

        public static IdentifierBuilder CreateIdentifier() => new ();
        public Identifier Build()
          => new (_Value);

        private string _Value;
        public IdentifierBuilder WithValue(string value){
            _Value = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IdentifierExpressionBuilder : INodeBuilder
    {
        private IdentifierExpressionBuilder()
        {
        }

        public static IdentifierExpressionBuilder CreateIdentifierExpression() => new ();
        public IdentifierExpression Build()
          => new (_Identifier);

        private Identifier _Identifier;
        public IdentifierExpressionBuilder WithIdentifier(Identifier value){
            _Identifier = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IfElseStatementBuilder : INodeBuilder
    {
        private IfElseStatementBuilder()
        {
        }

        public static IfElseStatementBuilder CreateIfElseStatement() => new ();
        public IfElseStatement Build()
          => new (_IfBlock, _ElseBlock, _Condition);

        private Block _IfBlock;
        public IfElseStatementBuilder WithIfBlock(Block value){
            _IfBlock = value;
            return this;
        }

        private Block _ElseBlock;
        public IfElseStatementBuilder WithElseBlock(Block value){
            _ElseBlock = value;
            return this;
        }

        private Expression _Condition;
        public IfElseStatementBuilder WithCondition(Expression value){
            _Condition = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ModuleImportBuilder : INodeBuilder
    {
        private ModuleImportBuilder()
        {
        }

        public static ModuleImportBuilder CreateModuleImport() => new ();
        public ModuleImport Build()
          => new (_ModuleName);

        private string _ModuleName;
        public ModuleImportBuilder WithModuleName(string value){
            _ModuleName = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DestructuringParamDeclBuilder : INodeBuilder
    {
        private DestructuringParamDeclBuilder()
        {
        _PropertyBindings = new List<PropertyBinding>();
        }

        public static DestructuringParamDeclBuilder CreateDestructuringParamDecl() => new ();
        public DestructuringParamDecl Build()
          => new (_PropertyBindings);

        private List<PropertyBinding> _PropertyBindings;
        public DestructuringParamDeclBuilder WithPropertyBindings(List<PropertyBinding> value){
            _PropertyBindings = value;
            return this;
        }

        public DestructuringParamDeclBuilder AddingItemToPropertyBindings(PropertyBinding value){
            _PropertyBindings.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ParameterDeclarationBuilder : INodeBuilder
    {
        private ParameterDeclarationBuilder()
        {
        }

        public static ParameterDeclarationBuilder CreateParameterDeclaration() => new ();
        public ParameterDeclaration Build()
          => new (_ParameterName, _TypeName, _Constraint);

        private Identifier _ParameterName;
        public ParameterDeclarationBuilder WithParameterName(Identifier value){
            _ParameterName = value;
            return this;
        }

        private string _TypeName;
        public ParameterDeclarationBuilder WithTypeName(string value){
            _TypeName = value;
            return this;
        }

        private Expression _Constraint;
        public ParameterDeclarationBuilder WithConstraint(Expression value){
            _Constraint = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ParameterDeclarationListBuilder : INodeBuilder
    {
        private ParameterDeclarationListBuilder()
        {
        _ParameterDeclarations = new List<IParameterListItem>();
        }

        public static ParameterDeclarationListBuilder CreateParameterDeclarationList() => new ();
        public ParameterDeclarationList Build()
          => new (_ParameterDeclarations);

        private List<IParameterListItem> _ParameterDeclarations;
        public ParameterDeclarationListBuilder WithParameterDeclarations(List<IParameterListItem> value){
            _ParameterDeclarations = value;
            return this;
        }

        public ParameterDeclarationListBuilder AddingItemToParameterDeclarations(IParameterListItem value){
            _ParameterDeclarations.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypeCreateInstExpressionBuilder : INodeBuilder
    {
        private TypeCreateInstExpressionBuilder()
        {
        }

        public static TypeCreateInstExpressionBuilder CreateTypeCreateInstExpression() => new ();
        public TypeCreateInstExpression Build()
          => new ();

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypeInitialiserBuilder : INodeBuilder
    {
        private TypeInitialiserBuilder()
        {
        _PropertyInitialisers = new List<TypePropertyInit>();
        }

        public static TypeInitialiserBuilder CreateTypeInitialiser() => new ();
        public TypeInitialiser Build()
          => new (_TypeName, _PropertyInitialisers);

        private string _TypeName;
        public TypeInitialiserBuilder WithTypeName(string value){
            _TypeName = value;
            return this;
        }

        private List<TypePropertyInit> _PropertyInitialisers;
        public TypeInitialiserBuilder WithPropertyInitialisers(List<TypePropertyInit> value){
            _PropertyInitialisers = value;
            return this;
        }

        public TypeInitialiserBuilder AddingItemToPropertyInitialisers(TypePropertyInit value){
            _PropertyInitialisers.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class PropertyBindingBuilder : INodeBuilder
    {
        private PropertyBindingBuilder()
        {
        }

        public static PropertyBindingBuilder CreatePropertyBinding() => new ();
        public PropertyBinding Build()
          => new (_BoundPropertyName, _BoundVariableName, _Constraint);

        private string _BoundPropertyName;
        public PropertyBindingBuilder WithBoundPropertyName(string value){
            _BoundPropertyName = value;
            return this;
        }

        private string _BoundVariableName;
        public PropertyBindingBuilder WithBoundVariableName(string value){
            _BoundVariableName = value;
            return this;
        }

        private Expression _Constraint;
        public PropertyBindingBuilder WithConstraint(Expression value){
            _Constraint = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypePropertyInitBuilder : INodeBuilder
    {
        private TypePropertyInitBuilder()
        {
        }

        public static TypePropertyInitBuilder CreateTypePropertyInit() => new ();
        public TypePropertyInit Build()
          => new (_Name, _Value);

        private string _Name;
        public TypePropertyInitBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private Expression _Value;
        public TypePropertyInitBuilder WithValue(Expression value){
            _Value = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class UnaryExpressionBuilder : INodeBuilder
    {
        private UnaryExpressionBuilder()
        {
        }

        public static UnaryExpressionBuilder CreateUnaryExpression() => new ();
        public UnaryExpression Build()
          => new (_Operand, _Op);

        private Expression _Operand;
        public UnaryExpressionBuilder WithOperand(Expression value){
            _Operand = value;
            return this;
        }

        private Operator _Op;
        public UnaryExpressionBuilder WithOp(Operator value){
            _Op = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class VariableDeclarationStatementBuilder : INodeBuilder
    {
        private VariableDeclarationStatementBuilder()
        {
        }

        public static VariableDeclarationStatementBuilder CreateVariableDeclarationStatement() => new ();
        public VariableDeclarationStatement Build()
          => new (_Expression, _Name);

        private Expression _Expression;
        public VariableDeclarationStatementBuilder WithExpression(Expression value){
            _Expression = value;
            return this;
        }

        private Identifier _Name;
        public VariableDeclarationStatementBuilder WithName(Identifier value){
            _Name = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class VariableReferenceBuilder : INodeBuilder
    {
        private VariableReferenceBuilder()
        {
        }

        public static VariableReferenceBuilder CreateVariableReference() => new ();
        public VariableReference Build()
          => new (_Name);

        private string _Name;
        public VariableReferenceBuilder WithName(string value){
            _Name = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class CompoundVariableReferenceBuilder : INodeBuilder
    {
        private CompoundVariableReferenceBuilder()
        {
        _ComponentReferences = new List<VariableReference>();
        }

        public static CompoundVariableReferenceBuilder CreateCompoundVariableReference() => new ();
        public CompoundVariableReference Build()
          => new (_ComponentReferences);

        private List<VariableReference> _ComponentReferences;
        public CompoundVariableReferenceBuilder WithComponentReferences(List<VariableReference> value){
            _ComponentReferences = value;
            return this;
        }

        public CompoundVariableReferenceBuilder AddingItemToComponentReferences(VariableReference value){
            _ComponentReferences.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class WhileExpBuilder : INodeBuilder
    {
        private WhileExpBuilder()
        {
        }

        public static WhileExpBuilder CreateWhileExp() => new ();
        public WhileExp Build()
          => new (_Condition, _LoopBlock);

        private Expression _Condition;
        public WhileExpBuilder WithCondition(Expression value){
            _Condition = value;
            return this;
        }

        private Block _LoopBlock;
        public WhileExpBuilder WithLoopBlock(Block value){
            _LoopBlock = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ExpressionStatementBuilder : INodeBuilder
    {
        private ExpressionStatementBuilder()
        {
        }

        public static ExpressionStatementBuilder CreateExpressionStatement() => new ();
        public ExpressionStatement Build()
          => new (_Expression);

        private Expression _Expression;
        public ExpressionStatementBuilder WithExpression(Expression value){
            _Expression = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ExpressionBuilder : INodeBuilder
    {
        private ExpressionBuilder()
        {
        }

        public static ExpressionBuilder CreateExpression() => new ();
        public Expression Build()
          => new ();

    }

