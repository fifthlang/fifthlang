namespace Fifth.TypeSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AST;
    using PrimitiveTypes;
    using Expression = System.Linq.Expressions.Expression;

    public static class TypeHelpers
    {
        public static IFunctionSignature GetFuncType(this MethodInfo method)
            => new FunctionSignature(method.ReturnType.LookupType(),
                method.GetParameters().Select(p => p.ParameterType.LookupType()).ToArray());

        public static IFunctionSignature GetFuncType(this FuncWrapper method)
            => new FunctionSignature(method.ResultType.LookupType(),
                method.ArgTypes.Select(p => p.LookupType()).ToArray());

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

        public static ScopeAstNode GlobalScope(this AstNode node)
        {
            // TODO: WARNING: This could return null (a non-scope AST Node), if the root is not a scope node
            if (node.ParentNode == null)
            {
                return node as ScopeAstNode;
            }

            return node?.ParentNode.GlobalScope();
        }

        public static bool Implements<TInterface>(this Type t)
            => t.GetInterfaces().Contains(typeof(TInterface));

        public static bool IsBuiltinType(string typename)
            => LookupBuiltinType(typename) != null;

        public static IType Lookup(this TypeId tid)
        {
            if (TypeRegistry.DefaultRegistry.TryGetType(tid, out var ft))
            {
                return ft;
            }

            return null;
        }


        public static TypeId LookupBuiltinType(string typename)
        {
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(typename, out var type))
            {
                return type.TypeId;
            }

            return null;
        }

        public static string LookupOperationName(Operator op)
        {
            var opName = Enum.GetName(typeof(Operator), op);
            if (typeof(Operator).GetField(opName).TryGetAttribute<OpAttribute>(out var attr))
            {
                return attr.CommonName;
            }

            throw new TypeCheckingException("Unrecognised Operation");
        }

        public static TypeId LookupType(string typename)
        {
            if (IsBuiltinType(typename))
            {
                return LookupBuiltinType(typename);
            }

            throw new TypeCheckingException("no way to lookup non native types yet");
        }

        public static TypeId LookupType(this Type type)
        {
            if (TypeRegistry.DefaultRegistry.TryLookupType(type, out var result))
            {
                return result.TypeId;
            }

            throw new TypeCheckingException("no way to lookup non native types yet");
        }

        public static IEnumerable<MethodInfo> MethodsHavingAttribute<TAttribute>(this Type t)
            => t.GetMethods().Where(mi => mi.GetCustomAttributes(true).Any(attr => attr is TAttribute));

        public static ScopeAstNode NearestScope(this AstNode node)
        {
            if (node is ScopeAstNode astNode)
            {
                return astNode;
            }

            return node?.ParentNode.NearestScope();
        }
        // tmp

        public static T PeekOrDefault<T>(this Stack<T> s)
        {
            if (s == null || s.Count == 0)
            {
                return default;
            }

            return s.Peek();
        }

        public static bool TryEncode(this BinaryExpression be, out ulong encoded)
            => TryPack(out encoded, (ushort)be.Op, be.Left.TypeId.Value, be.Right.TypeId.Value);

        public static bool TryEncode(this UnaryExpression ue, out ulong encoded)
            => TryPack(out encoded, (ushort)ue.Op, ue.Operand.TypeId.Value);

        public static bool TryGetAttribute<T>(this Type t, out T attr)
        {
            attr = (T)t.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
            return attr != null;
        }

        public static bool TryGetAttribute<T>(this FieldInfo mi, out T attr)
        {
            attr = (T)mi.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
            return attr != null;
        }

        public static bool TryGetAttribute<T>(this MethodInfo mi, out T attr)
        {
            attr = (T)mi.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
            return attr != null;
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

        public static bool TryGetNearestFifthTypeToListType(Type nt, out IType ft)
        {
            if (typeof(IList).IsAssignableFrom(nt) && nt.IsGenericType)
            {
                var typeParam = nt.GenericTypeArguments.First();
                if (!TryGetNearestFifthTypeToNativeType(typeParam, out var typeParamAsFifthType))
                {
                    throw new TypeCheckingException("Unable to make sense of type param for list");
                }

                ft = new PrimitiveList(typeParamAsFifthType);
                return true;
            }

            ft = null;
            return false;
        }

        public static bool TryGetNearestFifthTypeToNativeType(Type nt, out TypeId ft)
        {
            ft = nt.LookupType();
            return true;
        }

        public static bool TryPack(out ulong result, params ushort[] shorts)
        {
            result = 0L;
            if (shorts.Length > 4)
            {
                return false;
            }

            foreach (var s in shorts)
            {
                result ^= s;
                result <<= 16;
            }

            return true;
        }

        public static IEnumerable<Type> TypesImplementingInterface<TInterfaceType, TSampleType>()
        {
            var type = typeof(TInterfaceType);
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(p => type.IsAssignableFrom(p));
        }

        public static FuncWrapper Wrap(this MethodInfo method) => WrapMethodInfo(method);

        public static FuncWrapper WrapMethodInfo(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var formalParams = parameters.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                                         .ToArray();
            var call = Expression.Call(null, method, formalParams);
            var result = new FuncWrapper(parameters.Select(p => p.ParameterType).ToList(), method.ReturnType,
                Expression.Lambda(call, formalParams).Compile(), method.MetadataToken);
            return result;
        }
    }
}
