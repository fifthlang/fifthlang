namespace Fifth.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AST;
    using AST.Visitors;
    using PrimitiveTypes;
    using TypeSystem;

    public class CodeGenVisitor : BaseAstVisitor
    {
        private Dictionary<TypeId, string> toDotnet = new Dictionary<TypeId, string>
        {
            {PrimitiveBool.Default.TypeId, "bool"},
            {PrimitiveChar.Default.TypeId, "char"},
            {PrimitiveDate.Default.TypeId, "System.DateTimeOffset"},
            {PrimitiveDecimal.Default.TypeId, "decimal"},
            {PrimitiveDouble.Default.TypeId, "float64"},
            {PrimitiveFloat.Default.TypeId, "float"},
            {PrimitiveInteger.Default.TypeId, "int32"},
            {PrimitiveLong.Default.TypeId, "int64"},
            {PrimitiveShort.Default.TypeId, "int16"},
            {PrimitiveString.Default.TypeId, "string"},
        };
        private readonly TextWriter writer;

        public CodeGenVisitor(TextWriter writer) => this.writer = writer;

        public override void EnterFifthProgram(FifthProgram ctx) =>
            writer.WriteLine(@"
.assembly Hello {}
.assembly extern mscorlib {}
.class public Program {
            ");

        public override void LeaveFifthProgram(FifthProgram ctx) => writer.WriteLine(@"}");

        public string MapType(TypeId tid)
            => toDotnet[tid];
        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            writer.Write($".method public static {MapType(ctx.ReturnType)} ");
            writer.Write(ctx.Name);
            writer.Write('(');
            writer.Write(ctx.ParameterDeclarations.ParameterDeclarations
                            .Join(pd => MapType(pd.ParameterType)));
            writer.WriteLine(") cil managed {");
            if (ctx.IsEntryPoint)
            {
                writer.WriteLine(".entrypoint");
            }
            var locals = new LocalDeclsGatherer();
            ctx.Accept(locals);
            if (locals.Decls.Any())
            {
                var localsList = locals.Decls.Join(vd => $"{MapType(vd.TypeId)} {vd.Name.Value}");
                writer.WriteLine($".locals init({localsList})");
            }
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
            => writer.WriteLine("}");

    }
}
