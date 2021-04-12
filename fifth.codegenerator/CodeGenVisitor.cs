namespace Fifth.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AST;
    using AST.Visitors;
    using PrimitiveTypes;

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

        public override void EnterAbsoluteIri(AbsoluteIri ctx)
            => writer.WriteLine(@"    // EnterAbsoluteIri");

        public override void EnterAliasDeclaration(AliasDeclaration ctx)
            => writer.WriteLine(@"    // EnterAliasDeclaration");

        public override void EnterAssignmentStmt(AssignmentStmt ctx)
            => writer.WriteLine(@"    // EnterAssignmentStmt");

        public override void EnterBinaryExpression(BinaryExpression ctx)
            => writer.WriteLine(@"    // EnterBinaryExpression");

        public override void EnterBlock(Block ctx)
            => writer.WriteLine(@"    // EnterBlock");

        public override void EnterBoolValueExpression(BoolValueExpression ctx)
            => writer.WriteLine(@"    // EnterBoolValueExpression");

        public override void EnterDateValueExpression(DateValueExpression ctx)
            => writer.WriteLine(@"    // EnterDateValueExpression");

        public override void EnterDecimalValueExpression(DecimalValueExpression ctx)
            => writer.WriteLine(@"    // EnterDecimalValueExpression");

        public override void EnterDoubleValueExpression(DoubleValueExpression ctx)
            => writer.WriteLine(@"    // EnterDoubleValueExpression");

        public override void EnterExpression(Expression ctx)
            => writer.WriteLine(@"    // EnterExpression");

        public override void EnterExpressionList(ExpressionList ctx)
            => writer.WriteLine(@"    // EnterExpressionList");

        public override void EnterExpressionStatement(ExpressionStatement ctx)
            => writer.WriteLine(@"    // EnterExpressionStatement");

        public override void EnterFifthProgram(FifthProgram ctx)
        {
            writer.WriteLine(@"
.assembly Hello {}
.assembly extern mscorlib {}
.class public Program {
            ");
            GenerateBuiltinFunctions();
        }

        public override void EnterFloatValueExpression(FloatValueExpression ctx)
            => writer.WriteLine(@"    // EnterFloatValueExpression");

        public override void EnterFuncCallExpression(FuncCallExpression ctx)
        {
            writer.WriteLine(@"    // compute actual params");
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

        public override void EnterIdentifier(Identifier ctx)
            => writer.WriteLine(@"    // EnterIdentifier");

        public override void EnterIdentifierExpression(IdentifierExpression ctx)
            => writer.WriteLine(@"    // EnterIdentifierExpression");

        public override void EnterIfElseStatement(IfElseStatement ctx)
            => writer.WriteLine(@"    // EnterIfElseStatement");

        public override void EnterIntValueExpression(IntValueExpression ctx)
            => writer.WriteLine($"ldc.i4.{ctx.Value}");

        public override void EnterLongValueExpression(LongValueExpression ctx)
            => writer.WriteLine($"ldc.i8.{ctx.Value}");

        public override void EnterModuleImport(ModuleImport ctx)
            => writer.WriteLine(@"    // EnterModuleImport");

        public override void EnterParameterDeclaration(ParameterDeclaration ctx)
            => writer.WriteLine(@"    // EnterParameterDeclaration");

        public override void EnterParameterDeclarationList(ParameterDeclarationList ctx)
            => writer.WriteLine(@"    // EnterParameterDeclarationList");

        public override void EnterReturnStatement(ReturnStatement ctx)
            => writer.WriteLine(@"    // EnterReturnStatement");

        public override void EnterShortValueExpression(ShortValueExpression ctx)
            => writer.WriteLine($"ldc.i2.{ctx.Value}");

        public override void EnterStatementList(StatementList ctx)
            => writer.WriteLine(@"    // EnterStatementList");

        public override void EnterStringValueExpression(StringValueExpression ctx)
            => writer.WriteLine($"ldstr \"{ctx.Value}\"");

        public override void EnterTypeCast(TypeCast ctx)
            => writer.WriteLine(@"    // EnterTypeCast");

        public override void EnterTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => writer.WriteLine(@"    // EnterTypeCreateInstExpression");

        public override void EnterTypeInitialiser(TypeInitialiser ctx)
            => writer.WriteLine(@"    // EnterTypeInitialiser");

        public override void EnterUnaryExpression(UnaryExpression ctx)
            => writer.WriteLine(@"    // EnterUnaryExpression");

        public override void EnterVariableDeclarationStatement(VariableDeclarationStatement ctx) =>
            writer.WriteLine(@"    // EnterVariableDeclarationStatement");

        public override void EnterVariableReference(VariableReference ctx)
            => writer.WriteLine(@"    // EnterVariableReference");

        public override void EnterWhileExp(WhileExp ctx)
            => writer.WriteLine(@"    // EnterWhileExp");

        public override void LeaveAbsoluteIri(AbsoluteIri ctx)
            => writer.WriteLine(@"    // LeaveAbsoluteIri");

        public override void LeaveAliasDeclaration(AliasDeclaration ctx)
            => writer.WriteLine(@"    // LeaveAliasDeclaration");

        public override void LeaveAssignmentStmt(AssignmentStmt ctx)
            => writer.WriteLine(@"    // LeaveAssignmentStmt");

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

        public override void LeaveBlock(Block ctx)
            => writer.WriteLine(@"    // LeaveBlock");

        public override void LeaveBoolValueExpression(BoolValueExpression ctx)
            => writer.WriteLine(@"    // LeaveBoolValueExpression");

        public override void LeaveDateValueExpression(DateValueExpression ctx)
            => writer.WriteLine(@"    // LeaveDateValueExpression");

        public override void LeaveDecimalValueExpression(DecimalValueExpression ctx)
            => writer.WriteLine(@"    // LeaveDecimalValueExpression");

        public override void LeaveDoubleValueExpression(DoubleValueExpression ctx)
            => writer.WriteLine(@"    // LeaveDoubleValueExpression");

        public override void LeaveExpression(Expression ctx)
            => writer.WriteLine(@"    // LeaveExpression");

        public override void LeaveExpressionList(ExpressionList ctx)
            => writer.WriteLine(@"    // LeaveExpressionList");

        public override void LeaveExpressionStatement(ExpressionStatement ctx)
            => writer.WriteLine(@"    // LeaveExpressionStatement");

        public override void LeaveFifthProgram(FifthProgram ctx)
            => writer.WriteLine(@"}");

        public override void LeaveFloatValueExpression(FloatValueExpression ctx)
            => writer.WriteLine(@"    // LeaveFloatValueExpression");

        public override void LeaveFuncCallExpression(FuncCallExpression ctx)
        {
            writer.WriteLine($"callvirt {ctx.Name}");
        }

        public override void LeaveFunctionDefinition(FunctionDefinition ctx)
            => writer.WriteLine("}");

        public override void LeaveIdentifier(Identifier ctx)
            => writer.WriteLine(@"    // LeaveIdentifier");

        public override void LeaveIdentifierExpression(IdentifierExpression ctx)
            => writer.WriteLine(@"    // LeaveIdentifierExpression");

        public override void LeaveIfElseStatement(IfElseStatement ctx)
            => writer.WriteLine(@"    // LeaveIfElseStatement");

        public override void LeaveIntValueExpression(IntValueExpression ctx)
            => writer.WriteLine(@"    // LeaveIntValueExpression");

        public override void LeaveLongValueExpression(LongValueExpression ctx)
            => writer.WriteLine(@"    // LeaveLongValueExpression");

        public override void LeaveModuleImport(ModuleImport ctx)
            => writer.WriteLine(@"    // LeaveModuleImport");

        public override void LeaveParameterDeclaration(ParameterDeclaration ctx)
            => writer.WriteLine(@"    // LeaveParameterDeclaration");

        public override void LeaveParameterDeclarationList(ParameterDeclarationList ctx)
            => writer.WriteLine(@"    // LeaveParameterDeclarationList");

        public override void LeaveReturnStatement(ReturnStatement ctx)
            => writer.WriteLine(@"ret");

        public override void LeaveShortValueExpression(ShortValueExpression ctx)
            => writer.WriteLine(@"    // LeaveShortValueExpression");

        public override void LeaveStatementList(StatementList ctx)
            => writer.WriteLine(@"    // LeaveStatementList");

        public override void LeaveStringValueExpression(StringValueExpression ctx)
            => writer.WriteLine(@"    // LeaveStringValueExpression");

        public override void LeaveTypeCast(TypeCast ctx)
            => writer.WriteLine(@"    // LeaveTypeCast");

        public override void LeaveTypeCreateInstExpression(TypeCreateInstExpression ctx)
            => writer.WriteLine(@"    // LeaveTypeCreateInstExpression");

        public override void LeaveTypeInitialiser(TypeInitialiser ctx)
            => writer.WriteLine(@"    // LeaveTypeInitialiser");

        public override void LeaveUnaryExpression(UnaryExpression ctx)
            => writer.WriteLine(@"    // LeaveUnaryExpression");

        public override void LeaveVariableDeclarationStatement(VariableDeclarationStatement ctx) =>
            writer.WriteLine(@"    // LeaveVariableDeclarationStatement");

        public override void LeaveVariableReference(VariableReference ctx)
            => writer.WriteLine(@"    // LeaveVariableReference");

        public override void LeaveWhileExp(WhileExp ctx)
            => writer.WriteLine(@"    // LeaveWhileExp");

        public string MapType(TypeId tid)
            => toDotnet[tid];

        private void GenerateBuiltinFunctions() =>
            writer.WriteLine(@"    // Builtin Function Definitions go here");
    }
}
