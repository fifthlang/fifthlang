// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Fifth.Runtime
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Uses the naming conventions of fifth, not csharp")]
    public static class BuiltinFunctions
    {
        public static Environment loadBuiltins(Environment e)
        {
            var t = typeof(BuiltinFunctions);
            e.AddFunctionDefinition(new BuiltinFunctionDefinition(t.GetMethod("read")));
            e.AddFunctionDefinition(new BuiltinFunctionDefinition(t.GetMethod("write")));
            return e;
        }

        public static string read() => Console.ReadLine();
        public static void write(string s) => Console.WriteLine(s);
    }
}
