#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0021 // Use expression body for constructors



namespace Fifth.AST
{
    using System;
    using Symbols;
    using Visitors;
    using TypeSystem;
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;
    using System.Collections.Generic;

#region AST Nodes


    public partial class Assembly : AstNode
    {

        public Assembly(string Name , string PublicKeyToken , string Version , FifthProgram Program , List<AssemblyRef> References     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = PublicKeyToken ?? throw new ArgumentNullException(nameof(PublicKeyToken));
            this.PublicKeyToken = PublicKeyToken;
                //_ = Version ?? throw new ArgumentNullException(nameof(Version));
            this.Version = Version;
                //_ = Program ?? throw new ArgumentNullException(nameof(Program));
            this.Program = Program;
                //_ = References ?? throw new ArgumentNullException(nameof(References));
            this.References = References;
            }

        public string Name{get;set;}
        public string PublicKeyToken{get;set;}
        public string Version{get;set;}
        public FifthProgram Program{get;set;}
        public List<AssemblyRef> References{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAssembly(this);
            Program?.Accept(visitor);
            if(References != null){
                foreach (var e in References)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveAssembly(this);
        }

        
            public Assembly(string name, string strongNameKey, string versionNumber)
            {
                Name = name;
                PublicKeyToken = strongNameKey;
                Version = versionNumber;
                References = new List<AssemblyRef>();
            }
        
    }

    public partial class AssemblyRef : AstNode
    {

        public AssemblyRef(string Name , string PublicKeyToken , string Version     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = PublicKeyToken ?? throw new ArgumentNullException(nameof(PublicKeyToken));
            this.PublicKeyToken = PublicKeyToken;
                //_ = Version ?? throw new ArgumentNullException(nameof(Version));
            this.Version = Version;
            }

        public string Name{get;set;}
        public string PublicKeyToken{get;set;}
        public string Version{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAssemblyRef(this);
            visitor.LeaveAssemblyRef(this);
        }

        
    }

    public partial class ClassDefinition : ScopeAstNode, ITypedAstNode, IFunctionCollection
    {

        public ClassDefinition(string Name , List<PropertyDefinition> Properties , List<IFunctionDefinition> Functions     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = Properties ?? throw new ArgumentNullException(nameof(Properties));
            this.Properties = Properties;
                //_ = Functions ?? throw new ArgumentNullException(nameof(Functions));
            this.Functions = Functions;
            }

        public string Name{get;set;}
        public List<PropertyDefinition> Properties{get;set;}
        public List<IFunctionDefinition> Functions{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterClassDefinition(this);
            if(Properties != null){
                foreach (var e in Properties)
                {
                    e.Accept(visitor);
                }
            }
            if(Functions != null){
                foreach (var e in Functions)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveClassDefinition(this);
        }

        
    }

    public partial class PropertyDefinition : TypedAstNode
    {

        public PropertyDefinition(string Name , string TypeName     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = TypeName ?? throw new ArgumentNullException(nameof(TypeName));
            this.TypeName = TypeName;
            }

        public string Name{get;set;}
        public string TypeName{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterPropertyDefinition(this);
            visitor.LeavePropertyDefinition(this);
        }

        
    }

    public partial class TypeCast : Expression
    {

        public TypeCast(Expression SubExpression , TypeId TargetTid     )

    
{
                //_ = SubExpression ?? throw new ArgumentNullException(nameof(SubExpression));
            this.SubExpression = SubExpression;
                //_ = TargetTid ?? throw new ArgumentNullException(nameof(TargetTid));
            this.TargetTid = TargetTid;
            }

        public Expression SubExpression{get;set;}
        public TypeId TargetTid{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCast(this);
            SubExpression?.Accept(visitor);
            visitor.LeaveTypeCast(this);
        }

        
    }

    public partial class ReturnStatement : Statement
    {

        public ReturnStatement(Expression SubExpression , TypeId TargetTid     )

    
{
                //_ = SubExpression ?? throw new ArgumentNullException(nameof(SubExpression));
            this.SubExpression = SubExpression;
                //_ = TargetTid ?? throw new ArgumentNullException(nameof(TargetTid));
            this.TargetTid = TargetTid;
            }

