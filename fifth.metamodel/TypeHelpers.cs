namespace Fifth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Fifth.PrimitiveTypes;

    public static class TypeHelpers
    {
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
                    continue;
                if (tt.Keyword == typename)
                {
                    var fi = pt.GetProperty("Default", BindingFlags.Public | BindingFlags.Static);
                    var v = (IFifthType)fi.GetValue(null);
                    return v;
                }
            }

            return null;
        }

        public static IEnumerable<Type> TypesHavingAttribute<TAttribute, TSampleType>()
        {
            var types = typeof(TSampleType).Assembly.GetTypes();
            if (types == null || types.Length == 0)
                throw new Exception("no types found");
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(true).Any(attr => attr is TAttribute))
                {
                    yield return type;
                }
            }
        }
    }
}
