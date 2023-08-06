
namespace Fifth.AST.Builders;

using System;
using Symbols;
using Visitors;
using TypeSystem;
using PrimitiveTypes;
using TypeSystem.PrimitiveTypes;
using System.Collections.Generic;
using fifth.metamodel.metadata;


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AssemblyBuilder : BaseNodeBuilder<AssemblyBuilder, Assembly>
    {
        private AssemblyBuilder()
        {
        _References = new List<AssemblyRef>();
        }

        public static AssemblyBuilder CreateAssembly() => new ();
        public Assembly Build()
        
        {
            var result = new Assembly(_Name, _PublicKeyToken, _Version, _Program, _References);
            CopyMetadataInto(result);
            return result;
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
    public partial class AssemblyRefBuilder : BaseNodeBuilder<AssemblyRefBuilder, AssemblyRef>
    {
        private AssemblyRefBuilder()
        {
        }

        public static AssemblyRefBuilder CreateAssemblyRef() => new ();
        public AssemblyRef Build()
        
        {
            var result = new AssemblyRef(_Name, _PublicKeyToken, _Version);
            CopyMetadataInto(result);
            return result;
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
    public partial class ClassDefinitionBuilder : BaseNodeBuilder<ClassDefinitionBuilder, ClassDefinition>
    {
        private ClassDefinitionBuilder()
        {
        _Fields = new List<FieldDefinition>();
        _Properties = new List<PropertyDefinition>();
        _Functions = new List<IFunctionDefinition>();
        }

        public static ClassDefinitionBuilder CreateClassDefinition() => new ();
        public ClassDefinition Build()
        
        {
            var result = new ClassDefinition(_Name, _Namespace, _Fields, _Properties, _Functions);
            CopyMetadataInto(result);
            return result;
        }

        private string _Name;
        public ClassDefinitionBuilder WithName(string value){
            _Name = value;
            return this;
        }

        private string _Namespace;
        public ClassDefinitionBuilder WithNamespace(string value){
            _Namespace = value;
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
    public partial class FieldDefinitionBuilder : BaseNodeBuilder<FieldDefinitionBuilder, FieldDefinition>
    {
        private FieldDefinitionBuilder()
        {
        }

        public static FieldDefinitionBuilder CreateFieldDefinition() => new ();
        public FieldDefinition Build()
        
        {
            var result = new FieldDefinition(_BackingFieldFor, _Name, _TypeName);
            CopyMetadataInto(result);
            return result;
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
    public partial class PropertyDefinitionBuilder : BaseNodeBuilder<PropertyDefinitionBuilder, PropertyDefinition>
    {
        private PropertyDefinitionBuilder()
        {
        }

        public static PropertyDefinitionBuilder CreatePropertyDefinition() => new ();
        public PropertyDefinition Build()
        
        {
            var result = new PropertyDefinition(_BackingField, _GetAccessor, _SetAccessor, _Name, _TypeName);
            CopyMetadataInto(result);
            return result;
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
    public partial class TypeCastBuilder : BaseNodeBuilder<TypeCastBuilder, TypeCast>
    {
        private TypeCastBuilder()
        {
        }

        public static TypeCastBuilder CreateTypeCast() => new ();
        public TypeCast Build()
        
        {
            var result = new TypeCast(_SubExpression, _TargetTid);
            CopyMetadataInto(result);
            return result;
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
    public partial class ReturnStatementBuilder : BaseNodeBuilder<ReturnStatementBuilder, ReturnStatement>
    {
        private ReturnStatementBuilder()
        {
        }

        public static ReturnStatementBuilder CreateReturnStatement() => new ();
        public ReturnStatement Build()
        
        {
            var result = new ReturnStatement(_SubExpression, _TargetTid);
            CopyMetadataInto(result);
            return result;
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
    public partial class StatementListBuilder : BaseNodeBuilder<StatementListBuilder, StatementList>
    {
        private StatementListBuilder()
        {
        _Statements = new List<Statement>();
        }

        public static StatementListBuilder CreateStatementList() => new ();
        public StatementList Build()
        
        {
            var result = new StatementList(_Statements);
            CopyMetadataInto(result);
            return result;
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
    public partial class AbsoluteIriBuilder : BaseNodeBuilder<AbsoluteIriBuilder, AbsoluteIri>
    {
        private AbsoluteIriBuilder()
        {
        }

        public static AbsoluteIriBuilder CreateAbsoluteIri() => new ();
        public AbsoluteIri Build()
        
        {
            var result = new AbsoluteIri(_Uri);
            CopyMetadataInto(result);
            return result;
        }

        private string _Uri;
        public AbsoluteIriBuilder WithUri(string value){
            _Uri = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class AliasDeclarationBuilder : BaseNodeBuilder<AliasDeclarationBuilder, AliasDeclaration>
    {
        private AliasDeclarationBuilder()
        {
        }

        public static AliasDeclarationBuilder CreateAliasDeclaration() => new ();
        public AliasDeclaration Build()
        
        {
            var result = new AliasDeclaration(_IRI, _Name);
            CopyMetadataInto(result);
            return result;
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
    public partial class AssignmentStmtBuilder : BaseNodeBuilder<AssignmentStmtBuilder, AssignmentStmt>
    {
        private AssignmentStmtBuilder()
        {
        }

        public static AssignmentStmtBuilder CreateAssignmentStmt() => new ();
        public AssignmentStmt Build()
        
        {
            var result = new AssignmentStmt(_Expression, _VariableRef);
            CopyMetadataInto(result);
            return result;
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
    public partial class BinaryExpressionBuilder : BaseNodeBuilder<BinaryExpressionBuilder, BinaryExpression>
    {
        private BinaryExpressionBuilder()
        {
        }

        public static BinaryExpressionBuilder CreateBinaryExpression() => new ();
        public BinaryExpression Build()
        
        {
            var result = new BinaryExpression(_Left, _Op, _Right);
            CopyMetadataInto(result);
            return result;
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
    public partial class BlockBuilder : BaseNodeBuilder<BlockBuilder, Block>
    {
        private BlockBuilder()
        {
        _Statements = new List<Statement>();
        }

        public static BlockBuilder CreateBlock() => new ();
        public Block Build()
        
        {
            var result = new Block(_Statements);
            CopyMetadataInto(result);
            return result;
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
    public partial class BoolValueExpressionBuilder : BaseNodeBuilder<BoolValueExpressionBuilder, BoolValueExpression>
    {
        private BoolValueExpressionBuilder()
        {
        }

        public static BoolValueExpressionBuilder CreateBoolValueExpression() => new ();
        public BoolValueExpression Build()
        
        {
            var result = new BoolValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private bool _TheValue;
        public BoolValueExpressionBuilder WithTheValue(bool value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ShortValueExpressionBuilder : BaseNodeBuilder<ShortValueExpressionBuilder, ShortValueExpression>
    {
        private ShortValueExpressionBuilder()
        {
        }

        public static ShortValueExpressionBuilder CreateShortValueExpression() => new ();
        public ShortValueExpression Build()
        
        {
            var result = new ShortValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private short _TheValue;
        public ShortValueExpressionBuilder WithTheValue(short value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IntValueExpressionBuilder : BaseNodeBuilder<IntValueExpressionBuilder, IntValueExpression>
    {
        private IntValueExpressionBuilder()
        {
        }

        public static IntValueExpressionBuilder CreateIntValueExpression() => new ();
        public IntValueExpression Build()
        
        {
            var result = new IntValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private int _TheValue;
        public IntValueExpressionBuilder WithTheValue(int value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class LongValueExpressionBuilder : BaseNodeBuilder<LongValueExpressionBuilder, LongValueExpression>
    {
        private LongValueExpressionBuilder()
        {
        }

        public static LongValueExpressionBuilder CreateLongValueExpression() => new ();
        public LongValueExpression Build()
        
        {
            var result = new LongValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private long _TheValue;
        public LongValueExpressionBuilder WithTheValue(long value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class FloatValueExpressionBuilder : BaseNodeBuilder<FloatValueExpressionBuilder, FloatValueExpression>
    {
        private FloatValueExpressionBuilder()
        {
        }

        public static FloatValueExpressionBuilder CreateFloatValueExpression() => new ();
        public FloatValueExpression Build()
        
        {
            var result = new FloatValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private float _TheValue;
        public FloatValueExpressionBuilder WithTheValue(float value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DoubleValueExpressionBuilder : BaseNodeBuilder<DoubleValueExpressionBuilder, DoubleValueExpression>
    {
        private DoubleValueExpressionBuilder()
        {
        }

        public static DoubleValueExpressionBuilder CreateDoubleValueExpression() => new ();
        public DoubleValueExpression Build()
        
        {
            var result = new DoubleValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private double _TheValue;
        public DoubleValueExpressionBuilder WithTheValue(double value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DecimalValueExpressionBuilder : BaseNodeBuilder<DecimalValueExpressionBuilder, DecimalValueExpression>
    {
        private DecimalValueExpressionBuilder()
        {
        }

        public static DecimalValueExpressionBuilder CreateDecimalValueExpression() => new ();
        public DecimalValueExpression Build()
        
        {
            var result = new DecimalValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private decimal _TheValue;
        public DecimalValueExpressionBuilder WithTheValue(decimal value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class StringValueExpressionBuilder : BaseNodeBuilder<StringValueExpressionBuilder, StringValueExpression>
    {
        private StringValueExpressionBuilder()
        {
        }

        public static StringValueExpressionBuilder CreateStringValueExpression() => new ();
        public StringValueExpression Build()
        
        {
            var result = new StringValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private string _TheValue;
        public StringValueExpressionBuilder WithTheValue(string value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class DateValueExpressionBuilder : BaseNodeBuilder<DateValueExpressionBuilder, DateValueExpression>
    {
        private DateValueExpressionBuilder()
        {
        }

        public static DateValueExpressionBuilder CreateDateValueExpression() => new ();
        public DateValueExpression Build()
        
        {
            var result = new DateValueExpression(_TheValue);
            CopyMetadataInto(result);
            return result;
        }

        private DateTimeOffset _TheValue;
        public DateValueExpressionBuilder WithTheValue(DateTimeOffset value){
            _TheValue = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ExpressionListBuilder : BaseNodeBuilder<ExpressionListBuilder, ExpressionList>
    {
        private ExpressionListBuilder()
        {
        _Expressions = new List<Expression>();
        }

        public static ExpressionListBuilder CreateExpressionList() => new ();
        public ExpressionList Build()
        
        {
            var result = new ExpressionList(_Expressions);
            CopyMetadataInto(result);
            return result;
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
    public partial class FifthProgramBuilder : BaseNodeBuilder<FifthProgramBuilder, FifthProgram>
    {
        private FifthProgramBuilder()
        {
        _Aliases = new List<AliasDeclaration>();
        _Classes = new List<ClassDefinition>();
        _Functions = new List<IFunctionDefinition>();
        }

        public static FifthProgramBuilder CreateFifthProgram() => new ();
        public FifthProgram Build()
        
        {
            var result = new FifthProgram(_Aliases, _Classes, _Functions);
            CopyMetadataInto(result);
            return result;
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
    public partial class FuncCallExpressionBuilder : BaseNodeBuilder<FuncCallExpressionBuilder, FuncCallExpression>
    {
        private FuncCallExpressionBuilder()
        {
        }

        public static FuncCallExpressionBuilder CreateFuncCallExpression() => new ();
        public FuncCallExpression Build()
        
        {
            var result = new FuncCallExpression(_ActualParameters, _Name);
            CopyMetadataInto(result);
            return result;
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
    public partial class FunctionDefinitionBuilder : BaseNodeBuilder<FunctionDefinitionBuilder, FunctionDefinition>
    {
        private FunctionDefinitionBuilder()
        {
        }

        public static FunctionDefinitionBuilder CreateFunctionDefinition() => new ();
        public FunctionDefinition Build()
        
        {
            var result = new FunctionDefinition(_ParameterDeclarations, _Body, _Typename, _Name, _IsEntryPoint, _IsInstanceFunction, _FunctionKind, _ReturnType);
            CopyMetadataInto(result);
            return result;
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

        private bool _IsInstanceFunction;
        public FunctionDefinitionBuilder WithIsInstanceFunction(bool value){
            _IsInstanceFunction = value;
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
    public partial class OverloadedFunctionDefinitionBuilder : BaseNodeBuilder<OverloadedFunctionDefinitionBuilder, OverloadedFunctionDefinition>
    {
        private OverloadedFunctionDefinitionBuilder()
        {
        _OverloadClauses = new List<IFunctionDefinition>();
        }

        public static OverloadedFunctionDefinitionBuilder CreateOverloadedFunctionDefinition() => new ();
        public OverloadedFunctionDefinition Build()
        
        {
            var result = new OverloadedFunctionDefinition(_OverloadClauses, _Signature);
            CopyMetadataInto(result);
            return result;
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
    public partial class IdentifierBuilder : BaseNodeBuilder<IdentifierBuilder, Identifier>
    {
        private IdentifierBuilder()
        {
        }

        public static IdentifierBuilder CreateIdentifier() => new ();
        public Identifier Build()
        
        {
            var result = new Identifier(_Value);
            CopyMetadataInto(result);
            return result;
        }

        private string _Value;
        public IdentifierBuilder WithValue(string value){
            _Value = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IdentifierExpressionBuilder : BaseNodeBuilder<IdentifierExpressionBuilder, IdentifierExpression>
    {
        private IdentifierExpressionBuilder()
        {
        }

        public static IdentifierExpressionBuilder CreateIdentifierExpression() => new ();
        public IdentifierExpression Build()
        
        {
            var result = new IdentifierExpression(_Identifier);
            CopyMetadataInto(result);
            return result;
        }

        private Identifier _Identifier;
        public IdentifierExpressionBuilder WithIdentifier(Identifier value){
            _Identifier = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class IfElseStatementBuilder : BaseNodeBuilder<IfElseStatementBuilder, IfElseStatement>
    {
        private IfElseStatementBuilder()
        {
        }

        public static IfElseStatementBuilder CreateIfElseStatement() => new ();
        public IfElseStatement Build()
        
        {
            var result = new IfElseStatement(_IfBlock, _ElseBlock, _Condition);
            CopyMetadataInto(result);
            return result;
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
    public partial class ModuleImportBuilder : BaseNodeBuilder<ModuleImportBuilder, ModuleImport>
    {
        private ModuleImportBuilder()
        {
        }

        public static ModuleImportBuilder CreateModuleImport() => new ();
        public ModuleImport Build()
        
        {
            var result = new ModuleImport(_ModuleName);
            CopyMetadataInto(result);
            return result;
        }

        private string _ModuleName;
        public ModuleImportBuilder WithModuleName(string value){
            _ModuleName = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ParameterDeclarationListBuilder : BaseNodeBuilder<ParameterDeclarationListBuilder, ParameterDeclarationList>
    {
        private ParameterDeclarationListBuilder()
        {
        _ParameterDeclarations = new List<IParameterListItem>();
        }

        public static ParameterDeclarationListBuilder CreateParameterDeclarationList() => new ();
        public ParameterDeclarationList Build()
        
        {
            var result = new ParameterDeclarationList(_ParameterDeclarations);
            CopyMetadataInto(result);
            return result;
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
    public partial class ParameterDeclarationBuilder : BaseNodeBuilder<ParameterDeclarationBuilder, ParameterDeclaration>
    {
        private ParameterDeclarationBuilder()
        {
        }

        public static ParameterDeclarationBuilder CreateParameterDeclaration() => new ();
        public ParameterDeclaration Build()
        
        {
            var result = new ParameterDeclaration(_ParameterName, _TypeName, _Constraint, _DestructuringDecl);
            CopyMetadataInto(result);
            return result;
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
    public partial class DestructuringDeclarationBuilder : BaseNodeBuilder<DestructuringDeclarationBuilder, DestructuringDeclaration>
    {
        private DestructuringDeclarationBuilder()
        {
        _Bindings = new List<DestructuringBinding>();
        }

        public static DestructuringDeclarationBuilder CreateDestructuringDeclaration() => new ();
        public DestructuringDeclaration Build()
        
        {
            var result = new DestructuringDeclaration(_Bindings);
            CopyMetadataInto(result);
            return result;
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
    public partial class DestructuringBindingBuilder : BaseNodeBuilder<DestructuringBindingBuilder, DestructuringBinding>
    {
        private DestructuringBindingBuilder()
        {
        }

        public static DestructuringBindingBuilder CreateDestructuringBinding() => new ();
        public DestructuringBinding Build()
        
        {
            var result = new DestructuringBinding(_Varname, _Propname, _PropDecl, _Constraint, _DestructuringDecl);
            CopyMetadataInto(result);
            return result;
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
    public partial class TypeCreateInstExpressionBuilder : BaseNodeBuilder<TypeCreateInstExpressionBuilder, TypeCreateInstExpression>
    {
        private TypeCreateInstExpressionBuilder()
        {
        }

        public static TypeCreateInstExpressionBuilder CreateTypeCreateInstExpression() => new ();
        public TypeCreateInstExpression Build()
        
        {
            var result = new TypeCreateInstExpression();
            CopyMetadataInto(result);
            return result;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class TypeInitialiserBuilder : BaseNodeBuilder<TypeInitialiserBuilder, TypeInitialiser>
    {
        private TypeInitialiserBuilder()
        {
        _PropertyInitialisers = new List<TypePropertyInit>();
        }

        public static TypeInitialiserBuilder CreateTypeInitialiser() => new ();
        public TypeInitialiser Build()
        
        {
            var result = new TypeInitialiser(_TypeName, _PropertyInitialisers);
            CopyMetadataInto(result);
            return result;
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
    public partial class TypePropertyInitBuilder : BaseNodeBuilder<TypePropertyInitBuilder, TypePropertyInit>
    {
        private TypePropertyInitBuilder()
        {
        }

        public static TypePropertyInitBuilder CreateTypePropertyInit() => new ();
        public TypePropertyInit Build()
        
        {
            var result = new TypePropertyInit(_Name, _Value);
            CopyMetadataInto(result);
            return result;
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
    public partial class UnaryExpressionBuilder : BaseNodeBuilder<UnaryExpressionBuilder, UnaryExpression>
    {
        private UnaryExpressionBuilder()
        {
        }

        public static UnaryExpressionBuilder CreateUnaryExpression() => new ();
        public UnaryExpression Build()
        
        {
            var result = new UnaryExpression(_Operand, _Op);
            CopyMetadataInto(result);
            return result;
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
    public partial class VariableDeclarationStatementBuilder : BaseNodeBuilder<VariableDeclarationStatementBuilder, VariableDeclarationStatement>
    {
        private VariableDeclarationStatementBuilder()
        {
        }

        public static VariableDeclarationStatementBuilder CreateVariableDeclarationStatement() => new ();
        public VariableDeclarationStatement Build()
        
        {
            var result = new VariableDeclarationStatement(_Expression, _Name, _UnresolvedTypeName);
            CopyMetadataInto(result);
            return result;
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
    public partial class VariableReferenceBuilder : BaseNodeBuilder<VariableReferenceBuilder, VariableReference>
    {
        private VariableReferenceBuilder()
        {
        }

        public static VariableReferenceBuilder CreateVariableReference() => new ();
        public VariableReference Build()
        
        {
            var result = new VariableReference(_Name);
            CopyMetadataInto(result);
            return result;
        }

        private string _Name;
        public VariableReferenceBuilder WithName(string value){
            _Name = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class MemberAccessExpressionBuilder : BaseNodeBuilder<MemberAccessExpressionBuilder, MemberAccessExpression>
    {
        private MemberAccessExpressionBuilder()
        {
        }

        public static MemberAccessExpressionBuilder CreateMemberAccessExpression() => new ();
        public MemberAccessExpression Build()
        
        {
            var result = new MemberAccessExpression(_LHS, _RHS);
            CopyMetadataInto(result);
            return result;
        }

        private Expression _LHS;
        public MemberAccessExpressionBuilder WithLHS(Expression value){
            _LHS = value;
            return this;
        }

        private Expression _RHS;
        public MemberAccessExpressionBuilder WithRHS(Expression value){
            _RHS = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class WhileExpBuilder : BaseNodeBuilder<WhileExpBuilder, WhileExp>
    {
        private WhileExpBuilder()
        {
        }

        public static WhileExpBuilder CreateWhileExp() => new ();
        public WhileExp Build()
        
        {
            var result = new WhileExp(_Condition, _LoopBlock);
            CopyMetadataInto(result);
            return result;
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
    public partial class ExpressionStatementBuilder : BaseNodeBuilder<ExpressionStatementBuilder, ExpressionStatement>
    {
        private ExpressionStatementBuilder()
        {
        }

        public static ExpressionStatementBuilder CreateExpressionStatement() => new ();
        public ExpressionStatement Build()
        
        {
            var result = new ExpressionStatement(_Expression);
            CopyMetadataInto(result);
            return result;
        }

        private Expression _Expression;
        public ExpressionStatementBuilder WithExpression(Expression value){
            _Expression = value;
            return this;
        }

    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Generated Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1225:Make class sealed.", Justification = "Generated Code")]
    public partial class ExpressionBuilder : BaseNodeBuilder<ExpressionBuilder, Expression>
    {
        private ExpressionBuilder()
        {
        }

        public static ExpressionBuilder CreateExpression() => new ();
        public Expression Build()
        
        {
            var result = new Expression();
            CopyMetadataInto(result);
            return result;
        }

    }
