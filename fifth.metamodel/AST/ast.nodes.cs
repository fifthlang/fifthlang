#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0021 // Use expression body for constructors


namespace Fifth.AST;
using System;
using Symbols;
using Visitors;
using TypeSystem;
using PrimitiveTypes;
using TypeSystem.PrimitiveTypes;
using System.Collections.Generic;

public partial class Assembly : AstNode
{
    public Assembly(string Name , string PublicKeyToken , string Version , FifthProgram Program , List<AssemblyRef> References )


{
        this.Name = Name;
        this.PublicKeyToken = PublicKeyToken;
        this.Version = Version;
        this.Program = Program;
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
    public AssemblyRef(string Name , string PublicKeyToken , string Version )


{
        this.Name = Name;
        this.PublicKeyToken = PublicKeyToken;
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
    public ClassDefinition(string Name , List<FieldDefinition> Fields , List<PropertyDefinition> Properties , List<IFunctionDefinition> Functions )


{
        this.Name = Name;
        this.Fields = Fields;
        this.Properties = Properties;
        this.Functions = Functions;
    }

    public string Name{get;set;}
    public List<FieldDefinition> Fields{get;set;}
    public List<PropertyDefinition> Properties{get;set;}
    public List<IFunctionDefinition> Functions{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterClassDefinition(this);
        if(Fields != null){
            foreach (var e in Fields)
            {
                e.Accept(visitor);
            }
        }
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

public partial class FieldDefinition : TypedAstNode
{
    public FieldDefinition(PropertyDefinition? BackingFieldFor , string Name , string TypeName )


{
        this.BackingFieldFor = BackingFieldFor;
        this.Name = Name;
        this.TypeName = TypeName;
    }

    public PropertyDefinition? BackingFieldFor{get;set;}
    public string Name{get;set;}
    public string TypeName{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterFieldDefinition(this);
        visitor.LeaveFieldDefinition(this);
    }


}

public partial class PropertyDefinition : TypedAstNode
{
    public PropertyDefinition(FieldDefinition? BackingField , FunctionDefinition? GetAccessor , FunctionDefinition? SetAccessor , string Name , string TypeName )


{
        this.BackingField = BackingField;
        this.GetAccessor = GetAccessor;
        this.SetAccessor = SetAccessor;
        this.Name = Name;
        this.TypeName = TypeName;
    }

    public FieldDefinition? BackingField{get;set;}
    public FunctionDefinition? GetAccessor{get;set;}
    public FunctionDefinition? SetAccessor{get;set;}
    public string Name{get;set;}
    public string TypeName{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterPropertyDefinition(this);
        GetAccessor?.Accept(visitor);
        SetAccessor?.Accept(visitor);
        visitor.LeavePropertyDefinition(this);
    }


}

public partial class TypeCast : Expression
{
    public TypeCast(Expression SubExpression , TypeId TargetTid )


{
        this.SubExpression = SubExpression;
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
    public ReturnStatement(Expression SubExpression , TypeId TargetTid )


{
        this.SubExpression = SubExpression;
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
    public StatementList(List<Statement> Statements )


{
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
    public AbsoluteIri(string Uri )


{
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
    public AliasDeclaration(AbsoluteIri IRI , string Name )


{
        this.IRI = IRI;
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
    public AssignmentStmt(Expression Expression , BaseVarReference VariableRef )


{
        this.Expression = Expression;
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
    public BinaryExpression(Expression Left , Operator? Op , Expression Right )


{
        this.Left = Left;
        this.Op = Op;
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
    public Block(List<Statement> Statements )


{
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
    public BoolValueExpression(bool TheValue )

: base(TheValue, PrimitiveBool.Default.TypeId)
{
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
    public ShortValueExpression(short TheValue )

: base(TheValue, PrimitiveShort.Default.TypeId)
{
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
    public IntValueExpression(int TheValue )

: base(TheValue, PrimitiveInteger.Default.TypeId)
{
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
    public LongValueExpression(long TheValue )

: base(TheValue, PrimitiveLong.Default.TypeId)
{
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
    public FloatValueExpression(float TheValue )

: base(TheValue, PrimitiveFloat.Default.TypeId)
{
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
    public DoubleValueExpression(double TheValue )

: base(TheValue, PrimitiveDouble.Default.TypeId)
{
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
    public DecimalValueExpression(decimal TheValue )

: base(TheValue, PrimitiveDecimal.Default.TypeId)
{
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
    public StringValueExpression(string TheValue )

: base(TheValue, PrimitiveString.Default.TypeId)
{
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
    public DateValueExpression(DateTimeOffset TheValue )

: base(TheValue, PrimitiveDate.Default.TypeId)
{
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
    public ExpressionList(List<Expression> Expressions )


{
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
    public FifthProgram(List<AliasDeclaration> Aliases , List<ClassDefinition> Classes , List<IFunctionDefinition> Functions )


{
        this.Aliases = Aliases;
        this.Classes = Classes;
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
    public FuncCallExpression(ExpressionList ActualParameters , string Name )


{
        this.ActualParameters = ActualParameters;
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

public partial class BuiltinFunctionDefinition : FunctionDefinition, IFunctionDefinition
{
    public BuiltinFunctionDefinition(ParameterDeclarationList ParameterDeclarations , Block? Body , string Typename , string Name , bool IsEntryPoint , FunctionKind FunctionKind , TypeId ReturnType )

:base(ParameterDeclarations , Body , Typename , Name , IsEntryPoint , FunctionKind , ReturnType )
{
    }


    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterBuiltinFunctionDefinition(this);
        visitor.LeaveBuiltinFunctionDefinition(this);
    }


            public BuiltinFunctionDefinition(string name, string typename, params (string, string)[] parameters)
               : base(new ParameterDeclarationList(parameters.Select(x => (IParameterListItem)new ParameterDeclaration(new Identifier(x.Item1), x.Item2, null, null)).ToList()),
                null, typename, name, false, FunctionKind.BuiltIn, null){}

}

public partial class FunctionDefinition : ScopeAstNode, IFunctionDefinition
{
    public FunctionDefinition(ParameterDeclarationList ParameterDeclarations , Block? Body , string Typename , string Name , bool IsEntryPoint , FunctionKind FunctionKind , TypeId ReturnType )


{
        this.ParameterDeclarations = ParameterDeclarations;
        this.Body = Body;
        this.Typename = Typename;
        this.Name = Name;
        this.IsEntryPoint = IsEntryPoint;
        this.FunctionKind = FunctionKind;
        this.ReturnType = ReturnType;
    }

    public ParameterDeclarationList ParameterDeclarations{get;set;}
    public Block? Body{get;set;}
    public string Typename{get;set;}
    public string Name{get;set;}
    public bool IsEntryPoint{get;set;}
    public FunctionKind FunctionKind{get;set;}
    public TypeId ReturnType{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterFunctionDefinition(this);
        ParameterDeclarations?.Accept(visitor);
        Body?.Accept(visitor);
        visitor.LeaveFunctionDefinition(this);
    }


}

public partial class OverloadedFunctionDefinition : ScopeAstNode, IFunctionDefinition, ITypedAstNode
{
    public OverloadedFunctionDefinition(List<IFunctionDefinition> OverloadClauses , IFunctionSignature Signature )


{
        this.OverloadClauses = OverloadClauses;
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
    public Identifier(string Value )


{
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
    public IdentifierExpression(Identifier Identifier )


{
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
    public IfElseStatement(Block IfBlock , Block ElseBlock , Expression Condition )


{
        this.IfBlock = IfBlock;
        this.ElseBlock = ElseBlock;
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
    public ModuleImport(string ModuleName )


{
        this.ModuleName = ModuleName;
    }

    public string ModuleName{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterModuleImport(this);
        visitor.LeaveModuleImport(this);
    }


}

public partial class ParameterDeclarationList : AstNode
{
    public ParameterDeclarationList(List<IParameterListItem> ParameterDeclarations )


{
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

public partial class ParameterDeclaration : TypedAstNode, IParameterListItem
{
    public ParameterDeclaration(Identifier ParameterName , string TypeName , Expression Constraint , DestructuringDeclaration DestructuringDecl )


{
        this.ParameterName = ParameterName;
        this.TypeName = TypeName;
        this.Constraint = Constraint;
        this.DestructuringDecl = DestructuringDecl;
    }

    public Identifier ParameterName{get;set;}
    public string TypeName{get;set;}
    public Expression Constraint{get;set;}
    public DestructuringDeclaration DestructuringDecl{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterParameterDeclaration(this);
        ParameterName?.Accept(visitor);
        Constraint?.Accept(visitor);
        DestructuringDecl?.Accept(visitor);
        visitor.LeaveParameterDeclaration(this);
    }


}

public partial class DestructuringDeclaration : AstNode
{
    public DestructuringDeclaration(List<DestructuringBinding> Bindings )


{
        this.Bindings = Bindings;
    }

    public List<DestructuringBinding> Bindings{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterDestructuringDeclaration(this);
        if(Bindings != null){
            foreach (var e in Bindings)
            {
                e.Accept(visitor);
            }
        }
        visitor.LeaveDestructuringDeclaration(this);
    }


}

public partial class DestructuringBinding : TypedAstNode
{
    public DestructuringBinding(string Varname , string Propname , PropertyDefinition PropDecl , Expression Constraint , DestructuringDeclaration DestructuringDecl )


{
        this.Varname = Varname;
        this.Propname = Propname;
        this.PropDecl = PropDecl;
        this.Constraint = Constraint;
        this.DestructuringDecl = DestructuringDecl;
    }

    public string Varname{get;set;}
    public string Propname{get;set;}
    public PropertyDefinition PropDecl{get;set;}
    public Expression Constraint{get;set;}
    public DestructuringDeclaration DestructuringDecl{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterDestructuringBinding(this);
        Constraint?.Accept(visitor);
        DestructuringDecl?.Accept(visitor);
        visitor.LeaveDestructuringBinding(this);
    }


}

public partial class TypeCreateInstExpression : Expression
{
    public TypeCreateInstExpression()


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
    public TypeInitialiser(string TypeName , List<TypePropertyInit> PropertyInitialisers )


{
        this.TypeName = TypeName;
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

public partial class TypePropertyInit : AstNode
{
    public TypePropertyInit(string Name , Expression Value )


{
        this.Name = Name;
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
    public UnaryExpression(Expression Operand , Operator Op )


{
        this.Operand = Operand;
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
    public VariableDeclarationStatement(Expression Expression , string Name , string UnresolvedTypeName )


{
        this.Expression = Expression;
        this.Name = Name;
        this.UnresolvedTypeName = UnresolvedTypeName;
    }

    public Expression Expression{get;set;}
    public string Name{get;set;}
    public string UnresolvedTypeName{get;set;}

    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterVariableDeclarationStatement(this);
        Expression?.Accept(visitor);
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
    public VariableReference(string Name )


{
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
    public CompoundVariableReference(List<VariableReference> ComponentReferences )


{
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
    public WhileExp(Expression Condition , Block LoopBlock )


{
        this.Condition = Condition;
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
    public ExpressionStatement(Expression Expression )


{
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
    public Expression()


{
    }


    public override void Accept(IAstVisitor visitor)
    {
        visitor.EnterExpression(this);
        visitor.LeaveExpression(this);
    }


}



#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0021 // Use expression body for constructors
