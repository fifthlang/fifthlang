namespace Fifth.TypeSystem
{
    using System;
    using AST;
    using Fifth.PrimitiveTypes;
    using Symbols;

    public delegate void TypeInferred(IAstNode node, IType type);

    public delegate void TypeNotFound(IAstNode node);

    public delegate void TypeMismatch(IAstNode node, IType type1, IType type2);

    /// <summary>
    ///     A framework for handling tasks related to type checking and type inference
    /// </summary>
    /// <remarks>
    ///     The basic process of performing type inference is baked into the class.  But what to do with the information
    ///     is not. For that purpose it is possible to pass in handler functions that can be used to act on type information
    ///     gleaned from the AST.  For example, to annotate the type of the ast node, or to keep track of type mismatch errors
    ///     etc
    ///     So, where does the action take place in the process of inference?
    ///     A.  The task of performing inference on the type takes place in the typed Infer function for the node type.
    ///     i.e. The BinaryExpression node, will own the inference for that kind of node, so it will be the first point
    ///     where the type is know, so is the first place where actions can be performed.
    ///     Similarly, when type checking, there is a corresponding handler that will be the first to know.
    /// </remarks>
    public class FunctionalTypeChecker
    {
        public FunctionalTypeChecker(TypeInferred typeInferred, TypeNotFound typeNotFound, TypeMismatch typeMismatch)
        {
            TypeInferred = typeInferred;
            TypeNotFound = typeNotFound;
            TypeMismatch = typeMismatch;
        }

        public TypeInferred TypeInferred { get; }
        public TypeNotFound TypeNotFound { get; }
        public TypeMismatch TypeMismatch { get; }

        #region Type Inference

        public IType Infer(IScope scope, IAstNode exp)
            => exp switch
            {
                ShortValueExpression i => PrimitiveShort.Default,
                IntValueExpression i => PrimitiveInteger.Default,
                LongValueExpression lve => PrimitiveLong.Default,
                FloatValueExpression node => PrimitiveFloat.Default,
                StringValueExpression s => PrimitiveString.Default,
                DoubleValueExpression s => PrimitiveDouble.Default,
                DecimalValueExpression s => PrimitiveDecimal.Default,
                DateValueExpression s => PrimitiveDate.Default,
                BinaryExpression be => Infer(scope, be),
                UnaryExpression ue => Infer(scope, ue),
                IdentifierExpression ie => Infer(scope, ie),
                FuncCallExpression fce => Infer(scope, fce),
                AbsoluteIri node => Infer(scope, node),
                AliasDeclaration node => Infer(scope, node),
                AssignmentStmt node => Infer(scope, node),
                Block node => Infer(scope, node),
                BooleanExpression node => Infer(scope, node),
                ExpressionList node => Infer(scope, node),
                FifthProgram node => Infer(scope, node),
                FunctionDefinition node => Infer(scope, node),
                Identifier node => Infer(scope, node),
                IfElseStatement node => Infer(scope, node),
                ModuleImport node => Infer(scope, node),
                ParameterDeclaration node => Infer(scope, node),
                ParameterDeclarationList node => Infer(scope, node),
                ScopeAstNode node => Infer(scope, node),
                TypeCreateInstExpression node => Infer(scope, node),
                TypeInitialiser node => Infer(scope, node),
                VariableDeclarationStatement node => Infer(scope, node),
                VariableReference node => Infer(scope, node),
                WhileExp node => Infer(scope, node),
                Statement node => Infer(scope, node),
                Expression node => Infer(scope, node),
                TypedAstNode node => Infer(scope, node),
                AstNode node => Infer(scope, node),

                { } => null //throw new NotImplementedException("Need to implement other exception types")
            };

        public IType Infer(IScope scope, AbsoluteIri node) => throw new NotImplementedException();

        public IType Infer(IScope scope, AliasDeclaration node) => throw new NotImplementedException();

        public IType Infer(IScope scope, AnnotatedThing node) => throw new NotImplementedException();

        public IType Infer(IScope scope, AssignmentStmt node) => throw new NotImplementedException();

        public IType Infer(IScope scope, AstNode node) => throw new NotImplementedException();

        public IType Infer(IScope scope, BinaryExpression be)
        {
            var lhsTid = Infer(scope, be.Left);
            var rhsTid = Infer(scope, be.Right);
            var (tid, lhsCoercion, rhsCoercion) =
                OperatorPrecedenceCalculator.GetResultType(be.Op, lhsTid.TypeId, rhsTid.TypeId);
            return tid.Lookup();
        }

        public IType Infer(IScope scope, Block node) => throw new NotImplementedException();

        public IType Infer(IScope scope, BooleanExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, Expression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ExpressionList node) => throw new NotImplementedException();

        public IType Infer(IScope scope, FifthProgram node) => throw new NotImplementedException();

        public IType Infer(IScope scope, FloatValueExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, FuncCallExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, FunctionDefinition node) => throw new NotImplementedException();

        public IType Infer(IScope scope, Identifier node) => throw new NotImplementedException();

        public IType Infer(IScope scope, IdentifierExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, IfElseStatement node) => throw new NotImplementedException();

        public IType Infer(IScope scope, IntValueExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ModuleImport node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ParameterDeclaration node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ParameterDeclarationList node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ScopeAstNode node) => throw new NotImplementedException();

        public IType Infer(IScope scope, Statement node) => throw new NotImplementedException();

        public IType Infer(IScope scope, StringValueExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, TypeCreateInstExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, TypeInitialiser node) => throw new NotImplementedException();

        public IType Infer(IScope scope, TypedAstNode node) => throw new NotImplementedException();

        public IType Infer(IScope scope, UnaryExpression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, VariableDeclarationStatement node) => throw new NotImplementedException();

        public IType Infer(IScope scope, VariableReference node) => throw new NotImplementedException();

        public IType Infer(IScope scope, WhileExp node) => throw new NotImplementedException();

        #endregion Type Inference

        #region Type Checking

        public void Check(IScope scope, Expression exp, IType type) => throw new NotImplementedException();

        public void Check(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public void Check(IScope scope, FifthProgram prog) => throw new NotImplementedException();

        #endregion Type Checking

        #region Helper Functions

        public IScope EmptyEnv() => throw new NotImplementedException();

        public IScope Extend(IScope scope, string identifier, IType type) => throw new NotImplementedException();

        public IScope Extend(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public IType Lookup(object o) => throw new NotImplementedException();

        public IType LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();

        public IScope NewBlock(IScope scope) => throw new NotImplementedException();

        public bool TryInferOperationResultType(Operator op, IType lhsType, IType rhsType,
            out IType resultType) => throw new NotImplementedException();

        #endregion Helper Functions

        // - AbsoluteIri
        // - AliasDeclaration
        // - FifthProgram
        // - FunctionDefinition
        // - Identifier
        // - ModuleImport
        // - ParameterDeclaration
        // - ParameterDeclarationList
        // - TypedAstNode : ITypedAstNode
        //     - Expression
        //         - AssignmentStmt
        //         - BinaryExpression
        //         - FuncCallExpression
        //         - IdentifierExpression
        //         - LiteralExpression
        //             - BooleanExpression
        //             - FloatValueExpression
        //             - IntValueExpression
        //             - StringValueExpression
        //         - Statement
        //             - IfElseStatement
        //             - VariableDeclarationStatement(?????)
        //             - WhileExp
        //         - TypeCreateInstExpression
        //         - UnaryExpression
        //         - VariableReference
        //     - ExpressionList
        //     - ScopeAstNode : IScope
        //         - Block
        // - TypeInitialiser
    }
}
