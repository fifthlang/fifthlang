namespace Fifth.TypeSystem
{
    using System;
    using AST;
    using Fifth.PrimitiveTypes;
    using Symbols;

    public delegate void TypeInferred(IAstNode node, IFifthType type);

    public delegate void TypeNotFound(IAstNode node);

    public delegate void TypeMismatch(IAstNode node, IFifthType type1, IFifthType type2);

    /// <summary>
    /// A framework for handling tasks related to type checking and type inference
    /// </summary>
    /// <remarks>
    /// The basic process of performing type inference is baked into the class.  But what to do with the information
    /// is not. For that purpose it is possible to pass in handler functions that can be used to act on type information
    /// gleaned from the AST.  For example, to annotate the type of the ast node, or to keep track of type mismatch errors etc
    ///
    /// So, where does the action take place in the process of inference?
    /// A.  The task of performing inference on the type takes place in the typed Infer function for the node type.
    ///     i.e. The BinaryExpression node, will own the inference for that kind of node, so it will be the first point
    ///     where the type is know, so is the first place where actions can be performed.
    ///     Similarly, when type checking, there is a corresponding handler that will be the first to know.
    /// </remarks>
    public class FunctionalTypeChecker
    {
        public TypeInferred TypeInferred { get; }
        public TypeNotFound TypeNotFound { get; }
        public TypeMismatch TypeMismatch { get; }

        public FunctionalTypeChecker(TypeInferred typeInferred, TypeNotFound typeNotFound, TypeMismatch typeMismatch)
        {
            TypeInferred = typeInferred;
            TypeNotFound = typeNotFound;
            TypeMismatch = typeMismatch;
        }

        #region Type Inference

        public IFifthType Infer(IScope scope, IAstNode exp)
            => exp switch
            {
                IntValueExpression i => PrimitiveInteger.Default,
                StringValueExpression s => PrimitiveString.Default,
                BinaryExpression be => Infer(scope, be),
                UnaryExpression ue => Infer(scope, ue),
                IdentifierExpression ie => Infer(scope, ie),
                FuncCallExpression fce => Infer(scope, fce),
                { } => null //throw new NotImplementedException("Need to implement other exception types")
            };

        public IFifthType Infer(IScope scope, AbsoluteIri node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, AliasDeclaration node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, AnnotatedThing node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, AssignmentStmt node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, AstNode node) => throw new NotImplementedException();

        public IFifthType Infer(IScope scope, BinaryExpression be)
        {
            Infer(scope, be.Left);
            Infer(scope, be.Right);
            if (be.TryEncode(out var id))
            {
                var fw = InbuiltOperatorRegistry.DefaultRegistry[new OperatorId(id)];
                if (TypeRegistry.PrimitiveMappings.TryGetValue(fw.ResultType, out var result))
                {
                    return result;
                }
            }

            return null;
        }

        public IFifthType Infer(IScope scope, Block node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, BooleanExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, Expression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, ExpressionList node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, FifthProgram node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, FloatValueExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, FuncCallExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, FunctionDefinition node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, Identifier node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, IdentifierExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, IfElseExp node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, IntValueExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, ModuleImport node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, ParameterDeclaration node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, ParameterDeclarationList node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, ScopeAstNode node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, Statement node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, StringValueExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, TypeCreateInstExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, TypeInitialiser node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, TypedAstNode node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, UnaryExpression node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, VariableDeclarationStatement node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, VariableReference node) => throw new NotImplementedException();
        public IFifthType Infer(IScope scope, WhileExp node) => throw new NotImplementedException();

        #endregion

        #region Type Checking

        public void Check(IScope scope, Expression exp, IFifthType type) => throw new NotImplementedException();
        public void Check(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();
        public void Check(IScope scope, FifthProgram prog) => throw new NotImplementedException();

        #endregion

        #region Helper Functions

        public IScope EmptyEnv() => throw new NotImplementedException();
        public IScope Extend(IScope scope, string identifier, IFifthType type) => throw new NotImplementedException();
        public IScope Extend(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public IFifthType Lookup(object o) => throw new NotImplementedException();

        public IFifthType LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();

        public IScope NewBlock(IScope scope) => throw new NotImplementedException();

        public bool TryInferOperationResultType(Operator op, IFifthType lhsType, IFifthType rhsType,
            out IFifthType resultType) => throw new NotImplementedException();

        #endregion

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
        //             - IfElseExp
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
