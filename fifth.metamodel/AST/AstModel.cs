
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0021 // Use expression body for constructors

namespace Fifth.AST.Visitors
{
    using System;
    using Symbols;
    using Fifth.AST;
    using TypeSystem;
    using PrimitiveTypes;
    using TypeSystem.PrimitiveTypes;
    using System.Collections.Generic;

    public interface IAstVisitor
    {
        public void EnterTypeCast(TypeCast ctx);
        public void LeaveTypeCast(TypeCast ctx);
        public void EnterReturnStatement(ReturnStatement ctx);
        public void LeaveReturnStatement(ReturnStatement ctx);
        public void EnterStatementList(StatementList ctx);
        public void LeaveStatementList(StatementList ctx);
        public void EnterAbsoluteIri(AbsoluteIri ctx);
        public void LeaveAbsoluteIri(AbsoluteIri ctx);
        public void EnterAliasDeclaration(AliasDeclaration ctx);
        public void LeaveAliasDeclaration(AliasDeclaration ctx);
        public void EnterAssignmentStmt(AssignmentStmt ctx);
        public void LeaveAssignmentStmt(AssignmentStmt ctx);
        public void EnterBinaryExpression(BinaryExpression ctx);
        public void LeaveBinaryExpression(BinaryExpression ctx);
        public void EnterBlock(Block ctx);
        public void LeaveBlock(Block ctx);
        public void EnterBoolValueExpression(BoolValueExpression ctx);
        public void LeaveBoolValueExpression(BoolValueExpression ctx);
        public void EnterShortValueExpression(ShortValueExpression ctx);
        public void LeaveShortValueExpression(ShortValueExpression ctx);
        public void EnterIntValueExpression(IntValueExpression ctx);
        public void LeaveIntValueExpression(IntValueExpression ctx);
        public void EnterLongValueExpression(LongValueExpression ctx);
        public void LeaveLongValueExpression(LongValueExpression ctx);
        public void EnterFloatValueExpression(FloatValueExpression ctx);
        public void LeaveFloatValueExpression(FloatValueExpression ctx);
        public void EnterDoubleValueExpression(DoubleValueExpression ctx);
        public void LeaveDoubleValueExpression(DoubleValueExpression ctx);
        public void EnterDecimalValueExpression(DecimalValueExpression ctx);
        public void LeaveDecimalValueExpression(DecimalValueExpression ctx);
        public void EnterStringValueExpression(StringValueExpression ctx);
        public void LeaveStringValueExpression(StringValueExpression ctx);
        public void EnterDateValueExpression(DateValueExpression ctx);
        public void LeaveDateValueExpression(DateValueExpression ctx);
        public void EnterExpressionList(ExpressionList ctx);
        public void LeaveExpressionList(ExpressionList ctx);
        public void EnterFifthProgram(FifthProgram ctx);
        public void LeaveFifthProgram(FifthProgram ctx);
        public void EnterFuncCallExpression(FuncCallExpression ctx);
        public void LeaveFuncCallExpression(FuncCallExpression ctx);
        public void EnterFunctionDefinition(FunctionDefinition ctx);
        public void LeaveFunctionDefinition(FunctionDefinition ctx);
        public void EnterIdentifier(Identifier ctx);
        public void LeaveIdentifier(Identifier ctx);
        public void EnterIdentifierExpression(IdentifierExpression ctx);
        public void LeaveIdentifierExpression(IdentifierExpression ctx);
        public void EnterIfElseStatement(IfElseStatement ctx);
        public void LeaveIfElseStatement(IfElseStatement ctx);
        public void EnterModuleImport(ModuleImport ctx);
        public void LeaveModuleImport(ModuleImport ctx);
        public void EnterParameterDeclaration(ParameterDeclaration ctx);
        public void LeaveParameterDeclaration(ParameterDeclaration ctx);
        public void EnterParameterDeclarationList(ParameterDeclarationList ctx);
        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx);
        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public void EnterTypeInitialiser(TypeInitialiser ctx);
        public void LeaveTypeInitialiser(TypeInitialiser ctx);
        public void EnterUnaryExpression(UnaryExpression ctx);
        public void LeaveUnaryExpression(UnaryExpression ctx);
        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void EnterVariableReference(VariableReference ctx);
        public void LeaveVariableReference(VariableReference ctx);
        public void EnterWhileExp(WhileExp ctx);
        public void LeaveWhileExp(WhileExp ctx);
        public void EnterExpressionStatement(ExpressionStatement ctx);
        public void LeaveExpressionStatement(ExpressionStatement ctx);
        public void EnterExpression(Expression ctx);
        public void LeaveExpression(Expression ctx);
    }
    public partial class BaseAstVisitor : IAstVisitor
    {
        public void EnterTypeCast(TypeCast ctx){}
        public void LeaveTypeCast(TypeCast ctx){}
        public void EnterReturnStatement(ReturnStatement ctx){}
        public void LeaveReturnStatement(ReturnStatement ctx){}
        public void EnterStatementList(StatementList ctx){}
        public void LeaveStatementList(StatementList ctx){}
        public void EnterAbsoluteIri(AbsoluteIri ctx){}
        public void LeaveAbsoluteIri(AbsoluteIri ctx){}
        public void EnterAliasDeclaration(AliasDeclaration ctx){}
        public void LeaveAliasDeclaration(AliasDeclaration ctx){}
        public void EnterAssignmentStmt(AssignmentStmt ctx){}
        public void LeaveAssignmentStmt(AssignmentStmt ctx){}
        public void EnterBinaryExpression(BinaryExpression ctx){}
        public void LeaveBinaryExpression(BinaryExpression ctx){}
        public void EnterBlock(Block ctx){}
        public void LeaveBlock(Block ctx){}
        public void EnterBoolValueExpression(BoolValueExpression ctx){}
        public void LeaveBoolValueExpression(BoolValueExpression ctx){}
        public void EnterShortValueExpression(ShortValueExpression ctx){}
        public void LeaveShortValueExpression(ShortValueExpression ctx){}
        public void EnterIntValueExpression(IntValueExpression ctx){}
        public void LeaveIntValueExpression(IntValueExpression ctx){}
        public void EnterLongValueExpression(LongValueExpression ctx){}
        public void LeaveLongValueExpression(LongValueExpression ctx){}
        public void EnterFloatValueExpression(FloatValueExpression ctx){}
        public void LeaveFloatValueExpression(FloatValueExpression ctx){}
        public void EnterDoubleValueExpression(DoubleValueExpression ctx){}
        public void LeaveDoubleValueExpression(DoubleValueExpression ctx){}
        public void EnterDecimalValueExpression(DecimalValueExpression ctx){}
        public void LeaveDecimalValueExpression(DecimalValueExpression ctx){}
        public void EnterStringValueExpression(StringValueExpression ctx){}
        public void LeaveStringValueExpression(StringValueExpression ctx){}
        public void EnterDateValueExpression(DateValueExpression ctx){}
        public void LeaveDateValueExpression(DateValueExpression ctx){}
        public void EnterExpressionList(ExpressionList ctx){}
        public void LeaveExpressionList(ExpressionList ctx){}
        public void EnterFifthProgram(FifthProgram ctx){}
        public void LeaveFifthProgram(FifthProgram ctx){}
        public void EnterFuncCallExpression(FuncCallExpression ctx){}
        public void LeaveFuncCallExpression(FuncCallExpression ctx){}
        public void EnterFunctionDefinition(FunctionDefinition ctx){}
        public void LeaveFunctionDefinition(FunctionDefinition ctx){}
        public void EnterIdentifier(Identifier ctx){}
        public void LeaveIdentifier(Identifier ctx){}
        public void EnterIdentifierExpression(IdentifierExpression ctx){}
        public void LeaveIdentifierExpression(IdentifierExpression ctx){}
        public void EnterIfElseStatement(IfElseStatement ctx){}
        public void LeaveIfElseStatement(IfElseStatement ctx){}
        public void EnterModuleImport(ModuleImport ctx){}
        public void LeaveModuleImport(ModuleImport ctx){}
        public void EnterParameterDeclaration(ParameterDeclaration ctx){}
        public void LeaveParameterDeclaration(ParameterDeclaration ctx){}
        public void EnterParameterDeclarationList(ParameterDeclarationList ctx){}
        public void LeaveParameterDeclarationList(ParameterDeclarationList ctx){}
        public void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public void EnterTypeInitialiser(TypeInitialiser ctx){}
        public void LeaveTypeInitialiser(TypeInitialiser ctx){}
        public void EnterUnaryExpression(UnaryExpression ctx){}
        public void LeaveUnaryExpression(UnaryExpression ctx){}
        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public void EnterVariableReference(VariableReference ctx){}
        public void LeaveVariableReference(VariableReference ctx){}
        public void EnterWhileExp(WhileExp ctx){}
        public void LeaveWhileExp(WhileExp ctx){}
        public void EnterExpressionStatement(ExpressionStatement ctx){}
        public void LeaveExpressionStatement(ExpressionStatement ctx){}
        public void EnterExpression(Expression ctx){}
        public void LeaveExpression(Expression ctx){}
    }

}

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


    public class TypeCast : Expression
    {
        public TypeCast(Expression SubExpression, TypeId TargetTid)
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
            SubExpression.Accept(visitor);
            visitor.LeaveTypeCast(this);
        }

        
    }

    public class ReturnStatement : AstNode
    {
        public ReturnStatement(Expression SubExpression, TypeId TargetTid)
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
            SubExpression.Accept(visitor);
            visitor.LeaveReturnStatement(this);
        }

        
    }

    public class StatementList : AstNode
    {
        public StatementList(Expression SubExpression, TypeId TargetTid)
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
            visitor.EnterStatementList(this);
            SubExpression.Accept(visitor);
            visitor.LeaveStatementList(this);
        }

        
    }

    public class AbsoluteIri : TypedAstNode
    {
        public AbsoluteIri(string Uri): base(PrimitiveUri.Default.TypeId)
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

    public class AliasDeclaration : AstNode
    {
        public AliasDeclaration(AbsoluteIri IRI, string Name)
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
            IRI.Accept(visitor);
            visitor.LeaveAliasDeclaration(this);
        }

        
    }

    public class AssignmentStmt : Statement
    {
        public AssignmentStmt(Expression Expression, VariableReference VariableRef)
        {
            //_ = Expression ?? throw new ArgumentNullException(nameof(Expression));
            this.Expression = Expression;
            //_ = VariableRef ?? throw new ArgumentNullException(nameof(VariableRef));
            this.VariableRef = VariableRef;
        }

        public Expression Expression{get;set;}
        public VariableReference VariableRef{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterAssignmentStmt(this);
            Expression.Accept(visitor);
            VariableRef.Accept(visitor);
            visitor.LeaveAssignmentStmt(this);
        }

        
    }

    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression Left, Operator? Op, Expression Right)
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
            Left.Accept(visitor);
            Right.Accept(visitor);
            visitor.LeaveBinaryExpression(this);
        }

        
    }

    public class Block : ScopeAstNode
    {
        public Block(List<Statement> Statements)
        {
            //_ = Statements ?? throw new ArgumentNullException(nameof(Statements));
            this.Statements = Statements;
        }

        public List<Statement> Statements{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterBlock(this);
            foreach (var e in Statements)
            {
                e.Accept(visitor);
            }
            visitor.LeaveBlock(this);
        }

        
    }

    public class BoolValueExpression : LiteralExpression<bool>
    {
        public BoolValueExpression(bool TheValue): base(TheValue, PrimitiveBool.Default.TypeId)
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

    public class ShortValueExpression : LiteralExpression<short>
    {
        public ShortValueExpression(short TheValue): base(TheValue, PrimitiveShort.Default.TypeId)
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

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int TheValue): base(TheValue, PrimitiveInteger.Default.TypeId)
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

    public class LongValueExpression : LiteralExpression<long>
    {
        public LongValueExpression(long TheValue): base(TheValue, PrimitiveLong.Default.TypeId)
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

    public class FloatValueExpression : LiteralExpression<float>
    {
        public FloatValueExpression(float TheValue): base(TheValue, PrimitiveFloat.Default.TypeId)
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

    public class DoubleValueExpression : LiteralExpression<double>
    {
        public DoubleValueExpression(double TheValue): base(TheValue, PrimitiveDouble.Default.TypeId)
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

    public class DecimalValueExpression : LiteralExpression<decimal>
    {
        public DecimalValueExpression(decimal TheValue): base(TheValue, PrimitiveDecimal.Default.TypeId)
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

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string TheValue): base(TheValue, PrimitiveString.Default.TypeId)
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

    public class DateValueExpression : LiteralExpression<DateTimeOffset>
    {
        public DateValueExpression(DateTimeOffset TheValue): base(TheValue, PrimitiveDate.Default.TypeId)
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

    public class ExpressionList : TypedAstNode
    {
        public ExpressionList(List<Expression> Expressions)
        {
            //_ = Expressions ?? throw new ArgumentNullException(nameof(Expressions));
            this.Expressions = Expressions;
        }

        public List<Expression> Expressions{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionList(this);
            foreach (var e in Expressions)
            {
                e.Accept(visitor);
            }
            visitor.LeaveExpressionList(this);
        }

        
    }

    public class FifthProgram : ScopeAstNode
    {
        public FifthProgram(List<AliasDeclaration> Aliases, List<FunctionDefinition> Functions)
        {
            //_ = Aliases ?? throw new ArgumentNullException(nameof(Aliases));
            this.Aliases = Aliases;
            //_ = Functions ?? throw new ArgumentNullException(nameof(Functions));
            this.Functions = Functions;
        }

        public List<AliasDeclaration> Aliases{get;set;}
        public List<FunctionDefinition> Functions{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFifthProgram(this);
            foreach (var e in Aliases)
            {
                e.Accept(visitor);
            }
            foreach (var e in Functions)
            {
                e.Accept(visitor);
            }
            visitor.LeaveFifthProgram(this);
        }

        
    }

    public class FuncCallExpression : Expression
    {
        public FuncCallExpression(ExpressionList ActualParameters, string Name)
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
            ActualParameters.Accept(visitor);
            visitor.LeaveFuncCallExpression(this);
        }

        
    }

    public class FunctionDefinition : ScopeAstNode
    {
        public FunctionDefinition(ParameterDeclarationList ParameterDeclarations, Block Body, string Typename, string Name, bool IsEntryPoint, TypeId ReturnType)
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
            ParameterDeclarations.Accept(visitor);
            Body.Accept(visitor);
            visitor.LeaveFunctionDefinition(this);
        }

        
    }

    public class Identifier : TypedAstNode
    {
        public Identifier(string Value)
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

    public class IdentifierExpression : Expression
    {
        public IdentifierExpression(Identifier Identifier)
        {
            //_ = Identifier ?? throw new ArgumentNullException(nameof(Identifier));
            this.Identifier = Identifier;
        }

        public Identifier Identifier{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterIdentifierExpression(this);
            Identifier.Accept(visitor);
            visitor.LeaveIdentifierExpression(this);
        }

        
    }

    public class IfElseStatement : Statement
    {
        public IfElseStatement(Block IfBlock, Block ElseBlock, Expression Condition)
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
            IfBlock.Accept(visitor);
            ElseBlock.Accept(visitor);
            Condition.Accept(visitor);
            visitor.LeaveIfElseStatement(this);
        }

        
    }

    public class ModuleImport : AstNode
    {
        public ModuleImport(string ModuleName)
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

    public class ParameterDeclaration : AstNode
    {
        public ParameterDeclaration(Expression ParameterName, TypeId TypeName, TypeId ParameterType)
        {
            //_ = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.ParameterName = ParameterName;
            //_ = TypeName ?? throw new ArgumentNullException(nameof(TypeName));
            this.TypeName = TypeName;
            //_ = ParameterType ?? throw new ArgumentNullException(nameof(ParameterType));
            this.ParameterType = ParameterType;
        }

        public Expression ParameterName{get;set;}
        public TypeId TypeName{get;set;}
        public TypeId ParameterType{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclaration(this);
            visitor.LeaveParameterDeclaration(this);
        }

        
    }

    public class ParameterDeclarationList : AstNode
    {
        public ParameterDeclarationList(List<ParameterDeclaration> ParameterDeclarations)
        {
            //_ = ParameterDeclarations ?? throw new ArgumentNullException(nameof(ParameterDeclarations));
            this.ParameterDeclarations = ParameterDeclarations;
        }

        public List<ParameterDeclaration> ParameterDeclarations{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterParameterDeclarationList(this);
            foreach (var e in ParameterDeclarations)
            {
                e.Accept(visitor);
            }
            visitor.LeaveParameterDeclarationList(this);
        }

        
    }

    public class TypeCreateInstExpression : Expression
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

    public class TypeInitialiser : Expression
    {
        public TypeInitialiser()
        {
        }


        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterTypeInitialiser(this);
            visitor.LeaveTypeInitialiser(this);
        }

        
    }

    public class UnaryExpression : Expression
    {
        public UnaryExpression(Expression Operand, Operator Op)
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
            Operand.Accept(visitor);
            visitor.LeaveUnaryExpression(this);
        }

        
    }

    public class VariableDeclarationStatement : Statement, ITypedAstNode
    {
        public VariableDeclarationStatement(Expression Expression, Identifier Name)
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
            Expression.Accept(visitor);
            Name.Accept(visitor);
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

    public class VariableReference : TypedAstNode
    {
        public VariableReference(string Name)
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

    public class WhileExp : Statement
    {
        public WhileExp(Expression Condition, Block LoopBlock)
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
            Condition.Accept(visitor);
            visitor.LeaveWhileExp(this);
        }

        
    }

    public class ExpressionStatement : Statement
    {
        public ExpressionStatement(Expression Expression)
        {
            //_ = Expression ?? throw new ArgumentNullException(nameof(Expression));
            this.Expression = Expression;
        }

        public Expression Expression{get;set;}

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterExpressionStatement(this);
            Expression.Accept(visitor);
            visitor.LeaveExpressionStatement(this);
        }

        
    }

    public class Expression : TypedAstNode
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


#endregion // AST Nodes

}
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0021 // Use expression body for constructors
