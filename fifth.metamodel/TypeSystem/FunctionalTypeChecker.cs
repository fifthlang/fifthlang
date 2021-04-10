namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AST;
    using Fifth.PrimitiveTypes;
    using PrimitiveTypes;
    using Symbols;

    public delegate void TypeInferred(IAstNode node, IType type);
    public delegate void TypeNotFound(IAstNode node);
    public delegate void TypeNotRelevant(IAstNode node);
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
    public partial class FunctionalTypeChecker : ITypeChecker
    {
        public FunctionalTypeChecker(TypeInferred typeInferred, TypeNotFound typeNotFound, TypeMismatch typeMismatch, TypeNotRelevant typeNotRelevant)
        {
            TypeInferred = typeInferred;
            TypeNotFound = typeNotFound;
            TypeMismatch = typeMismatch;
            TypeNotRelevant = typeNotRelevant;
        }

        public TypeInferred TypeInferred { get; }
        public TypeNotFound TypeNotFound { get; }
        public TypeMismatch TypeMismatch { get; }
        public TypeNotRelevant TypeNotRelevant { get; }

        #region Type Inference

        /*
        public IType Infer(AstNode exp)
        {
            var scope = exp.NearestScope();
            return exp switch
            {
                TypeCast node => Infer(scope, node),
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
                ReturnStatement node => Infer(scope, node),
                Expression node => Infer(scope, node),
                TypedAstNode node => Infer(scope, node),
                { } node => Infer(scope, node),
            };
        }
        */

        public IType Infer(IScope scope, AbsoluteIri node)
        {
            return PrimitiveUri.Default;
        }

        public IType Infer(IScope scope, AliasDeclaration node) => default;

        public IType Infer(IScope scope, AnnotatedThing node) => default;

        public IType Infer(IScope _, AssignmentStmt node)
        {
            var scope = node.NearestScope();
            IType expType;
            var varType = Infer(scope, node.VariableRef);
            if (node.Expression != null)
            {
                expType = Infer(scope, node.Expression);
                if (expType.TypeId != varType.TypeId)
                {
                    TypeMismatch(node, varType, expType);
                }
            }

            TypeInferred(node, varType);
            return varType;
        }

        public IType Infer(IScope scope, BinaryExpression be)
        {
            var lhsTid = Infer(be.Left);
            var rhsTid = Infer(be.Right);
            var (tid, lhsCoercion, rhsCoercion) =
                OperatorPrecedenceCalculator.GetResultType(be.Op, lhsTid.TypeId, rhsTid.TypeId);
            return tid.Lookup();
        }

        public IType Infer(IScope scope, Block node)
        {
            foreach (var statement in node.Statements)
            {
                Infer(statement);
            }

            return default;
        }

        public IType Infer(IScope scope, BooleanExpression node)
        {
            var result = PrimitiveBool.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, Expression node) => throw new NotImplementedException();

        public IType Infer(IScope scope, ExpressionList node)
        {
            var types = new List<IType>();
            foreach (var exp in node.Expressions)
            {
                types.Add(Infer(exp));
                ;
            }

            var result = types.Last();
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, ExpressionStatement node) => throw new NotImplementedException();

        public IType Infer(IScope scope, FifthProgram node)
        {
            foreach (var functionDefinition in node.Functions)
            {
                Infer(scope, functionDefinition);
            }

            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, FloatValueExpression node)
        {
            var result = PrimitiveFloat.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, FuncCallExpression node)
        {
            Infer(node.ActualParameters);
            var ste = scope.Resolve(node.Name);
            if (ste.SymbolKind != SymbolKind.FunctionDeclaration)
            {
                TypeNotFound(node);
            }
            else
            {
                var type = Infer(ste.Context as AstNode);
                TypeInferred(node, type);
                return type;
            }

            return default;
        }

        public IType Infer(IScope _, FunctionDefinition node)
        {
            var returnType = node.ReturnType;
            Infer(node.ParameterDeclarations);
            Infer(node.Body);
            if (node.ReturnType == null || node.Typename == "void")
            {
                TypeNotRelevant(node);
                return default;
            }
            var result = returnType.Lookup();
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, Identifier node)
        {
            if (!scope.TryResolve(node.Value, out var ste))
            {
                TypeNotFound(node);
            }

            var result = Infer(ste.Context as AstNode);
            if (result != default)
            {
                TypeInferred(node, result);
                return result;
            }
            else
            {
                TypeNotFound(node);
                return default;
            }
        }

        public IType Infer(IScope _, IdentifierExpression node)
        {
            var t = Infer(node.Identifier);
            TypeInferred(node, t);
            return t;
        }

        public IType Infer(IScope scope, IfElseStatement node)
        {
            Infer(node.Condition);
            Infer(node.IfBlock);
            Infer(node.ElseBlock);
            return default;
        }

        public IType Infer(IScope scope, IntValueExpression node)
        {
            var result = PrimitiveInteger.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, LongValueExpression node)
        {
            var result = PrimitiveLong.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, ShortValueExpression node)
        {
            var result = PrimitiveShort.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, StatementList node) => throw new NotImplementedException();

        public IType Infer(IScope scope, DoubleValueExpression node)
        {
            var result = PrimitiveDouble.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, DecimalValueExpression node)
        {
            var result = PrimitiveDecimal.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, DateValueExpression node)
        {
            var result = PrimitiveDate.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, StringValueExpression node)
        {
            var result = PrimitiveString.Default;
            TypeInferred(node, result);
            return result;
        }


        public IType Infer(IScope scope, ModuleImport node) => default;

        public IType Infer(IScope scope, ParameterDeclaration node)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(node.TypeName, out var t))
            {
                TypeInferred(node, t);
                return t;
            }

            TypeNotFound(node);
            return default;
        }

        public IType Infer(IScope scope, ParameterDeclarationList node)
        {
            foreach (var pd in node.ParameterDeclarations)
            {
                Infer(pd);
            }

            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, ScopeAstNode node) => default;

        public IType Infer(IScope scope, ReturnStatement node)
        {
            var result = Infer(node.Expression);
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, Statement node) => default;

        public IType Infer(IScope scope, TypeCreateInstExpression node) => default;

        public IType Infer(IScope scope, TypeInitialiser node) => default;

        public IType Infer(IScope scope, TypeCast node)
        {
            // need to check that the type can be cast to the target type...
            var subExpType = Infer(node.SubExpression);
            var coercionType = node.TargetTid.Lookup();
            // do some check that the coercion is possible
            return coercionType;
        }
        public IType Infer(IScope scope, TypedAstNode node) => default;

        public IType Infer(IScope scope, UnaryExpression node) => default;

        public IType Infer(IScope scope, VariableDeclarationStatement node)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(node.TypeName, out var t))
            {
                TypeInferred(node, t);
                return t;
            }

            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, VariableReference node) => default;

        public IType Infer(IScope scope, WhileExp node) => default;

        #endregion Type Inference

        #region Type Checking

        public void Check(IScope scope, Expression exp, IType type) { }

        public void Check(IScope scope, TypeDefinition typeDef) { }

        public void Check(IScope scope, FifthProgram prog) { }

        #endregion Type Checking

        #region Helper Functions

        public IScope EmptyEnv() => default;

        public IScope Extend(IScope scope, string identifier, IType type) => default;

        public IScope Extend(IScope scope, TypeDefinition typeDef) => default;

        public IType Lookup(object o) => default;

        public IType LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();

        public IScope NewBlock(IScope scope) => default;

        public bool TryInferOperationResultType(Operator op, IType lhsType, IType rhsType,
            out IType result)
        {
            try
            {
                var (resultType, coercionLhs, coercionRhs) =
                    OperatorPrecedenceCalculator.GetResultType(op, lhsType.TypeId, rhsType.TypeId);
                result = resultType.Lookup();
                return true;
            }
            catch (TypeCheckingException e)
            {
                result = default;
                return false;
            }
        }

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