        public Expression SubExpression{get;set;}
        public TypeId TargetTid{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterReturnStatement(this);
            SubExpression?.Accept(visitor);
            visitor.LeaveReturnStatement(this);
        }

        
    }

    public partial class StatementList : AstNode
    {

        public StatementList(List<Statement> Statements     )

    
{
                //_ = Statements ?? throw new ArgumentNullException(nameof(Statements));
            this.Statements = Statements;
            }

        public List<Statement> Statements{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStatementList(this);
            if(Statements != null){
                foreach (var e in Statements)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveStatementList(this);
        }

        
    }

    public partial class AbsoluteIri : TypedAstNode
    {

        public AbsoluteIri(string Uri     )

    
{
                //_ = Uri ?? throw new ArgumentNullException(nameof(Uri));
            this.Uri = Uri;
            }

        public string Uri{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAbsoluteIri(this);
            visitor.LeaveAbsoluteIri(this);
        }

        
    }

    public partial class AliasDeclaration : AstNode
    {

        public AliasDeclaration(AbsoluteIri IRI , string Name     )

    
{
                //_ = IRI ?? throw new ArgumentNullException(nameof(IRI));
            this.IRI = IRI;
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
            }

        public AbsoluteIri IRI{get;set;}
        public string Name{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAliasDeclaration(this);
            IRI?.Accept(visitor);
            visitor.LeaveAliasDeclaration(this);
        }

        
    }

    public partial class AssignmentStmt : Statement
    {

        public AssignmentStmt(Expression Expression , BaseVarReference VariableRef     )

    
{
                //_ = Expression ?? throw new ArgumentNullException(nameof(Expression));
            this.Expression = Expression;
                //_ = VariableRef ?? throw new ArgumentNullException(nameof(VariableRef));
            this.VariableRef = VariableRef;
            }

        public Expression Expression{get;set;}
        public BaseVarReference VariableRef{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAssignmentStmt(this);
            Expression?.Accept(visitor);
            VariableRef?.Accept(visitor);
            visitor.LeaveAssignmentStmt(this);
        }

        
    }

    public partial class BinaryExpression : Expression
    {

        public BinaryExpression(Expression Left , Operator? Op , Expression Right     )

    
{
                //_ = Left ?? throw new ArgumentNullException(nameof(Left));
            this.Left = Left;
                //_ = Op ?? throw new ArgumentNullException(nameof(Op));
            this.Op = Op;
                //_ = Right ?? throw new ArgumentNullException(nameof(Right));
            this.Right = Right;
            }

        public Expression Left{get;set;}
        public Operator? Op{get;set;}
        public Expression Right{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBinaryExpression(this);
            Left?.Accept(visitor);
            Right?.Accept(visitor);
            visitor.LeaveBinaryExpression(this);
        }

        
    }

    public partial class Block : ScopeAstNode
    {

        public Block(List<Statement> Statements     )

    
{
                //_ = Statements ?? throw new ArgumentNullException(nameof(Statements));
            this.Statements = Statements;
            }

        public List<Statement> Statements{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBlock(this);
            if(Statements != null){
                foreach (var e in Statements)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveBlock(this);
        }

        
        public Block(StatementList sl):this(sl.Statements){}
        
    }

    public partial class BoolValueExpression : LiteralExpression<bool>
    {

        public BoolValueExpression(bool TheValue     )

    : base(TheValue, PrimitiveBool.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public bool TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBoolValueExpression(this);
            visitor.LeaveBoolValueExpression(this);
        }

        
    }

    public partial class ShortValueExpression : LiteralExpression<short>
    {

        public ShortValueExpression(short TheValue     )

    : base(TheValue, PrimitiveShort.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public short TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterShortValueExpression(this);
            visitor.LeaveShortValueExpression(this);
        }

        
    }

    public partial class IntValueExpression : LiteralExpression<int>
    {

        public IntValueExpression(int TheValue     )

    : base(TheValue, PrimitiveInteger.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public int TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIntValueExpression(this);
            visitor.LeaveIntValueExpression(this);
        }

        
    }

    public partial class LongValueExpression : LiteralExpression<long>
    {

        public LongValueExpression(long TheValue     )

    : base(TheValue, PrimitiveLong.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public long TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterLongValueExpression(this);
            visitor.LeaveLongValueExpression(this);
        }

        
    }

