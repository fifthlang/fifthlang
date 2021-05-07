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
            w(".assembly extern mscorlib { .ver 4:0:0:0 auto }");
            w(".assembly fifth { }");
            w($".module {ctx.TargetAssemblyFileName}");
        }

        private void w(string s)
            => writer.WriteLine(s);
        public override void EnterClassDefinition(ClassDefinition ctx)
        {
            w($".class public  {ctx.Name}{{");
        }

        public override void LeaveClassDefinition(ClassDefinition ctx)
        {
            w("}");
        }

        public override void EnterPropertyDefinition(PropertyDefinition ctx)
        {
            w($"  .property instance {ctx.TypeName} {ctx.Name}(){{");
            w($"      .get instance {ctx.TypeName} NamespaceName.Class::get_{ctx.Name}()");
            w($"      .set instance void Namespace.Class::set_{ctx.Name}({ctx.TypeName})");
            w("  }");
        }                        

        public override void LeavePropertyDefinition(PropertyDefinition ctx)
        {
        }

        public override void EnterFunctionDefinition(FunctionDefinition ctx)
        {
            if (ctx.ParameterDeclarations.ParameterDeclarations.Any(pd => pd is TypeCreateInstExpression))
            {
                throw new NotImplementedException("Don't gen code for pattern matches yet");
            }
            var args = ctx.ParameterDeclarations.ParameterDeclarations.Cast<IParameterListItem>()
                            .Join(pd => $"{MapType(pd.TypeId)} {pd.ParameterName.Value}");
            w($".method public static {MapType(ctx.ReturnType)} {ctx.Name} ({args}) cil managed {{");
            if (ctx.IsEntryPoint)
            {
                w(".entrypoint");
            }

            var locals = new LocalDeclsGatherer();
            ctx.Accept(locals);
            if (locals.Decls.Any())
            {
                var localsList = locals.Decls.Join(vd => $"{MapType(vd.TypeId)} {vd.Name.Value}");
                w($".locals init({localsList})");
            }
        }

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => w($"ldc.i4.{ctx.Value}");

        public override void EnterLongValueExpression(LongValueExpression ctx)
            => w($"ldc.i8.{ctx.Value}");

        public override void EnterShortValueExpression(ShortValueExpression ctx)
            => w($"ldc.i2.{ctx.Value}");

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => w($"ldstr \"{ctx.Value}\"");

        public override void LeaveBinaryExpression(BinaryExpression ctx)
        {
            switch (ctx.Op)
            {
                case Operator.Add:
                    w(@"add");
                    break;
                case Operator.Subtract:
                    w(@"sub");
                    break;
                case Operator.Multiply:
                    w(@"mul");
                    break;
                case Operator.Divide:
                    w(@"div");
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
            w(@".class public Program { }");
        }

        public override void LeaveFuncCallExpression(FuncCallExpression ctx)
        {
            if (ctx.TypeId == null)
            {
                if (ctx.HasAnnotation(Constants.FunctionImplementation))
                {
                    var funcImpl = ctx[Constants.FunctionImplementation] as IFunctionDefinition;
                    if (funcImpl is BuiltinFunctionDefinition)
                    {
                        if(funcImpl.Name == "print")
                            w($"call void System::Console.WriteLine(string)");
                        return;
                    }
                }
            }
            var return5thType = ctx.TypeId.Lookup();
            var x = toDotnet[return5thType.TypeId];

            List<string> dotnetTypes = new();
            foreach (var e in ctx.ActualParameters.Expressions)
            {
                if (toDotnet.ContainsKey(e.TypeId))
                {
                    dotnetTypes.Add(toDotnet[e.TypeId]);
                }
                else
                {
                    if (e.TypeId.Lookup() is UserDefinedType udt)
                    {
                        dotnetTypes.Add(udt.Definition.Name);
                    }
                }
            }

            var argTypeNames = dotnetTypes.Join(t => t);
            w($"call {x} Program::{ctx.Name}({argTypeNames})");
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
            => w("}");

        public override void LeaveReturnStatement(ReturnStatement ctx)
            => w(@"ret");

        public string MapType(TypeId tid)
        {
            if (tid == null)
            {
                return "void";
            }

            if (toDotnet.ContainsKey(tid))
            {
                return toDotnet[tid];
            }

            return tid.Lookup().Name;
        }
    }
}
