// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace fifthlang.system
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Uses the naming conventions of fifth, not csharp")]
    public static class GlobalBuiltinFunctions
    {
        public static string read()
        {
            return Console.ReadLine();
        }

        public static string write(string s)
        {
            Console.WriteLine(s);
            return s;
        }

        public static double sqrt(double f)
        {
            return Math.Sqrt(f);
        }

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