    public partial class FloatValueExpression : LiteralExpression<float>
    {

        public FloatValueExpression(float TheValue     )

    : base(TheValue, PrimitiveFloat.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public float TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFloatValueExpression(this);
            visitor.LeaveFloatValueExpression(this);
        }

        
    }

    public partial class DoubleValueExpression : LiteralExpression<double>
    {

        public DoubleValueExpression(double TheValue     )

    : base(TheValue, PrimitiveDouble.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public double TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDoubleValueExpression(this);
            visitor.LeaveDoubleValueExpression(this);
        }

        
    }

    public partial class DecimalValueExpression : LiteralExpression<decimal>
    {

        public DecimalValueExpression(decimal TheValue     )

    : base(TheValue, PrimitiveDecimal.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public decimal TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDecimalValueExpression(this);
            visitor.LeaveDecimalValueExpression(this);
        }

        
    }

    public partial class StringValueExpression : LiteralExpression<string>
    {

        public StringValueExpression(string TheValue     )

    : base(TheValue, PrimitiveString.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public string TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterStringValueExpression(this);
            visitor.LeaveStringValueExpression(this);
        }

        
    }

    public partial class DateValueExpression : LiteralExpression<DateTimeOffset>
    {

        public DateValueExpression(DateTimeOffset TheValue     )

    : base(TheValue, PrimitiveDate.Default.TypeId)
{
                //_ = TheValue ?? throw new ArgumentNullException(nameof(TheValue));
            this.TheValue = TheValue;
            }

        public DateTimeOffset TheValue{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDateValueExpression(this);
            visitor.LeaveDateValueExpression(this);
        }

        
    }

    public partial class ExpressionList : TypedAstNode
    {

        public ExpressionList(List<Expression> Expressions     )

    
{
                //_ = Expressions ?? throw new ArgumentNullException(nameof(Expressions));
            this.Expressions = Expressions;
            }

        public List<Expression> Expressions{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionList(this);
            if(Expressions != null){
                foreach (var e in Expressions)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveExpressionList(this);
        }

        
    }

    public partial class FifthProgram : ScopeAstNode, IFunctionCollection
    {

        public FifthProgram(List<AliasDeclaration> Aliases , List<ClassDefinition> Classes , List<IFunctionDefinition> Functions     )

    
{
                //_ = Aliases ?? throw new ArgumentNullException(nameof(Aliases));
            this.Aliases = Aliases;
                //_ = Classes ?? throw new ArgumentNullException(nameof(Classes));
            this.Classes = Classes;
                //_ = Functions ?? throw new ArgumentNullException(nameof(Functions));
            this.Functions = Functions;
            }

        public List<AliasDeclaration> Aliases{get;set;}
        public List<ClassDefinition> Classes{get;set;}
        public List<IFunctionDefinition> Functions{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFifthProgram(this);
            if(Aliases != null){
                foreach (var e in Aliases)
                {
                    e.Accept(visitor);
                }
            }
            if(Classes != null){
                foreach (var e in Classes)
                {
                    e.Accept(visitor);
                }
            }
            if(Functions != null){
                foreach (var e in Functions)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveFifthProgram(this);
        }

        
    }

    public partial class FuncCallExpression : Expression
    {

        public FuncCallExpression(ExpressionList ActualParameters , string Name     )

    
{
                //_ = ActualParameters ?? throw new ArgumentNullException(nameof(ActualParameters));
            this.ActualParameters = ActualParameters;
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
            }

        public ExpressionList ActualParameters{get;set;}
        public string Name{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFuncCallExpression(this);
            ActualParameters?.Accept(visitor);
            visitor.LeaveFuncCallExpression(this);
        }

        
    }

    public partial class FunctionDefinition : ScopeAstNode, IFunctionDefinition
    {

        public FunctionDefinition(ParameterDeclarationList ParameterDeclarations , Block Body , string Typename , string Name , bool IsEntryPoint , TypeId ReturnType     )

    
{
                //_ = ParameterDeclarations ?? throw new ArgumentNullException(nameof(ParameterDeclarations));
            this.ParameterDeclarations = ParameterDeclarations;
                //_ = Body ?? throw new ArgumentNullException(nameof(Body));
            this.Body = Body;
                //_ = Typename ?? throw new ArgumentNullException(nameof(Typename));
            this.Typename = Typename;
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = IsEntryPoint ?? throw new ArgumentNullException(nameof(IsEntryPoint));
            this.IsEntryPoint = IsEntryPoint;
                //_ = ReturnType ?? throw new ArgumentNullException(nameof(ReturnType));
            this.ReturnType = ReturnType;
            }

