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
        public FunctionalTypeChecker(TypeInferred typeInferred, TypeNotFound typeNotFound, TypeMismatch typeMismatch,
            TypeNotRelevant typeNotRelevant)
        {
            TypeInferred = typeInferred;
            TypeNotFound = typeNotFound;
            TypeMismatch = typeMismatch;
            TypeNotRelevant = typeNotRelevant;
        }

        public TypeInferred TypeInferred { get; }
        public TypeMismatch TypeMismatch { get; }
        public TypeNotFound TypeNotFound { get; }
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
            => PrimitiveUri.Default;

        public IType Infer(IScope scope, AliasDeclaration node)
            => default;

        public IType Infer(IScope scope, AnnotatedThing node)
            => default;

        public IType Infer(IScope _, AssignmentStmt node)
        {
            var scope = node.NearestScope();
            IType expType;
            var varType = Infer(scope, node.VariableRef);
            if (node.Expression != null)
            {
                expType = Infer(node.Expression);
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

            TypeNotRelevant(node); // TODO: either type is not relevant, or I should not be returning default
            return default;
        }

        public IType Infer(IScope scope, BoolValueExpression node)
        {
            var result = PrimitiveBool.Default;
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, Expression node)
            => Infer(node);

        // throw new NotImplementedException();
        public IType Infer(IScope scope, ExpressionList node)
        {
            var types = new List<IType>();
            foreach (var exp in node.Expressions)
            {
                types.Add(Infer(exp));
                ;
            }

            // var result = types.Last();
            // I don't think there is a case where we either need, or can use, the type of an expression list.
            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, ExpressionStatement node)
        {
            var expTid = Infer(scope, node.Expression);
            if (expTid == null)
            {
                if (node.Expression is FuncCallExpression)
                {
                    TypeNotRelevant(node);
                }
                else
                {
                    TypeNotFound(node);
                }
                return default;
            }

            TypeInferred(node, expTid);
            return expTid;
        }

        public IType Infer(IScope scope, FifthProgram node)
        {
            foreach (var classDefinition in node.Classes)
            {
                Infer(scope, classDefinition);
            }

            foreach (var functionDefinition in node.Functions)
            {
                if (functionDefinition is FunctionDefinition fd)
                {
                    Infer(scope, fd);
                }
                if (functionDefinition is OverloadedFunctionDefinition ofd)
                {
                    Infer(scope, ofd);
                }
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

            switch (ste.SymbolKind)
            {
                case SymbolKind.FunctionDeclaration:
                case SymbolKind.BuiltinFunctionDeclaration:
                    node[Constants.FunctionImplementation] = ste.Context;
                    var type = Infer(ste.Context as AstNode);
                    if (type != null)
                    {
                        TypeInferred(node, type);
                        return type;
                    }
                        
                    if (ste.Context is IFunctionDefinition fd && fd.Typename == "void")
                    {
                        TypeNotRelevant(node);
                    }
                    else
                    {
                        TypeNotFound(node);
                    }

                    return default;
                default:
                    TypeNotFound(node);
                    return default;
            }
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

        public IType Infer(IScope scope, BuiltinFunctionDefinition node)
        {
            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, OverloadedFunctionDefinition node)
            => throw new NotImplementedException();

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

            TypeNotFound(node);
            return default;
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
            if(node.ElseBlock != null)
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

        public IType Infer(IScope scope, StatementList node)
            => throw new NotImplementedException();

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


        public IType Infer(IScope scope, ModuleImport node)
            => default;

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
            foreach (var e in node.ParameterDeclarations)
            {
                if (e is ParameterDeclaration pd)
                {
                    Infer(pd);
                }
                else if (e is TypeCreateInstExpression tcie)
                {
                    Infer(tcie);
                }
            }

            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, ReturnStatement node)
        {
            var result = Infer(node.SubExpression);
            if (result != null)
            {
                TypeInferred(node, result);
                node.TargetTid = result.TypeId;
            }
            else
            {
                TypeNotFound(node);
            }
            return result;
        }

        public IType Infer(IScope scope, Statement node)
            => default;

        public IType Infer(IScope scope, TypeCreateInstExpression node)
            => default;

        public IType Infer(IScope scope, TypeInitialiser node)
            => default;

        public IType Infer(IScope scope, DestructuringParamDecl node)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(node.TypeName, out var t))
            {
                TypeInferred(node, t);
                return t;
            }

            TypeNotFound(node);
            return default;
        }

        public IType Infer(IScope scope, PropertyBinding node)
        {
            // resolve the base property definition that the node's unboundVariable is to be bound to.
            var boundProperty = node.BoundProperty;
            var boundPropertyTypeId = boundProperty?.TypeId;
            if (boundProperty != null && boundPropertyTypeId != null)
            {
                var result = boundPropertyTypeId.Lookup();
                TypeInferred(node, result);
                return result;
            }

            return default;
        }

        public IType Infer(IScope scope, ClassDefinition node)
        {
            node.Properties.ForEach(propDef => Infer(node, propDef));
            node.Functions.ForEach(propDef => Infer(node, propDef));
            return node.TypeId.Lookup();
        }

        public IType Infer(IScope scope, IFunctionDefinition node)
        {
            if (node is FunctionDefinition fd)
            {
                return Infer(scope, fd);
            }
            if (node is OverloadedFunctionDefinition ofd)
            {
                return Infer(scope, ofd);
            }
            TypeNotFound(node);
            return default;
        }

        public IType Infer(IScope scope, PropertyDefinition node)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(node.TypeName, out var t))
            {
                TypeInferred(node, t);
                return t;
            }

            TypeNotRelevant(node);
            return default;
        }

        public IType Infer(IScope scope, TypeCast node)
        {
            // need to check that the type can be cast to the target type...
            var subExpType = Infer(node.SubExpression);
            var coercionType = node.TargetTid.Lookup();
            // do some check that the coercion is possible
            return coercionType;
        }

        public IType Infer(IScope scope, UnaryExpression node)
            => default;

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

        public IType Infer(IScope scope, VariableReference node)
        {
            IType result = default;
            if (scope.TryResolve(node.Name, out var ste) )
            {
                if (ste.Context is PropertyBinding pb)
                {
                    result = pb.BoundProperty.TypeId.Lookup();
                    TypeInferred(node, result);
                }
                if (ste.Context is VariableDeclarationStatement vds)
                {
                    result = vds.TypeId.Lookup();
                    TypeInferred(node, result);
                }
                if (ste.Context is PropertyDefinition propDef)
                {
                    result = propDef.TypeId.Lookup();
                    TypeInferred(node, result);
                }
                if (ste.Context is ParameterDeclaration paramDef)
                {
                    result = paramDef.TypeId.Lookup();
                    TypeInferred(node, result);
                }
            }
            return result;
        }

        public IType Infer(IScope scope, CompoundVariableReference node)
        {
            // this is a variable that, to be resolved, one must walk up a chain to find
            IScope innerScope = scope;
            foreach (var vr in node.ComponentReferences)
            {
                var itype = Infer(innerScope, vr);
                if (itype is UserDefinedType udt)
                {
                    innerScope = udt.Definition;
                }
            }

            var result = node.ComponentReferences.Last().TypeId.Lookup();
            TypeInferred(node, result);
            return result;
        }

        public IType Infer(IScope scope, WhileExp node)
            => default;

        public IType Infer(IScope scope, TypedAstNode node)
            => throw new NotImplementedException();

        public IType Infer(IScope scope, ScopeAstNode node)
            => throw new NotImplementedException();

        public IType Infer(IScope scope, TypePropertyInit node)
            => throw new NotImplementedException();

        #endregion Type Inference

        #region Type Checking

        /*
        public void Check(IScope scope, Expression exp, IType type) { }

        public void Check(IScope scope, TypeDefinition typeDef) { }

        public void Check(IScope scope, FifthProgram prog) { }
        */

        #endregion Type Checking

        #region Helper Functions

        /*
        public IScope EmptyEnv() => default;

        public IScope Extend(IScope scope, string identifier, IType type) => default;

        public IScope Extend(IScope scope, TypeDefinition typeDef) => default;

        public IType Lookup(object o) => default;

        public IType LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();
        */

        #endregion Helper Functions
    }
}
