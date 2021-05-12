
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
        public void EnterClassDefinition(ClassDefinition ctx);
        public void LeaveClassDefinition(ClassDefinition ctx);
        public void EnterPropertyDefinition(PropertyDefinition ctx);
        public void LeavePropertyDefinition(PropertyDefinition ctx);
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
        public void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
        public void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
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
        public void EnterDestructuringParamDecl(DestructuringParamDecl ctx);
        public void LeaveDestructuringParamDecl(DestructuringParamDecl ctx);
        public void EnterPropertyBinding(PropertyBinding ctx);
        public void LeavePropertyBinding(PropertyBinding ctx);
        public void EnterTypePropertyInit(TypePropertyInit ctx);
        public void LeaveTypePropertyInit(TypePropertyInit ctx);
        public void EnterUnaryExpression(UnaryExpression ctx);
        public void LeaveUnaryExpression(UnaryExpression ctx);
        public void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public void EnterVariableReference(VariableReference ctx);
        public void LeaveVariableReference(VariableReference ctx);
        public void EnterCompoundVariableReference(CompoundVariableReference ctx);
        public void LeaveCompoundVariableReference(CompoundVariableReference ctx);
        public void EnterWhileExp(WhileExp ctx);
        public void LeaveWhileExp(WhileExp ctx);
        public void EnterExpressionStatement(ExpressionStatement ctx);
        public void LeaveExpressionStatement(ExpressionStatement ctx);
        public void EnterExpression(Expression ctx);
        public void LeaveExpression(Expression ctx);
    }

    public partial class BaseAstVisitor : IAstVisitor
    {
        public virtual void EnterClassDefinition(ClassDefinition ctx){}
        public virtual void LeaveClassDefinition(ClassDefinition ctx){}
        public virtual void EnterPropertyDefinition(PropertyDefinition ctx){}
        public virtual void LeavePropertyDefinition(PropertyDefinition ctx){}
        public virtual void EnterTypeCast(TypeCast ctx){}
        public virtual void LeaveTypeCast(TypeCast ctx){}
        public virtual void EnterReturnStatement(ReturnStatement ctx){}
        public virtual void LeaveReturnStatement(ReturnStatement ctx){}
        public virtual void EnterStatementList(StatementList ctx){}
        public virtual void LeaveStatementList(StatementList ctx){}
        public virtual void EnterAbsoluteIri(AbsoluteIri ctx){}
        public virtual void LeaveAbsoluteIri(AbsoluteIri ctx){}
        public virtual void EnterAliasDeclaration(AliasDeclaration ctx){}
        public virtual void LeaveAliasDeclaration(AliasDeclaration ctx){}
        public virtual void EnterAssignmentStmt(AssignmentStmt ctx){}
        public virtual void LeaveAssignmentStmt(AssignmentStmt ctx){}
        public virtual void EnterBinaryExpression(BinaryExpression ctx){}
        public virtual void LeaveBinaryExpression(BinaryExpression ctx){}
        public virtual void EnterBlock(Block ctx){}
        public virtual void LeaveBlock(Block ctx){}
        public virtual void EnterBoolValueExpression(BoolValueExpression ctx){}
        public virtual void LeaveBoolValueExpression(BoolValueExpression ctx){}
        public virtual void EnterShortValueExpression(ShortValueExpression ctx){}
        public virtual void LeaveShortValueExpression(ShortValueExpression ctx){}
        public virtual void EnterIntValueExpression(IntValueExpression ctx){}
        public virtual void LeaveIntValueExpression(IntValueExpression ctx){}
        public virtual void EnterLongValueExpression(LongValueExpression ctx){}
        public virtual void LeaveLongValueExpression(LongValueExpression ctx){}
        public virtual void EnterFloatValueExpression(FloatValueExpression ctx){}
        public virtual void LeaveFloatValueExpression(FloatValueExpression ctx){}
        public virtual void EnterDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void LeaveDoubleValueExpression(DoubleValueExpression ctx){}
        public virtual void EnterDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void LeaveDecimalValueExpression(DecimalValueExpression ctx){}
        public virtual void EnterStringValueExpression(StringValueExpression ctx){}
        public virtual void LeaveStringValueExpression(StringValueExpression ctx){}
        public virtual void EnterDateValueExpression(DateValueExpression ctx){}
        public virtual void LeaveDateValueExpression(DateValueExpression ctx){}
        public virtual void EnterExpressionList(ExpressionList ctx){}
        public virtual void LeaveExpressionList(ExpressionList ctx){}
        public virtual void EnterFifthProgram(FifthProgram ctx){}
        public virtual void LeaveFifthProgram(FifthProgram ctx){}
        public virtual void EnterFuncCallExpression(FuncCallExpression ctx){}
        public virtual void LeaveFuncCallExpression(FuncCallExpression ctx){}
        public virtual void EnterFunctionDefinition(FunctionDefinition ctx){}
        public virtual void LeaveFunctionDefinition(FunctionDefinition ctx){}
        public virtual void EnterBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx){}
        public virtual void LeaveBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx){}
        public virtual void EnterOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx){}
        public virtual void LeaveOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx){}
        public virtual void EnterIdentifier(Identifier ctx){}
        public virtual void LeaveIdentifier(Identifier ctx){}
        public virtual void EnterIdentifierExpression(IdentifierExpression ctx){}
        public virtual void LeaveIdentifierExpression(IdentifierExpression ctx){}
        public virtual void EnterIfElseStatement(IfElseStatement ctx){}
        public virtual void LeaveIfElseStatement(IfElseStatement ctx){}
        public virtual void EnterModuleImport(ModuleImport ctx){}
        public virtual void LeaveModuleImport(ModuleImport ctx){}
        public virtual void EnterParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void LeaveParameterDeclaration(ParameterDeclaration ctx){}
        public virtual void EnterParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void LeaveParameterDeclarationList(ParameterDeclarationList ctx){}
        public virtual void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx){}
        public virtual void EnterTypeInitialiser(TypeInitialiser ctx){}
        public virtual void LeaveTypeInitialiser(TypeInitialiser ctx){}
        public virtual void EnterDestructuringParamDecl(DestructuringParamDecl ctx){}
        public virtual void LeaveDestructuringParamDecl(DestructuringParamDecl ctx){}
        public virtual void EnterPropertyBinding(PropertyBinding ctx){}
        public virtual void LeavePropertyBinding(PropertyBinding ctx){}
        public virtual void EnterTypePropertyInit(TypePropertyInit ctx){}
        public virtual void LeaveTypePropertyInit(TypePropertyInit ctx){}
        public virtual void EnterUnaryExpression(UnaryExpression ctx){}
        public virtual void LeaveUnaryExpression(UnaryExpression ctx){}
        public virtual void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx){}
        public virtual void EnterVariableReference(VariableReference ctx){}
        public virtual void LeaveVariableReference(VariableReference ctx){}
        public virtual void EnterCompoundVariableReference(CompoundVariableReference ctx){}
        public virtual void LeaveCompoundVariableReference(CompoundVariableReference ctx){}
        public virtual void EnterWhileExp(WhileExp ctx){}
        public virtual void LeaveWhileExp(WhileExp ctx){}
        public virtual void EnterExpressionStatement(ExpressionStatement ctx){}
        public virtual void LeaveExpressionStatement(ExpressionStatement ctx){}
        public virtual void EnterExpression(Expression ctx){}
        public virtual void LeaveExpression(Expression ctx){}
    }


    public interface IAstRecursiveDescentVisitor
    {
        public ClassDefinition VisitClassDefinition(ClassDefinition ctx);
        public PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx);
        public TypeCast VisitTypeCast(TypeCast ctx);
        public ReturnStatement VisitReturnStatement(ReturnStatement ctx);
        public StatementList VisitStatementList(StatementList ctx);
        public AbsoluteIri VisitAbsoluteIri(AbsoluteIri ctx);
        public AliasDeclaration VisitAliasDeclaration(AliasDeclaration ctx);
        public AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx);
        public BinaryExpression VisitBinaryExpression(BinaryExpression ctx);
        public Block VisitBlock(Block ctx);
        public BoolValueExpression VisitBoolValueExpression(BoolValueExpression ctx);
        public ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx);
        public IntValueExpression VisitIntValueExpression(IntValueExpression ctx);
        public LongValueExpression VisitLongValueExpression(LongValueExpression ctx);
        public FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx);
        public DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx);
        public DecimalValueExpression VisitDecimalValueExpression(DecimalValueExpression ctx);
        public StringValueExpression VisitStringValueExpression(StringValueExpression ctx);
        public DateValueExpression VisitDateValueExpression(DateValueExpression ctx);
        public ExpressionList VisitExpressionList(ExpressionList ctx);
        public FifthProgram VisitFifthProgram(FifthProgram ctx);
        public FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx);
        public FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx);
        public BuiltinFunctionDefinition VisitBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx);
        public OverloadedFunctionDefinition VisitOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx);
        public Identifier VisitIdentifier(Identifier ctx);
        public IdentifierExpression VisitIdentifierExpression(IdentifierExpression ctx);
        public IfElseStatement VisitIfElseStatement(IfElseStatement ctx);
        public ModuleImport VisitModuleImport(ModuleImport ctx);
        public ParameterDeclaration VisitParameterDeclaration(ParameterDeclaration ctx);
        public ParameterDeclarationList VisitParameterDeclarationList(ParameterDeclarationList ctx);
        public TypeCreateInstExpression VisitTypeCreateInstExpression(TypeCreateInstExpression ctx);
        public TypeInitialiser VisitTypeInitialiser(TypeInitialiser ctx);
        public DestructuringParamDecl VisitDestructuringParamDecl(DestructuringParamDecl ctx);
        public PropertyBinding VisitPropertyBinding(PropertyBinding ctx);
        public TypePropertyInit VisitTypePropertyInit(TypePropertyInit ctx);
        public UnaryExpression VisitUnaryExpression(UnaryExpression ctx);
        public VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx);
        public VariableReference VisitVariableReference(VariableReference ctx);
        public CompoundVariableReference VisitCompoundVariableReference(CompoundVariableReference ctx);
        public WhileExp VisitWhileExp(WhileExp ctx);
        public ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx);
        public Expression VisitExpression(Expression ctx);
    }

    public class DefaultRecursiveDescentVisitor : IAstRecursiveDescentVisitor
    {
        public virtual ClassDefinition VisitClassDefinition(ClassDefinition ctx)
            => ctx;
        public virtual PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
            => ctx;
        public virtual TypeCast VisitTypeCast(TypeCast ctx)
            => ctx;
        public virtual ReturnStatement VisitReturnStatement(ReturnStatement ctx)
            => ctx;
        public virtual StatementList VisitStatementList(StatementList ctx)
            => ctx;
        public virtual AbsoluteIri VisitAbsoluteIri(AbsoluteIri ctx)
            => ctx;
        public virtual AliasDeclaration VisitAliasDeclaration(AliasDeclaration ctx)
            => ctx;
        public virtual AssignmentStmt VisitAssignmentStmt(AssignmentStmt ctx)
            => ctx;
        public virtual BinaryExpression VisitBinaryExpression(BinaryExpression ctx)
            => ctx;
        public virtual Block VisitBlock(Block ctx)
            => ctx;
        public virtual BoolValueExpression VisitBoolValueExpression(BoolValueExpression ctx)
            => ctx;
        public virtual ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx)
            => ctx;
        public virtual IntValueExpression VisitIntValueExpression(IntValueExpression ctx)
            => ctx;
        public virtual LongValueExpression VisitLongValueExpression(LongValueExpression ctx)
            => ctx;
        public virtual FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx)
            => ctx;
        public virtual DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx)
            => ctx;
        public virtual DecimalValueExpression VisitDecimalValueExpression(DecimalValueExpression ctx)
            => ctx;
        public virtual StringValueExpression VisitStringValueExpression(StringValueExpression ctx)
            => ctx;
        public virtual DateValueExpression VisitDateValueExpression(DateValueExpression ctx)
            => ctx;
        public virtual ExpressionList VisitExpressionList(ExpressionList ctx)
            => ctx;
        public virtual FifthProgram VisitFifthProgram(FifthProgram ctx)
            => ctx;
        public virtual FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx)
            => ctx;
        public virtual FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx)
            => ctx;
        public virtual BuiltinFunctionDefinition VisitBuiltinFunctionDefinition(BuiltinFunctionDefinition ctx)
            => ctx;
        public virtual OverloadedFunctionDefinition VisitOverloadedFunctionDefinition(OverloadedFunctionDefinition ctx)
            => ctx;
        public virtual Identifier VisitIdentifier(Identifier ctx)
            => ctx;
        public virtual IdentifierExpression VisitIdentifierExpression(IdentifierExpression ctx)
            => ctx;
        public virtual IfElseStatement VisitIfElseStatement(IfElseStatement ctx)
            => ctx;
        public virtual ModuleImport VisitModuleImport(ModuleImport ctx)
            => ctx;
        public virtual ParameterDeclaration VisitParameterDeclaration(ParameterDeclaration ctx)
            => ctx;
        public virtual ParameterDeclarationList VisitParameterDeclarationList(ParameterDeclarationList ctx)
            => ctx;
        public virtual TypeCreateInstExpression VisitTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => ctx;
        public virtual TypeInitialiser VisitTypeInitialiser(TypeInitialiser ctx)
            => ctx;
        public virtual DestructuringParamDecl VisitDestructuringParamDecl(DestructuringParamDecl ctx)
            => ctx;
        public virtual PropertyBinding VisitPropertyBinding(PropertyBinding ctx)
            => ctx;
        public virtual TypePropertyInit VisitTypePropertyInit(TypePropertyInit ctx)
            => ctx;
        public virtual UnaryExpression VisitUnaryExpression(UnaryExpression ctx)
            => ctx;
        public virtual VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx)
            => ctx;
        public virtual VariableReference VisitVariableReference(VariableReference ctx)
            => ctx;
        public virtual CompoundVariableReference VisitCompoundVariableReference(CompoundVariableReference ctx)
            => ctx;
        public virtual WhileExp VisitWhileExp(WhileExp ctx)
            => ctx;
        public virtual ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx)
            => ctx;
        public virtual Expression VisitExpression(Expression ctx)
            => ctx;
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


    public partial class ClassDefinition : ScopeAstNode, ITypedAstNode, IFunctionCollection
    {
        public ClassDefinition(string Name, List<PropertyDefinition> Properties, List<IFunctionDefinition> Functions)
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
        public PropertyDefinition(string Name, string TypeName)
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
            if(SubExpression != null) {
                SubExpression.Accept(visitor);
            }
            visitor.LeaveTypeCast(this);
        }

        
    }

    public partial class ReturnStatement : Statement
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
            if(SubExpression != null) {
                SubExpression.Accept(visitor);
            }
            visitor.LeaveReturnStatement(this);
        }

        
    }

    public partial class StatementList : AstNode
    {
        public StatementList(List<Statement> Statements)
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

    public partial class AliasDeclaration : AstNode
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
            if(IRI != null) {
                IRI.Accept(visitor);
            }
            visitor.LeaveAliasDeclaration(this);
        }

        
    }

    public partial class AssignmentStmt : Statement
    {
        public AssignmentStmt(Expression Expression, BaseVarReference VariableRef)
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
            if(Expression != null) {
                Expression.Accept(visitor);
            }
            if(VariableRef != null) {
                VariableRef.Accept(visitor);
            }
            visitor.LeaveAssignmentStmt(this);
        }

        
    }

    public partial class BinaryExpression : Expression
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
            if(Left != null) {
                Left.Accept(visitor);
            }
            if(Right != null) {
                Right.Accept(visitor);
            }
            visitor.LeaveBinaryExpression(this);
        }

        
    }

    public partial class Block : ScopeAstNode
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

    public partial class ShortValueExpression : LiteralExpression<short>
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

    public partial class IntValueExpression : LiteralExpression<int>
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

    public partial class LongValueExpression : LiteralExpression<long>
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

    public partial class FloatValueExpression : LiteralExpression<float>
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

    public partial class DoubleValueExpression : LiteralExpression<double>
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

    public partial class DecimalValueExpression : LiteralExpression<decimal>
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

    public partial class StringValueExpression : LiteralExpression<string>
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

    public partial class DateValueExpression : LiteralExpression<DateTimeOffset>
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

    public partial class ExpressionList : TypedAstNode
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
        public FifthProgram(List<AliasDeclaration> Aliases, List<ClassDefinition> Classes, List<IFunctionDefinition> Functions)
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
            if(ActualParameters != null) {
                ActualParameters.Accept(visitor);
            }
            visitor.LeaveFuncCallExpression(this);
        }

        
    }

    public partial class FunctionDefinition : ScopeAstNode, IFunctionDefinition
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
            if(ParameterDeclarations != null) {
                ParameterDeclarations.Accept(visitor);
            }
            if(Body != null) {
                Body.Accept(visitor);
            }
            visitor.LeaveFunctionDefinition(this);
        }

        
    }

    public partial class BuiltinFunctionDefinition : AstNode, IFunctionDefinition
    {
        public BuiltinFunctionDefinition()
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
        public OverloadedFunctionDefinition(List<IFunctionDefinition> OverloadClauses, IFunctionSignature Signature)
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

    public partial class IdentifierExpression : Expression
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
            if(Identifier != null) {
                Identifier.Accept(visitor);
            }
            visitor.LeaveIdentifierExpression(this);
        }

        
    }

    public partial class IfElseStatement : Statement
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
            if(IfBlock != null) {
                IfBlock.Accept(visitor);
            }
            if(ElseBlock != null) {
                ElseBlock.Accept(visitor);
            }
            if(Condition != null) {
                Condition.Accept(visitor);
            }
            visitor.LeaveIfElseStatement(this);
        }

        
    }

    public partial class ModuleImport : AstNode
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

    public partial class ParameterDeclaration : TypedAstNode, IParameterListItem
    {
        public ParameterDeclaration(Identifier ParameterName, string TypeName, Expression Constraint)
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
            if(ParameterName != null) {
                ParameterName.Accept(visitor);
            }
            if(Constraint != null) {
                Constraint.Accept(visitor);
            }
            visitor.LeaveParameterDeclaration(this);
        }

        
    }

    public partial class ParameterDeclarationList : AstNode
    {
        public ParameterDeclarationList(List<IParameterListItem> ParameterDeclarations)
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
        public TypeInitialiser(string TypeName, List<TypePropertyInit> PropertyInitialisers)
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

    public partial class DestructuringParamDecl : TypedAstNode, IParameterListItem
    {
        public DestructuringParamDecl(string TypeName, Identifier ParameterName, List<PropertyBinding> PropertyBindings)
        {
            //_ = TypeName ?? throw new ArgumentNullException(nameof(TypeName));
            this.TypeName = TypeName;
            //_ = ParameterName ?? throw new ArgumentNullException(nameof(ParameterName));
            this.ParameterName = ParameterName;
            //_ = PropertyBindings ?? throw new ArgumentNullException(nameof(PropertyBindings));
            this.PropertyBindings = PropertyBindings;
        }

        public string TypeName{get;set;}
        public Identifier ParameterName{get;set;}
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

    public partial class PropertyBinding : AstNode
    {
        public PropertyBinding(string BoundPropertyName, string BoundVariableName, Expression Constraint)
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
            if(Constraint != null) {
                Constraint.Accept(visitor);
            }
            visitor.LeavePropertyBinding(this);
        }

        
        public PropertyDefinition BoundProperty { get; set; }
    
    }

    public partial class TypePropertyInit : AstNode
    {
        public TypePropertyInit(string Name, Expression Value)
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
            visitor.LeaveTypePropertyInit(this);
        }

        
    }

    public partial class UnaryExpression : Expression
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
            if(Operand != null) {
                Operand.Accept(visitor);
            }
            visitor.LeaveUnaryExpression(this);
        }

        
    }

    public partial class VariableDeclarationStatement : Statement, ITypedAstNode
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
            if(Expression != null) {
                Expression.Accept(visitor);
            }
            if(Name != null) {
                Name.Accept(visitor);
            }
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

    public partial class CompoundVariableReference : BaseVarReference
    {
        public CompoundVariableReference(List<VariableReference> ComponentReferences)
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
            if(Condition != null) {
                Condition.Accept(visitor);
            }
            visitor.LeaveWhileExp(this);
        }

        
    }

    public partial class ExpressionStatement : Statement
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
            if(Expression != null) {
                Expression.Accept(visitor);
            }
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