        public ParameterDeclarationList ParameterDeclarations{get;set;}
        public Block Body{get;set;}
        public string Typename{get;set;}
        public string Name{get;set;}
        public bool IsEntryPoint{get;set;}
        public TypeId ReturnType{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFunctionDefinition(this);
            ParameterDeclarations?.Accept(visitor);
            Body?.Accept(visitor);
            visitor.LeaveFunctionDefinition(this);
        }

        
    }

    public partial class BuiltinFunctionDefinition : AstNode, IFunctionDefinition
    {

        public BuiltinFunctionDefinition(    )

    
{
            }


        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBuiltinFunctionDefinition(this);
            visitor.LeaveBuiltinFunctionDefinition(this);
        }

        
            public ParameterDeclarationList ParameterDeclarations { get; set; }
            public string Typename { get; set; }
            public string Name { get; set; }
            public bool IsEntryPoint { get; set; }
            public TypeId ReturnType { get; set; }

        public BuiltinFunctionDefinition(string name, string typename, params (string, string)[] parameters)
            {
                Name = name;
                Typename = typename;
                var list = new List<IParameterListItem>();

                foreach (var (pname, ptypename) in parameters)
                {
                    var paramDef = new ParameterDeclaration(new Identifier(pname), ptypename, null);
                    list.Add(paramDef);
                }

                var paramDeclList = new ParameterDeclarationList(list);

                ParameterDeclarations = paramDeclList;
                IsEntryPoint = false;
            }
        
    }

    public partial class OverloadedFunctionDefinition : ScopeAstNode, IFunctionDefinition, ITypedAstNode
    {

        public OverloadedFunctionDefinition(List<IFunctionDefinition> OverloadClauses , IFunctionSignature Signature     )

    
{
                //_ = OverloadClauses ?? throw new ArgumentNullException(nameof(OverloadClauses));
            this.OverloadClauses = OverloadClauses;
                //_ = Signature ?? throw new ArgumentNullException(nameof(Signature));
            this.Signature = Signature;
            }

        public List<IFunctionDefinition> OverloadClauses{get;set;}
        public IFunctionSignature Signature{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterOverloadedFunctionDefinition(this);
            if(OverloadClauses != null){
                foreach (var e in OverloadClauses)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveOverloadedFunctionDefinition(this);
        }

        
    }

    public partial class Identifier : TypedAstNode
    {

        public Identifier(string Value     )

    
{
                //_ = Value ?? throw new ArgumentNullException(nameof(Value));
            this.Value = Value;
            }

        public string Value{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifier(this);
            visitor.LeaveIdentifier(this);
        }

        
    }

    public partial class IdentifierExpression : Expression
    {

        public IdentifierExpression(Identifier Identifier     )

    
{
                //_ = Identifier ?? throw new ArgumentNullException(nameof(Identifier));
            this.Identifier = Identifier;
            }

        public Identifier Identifier{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            Identifier?.Accept(visitor);
            visitor.LeaveIdentifierExpression(this);
        }

        
    }

    public partial class IfElseStatement : Statement
    {

        public IfElseStatement(Block IfBlock , Block ElseBlock , Expression Condition     )

    
{
                //_ = IfBlock ?? throw new ArgumentNullException(nameof(IfBlock));
            this.IfBlock = IfBlock;
                //_ = ElseBlock ?? throw new ArgumentNullException(nameof(ElseBlock));
            this.ElseBlock = ElseBlock;
                //_ = Condition ?? throw new ArgumentNullException(nameof(Condition));
            this.Condition = Condition;
            }

        public Block IfBlock{get;set;}
        public Block ElseBlock{get;set;}
        public Expression Condition{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIfElseStatement(this);
            IfBlock?.Accept(visitor);
            ElseBlock?.Accept(visitor);
            Condition?.Accept(visitor);
            visitor.LeaveIfElseStatement(this);
        }

        
    }

