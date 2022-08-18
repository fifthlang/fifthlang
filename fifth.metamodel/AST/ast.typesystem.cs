
namespace Fifth.TypeSystem;
using AST;
using Symbols;

public interface ITypeChecker
{
    public IType Infer(IScope scope, Assembly node);
    public IType Infer(IScope scope, AssemblyRef node);
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
    public IType Infer(IScope scope, ParameterDeclarationList node);
    public IType Infer(IScope scope, ParameterDeclaration node);
    public IType Infer(IScope scope, DestructuringDeclaration node);
    public IType Infer(IScope scope, DestructuringBinding node);
    public IType Infer(IScope scope, TypeCreateInstExpression node);
    public IType Infer(IScope scope, TypeInitialiser node);
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
            Assembly node => Infer(scope, node),
            AssemblyRef node => Infer(scope, node),
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
            ParameterDeclarationList node => Infer(scope, node),
            ParameterDeclaration node => Infer(scope, node),
            DestructuringDeclaration node => Infer(scope, node),
            DestructuringBinding node => Infer(scope, node),
            TypeCreateInstExpression node => Infer(scope, node),
            TypeInitialiser node => Infer(scope, node),
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
