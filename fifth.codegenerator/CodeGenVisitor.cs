namespace Fifth.CodeGeneration
{
    using System.IO;
    using System.Linq;
    using AST;
    using AST.Visitors;
    using TypeSystem;

    public class CodeGenVisitor : BaseAstVisitor
    {
        private readonly TextWriter writer;

        public CodeGenVisitor(TextWriter writer) => this.writer = writer;

        public override void EnterFifthProgram(FifthProgram ctx) =>
            writer.WriteLine(@"
.assembly Hello {}
.assembly extern mscorlib {}
.class public Program {
            ");

        public override void LeaveFifthProgram(FifthProgram ctx) => writer.WriteLine(@"}");

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            writer.Write($".method public static {ctx.ReturnType.Lookup().Name} ");
            writer.Write(ctx.Name);
            writer.Write('(');
            var typeList = ctx.ParameterDeclarations.ParameterDeclarations
               .Select(pd => pd.TypeName)
               .ToArray();
            writer.Write(string.Join(", ", typeList));
            writer.WriteLine(") cil managed {");
            if (ctx.IsEntryPoint)
            {
                writer.WriteLine(".entrypoint");
            }
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx) => writer.WriteLine("}");
    }
}
