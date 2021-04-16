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
        private readonly TextWriter writer;

        private readonly Dictionary<TypeId, string> toDotnet = new()
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
            {PrimitiveString.Default.TypeId, "string"}
        };

        public CodeGenVisitor(TextWriter writer) => this.writer = writer;

        public override void EnterFifthProgram(FifthProgram ctx)
        {
            writer.WriteLine(@"
.assembly extern mscorlib { .ver 4:0:0:0 auto }
.assembly fifth { }
.module fifth_test.exe
            ");
        }

        public override void EnterClassDefinition(ClassDefinition ctx)
        {
            writer.WriteLine($".class public  {ctx.Name}{{");
        }

        public override void LeaveClassDefinition(ClassDefinition ctx)
        {
            writer.WriteLine("}");
        }

        public override void EnterPropertyDefinition(PropertyDefinition ctx)
        {
            base.EnterPropertyDefinition(ctx);
        }

        public override void LeavePropertyDefinition(PropertyDefinition ctx)
        {
            base.LeavePropertyDefinition(ctx);
        }

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            writer.Write($".method public static {MapType(ctx.ReturnType)} ");
            writer.Write(ctx.Name);
            writer.Write('(');
            writer.Write(ctx.ParameterDeclarations.ParameterDeclarations
                            .Join(pd => $"{MapType(pd.TypeId)} {pd.ParameterName.Value}"));
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

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => writer.WriteLine($"ldc.i4.{ctx.Value}");

        public override void EnterLongValueExpression(LongValueExpression ctx)
            => writer.WriteLine($"ldc.i8.{ctx.Value}");

        public override void EnterShortValueExpression(ShortValueExpression ctx)
            => writer.WriteLine($"ldc.i2.{ctx.Value}");

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => writer.WriteLine($"ldstr \"{ctx.Value}\"");

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            switch (ctx.Op)
            {
                case Operator.Add:
                    writer.WriteLine(@"add");
                    break;
                case Operator.Subtract:
                    writer.WriteLine(@"sub");
                    break;
                case Operator.Multiply:
                    writer.WriteLine(@"mul");
                    break;
                case Operator.Divide:
                    writer.WriteLine(@"div");
                    break;
                case Operator.Rem:
                    break;
                case Operator.Mod:
                    break;
                case Operator.And:
                    break;
                case Operator.Or:
                    break;
                case Operator.Not:
                    break;
                case Operator.Nand:
                    break;
                case Operator.Nor:
                    break;
                case Operator.Xor:
                    break;
                case Operator.Equal:
                    break;
                case Operator.NotEqual:
                    break;
                case Operator.LessThan:
                    break;
                case Operator.GreaterThan:
                    break;
                case Operator.LessThanOrEqual:
                    break;
                case Operator.GreaterThanOrEqual:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void LeaveFifthProgram(FifthProgram ctx)
        {
            writer.WriteLine(@"
.class public Program {
            ");
            writer.WriteLine(@"}");
        }

        public override void LeaveFuncCallExpression(FuncCallExpression ctx)
        {
            var return5thType = ctx.TypeId.Lookup();
            var x = toDotnet[return5thType.TypeId];
            var argTypeNames = ctx.ActualParameters.Expressions.Select(e => toDotnet[e.TypeId]).Join(t => t);
            writer.WriteLine($"call {x} Program::{ctx.Name}({argTypeNames})");
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
            => writer.WriteLine("}");

        public override void LeaveReturnStatement(ReturnStatement ctx)
            => writer.WriteLine(@"ret");

        public string MapType(TypeId tid)
            => toDotnet[tid];
    }
}