    public partial class ModuleImport : AstNode
    {

        public ModuleImport(string ModuleName     )

    
{
                //_ = ModuleName ?? throw new ArgumentNullException(nameof(ModuleName));
            this.ModuleName = ModuleName;
            }

        public string ModuleName{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterModuleImport(this);
            visitor.LeaveModuleImport(this);
        }

        
    }

    public partial class DestructuringParamDecl : ParameterDeclaration, IParameterListItem
    {

        public DestructuringParamDecl(Identifier ParameterName , string TypeName , Expression Constraint , List<PropertyBinding> PropertyBindings     )

:base(ParameterName , TypeName , Constraint     )
{
                //_ = PropertyBindings ?? throw new ArgumentNullException(nameof(PropertyBindings));
            this.PropertyBindings = PropertyBindings;
            }

        public List<PropertyBinding> PropertyBindings{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterDestructuringParamDecl(this);
            if(PropertyBindings != null){
                foreach (var e in PropertyBindings)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveDestructuringParamDecl(this);
        }

        
    }

    public partial class ParameterDeclaration : TypedAstNode, IParameterListItem
    {

        public ParameterDeclaration(Identifier ParameterName , string TypeName , Expression Constraint     )

    
{
                //_ = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.ParameterName = ParameterName;
                //_ = TypeName ?? throw new ArgumentNullException(nameof(TypeName));
            this.TypeName = TypeName;
                //_ = Constraint ?? throw new ArgumentNullException(nameof(Constraint));
            this.Constraint = Constraint;
            }

        public Identifier ParameterName{get;set;}
        public string TypeName{get;set;}
        public Expression Constraint{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            ParameterName?.Accept(visitor);
            Constraint?.Accept(visitor);
            visitor.LeaveParameterDeclaration(this);
        }

        
    }

    public partial class ParameterDeclarationList : AstNode
    {

        public ParameterDeclarationList(List<IParameterListItem> ParameterDeclarations     )

    
{
                //_ = ParameterDeclarations ?? throw new ArgumentNullException(nameof(ParameterDeclarations));
            this.ParameterDeclarations = ParameterDeclarations;
            }

        public List<IParameterListItem> ParameterDeclarations{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclarationList(this);
            if(ParameterDeclarations != null){
                foreach (var e in ParameterDeclarations)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveParameterDeclarationList(this);
        }

        
    }

    public partial class TypeCreateInstExpression : Expression
    {

        public TypeCreateInstExpression(    )

    
{
            }


        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeCreateInstExpression(this);
            visitor.LeaveTypeCreateInstExpression(this);
        }

        
    }

    public partial class TypeInitialiser : Expression
    {

        public TypeInitialiser(string TypeName , List<TypePropertyInit> PropertyInitialisers     )

    
{
                //_ = TypeName ?? throw new ArgumentNullException(nameof(TypeName));
            this.TypeName = TypeName;
                //_ = PropertyInitialisers ?? throw new ArgumentNullException(nameof(PropertyInitialisers));
            this.PropertyInitialisers = PropertyInitialisers;
            }

        public string TypeName{get;set;}
        public List<TypePropertyInit> PropertyInitialisers{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            if(PropertyInitialisers != null){
                foreach (var e in PropertyInitialisers)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveTypeInitialiser(this);
        }

        
    }

    public partial class PropertyBinding : AstNode
    {

        public PropertyBinding(string BoundPropertyName , string BoundVariableName , Expression Constraint     )

    
{
                //_ = BoundPropertyName ?? throw new ArgumentNullException(nameof(BoundPropertyName));
            this.BoundPropertyName = BoundPropertyName;
                //_ = BoundVariableName ?? throw new ArgumentNullException(nameof(BoundVariableName));
            this.BoundVariableName = BoundVariableName;
                //_ = Constraint ?? throw new ArgumentNullException(nameof(Constraint));
            this.Constraint = Constraint;
            }

        public string BoundPropertyName{get;set;}
        public string BoundVariableName{get;set;}
        public Expression Constraint{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterPropertyBinding(this);
            Constraint?.Accept(visitor);
            visitor.LeavePropertyBinding(this);
        }

        
            public PropertyDefinition BoundProperty { get; set; }
        
    }

