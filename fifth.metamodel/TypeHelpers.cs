namespace Fifth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AST;
    using PrimitiveTypes;
    using Expression = System.Linq.Expressions.Expression;

    public static class TypeHelpers
    {
        public static string GetTypeName(this IFifthType ft)
        {
            if (ft.GetType().TryGetTypeTraits(out var tr))
            {
                return tr.Keyword;
            }

            throw new TypeCheckingException("Type does not have a name");
        }

        /// <summary>
        ///     try to resolve the type of the value and get its internal value
        /// </summary>
        /// <returns>Value if it can find it, as an object</returns>
        public static object GetValueOfValueObject(this object vo)
        {
            var pi = vo.GetType().GetProperty("Value");

            if (pi?.CanRead ?? false)
            {
                return pi!.GetMethod!.Invoke(vo, new object[] { });
            }

            return null;
        }

        public static bool Implements<TInterface>(this Type t)
            => t.GetInterfaces().Contains(typeof(TInterface));

        public static bool IsBuiltinType(string typename)
            => LookupBuiltinType(typename) != null;

        public static IFifthType LookupBuiltinType(string typename)
        {
            var builtinTypes = TypesHavingAttribute<TypeTraitsAttribute, IFifthType>();

            foreach (var pt in builtinTypes.Where(t => t.Implements<IFifthType>()))
            {
                var tt = pt.GetCustomAttributes(false).Cast<TypeTraitsAttribute>().FirstOrDefault();
                if (tt == null)
                {
                    continue;
                }

                if (tt.Keyword == typename)
                {
                    var fi = pt.GetProperty("Default", BindingFlags.Public | BindingFlags.Static);
                    var v = (IFifthType)fi.GetValue(null);
                    return v;
                }
            }

            return null;
        }

        public static ScopeAstNode GlobalScope(this AstNode node)
        {
            // TODO: WARNING: This could return null (a non-scope AST Node), if the root is not a scope node
            if (node.ParentNode == null)
            {
                return node as ScopeAstNode;
            }

            return node?.ParentNode.GlobalScope();
        }

        public static ScopeAstNode NearestScope(this AstNode node)
        {
            if (node is ScopeAstNode astNode)
            {
                return astNode;
            }

            return node?.ParentNode.NearestScope();
        }

        public static bool TryGetMethodByName(this Type t, string name, out FuncWrapper fw)
        {
            var methods = t.GetMethods(BindingFlags.Static | BindingFlags.Public);

            fw = methods
                .Where(m => m.Name == name)
                .Select(WrapMethodInfo)
                .FirstOrDefault();
            return fw != null;
        }


        public static bool TryInferOperationResultType(Operator op, IFifthType lhsType, IFifthType rhsType, out IFifthType resultType)
        {
            // get the type traits that hold keyword name (that's used in the operator naming
            // convention for binary operators)
            if (!lhsType.GetType().TryGetTypeTraits(out var lhsTraits))
            {
                throw new TypeCheckingException($"unable to find type traits for type {lhsType.GetType().FullName}");
            }

            if (!rhsType.GetType().TryGetTypeTraits(out var rhsTraits))
            {
                throw new TypeCheckingException($"unable to find type traits for type {rhsType.GetType().FullName}");
            }

            // work out what the suffix will be given the short names of the left and right side
            // expression types
            var suffix = $"_{lhsTraits.Keyword}_{rhsTraits.Keyword}";
            // now work out what the rest of the name will be based on the operator kind
            var operator_name = op switch
            {
                Operator.Add => $"add{suffix}",
                Operator.Subtract => $"subtract{suffix}",
                Operator.Multiply => $"multiply{suffix}",
                Operator.Divide => $"divide{suffix}",
                Operator.Rem => $"remainder{suffix}",
                Operator.Mod => $"modulo{suffix}",
                Operator.And => $"logical_and{suffix}",
                Operator.Or => $"logical_or{suffix}",
                Operator.Not => $"logical_not{suffix}",
                Operator.Nand => $"logical_nand{suffix}",
                Operator.Nor => $"logical_nor{suffix}",
                Operator.Xor => $"logical_xor{suffix}",
                Operator.Equal => $"equals{suffix}",
                Operator.NotEqual => $"not_equals{suffix}",
                Operator.LessThan => $"less_than{suffix}",
                Operator.GreaterThan => $"greater_than{suffix}",
                Operator.LessThanOrEqual => $"less_than_or_equal{suffix}",
                Operator.GreaterThanOrEqual => $"greater_than_or_equal{suffix}",
                _ => "noop"
            };

            // now lookup (always on the LHS type) the operation we're looking for
            if (lhsType.GetType().TryGetMethodByName(operator_name, out var fw))
            {
                return TryGetNearestFifthTypeToNativeType(fw.ResultType, out resultType);
            }

            resultType = default;
            return false;
        }

        /// <summary>
        /// Infer type of operation, on unary expression
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand"></param>
        /// <param name="resultType"></param>
        /// <returns></returns>
        public static bool TryInferOperationResultType(Operator op, IFifthType operandType, out IFifthType resultType)
        {
            // get the type traits that hold keyword name (that's used in the operator naming
            // convention for binary operators)
            var operandTypeType = operandType.GetType();
            if (!operandTypeType.TryGetTypeTraits(out var operandTraits))
            {
                throw new TypeCheckingException($"unable to find type traits for type {operandTypeType.FullName}");
            }

            // work out what the suffix will be given the short names of the left and right side
            // expression types
            var suffix = $"_{operandTraits.Keyword}";
            // now work out what the rest of the name will be based on the operator kind
            var operatorName = op switch
            {
                Operator.Subtract => $"subtract{suffix}",
                Operator.Not => $"logical_not{suffix}",
                _ => "noop"
            };

            // now lookup (always on the LHS type) the operation we're looking for
            if (operandTypeType.TryGetMethodByName(operatorName, out var fw))
            {
                return TryGetNearestFifthTypeToNativeType(fw.ResultType, out resultType);
            }

            resultType = default;
            return false;
        }

        public static bool TryGetNearestFifthTypeToNativeType(Type nt, out IFifthType ft)
        {
            switch(nt.Name)
            {
                case "Void":
                    ft = PrimitiveVoid.Default;
                    return true;
                case "Int32":
                    ft = PrimitiveInteger.Default;
                    return true;
                case "Double":
                    ft = PrimitiveDouble.Default;
                    return true;
                case "Float":
                    ft = PrimitiveFloat.Default;
                    return true;
                case "String":
                    ft = PrimitiveString.Default;
                    return true;
                default:
                    ft = null;
                    return false;
            };
        }

        public static bool TryGetOperatorByNameAndTypes(Operator op, Type lhsType, Type rhsType, out FuncWrapper fw)
        {
            // get the type traits that hold keyword name (that's used in the operator naming
            // convention for binary operators)
            if (!lhsType.TryGetTypeTraits(out var lhsTraits))
            {
                throw new TypeCheckingException($"unable to find type traits for type {lhsType.FullName}");
            }

            if (!rhsType.TryGetTypeTraits(out var rhsTraits))
            {
                throw new TypeCheckingException($"unable to find type traits for type {rhsType.FullName}");
            }

            // work out what the suffix will be given the short names of the left and right side
            // expression types
            var suffix = $"_{lhsTraits.Keyword}_{rhsTraits.Keyword}";
            // now work out what the rest of the name will be based on the operator kind
            var operator_name = op switch
            {
                Operator.Add => $"add{suffix}",
                Operator.Subtract => $"subtract{suffix}",
                Operator.Multiply => $"multiply{suffix}",
                Operator.Divide => $"divide{suffix}",
                Operator.Rem => $"remainder{suffix}",
                Operator.Mod => $"modulo{suffix}",
                Operator.And => $"logical_and{suffix}",
                Operator.Or => $"logical_or{suffix}",
                Operator.Not => $"logical_not{suffix}",
                Operator.Nand => $"logical_nand{suffix}",
                Operator.Nor => $"logical_nor{suffix}",
                Operator.Xor => $"logical_xor{suffix}",
                Operator.Equal => $"equals{suffix}",
                Operator.NotEqual => $"not_equals{suffix}",
                Operator.LessThan => $"less_than{suffix}",
                Operator.GreaterThan => $"greater_than{suffix}",
                Operator.LessThanOrEqual => $"less_than_or_equal{suffix}",
                Operator.GreaterThanOrEqual => $"greater_than_or_equal{suffix}",
                _ => "noop"
            };

            // now lookup (always on the LHS type) the operation we're looking for
            return lhsType.TryGetMethodByName(operator_name, out fw);
        }

        public static bool TryGetAttribute<T>(this Type t, out T attr)
        {
            attr = (T)t.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
            return attr != null;
        }

        public static bool TryGetAttribute<T>(this MethodInfo mi, out T attr)
        {
            attr = (T)mi.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
            return attr != null;
        }

        public static bool TryGetTypeTraits(this Type t, out TypeTraitsAttribute tr) =>
            t.TryGetAttribute(out tr);

        public static IEnumerable<Type> TypesHavingAttribute<TAttribute, TSampleType>()
        {
            var types = typeof(TSampleType).Assembly.GetTypes();
            if (types == null || types.Length == 0)
            {
                throw new Exception("no types found");
            }

            foreach (var type in types)
            {
                if (type.GetCustomAttributes(true).Any(attr => attr is TAttribute))
                {
                    yield return type;
                }
            }
        }
        public static IEnumerable<MethodInfo> MethodsHavingAttribute<TAttribute>(this Type t)
         => t.GetMethods().Where(mi => mi.GetCustomAttributes(true).Any(attr => attr is TAttribute));

        public static FuncWrapper WrapMethodInfo(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var formalParams = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToArray();
            var call = Expression.Call(null, method, formalParams);
            return new FuncWrapper(parameters.Select(p => p.ParameterType).ToList(), method.ReturnType, Expression.Lambda(call, formalParams).Compile(), method.MetadataToken);
        }

        public static FuncWrapper Wrap(this MethodInfo method) => WrapMethodInfo(method);

        public static IFifthType LookupType(string typename)
        {
            if (IsBuiltinType(typename))
            {
                return LookupBuiltinType(typename);
            }

            throw new TypeCheckingException("no way to lookup non native types yet");
        }

        public static T PeekOrDefault<T>(this Stack<T> s)
        {
            if (s == null || s.Count == 0)
            {
                return default;
            }

            return s.Peek();
        }
    }
}
