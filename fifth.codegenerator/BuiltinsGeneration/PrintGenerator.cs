namespace Fifth.CodeGeneration.BuiltinsGeneration
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AST;
    using PrimitiveTypes;

    public class PrintGenerator
    {
        private readonly TextWriter writer;

        public PrintGenerator(TextWriter writer)
            => this.writer = writer;


        public void GenerateFor(FuncCallExpression ctx, BuiltinFunctionDefinition bif)
        {
            var typeNames = ctx.ActualParameters.Expressions.Select(e => TypeMappings.ToDotnetType(e.TypeId)).ToArray();
            if (typeNames.First() == "string" && typeNames.Count() > 1)
            {
                var typesAsObjects = new List<string>
                {
                    "string"
                };

                foreach (var t in ctx.ActualParameters.Expressions.Skip(1))
                {
                    w($"box [System.Runtime]{TypeMappings.ToBoxedDotnetType(t.TypeId)}");
                    typesAsObjects.Add("object");
                }

                // w("ldstr        \"en-GB\"");
                // w("ldc.i4.0");
                // w("newobj       instance void [mscorlib]System.Globalization.CultureInfo::.ctor(string, bool)");
                // w("call         void [mscorlib]System.Globalization.CultureInfo::set_CurrentCulture(class [System.Runtime]System.Globalization.CultureInfo)");
                //
                w($"call    void [mscorlib]System.Console::WriteLine({typesAsObjects.Join(s => s)})");
                return;
            }

            if (typeNames.First() == "int16" && typeNames.Count() == 1)
            {
                typeNames[0] = "int32";
            }

            var types = typeNames.Join(s => s);
            w($"call    void [mscorlib]System.Console::WriteLine({types})");
        }

        private string GetPrintArgType(FuncCallExpression ctx)
        {
            var firstArgTid = ctx.ActualParameters.Expressions[0].TypeId;
            if (firstArgTid == PrimitiveShort.Default.TypeId)
            {
                firstArgTid = PrimitiveInteger.Default.TypeId;
            }

            return TypeMappings.ToDotnetType(firstArgTid);
        }

#pragma warning disable IDE1006 // Naming Styles
        private void w(string s)
#pragma warning restore IDE1006 // Naming Styles
            => writer.WriteLine(s);
    }
}
