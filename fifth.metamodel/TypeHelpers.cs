namespace Fifth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using PrimitiveTypes;

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
            string operator_name;
            // now work out what the rest of the name will be based on the operator kind
            switch (op)
            {
                case Operator.Add:
                    operator_name = $"add{suffix}";
                    break;

                case Operator.Subtract:
                    operator_name = $"subtract{suffix}";
                    break;

                case Operator.Multiply:
                    operator_name = $"multiply{suffix}";
                    break;

                case Operator.Divide:
                    operator_name = $"divide{suffix}";
                    break;

                case Operator.Rem:
                    operator_name = $"remainder{suffix}";
                    break;

                case Operator.Mod:
                    operator_name = $"modulo{suffix}";
                    break;

                case Operator.And:
                    operator_name = $"logical_and{suffix}";
                    break;

                case Operator.Or:
                    operator_name = $"logical_or{suffix}";
                    break;

                case Operator.Not:
                    operator_name = $"logical_not{suffix}";
                    break;

                case Operator.Nand:
                    operator_name = $"logical_nand{suffix}";
                    break;

                case Operator.Nor:
                    operator_name = $"logical_nor{suffix}";
                    break;

                case Operator.Xor:
                    operator_name = $"logical_xor{suffix}";
                    break;

                case Operator.Equal:
                    operator_name = $"equals{suffix}";
                    break;

                case Operator.NotEqual:
                    operator_name = $"not_equals{suffix}";
                    break;

                case Operator.LessThan:
                    operator_name = $"less_than{suffix}";
                    break;

                case Operator.GreaterThan:
                    operator_name = $"greater_than{suffix}";
                    break;

                case Operator.LessThanOrEqual:
                    operator_name = $"less_than_or_equal{suffix}";
                    break;

                case Operator.GreaterThanOrEqual:
                    operator_name = $"greater_than_or_equal{suffix}";
                    break;

                default:
                    operator_name = "noop";
                    break;
            }

            // now lookup (always on the LHS type) the operation we're looking for
            if (lhsType.GetType().TryGetMethodByName(operator_name, out var fw))
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
            string operator_name;
            // now work out what the rest of the name will be based on the operator kind
            switch (op)
            {
                case Operator.Add:
                    operator_name = $"add{suffix}";
                    break;

                case Operator.Subtract:
                    operator_name = $"subtract{suffix}";
                    break;

                case Operator.Multiply:
                    operator_name = $"multiply{suffix}";
                    break;

                case Operator.Divide:
                    operator_name = $"divide{suffix}";
                    break;

                case Operator.Rem:
                    operator_name = $"remainder{suffix}";
                    break;

                case Operator.Mod:
                    operator_name = $"modulo{suffix}";
                    break;

                case Operator.And:
                    operator_name = $"logical_and{suffix}";
                    break;

                case Operator.Or:
                    operator_name = $"logical_or{suffix}";
                    break;

                case Operator.Not:
                    operator_name = $"logical_not{suffix}";
                    break;

                case Operator.Nand:
                    operator_name = $"logical_nand{suffix}";
                    break;

                case Operator.Nor:
                    operator_name = $"logical_nor{suffix}";
                    break;

                case Operator.Xor:
                    operator_name = $"logical_xor{suffix}";
                    break;

                case Operator.Equal:
                    operator_name = $"equals{suffix}";
                    break;

                case Operator.NotEqual:
                    operator_name = $"not_equals{suffix}";
                    break;

                case Operator.LessThan:
                    operator_name = $"less_than{suffix}";
                    break;

                case Operator.GreaterThan:
                    operator_name = $"greater_than{suffix}";
                    break;

                case Operator.LessThanOrEqual:
                    operator_name = $"less_than_or_equal{suffix}";
                    break;

                case Operator.GreaterThanOrEqual:
                    operator_name = $"greater_than_or_equal{suffix}";
                    break;

                default:
                    operator_name = "noop";
                    break;
            }

            // now lookup (always on the LHS type) the operation we're looking for
            return lhsType.TryGetMethodByName(operator_name, out fw);
        }

        public static bool TryGetTypeTraits(this Type t, out TypeTraitsAttribute tr)
        {
            tr = (TypeTraitsAttribute)t.GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is TypeTraitsAttribute);
            return tr != null;
        }

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

        public static FuncWrapper WrapMethodInfo(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var formalParams = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToArray();
            var call = Expression.Call(null, method, formalParams);
            return new FuncWrapper(parameters.Select(p => p.ParameterType).ToList(), method.ReturnType, Expression.Lambda(call, formalParams).Compile(), method.MetadataToken);
        }


        private static Delegate DelegateFromMethodInfo(MethodInfo mi)
        {
            var parameters = mi.GetParameters();
            var formalParams = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                .ToArray();
            var call = Expression.Call(null, mi, formalParams);
            return Expression.Lambda(call, formalParams).Compile();
        }
    }
}
