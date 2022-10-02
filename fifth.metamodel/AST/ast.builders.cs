
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

        public static AssemblyBuilder CreateAssembly()
        {
            return new();
        }

        public Assembly Build()
        {
            return new(_Name, _PublicKeyToken, _Version, _Program, _References);
        }

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

        public static AssemblyRefBuilder CreateAssemblyRef()
        {
            return new();
        }

        public AssemblyRef Build()
        {
            return new(_Name, _PublicKeyToken, _Version);
        }

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
        _Fields = new List<FieldDefinition>();
        _Properties = new List<PropertyDefinition>();
        _Functions = new List<IFunctionDefinition>();
        }

        public static ClassDefinitionBuilder CreateClassDefinition()
        {
            return new();
        }

        public ClassDefinition Build()
        {
            return new(_Name, _Fields, _Properties, _Functions);
        }

        private string _Name;
        public ClassDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private List<FieldDefinition> _Fields;
        public ClassDefinitionBuilder WithFields(List<FieldDefinition> value){
            _Fields = value;
            return this;
        }

        public ClassDefinitionBuilder AddingItemToFields(FieldDefinition value){
            _Fields.Add(value);
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
    public partial class FieldDefinitionBuilder : INodeBuilder
    {
        private FieldDefinitionBuilder()
        {
        }

        public static FieldDefinitionBuilder CreateFieldDefinition()
        {
            return new();
        }

        public FieldDefinition Build()
        {
            return new(_BackingFieldFor, _Name, _TypeName);
        }

        private PropertyDefinition? _BackingFieldFor;
        public FieldDefinitionBuilder WithBackingFieldFor(PropertyDefinition? value){
            _BackingFieldFor = value;
            return this;
        }

        private string _Name;
        public FieldDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _TypeName;
        public FieldDefinitionBuilder WithTypeName(string value){
            _TypeName = value;
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

        public static PropertyDefinitionBuilder CreatePropertyDefinition()
        {
            return new();
        }

        public PropertyDefinition Build()
        {
            return new(_BackingField, _GetAccessor, _SetAccessor, _Name, _TypeName);
        }

        private FieldDefinition? _BackingField;
        public PropertyDefinitionBuilder WithBackingField(FieldDefinition? value){
            _BackingField = value;
            return this;
        }

        private FunctionDefinition? _GetAccessor;
        public PropertyDefinitionBuilder WithGetAccessor(FunctionDefinition? value){
            _GetAccessor = value;
            return this;
        }

        private FunctionDefinition? _SetAccessor;
        public PropertyDefinitionBuilder WithSetAccessor(FunctionDefinition? value){
            _SetAccessor = value;
            return this;
        }

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

        public static TypeCastBuilder CreateTypeCast()
        {
            return new();
        }

        public TypeCast Build()
        {
            return new(_SubExpression, _TargetTid);
        }

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

        public static ReturnStatementBuilder CreateReturnStatement()
        {
            return new();
        }

        public ReturnStatement Build()
        {
            return new(_SubExpression, _TargetTid);
        }

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

        public static StatementListBuilder CreateStatementList()
        {
            return new();
        }

        public StatementList Build()
        {
            return new(_Statements);
        }

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

        public static AbsoluteIriBuilder CreateAbsoluteIri()
        {
            return new();
        }

        public AbsoluteIri Build()
        {
            return new(_Uri);
        }

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

        public static AliasDeclarationBuilder CreateAliasDeclaration()
        {
            return new();
        }

        public AliasDeclaration Build()
        {
            return new(_IRI, _Name);
        }

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

        public static AssignmentStmtBuilder CreateAssignmentStmt()
        {
            return new();
        }

        public AssignmentStmt Build()
        {
            return new(_Expression, _VariableRef);
        }

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

        public static BinaryExpressionBuilder CreateBinaryExpression()
        {
            return new();
        }

        public BinaryExpression Build()
        {
            return new(_Left, _Op, _Right);
        }

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

        public static BlockBuilder CreateBlock()
        {
            return new();
        }

        public Block Build()
        {
            return new(_Statements);
        }

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

        public static BoolValueExpressionBuilder CreateBoolValueExpression()
        {
            return new();
        }

        public BoolValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static ShortValueExpressionBuilder CreateShortValueExpression()
        {
            return new();
        }

        public ShortValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static IntValueExpressionBuilder CreateIntValueExpression()
        {
            return new();
        }

        public IntValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static LongValueExpressionBuilder CreateLongValueExpression()
        {
            return new();
        }

        public LongValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static FloatValueExpressionBuilder CreateFloatValueExpression()
        {
            return new();
        }

        public FloatValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static DoubleValueExpressionBuilder CreateDoubleValueExpression()
        {
            return new();
        }

        public DoubleValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static DecimalValueExpressionBuilder CreateDecimalValueExpression()
        {
            return new();
        }

        public DecimalValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static StringValueExpressionBuilder CreateStringValueExpression()
        {
            return new();
        }

        public StringValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static DateValueExpressionBuilder CreateDateValueExpression()
        {
            return new();
        }

        public DateValueExpression Build()
        {
            return new(_TheValue);
        }

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

        public static ExpressionListBuilder CreateExpressionList()
        {
            return new();
        }

        public ExpressionList Build()
        {
            return new(_Expressions);
        }

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

        public static FifthProgramBuilder CreateFifthProgram()
        {
            return new();
        }

        public FifthProgram Build()
        {
            return new(_Aliases, _Classes, _Functions);
        }

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

        public static FuncCallExpressionBuilder CreateFuncCallExpression()
        {
            return new();
        }

        public FuncCallExpression Build()
        {
            return new(_ActualParameters, _Name);
        }

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
    public partial class BuiltinFunctionDefinitionBuilder : INodeBuilder
    {
        private BuiltinFunctionDefinitionBuilder()
        {
        }

        public static BuiltinFunctionDefinitionBuilder CreateBuiltinFunctionDefinition()
        {
            return new();
        }

        public BuiltinFunctionDefinition Build()
        {
            return new(_ParameterDeclarations, _Body, _Typename, _Name, _IsEntryPoint, _FunctionKind, _ReturnType);
        }

        private ParameterDeclarationList _ParameterDeclarations;
        public BuiltinFunctionDefinitionBuilder WithParameterDeclarations(ParameterDeclarationList value){
            _ParameterDeclarations = value;
            return this;
        }

        private Block? _Body;
        public BuiltinFunctionDefinitionBuilder WithBody(Block? value){
            _Body = value;
            return this;
        }

        private string _Typename;
        public BuiltinFunctionDefinitionBuilder WithTypename(string value){
            _Typename = value;
            return this;
        }

        private string _Name;
        public BuiltinFunctionDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private bool _IsEntryPoint;
        public BuiltinFunctionDefinitionBuilder WithIsEntryPoint(bool value){
            _IsEntryPoint = value;
            return this;
        }

        private FunctionKind _FunctionKind;
        public BuiltinFunctionDefinitionBuilder WithFunctionKind(FunctionKind value){
            _FunctionKind = value;
            return this;
        }

        private TypeId _ReturnType;
        public BuiltinFunctionDefinitionBuilder WithReturnType(TypeId value){
            _ReturnType = value;
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

        public static FunctionDefinitionBuilder CreateFunctionDefinition()
        {
            return new();
        }

        public FunctionDefinition Build()
        {
            return new(_ParameterDeclarations, _Body, _Typename, _Name, _IsEntryPoint, _FunctionKind, _ReturnType);
        }

        private ParameterDeclarationList _ParameterDeclarations;
        public FunctionDefinitionBuilder WithParameterDeclarations(ParameterDeclarationList value){
            _ParameterDeclarations = value;
            return this;
        }

        private Block? _Body;
        public FunctionDefinitionBuilder WithBody(Block? value){
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

        private FunctionKind _FunctionKind;
        public FunctionDefinitionBuilder WithFunctionKind(FunctionKind value){
            _FunctionKind = value;
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
    public partial class OverloadedFunctionDefinitionBuilder : INodeBuilder
    {
        private OverloadedFunctionDefinitionBuilder()
        {
        _OverloadClauses = new List<IFunctionDefinition>();
        }

        public static OverloadedFunctionDefinitionBuilder CreateOverloadedFunctionDefinition()
        {
            return new();
        }

        public OverloadedFunctionDefinition Build()
        {
            return new(_OverloadClauses, _Signature);
        }

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

        public static IdentifierBuilder CreateIdentifier()
        {
            return new();
        }

        public Identifier Build()
        {
            return new(_Value);
        }

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

        public static IdentifierExpressionBuilder CreateIdentifierExpression()
        {
            return new();
        }

        public IdentifierExpression Build()
        {
            return new(_Identifier);
        }

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

        public static IfElseStatementBuilder CreateIfElseStatement()
        {
            return new();
        }

        public IfElseStatement Build()
        {
            return new(_IfBlock, _ElseBlock, _Condition);
        }

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

        public static ModuleImportBuilder CreateModuleImport()
        {
            return new();
        }

        public ModuleImport Build()
        {
            return new(_ModuleName);
        }

        private string _ModuleName;
        public ModuleImportBuilder WithModuleName(string value){
            _ModuleName = value;
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

        public static ParameterDeclarationListBuilder CreateParameterDeclarationList()
        {
            return new();
        }

        public ParameterDeclarationList Build()
        {
            return new(_ParameterDeclarations);
        }

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
    public partial class ParameterDeclarationBuilder : INodeBuilder
    {
        private ParameterDeclarationBuilder()
        {
        }

        public static ParameterDeclarationBuilder CreateParameterDeclaration()
        {
            return new();
        }

        public ParameterDeclaration Build()
        {
            return new(_ParameterName, _TypeName, _Constraint, _DestructuringDecl);
        }

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

        private DestructuringDeclaration _DestructuringDecl;
        public ParameterDeclarationBuilder WithDestructuringDecl(DestructuringDeclaration value){
            _DestructuringDecl = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DestructuringDeclarationBuilder : INodeBuilder
    {
        private DestructuringDeclarationBuilder()
        {
        _Bindings = new List<DestructuringBinding>();
        }

        public static DestructuringDeclarationBuilder CreateDestructuringDeclaration()
        {
            return new();
        }

        public DestructuringDeclaration Build()
        {
            return new(_Bindings);
        }

        private List<DestructuringBinding> _Bindings;
        public DestructuringDeclarationBuilder WithBindings(List<DestructuringBinding> value){
            _Bindings = value;
            return this;
        }

        public DestructuringDeclarationBuilder AddingItemToBindings(DestructuringBinding value){
            _Bindings.Add(value);
            return this;
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DestructuringBindingBuilder : INodeBuilder
    {
        private DestructuringBindingBuilder()
        {
        }

        public static DestructuringBindingBuilder CreateDestructuringBinding()
        {
            return new();
        }

        public DestructuringBinding Build()
        {
            return new(_Varname, _Propname, _PropDecl, _Constraint, _DestructuringDecl);
        }

        private string _Varname;
        public DestructuringBindingBuilder WithVarname(string value){
            _Varname = value;
            return this;
        }

        private string _Propname;
        public DestructuringBindingBuilder WithPropname(string value){
            _Propname = value;
            return this;
        }

        private PropertyDefinition _PropDecl;
        public DestructuringBindingBuilder WithPropDecl(PropertyDefinition value){
            _PropDecl = value;
            return this;
        }

        private Expression _Constraint;
        public DestructuringBindingBuilder WithConstraint(Expression value){
            _Constraint = value;
            return this;
        }

        private DestructuringDeclaration _DestructuringDecl;
        public DestructuringBindingBuilder WithDestructuringDecl(DestructuringDeclaration value){
            _DestructuringDecl = value;
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

        public static TypeCreateInstExpressionBuilder CreateTypeCreateInstExpression()
        {
            return new();
        }

        public TypeCreateInstExpression Build()
        {
            return new();
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypeInitialiserBuilder : INodeBuilder
    {
        private TypeInitialiserBuilder()
        {
        _PropertyInitialisers = new List<TypePropertyInit>();
        }

        public static TypeInitialiserBuilder CreateTypeInitialiser()
        {
            return new();
        }

        public TypeInitialiser Build()
        {
            return new(_TypeName, _PropertyInitialisers);
        }

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
    public partial class TypePropertyInitBuilder : INodeBuilder
    {
        private TypePropertyInitBuilder()
        {
        }

        public static TypePropertyInitBuilder CreateTypePropertyInit()
        {
            return new();
        }

        public TypePropertyInit Build()
        {
            return new(_Name, _Value);
        }

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

        public static UnaryExpressionBuilder CreateUnaryExpression()
        {
            return new();
        }

        public UnaryExpression Build()
        {
            return new(_Operand, _Op);
        }

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

        public static VariableDeclarationStatementBuilder CreateVariableDeclarationStatement()
        {
            return new();
        }

        public VariableDeclarationStatement Build()
        {
            return new(_Expression, _Name, _UnresolvedTypeName);
        }

        private Expression _Expression;
        public VariableDeclarationStatementBuilder WithExpression(Expression value){
            _Expression = value;
            return this;
        }

        private string _Name;
        public VariableDeclarationStatementBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _UnresolvedTypeName;
        public VariableDeclarationStatementBuilder WithUnresolvedTypeName(string value){
            _UnresolvedTypeName = value;
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

        public static VariableReferenceBuilder CreateVariableReference()
        {
            return new();
        }

        public VariableReference Build()
        {
            return new(_Name);
        }

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

        public static CompoundVariableReferenceBuilder CreateCompoundVariableReference()
        {
            return new();
        }

        public CompoundVariableReference Build()
        {
            return new(_ComponentReferences);
        }

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

        public static WhileExpBuilder CreateWhileExp()
        {
            return new();
        }

        public WhileExp Build()
        {
            return new(_Condition, _LoopBlock);
        }

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

        public static ExpressionStatementBuilder CreateExpressionStatement()
        {
            return new();
        }

        public ExpressionStatement Build()
        {
            return new(_Expression);
        }

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

        public static ExpressionBuilder CreateExpression()
        {
            return new();
        }

        public Expression Build()
        {
            return new();
        }
    }
