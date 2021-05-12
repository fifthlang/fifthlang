namespace Fifth.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using AST;
    using AST.Visitors;
    using PrimitiveTypes;
    using TypeSystem;

    public class CodeGenVisitor : DefaultRecursiveDescentVisitor
    {
        private ulong labelCounter;
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

        private readonly TextWriter writer;

        public CodeGenVisitor(TextWriter writer)
            => this.writer = writer;


        public override IfElseStatement VisitIfElseStatement(IfElseStatement ctx)
        {
            Interlocked.Increment(ref labelCounter);
            VisitExpression(ctx.Condition);
            w($"brfalse.s LBL_ELSE_{labelCounter}");

            w($"LBL_IF_{labelCounter}:");
            VisitBlock(ctx.IfBlock);
            w($"br.s LBL_END_{labelCounter}");

            w($"LBL_ELSE_{labelCounter}:");
            VisitBlock(ctx.ElseBlock);
            w($"LBL_END_{labelCounter}:");
            return ctx;
        }

        public override ClassDefinition VisitClassDefinition(ClassDefinition ctx)
        {
            w($".class public  {ctx.Name}{{");
            foreach (var functionDefinition in ctx.Functions)
            {
                VisitFunctionDefinition(functionDefinition as FunctionDefinition);
            }
            w("}");
            return ctx;
        }

        public override FifthProgram VisitFifthProgram(FifthProgram ctx)
        {
            w(".assembly extern mscorlib { .ver 4:0:0:0 auto }");
            w(".assembly fifth { }");
            w($".module {ctx.TargetAssemblyFileName}");
            foreach (var functionDefinition in ctx.Functions)
            {
                VisitFunctionDefinition(functionDefinition as FunctionDefinition);
            }
            w(@".class public Program { }");
            return ctx;
        }

        public override FunctionDefinition VisitFunctionDefinition(FunctionDefinition ctx)
        {
            if (ctx == null)
            {
                return ctx;
            }
            var args = ctx.ParameterDeclarations.ParameterDeclarations
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
            return ctx;
        }

        public override IntValueExpression VisitIntValueExpression(IntValueExpression ctx)
        {
            w($"ldc.i4.{ctx.Value}");
            return ctx;
        }

        public override LongValueExpression VisitLongValueExpression(LongValueExpression ctx)
        {
            w($"ldc.i8.{ctx.Value}");
            return ctx;
        }

        public override PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
        {
            w($"  .property instance {ctx.TypeName} {ctx.Name}(){{");
            w($"      .get instance {ctx.TypeName} NamespaceName.Class::get_{ctx.Name}()");
            w($"      .set instance void Namespace.Class::set_{ctx.Name}({ctx.TypeName})");
            w("  }");
            return ctx;
        }

        public override ShortValueExpression VisitShortValueExpression(ShortValueExpression ctx)
        {
            w($"ldc.i2.{ctx.Value}");
            return ctx;
        }

        public override StringValueExpression VisitStringValueExpression(StringValueExpression ctx)
        {
            w($"ldstr \"{ctx.Value}\"");
            return ctx;
        }

        public override BinaryExpression VisitBinaryExpression(BinaryExpression ctx)
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
            return ctx;
        }

        public override FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx)
        {
            if (ctx.TypeId == null)
            {
                if (ctx.HasAnnotation(Constants.FunctionImplementation))
                {
                    var funcImpl = ctx[Constants.FunctionImplementation] as IFunctionDefinition;
                    if (funcImpl is BuiltinFunctionDefinition)
                    {
                        if (funcImpl.Name == "print")
                        {
                            w("call void System::Console.WriteLine(string)");
                        }

                        return ctx;
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
            return ctx;
        }

        public override ReturnStatement VisitReturnStatement(ReturnStatement ctx)
        {
            w(@"ret");
            return ctx;
        }

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

        // ReSharper disable once InconsistentNaming
        private void w(string s)
            => writer.WriteLine(s);
    }
}
