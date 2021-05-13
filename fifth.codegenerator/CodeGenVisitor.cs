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
    using Exception = Fifth.Exception;

    public class CodeGenVisitor : DefaultRecursiveDescentVisitor
    {
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
        private ulong labelCounter;

        public CodeGenVisitor(TextWriter writer)
            => this.writer = writer;

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

        public override BinaryExpression VisitBinaryExpression(BinaryExpression ctx)
        {
            Visit(ctx.Left);
            Visit(ctx.Right);
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

        public override Block VisitBlock(Block ctx)
        {
            if (ctx == null)
            {
                return ctx;
            }

            foreach (var statement in ctx.Statements)
            {
                Visit(statement);
            }

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

        public override FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx)
        {
            List<string> dotnetTypes = new();
            if (ctx.ActualParameters != null && ctx.ActualParameters.Expressions != null)
            {
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

            }

            var argTypeNames = dotnetTypes.Join(t => t);
            var dotNetReturnType = "void";
            if (ctx.TypeId != null)
            {
                var return5thType = ctx.TypeId.Lookup();
                dotNetReturnType = toDotnet[return5thType.TypeId];
            }

            if (!ctx.HasAnnotation(Constants.FunctionImplementation))
            {
                throw new Exception("No implementation found to call");
            }

            if (ctx.ActualParameters != null && ctx.ActualParameters.Expressions != null)
            {
                foreach (var e in ctx.ActualParameters.Expressions)
                {
                    Visit(e);
                }
            }

            var funcImpl = ctx[Constants.FunctionImplementation] as IFunctionDefinition;
            if (funcImpl is BuiltinFunctionDefinition)
            {
                if (funcImpl.Name == "print")
                {
                    w("call void System::Console.WriteLine(string)");
                }

                return ctx;
            }

            w($"call {dotNetReturnType} Program::{ctx.Name}({argTypeNames})");

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
            var declCtr = 0;
            var sep = "";
            if (locals.Decls.Any())
            {
                w(".locals init(");
                foreach (var vd in locals.Decls)
                {
                    vd[Constants.DeclarationOrdinal] = declCtr;
                    w($"{sep} [{declCtr}] {MapType(vd.TypeId)} {vd.Name.Value}");
                    declCtr++;
                    sep = ",";
                }

                w(")");
            }

            Visit(ctx.Body);
            w("}");
            return ctx;
        }

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

        public override ReturnStatement VisitReturnStatement(ReturnStatement ctx)
        {
            Visit(ctx.SubExpression);
            w(@"ret");
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

        public override VariableDeclarationStatement VisitVariableDeclarationStatement(VariableDeclarationStatement ctx)
        {
            var ord = ctx.HasAnnotation(Constants.DeclarationOrdinal) ? ctx[Constants.DeclarationOrdinal] : ctx.Name;
            if (ctx.Expression != null)
            {
                Visit(ctx.Expression);
                w($"stloc.{ord}");
            }

            return ctx;
        }

        public override VariableReference VisitVariableReference(VariableReference ctx)
        {
            var ord = GetVarOrdinal(ctx);
            if (ord.HasValue)
            {
                w($"ldloc.{ord}");
            }
            else
            {
                w($"ldloc.{ctx.Name}");
            }

            return ctx;
        }

        private int? GetVarOrdinal(VariableReference ctx)
        {
            if (ctx.NearestScope().TryResolve(ctx.Name, out var ste))
            {
                if (ste.Context is VariableDeclarationStatement vds)
                {
                    if (vds.HasAnnotation(Constants.DeclarationOrdinal))
                    {
                        return (int)vds[Constants.DeclarationOrdinal];
                    }
                }
            }

            return null;
        }

        // ReSharper disable once InconsistentNaming
        private void w(string s)
            => writer.WriteLine(s);
    }
}