    public partial class TypePropertyInit : AstNode
    {

        public TypePropertyInit(string Name , Expression Value     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
                //_ = Value ?? throw new ArgumentNullException(nameof(Value));
            this.Value = Value;
            }

        public string Name{get;set;}
        public Expression Value{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypePropertyInit(this);
            Value?.Accept(visitor);
            visitor.LeaveTypePropertyInit(this);
        }

        
    }

    public partial class UnaryExpression : Expression
    {

        public UnaryExpression(Expression Operand , Operator Op     )

    
{
                //_ = Operand ?? throw new ArgumentNullException(nameof(Operand));
            this.Operand = Operand;
                //_ = Op ?? throw new ArgumentNullException(nameof(Op));
            this.Op = Op;
            }

        public Expression Operand{get;set;}
        public Operator Op{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterUnaryExpression(this);
            Operand?.Accept(visitor);
            visitor.LeaveUnaryExpression(this);
        }

        
    }

    public partial class VariableDeclarationStatement : Statement, ITypedAstNode
    {

        public VariableDeclarationStatement(Expression Expression , Identifier Name     )

    
{
                //_ = Expression ?? throw new ArgumentNullException(nameof(Expression));
            this.Expression = Expression;
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
            }

        public Expression Expression{get;set;}
        public Identifier Name{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableDeclarationStatement(this);
            Expression?.Accept(visitor);
            Name?.Accept(visitor);
            visitor.LeaveVariableDeclarationStatement(this);
        }

        
            private string typeName;
            public string TypeName
            {
                get
                {
                    if (TypeId != null)
                    {
                        return TypeId.Lookup().Name;
                    }
                    return typeName;
                }
                set
                {
                    if (!TypeRegistry.DefaultRegistry.TryGetTypeByName(value, out var type))
                    {
                        throw new TypeCheckingException("Setting unrecognised type for variable");
                    }

                    typeName = type.Name; // in case we want to use some sort of mapping onto a canonical name
                    TypeId = type.TypeId;
                }
            }
            public TypeId TypeId { get; set; }

        
    }

    public partial class VariableReference : BaseVarReference
    {

        public VariableReference(string Name     )

    
{
                //_ = Name ?? throw new ArgumentNullException(nameof(Name));
            this.Name = Name;
            }

        public string Name{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterVariableReference(this);
            visitor.LeaveVariableReference(this);
        }

        
    }

    public partial class CompoundVariableReference : BaseVarReference
    {

        public CompoundVariableReference(List<VariableReference> ComponentReferences     )

    
{
                //_ = ComponentReferences ?? throw new ArgumentNullException(nameof(ComponentReferences));
            this.ComponentReferences = ComponentReferences;
            }

        public List<VariableReference> ComponentReferences{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterCompoundVariableReference(this);
            if(ComponentReferences != null){
                foreach (var e in ComponentReferences)
                {
                    e.Accept(visitor);
                }
            }
            visitor.LeaveCompoundVariableReference(this);
        }

        
    }

    public partial class WhileExp : Statement
    {

        public WhileExp(Expression Condition , Block LoopBlock     )

    
{
                //_ = Condition ?? throw new ArgumentNullException(nameof(Condition));
            this.Condition = Condition;
                //_ = LoopBlock ?? throw new ArgumentNullException(nameof(LoopBlock));
            this.LoopBlock = LoopBlock;
            }

        public Expression Condition{get;set;}
        public Block LoopBlock{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterWhileExp(this);
            Condition?.Accept(visitor);
            visitor.LeaveWhileExp(this);
        }

        
    }

    public partial class ExpressionStatement : Statement
    {

        public ExpressionStatement(Expression Expression     )

    
{
                //_ = Expression ?? throw new ArgumentNullException(nameof(Expression));
            this.Expression = Expression;
            }

        public Expression Expression{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionStatement(this);
            Expression?.Accept(visitor);
            visitor.LeaveExpressionStatement(this);
        }

        
    }

    public partial class Expression : TypedAstNode
    {

        public Expression(    )

    
{
            }


        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpression(this);
            visitor.LeaveExpression(this);
        }

        
    }


#endregion // AST Nodes
}



#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0021 // Use expression body for constructors
