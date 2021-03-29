// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Fifth.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Symbols;
    using TypeSystem;

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Uses the naming conventions of fifth, not csharp")]
    public static class BuiltinFunctions
    {
        public static Environment loadBuiltins(Environment e)
        {
            foreach (var mi in typeof(BuiltinFunctions).MethodsHavingAttribute<BuiltinAttribute>())
            {
                if (mi.TryGetAttribute<BuiltinAttribute>(out var attr))
                {
                    e.AddFunctionDefinition(new BuiltinFunctionDefinition(mi), attr.Keyword);
                }
                else
                {
                    e.AddFunctionDefinition(new BuiltinFunctionDefinition(mi));
                }
            }
            return e;
        }

        [Builtin(Keyword = "read")]
        public static string read() => Console.ReadLine();

        [Builtin(Keyword = "write")]
        public static string write(string s)
        {
            Console.WriteLine(s);
            return s;
        }

        [Builtin(Keyword = "sqrt")]
        public static double sqrt(double f) => Math.Sqrt(f);

        /*[Builtin(Keyword = "map")]
        public static List<object> map(List<object> l, FuncWrapper f)
        {
            foreach (var item in l)
            {
                f.Invoke(item);
            }

            return l;
        }*/
    }
}