#endregion // AST Nodes

}

namespace Fifth.TypeSystem
{
    using AST;
    using Symbols;

    public interface ITypeChecker
    {
        public IType Infer(IScope scope, ClassDefinition node);
        public IType Infer(IScope scope, PropertyDefinition node);
        public IType Infer(IScope scope, TypeCast node);
        public IType Infer(IScope scope, ReturnStatement node);
        public IType Infer(IScope scope, StatementList node);
        public IType Infer(IScope scope, AbsoluteIri node);
        public IType Infer(IScope scope, AliasDeclaration node);
        public IType Infer(IScope scope, AssignmentStmt node);
        public IType Infer(IScope scope, BinaryExpression node);
        public IType Infer(IScope scope, Block node);
        public IType Infer(IScope scope, BoolValueExpression node);
        public IType Infer(IScope scope, ShortValueExpression node);
        public IType Infer(IScope scope, IntValueExpression node);
        public IType Infer(IScope scope, LongValueExpression node);
        public IType Infer(IScope scope, FloatValueExpression node);
        public IType Infer(IScope scope, DoubleValueExpression node);
        public IType Infer(IScope scope, DecimalValueExpression node);
        public IType Infer(IScope scope, StringValueExpression node);
        public IType Infer(IScope scope, DateValueExpression node);
        public IType Infer(IScope scope, ExpressionList node);
        public IType Infer(IScope scope, FifthProgram node);
        public IType Infer(IScope scope, FuncCallExpression node);
        public IType Infer(IScope scope, FunctionDefinition node);
        public IType Infer(IScope scope, BuiltinFunctionDefinition node);
        public IType Infer(IScope scope, OverloadedFunctionDefinition node);
        public IType Infer(IScope scope, Identifier node);
        public IType Infer(IScope scope, IdentifierExpression node);
        public IType Infer(IScope scope, IfElseStatement node);
        public IType Infer(IScope scope, ModuleImport node);
        public IType Infer(IScope scope, ParameterDeclaration node);
        public IType Infer(IScope scope, ParameterDeclarationList node);
        public IType Infer(IScope scope, TypeCreateInstExpression node);
        public IType Infer(IScope scope, TypeInitialiser node);
        public IType Infer(IScope scope, DestructuringParamDecl node);
        public IType Infer(IScope scope, PropertyBinding node);
        public IType Infer(IScope scope, TypePropertyInit node);
        public IType Infer(IScope scope, UnaryExpression node);
        public IType Infer(IScope scope, VariableDeclarationStatement node);
        public IType Infer(IScope scope, VariableReference node);
        public IType Infer(IScope scope, CompoundVariableReference node);
        public IType Infer(IScope scope, WhileExp node);
        public IType Infer(IScope scope, ExpressionStatement node);
        public IType Infer(IScope scope, Expression node);
    }

