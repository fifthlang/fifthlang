namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Generic;
    using AST;
    using Fifth.PrimitiveTypes;
    using Symbols;

    public static class TypeChecker
    {
        public static readonly Dictionary<Type, IFifthType> PrimitiveMappings = new()
        {
            // {typeof(IList), PrimitiveList.Default}, 
            {typeof(string), PrimitiveString.Default},
            {typeof(short), PrimitiveShort.Default},
            {typeof(int), PrimitiveInteger.Default},
            {typeof(long), PrimitiveLong.Default},
            {typeof(bool), PrimitiveBool.Default},
            {typeof(char), PrimitiveChar.Default},
            {typeof(float), PrimitiveFloat.Default},
            {typeof(double), PrimitiveDouble.Default},
            {typeof(decimal), PrimitiveDecimal.Default},
            {typeof(DateTimeOffset), PrimitiveDate.Default},
            {typeof(DateTime), PrimitiveDate.Default}
        };

        public static void Check(IScope scope, Expression exp, IFifthType type) => throw new NotImplementedException();

        public static void Check(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public static void Check(IScope scope, FifthProgram prog) => throw new NotImplementedException();

        public static IScope EmptyEnv() => throw new NotImplementedException();

        public static IScope Extend(IScope scope, string identifier, IFifthType type) =>
            throw new NotImplementedException();

        public static IScope Extend(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public static IFifthType Infer(IScope scope, Expression exp)
            => exp switch
            {
                IntValueExpression i => PrimitiveInteger.Default,
                StringValueExpression s => PrimitiveString.Default,
                BinaryExpression be => InferBinaryExpression(scope, be),
                UnaryExpression ue => InferUnaryExpression(scope, ue),
                IdentifierExpression ie => InferIdentifierExpression(scope, ie),
                FuncCallExpression fce => InferFuncCallExpression(scope, fce),
                { } => null //throw new NotImplementedException("Need to implement other exception types")
            };

        private static IFifthType InferFuncCallExpression(IScope scope, FuncCallExpression fce)
        {
            if (scope.TryResolve(fce.Name, out var ste))
            {
                if (ste.SymbolKind == SymbolKind.FunctionDeclaration && ste.Context is ITypedAstNode tn)
                {
                    return tn.FifthType;
                }
            }
            // if it cant be resolved, then perhaps it is a builtin

            return null;
        }


        public static IFifthType Lookup(this object o)
            => PrimitiveMappings[o.GetType()];

        public static IFifthType LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();

        public static IFifthType LookupType(this Type t)
            => PrimitiveMappings[t];

        public static IFifthType LookupType(string identifier, IScope scope) => throw new NotImplementedException();

        public static IScope NewBlock(IScope scope) => throw new NotImplementedException();

        public static bool TryInferOperationResultType(Operator op, IFifthType lhsType, IFifthType rhsType,
            out IFifthType resultType)
        {
            if (TypeHelpers.TryPack(out var id, (ushort)op, lhsType.TypeId.Value, rhsType.TypeId.Value))
            {
                var opId = new OperatorId(id);
                var funType = InbuiltOperatorRegistry.DefaultRegistry.LookupOperationType(opId);
                if (TypeRegistry.DefaultRegistry.TryGetType(funType, out var ft))
                {
                    var x = ft as IFunctionType;
                    resultType = x.ReturnType;
                    return true;
                }
            }

            resultType = null;
            return false;
        }

        private static IFifthType InferBinaryExpression(IScope scope, BinaryExpression be)
        {
            var lhsType = Infer(scope, be.Left);
            var rhsType = Infer(scope, be.Right);
            if (be.TryEncode(out var id))
            {
                var fw = InbuiltOperatorRegistry.DefaultRegistry[new OperatorId(id)];
                if (PrimitiveMappings.TryGetValue(fw.ResultType, out var result))
                {
                    return result;
                }
            }

            return null;
        }

        private static IFifthType InferIdentifierExpression(IScope scope, IdentifierExpression ie)
        {
            if (scope.TryResolve(ie.Identifier.Value, out var ste))
            {
                var originTyped = ste.Context as ITypedAstNode;
                return originTyped.FifthType;
            }

            return null;
        }

        private static IFifthType InferUnaryExpression(IScope scope, UnaryExpression ue)
        {
            IFifthType result;
            var type = Infer(scope, ue.Operand);
            _ = ue.TryEncode(out var id);
            var fw = InbuiltOperatorRegistry.DefaultRegistry[new OperatorId(id)];
            if (!PrimitiveMappings.TryGetValue(fw.ResultType, out result))
            {
                throw new TypeCheckingException("unable to infer result type of expression");
            }

            return result;
        }
    }
}
