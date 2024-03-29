namespace Fifth.CodeGeneration.LangProcessingPhases
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using AST;
    using AST.Visitors;
    using BuiltinsGeneration;
    using PrimitiveTypes;
    using TypeSystem;
    using Exception = Fifth.Exception;

    public class CodeGenVisitor : DefaultRecursiveDescentVisitor
    {

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

            if (TypeMappings.HasMapping(tid))
            {
                return TypeMappings.ToDotnetType(tid);
            }

            return tid.Lookup().Name;
        }

        public override Assembly VisitAssembly(Assembly ctx)
        {
            foreach (var assemblyRef in ctx.References ?? new List<AssemblyRef>())
            {
                VisitAssemblyRef(assemblyRef);
            }

            // w(".assembly extern mscorlib { }");
            // w(".assembly extern System.Console { }");

            w($@".assembly {ctx.Name}
            {{
              .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 )
              .custom instance void [System.Runtime]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                                               63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

              // --- The following custom attribute is added automatically, do not uncomment -------
              //  .custom instance void [System.Runtime]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 )

              .custom instance void [System.Runtime]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 18 2E 4E 45 54 43 6F 72 65 41 70 70 2C 56   // ....NETCoreApp,V
                                                                                                                          65 72 73 69 6F 6E 3D 76 35 2E 30 01 00 54 0E 14   // ersion=v5.0..T..
                                                                                                                          46 72 61 6D 65 77 6F 72 6B 44 69 73 70 6C 61 79   // FrameworkDisplay
                                                                                                                          4E 61 6D 65 00 )                                  // Name.
              .hash algorithm 0x00008004
              .ver 1:0:0:0
            }}");
            VisitFifthProgram(ctx.Program);
            return ctx;
        }

        public override AssemblyRef VisitAssemblyRef(AssemblyRef ctx)
        {
            w($@".assembly extern {ctx.Name}
            {{
              .publickeytoken = ( {ctx.PublicKeyToken} )
              .ver {ctx.Version}
            }}");
            return ctx;
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
            w($".class public  {ctx.Name} extends [System.Runtime]System.Object {{");
            
            foreach (var propertyDefinition in ctx.Properties)
            {
                VisitPropertyDefinition(propertyDefinition);
            }
            foreach (var functionDefinition in ctx.Functions)
            {
                VisitFunctionDefinition(functionDefinition as FunctionDefinition);
            }

            w("}");
            return ctx;
        }

        public override DoubleValueExpression VisitDoubleValueExpression(DoubleValueExpression ctx)
        {
            w($"ldc.r8 {ctx.Value}");
            return ctx;
        }

        public override ExpressionStatement VisitExpressionStatement(ExpressionStatement ctx)
        {
            Visit(ctx.Expression);
            return ctx;
        }

        public override FifthProgram VisitFifthProgram(FifthProgram ctx)
        {
            w($".module {ctx.TargetAssemblyFileName}");

            foreach (var classDefinition in ctx.Classes)
            {
                VisitClassDefinition(classDefinition);
            }
            w(@".class public Program {");
            foreach (var functionDefinition in ctx.Functions)
            {
                VisitFunctionDefinition(functionDefinition as FunctionDefinition);
            }

            w("}");
            w("");
            return ctx;
        }

        public override FloatValueExpression VisitFloatValueExpression(FloatValueExpression ctx)
        {
            w($"ldc.r4 {ctx.Value}");
            return ctx;
        }

        public override FuncCallExpression VisitFuncCallExpression(FuncCallExpression ctx)
        {
            List<string> dotnetTypes = new();
            if (ctx.ActualParameters != null && ctx.ActualParameters.Expressions != null)
            {
                foreach (var e in ctx.ActualParameters.Expressions)
                {
                    if (TypeMappings.HasMapping(e.TypeId))
                    {
                        dotnetTypes.Add(TypeMappings.ToDotnetType(e.TypeId));
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
                dotNetReturnType = TypeMappings.ToDotnetType(return5thType.TypeId);
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
            if (funcImpl is BuiltinFunctionDefinition bif)
            {
                if (bif.Name == "print")
                {
                    new PrintGenerator(writer).GenerateFor(ctx, bif);
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
            w($"ldc.i4 {ctx.Value}");
            return ctx;
        }

        public override LongValueExpression VisitLongValueExpression(LongValueExpression ctx)
        {
            w($"ldc.i8 {ctx.Value}");
            return ctx;
        }

        public override PropertyDefinition VisitPropertyDefinition(PropertyDefinition ctx)
        {
            var owningClassDefinition = ctx.ParentNode as ClassDefinition;
            var className = owningClassDefinition.Name;
            w($"  .property instance {ctx.TypeName} {ctx.Name}(){{");
            w($"      .get instance {ctx.TypeName} {className}::get_{ctx.Name}()");
            w($"      .set instance void {className}::set_{ctx.Name}({ctx.TypeName})");
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
            w($"ldc.i2 {ctx.Value}");
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
#pragma warning disable IDE1006 // Naming Styles
        private void w(string s)
#pragma warning restore IDE1006 // Naming Styles
            => writer.WriteLine(s);
    }
}
