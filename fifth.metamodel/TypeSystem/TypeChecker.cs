namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Generic;
    using AST;
    using Fifth.PrimitiveTypes;
    using Symbols;

    public static class TypeChecker
    {
        public static readonly Dictionary<Type, TypeId> PrimitiveMappings = new()
        {
            // {typeof(IList), PrimitiveList.Default}, 
            {typeof(string), PrimitiveString.Default.TypeId },
            {typeof(short), PrimitiveShort.Default.TypeId},
            {typeof(int), PrimitiveInteger.Default.TypeId},
            {typeof(long), PrimitiveLong.Default.TypeId},
            {typeof(bool), PrimitiveBool.Default.TypeId},
            {typeof(char), PrimitiveChar.Default.TypeId},
            {typeof(float), PrimitiveFloat.Default.TypeId},
            {typeof(double), PrimitiveDouble.Default.TypeId},
            {typeof(decimal), PrimitiveDecimal.Default.TypeId},
            {typeof(DateTimeOffset), PrimitiveDate.Default.TypeId},
            {typeof(DateTime), PrimitiveDate.Default.TypeId}
        };

        public static void Check(IScope scope, Expression exp, IFifthType type) => throw new NotImplementedException();

        public static void Check(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public static void Check(IScope scope, FifthProgram prog) => throw new NotImplementedException();

        public static IScope EmptyEnv() => throw new NotImplementedException();

        public static IScope Extend(IScope scope, string identifier, IFifthType type) =>
            throw new NotImplementedException();

        public static IScope Extend(IScope scope, TypeDefinition typeDef) => throw new NotImplementedException();

        public static TypeId Infer(IScope scope, Expression exp)
            => exp switch
            {
                IntValueExpression i => PrimitiveInteger.Default.TypeId,
                StringValueExpression s => PrimitiveString.Default.TypeId,
                BinaryExpression be => InferBinaryExpression(scope, be),
                UnaryExpression ue => InferUnaryExpression(scope, ue),
                IdentifierExpression ie => InferIdentifierExpression(scope, ie),
                FuncCallExpression fce => InferFuncCallExpression(scope, fce),
                { } => null //throw new NotImplementedException("Need to implement other exception types")
            };

        private static TypeId InferFuncCallExpression(IScope scope, FuncCallExpression fce)
        {
            if (scope.TryResolve(fce.Name, out var ste))
            {
                if (ste.SymbolKind == SymbolKind.FunctionDeclaration && ste.Context is ITypedAstNode tn)
                {
                    return tn.TypeId;
                }
            }
            // if it cant be resolved, then perhaps it is a builtin

            return null;
        }


        public static TypeId Lookup(this object o)
            => PrimitiveMappings[o.GetType()];

        public static TypeId LookupFunctionResultType(string identifier, IScope scope) =>
            throw new NotImplementedException();

        public static TypeId LookupType(this Type t)
            => PrimitiveMappings[t];

        public static TypeId LookupType(string identifier, IScope scope) => throw new NotImplementedException();

        public static IScope NewBlock(IScope scope) => throw new NotImplementedException();

        public static bool TryInferOperationResultType(Operator op, TypeId lhsType, TypeId rhsType,
            out TypeId resultType)
        {
            if (TypeHelpers.TryPack(out var id, (ushort)op, lhsType.Value, rhsType.Value))
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

        private static TypeId InferBinaryExpression(IScope scope, BinaryExpression be)
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

        private static TypeId InferIdentifierExpression(IScope scope, IdentifierExpression ie)
        {
            if (scope.TryResolve(ie.Identifier.Value, out var ste))
            {
                var originTyped = ste.Context as ITypedAstNode;
                return originTyped.TypeId;
            }

            return null;
        }

        private static TypeId InferUnaryExpression(IScope scope, UnaryExpression ue)
        {
            TypeId result;
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