    public partial class FunctionalTypeChecker
    {

        public IType Infer(AstNode exp)
        {
            var scope = exp.NearestScope();
            return exp switch
            {
                ClassDefinition node => Infer(scope, node),
                PropertyDefinition node => Infer(scope, node),
                TypeCast node => Infer(scope, node),
                ReturnStatement node => Infer(scope, node),
                StatementList node => Infer(scope, node),
                AbsoluteIri node => Infer(scope, node),
                AliasDeclaration node => Infer(scope, node),
                AssignmentStmt node => Infer(scope, node),
                BinaryExpression node => Infer(scope, node),
                Block node => Infer(scope, node),
                BoolValueExpression node => Infer(scope, node),
                ShortValueExpression node => Infer(scope, node),
                IntValueExpression node => Infer(scope, node),
                LongValueExpression node => Infer(scope, node),
                FloatValueExpression node => Infer(scope, node),
                DoubleValueExpression node => Infer(scope, node),
                DecimalValueExpression node => Infer(scope, node),
                StringValueExpression node => Infer(scope, node),
                DateValueExpression node => Infer(scope, node),
                ExpressionList node => Infer(scope, node),
                FifthProgram node => Infer(scope, node),
                FuncCallExpression node => Infer(scope, node),
                FunctionDefinition node => Infer(scope, node),
                BuiltinFunctionDefinition node => Infer(scope, node),
                OverloadedFunctionDefinition node => Infer(scope, node),
                Identifier node => Infer(scope, node),
                IdentifierExpression node => Infer(scope, node),
                IfElseStatement node => Infer(scope, node),
                ModuleImport node => Infer(scope, node),
                ParameterDeclaration node => Infer(scope, node),
                ParameterDeclarationList node => Infer(scope, node),
                TypeCreateInstExpression node => Infer(scope, node),
                TypeInitialiser node => Infer(scope, node),
                DestructuringParamDecl node => Infer(scope, node),
                PropertyBinding node => Infer(scope, node),
                TypePropertyInit node => Infer(scope, node),
                UnaryExpression node => Infer(scope, node),
                VariableDeclarationStatement node => Infer(scope, node),
                VariableReference node => Infer(scope, node),
                CompoundVariableReference node => Infer(scope, node),
                WhileExp node => Infer(scope, node),
                ExpressionStatement node => Infer(scope, node),
                Expression node => Infer(scope, node),

                { } node => Infer(scope, node),
            };
        }


    }
}


#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0021 // Use expression body for constructors
