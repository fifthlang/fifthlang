namespace Fifth.TypeSystem
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using AST;
    using Fifth.PrimitiveTypes;

    public sealed class InbuiltOperatorRegistry
    {
        public static readonly InbuiltOperatorRegistry DefaultRegistry = new();
        private readonly ConcurrentDictionary<OperatorId, FuncWrapper> operationRegister = new();
        private readonly ConcurrentDictionary<OperatorId, TypeId> operationTypes = new();

        private InbuiltOperatorRegistry()
        {
        }

        public FuncWrapper this[OperatorId index] => operationRegister[index];

        public void LoadBuiltinOperators()
        {
            LoadBuiltinOperators(PrimitiveString.Default);
            LoadBuiltinOperators(PrimitiveShort.Default);
            LoadBuiltinOperators(PrimitiveInteger.Default);
            LoadBuiltinOperators(PrimitiveLong.Default);
            LoadBuiltinOperators(PrimitiveBool.Default);
            LoadBuiltinOperators(PrimitiveChar.Default);
            LoadBuiltinOperators(PrimitiveFloat.Default);
            LoadBuiltinOperators(PrimitiveDouble.Default);
            LoadBuiltinOperators(PrimitiveDecimal.Default);
            LoadBuiltinOperators(PrimitiveDate.Default);
            LoadBuiltinOperators(PrimitiveDate.Default);
        }

        public void LoadBuiltinOperators(IType primitiveType)
        {
            _ = primitiveType ?? throw new ArgumentNullException(nameof(primitiveType));
            var methods = primitiveType.GetType().MethodsHavingAttribute<OperationAttribute>();
            foreach (var m in methods)
            {
                OperatorId opId = null;
                if (m.TryGetAttribute<OperationAttribute>(out var attr))
                {
                    if (attr.Position == OperatorPosition.Infix)
                    {
                        var args = m.GetParameters();
                        switch (args.Count())
                        {
                            case 1:
                            {
                                var t = args[0].ParameterType;
                                if (TypeChecker.PrimitiveMappings.TryGetValue(t, out var operand))
                                {
                                    if (TypeHelpers.TryPack(out var ord, (ushort)attr.Op, operand.Value))
                                    {
                                        opId = new OperatorId(ord);
                                        operationRegister[opId] = m.Wrap();
                                    }
                                }
                            }
                                break;
                            case 2:
                            {
                                var t1 = args[0].ParameterType;
                                var t2 = args[1].ParameterType;
                                if (!TypeChecker.PrimitiveMappings.TryGetValue(t1, out var leftFifthType))
                                {
                                    throw new TypeCheckingException("unable to resolve type details for left operand");
                                }

                                if (!TypeChecker.PrimitiveMappings.TryGetValue(t2, out var rightFifthType))
                                {
                                    throw new TypeCheckingException("unable to resolve type details for right operand");
                                }

                                if (TypeHelpers.TryPack(out var ord,
                                    (ushort)attr.Op,
                                    leftFifthType.Value,
                                    rightFifthType.Value))
                                {
                                    opId = new OperatorId(ord);
                                    operationRegister[opId] = m.Wrap();
                                }
                            }

                                break;
                            default:
                                throw new TypeCheckingException("wrong number of arguments to method");
                        }
                    }

                    if (opId != null)
                    {
                        var operationType = m.GetFuncType();
                        TypeRegistry.DefaultRegistry.RegisterType(operationType);
                        operationTypes[opId] = operationType.TypeId;
                    }
                }
            }
        }

        public bool TryGetOperationType(Expression e, out TypeId t)
        {
            if (TryGetOperation(e, out var fw))
            {
                t = fw.ResultType.LookupType();
                return true;
            }

            t = null;
            return false;
        }

        public TypeId LookupOperationType(OperatorId id) => operationTypes[id];

        public bool TryGetOperation(Expression e, out FuncWrapper f)
        {
            if (e is UnaryExpression ue && ue.TryEncode(out var ueEncoding))
            {
                return operationRegister.TryGetValue(new OperatorId(ueEncoding), out f);
            }

            if (e is BinaryExpression be && be.TryEncode(out var beEncoding))
            {
                return operationRegister.TryGetValue(new OperatorId(beEncoding), out f);
            }

            f = null;
            return false;
        }
    }
}
