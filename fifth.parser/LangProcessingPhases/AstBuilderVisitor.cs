namespace Fifth.Parser.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.Linq;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using AST;
    using PrimitiveTypes;
    using TypeSystem;

    public class AstBuilderVisitor : FifthBaseVisitor<IAstNode>
    {
        public override IAstNode Visit(IParseTree tree) => base.Visit(tree);

        public override IAstNode VisitAlias([NotNull] FifthParser.AliasContext context) => base.VisitAlias(context);

        public override IAstNode VisitAssignmentStmt([NotNull] FifthParser.AssignmentStmtContext context)
        {
            var id = Visit(context.var_name()) as Identifier;
            var expression = Visit(context.exp()) as Expression;
            var varName = id.Value;
            var result = new AssignmentStmt(null) { Expression = expression };
            var variableReference = new VariableReference(result, null) { Name = varName };
            result.VariableRef = variableReference;
            return result;
        }

        public override IAstNode VisitBlock([NotNull] FifthParser.BlockContext context) =>
            VisitExplist(context.explist());

        public override IAstNode VisitBoolean(FifthParser.BooleanContext context)
            => new BooleanExpression(bool.Parse(context.value.Text), PrimitiveBool.Default.TypeId);

        public override IAstNode VisitChildren(IRuleNode node) => base.VisitChildren(node);

        public override IAstNode VisitEAdd([NotNull] FifthParser.EAddContext context)
            => BinExp(context.left, context.right, Operator.Add);

        public override IAstNode VisitEAnd([NotNull] FifthParser.EAndContext context)
            => BinExp(context.left, context.right, Operator.And);

        public override IAstNode VisitEArithNegation([NotNull] FifthParser.EArithNegationContext context)
            => UnExp(context.operand, Operator.Subtract);

        public override IAstNode VisitEDiv([NotNull] FifthParser.EDivContext context)
            => BinExp(context.left, context.right, Operator.Divide);

        public override IAstNode VisitEDouble([NotNull] FifthParser.EDoubleContext context) =>
            new FloatValueExpression(float.Parse(context.value.Text), PrimitiveFloat.Default.TypeId);

        public override IAstNode VisitEFuncCall([NotNull] FifthParser.EFuncCallContext context)
        {
            var name = context.funcname.GetText();
            var actualParams = (ExpressionList)VisitExplist(context.args);
            return new FuncCallExpression(name, actualParams); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitEGEQ([NotNull] FifthParser.EGEQContext context)
            => BinExp(context.left, context.right, Operator.GreaterThanOrEqual);

        public override IAstNode VisitEGT([NotNull] FifthParser.EGTContext context)
            => BinExp(context.left, context.right, Operator.GreaterThan);

        public override IAstNode VisitEInt([NotNull] FifthParser.EIntContext context) =>
            new IntValueExpression(int.Parse(context.value.Text), PrimitiveInteger.Default.TypeId);

        public override IAstNode VisitELEQ([NotNull] FifthParser.ELEQContext context)
            => BinExp(context.left, context.right, Operator.LessThanOrEqual);

        public override IAstNode VisitELogicNegation([NotNull] FifthParser.ELogicNegationContext context)
            => UnExp(context.operand, Operator.Not);

        public override IAstNode VisitELT([NotNull] FifthParser.ELTContext context)
            => BinExp(context.left, context.right, Operator.LessThan);

        public override IAstNode VisitEMul([NotNull] FifthParser.EMulContext context)
            => BinExp(context.left, context.right, Operator.Multiply);

        public override IAstNode VisitEParen([NotNull] FifthParser.EParenContext context) => Visit(context.innerexp);

        public override IAstNode VisitErrorNode(IErrorNode node) => base.VisitErrorNode(node);

        public override IAstNode VisitEString([NotNull] FifthParser.EStringContext context)
        {
            var s = context.value.Text;
            if ((s.StartsWith("\'") && s.EndsWith("\'")) || (s.StartsWith("\"") && s.EndsWith("\"")))
            {
                s = s.Substring(1, s.Length - 2);
            }

            return new StringValueExpression(s, PrimitiveString.Default.TypeId);
        }

        public override IAstNode VisitESub([NotNull] FifthParser.ESubContext context)
            => BinExp(context.left, context.right, Operator.Subtract);

        public override IAstNode VisitEVarname([NotNull] FifthParser.EVarnameContext context)
        {
            var id = (Identifier)Visit(context.var_name());
            return new IdentifierExpression(id, null); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitEWhile(FifthParser.EWhileContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var expressionList = Visit(context.looppart) as ExpressionList;
            var loopBlock = new Block(null, expressionList);
            var result = new WhileExp(condNode, loopBlock);
            return result;
        }

        public override IAstNode VisitExplist([NotNull] FifthParser.ExplistContext context)
        {
            var exps = new List<Expression>();
            foreach (var e in context.exp())
            {
                exps.Add((Expression)base.Visit(e));
            }

            return new ExpressionList(exps, null);
        }

        public override IAstNode VisitFifth([NotNull] FifthParser.FifthContext context)
        {
            var functionDeclarations = context._functions
                .Select(fctx => VisitFunction_declaration(fctx))
                .Cast<FunctionDefinition>()
                .ToList();
            var aliasDeclarations = context.alias()
                .Select(actx => VisitAlias(actx))
                .Cast<AliasDeclaration>()
                .ToList();
            return new FifthProgram { Functions = functionDeclarations, Aliases = aliasDeclarations };
        }

        public override IAstNode VisitFormal_parameters([NotNull] FifthParser.Formal_parametersContext context)
        {
            if (context == null)
            {
                return null;
            }

            var parameters = new List<ParameterDeclaration>();
            parameters.AddRange(
                context
                    .children
                    .Where(c => c is FifthParser.Parameter_declarationContext)
                    .Select(c => VisitParameter_declaration((FifthParser.Parameter_declarationContext)c))
                    .Cast<ParameterDeclaration>());
            return new ParameterDeclarationList(parameters);
        }

        public override IAstNode VisitFunction_args([NotNull] FifthParser.Function_argsContext context) =>
            base.VisitFunction_args(context);

        public override IAstNode VisitFunction_body([NotNull] FifthParser.Function_bodyContext context) =>
            VisitExplist(context.explist());

        public override IAstNode VisitFunction_call([NotNull] FifthParser.Function_callContext context) =>
            base.VisitFunction_call(context);

        public override IAstNode VisitFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            var formals = context.function_args().formal_parameters();
            var parameterList = VisitFormal_parameters(formals) as ParameterDeclarationList;
            var body = VisitFunction_body(context.function_body()) as ExpressionList;
            var name = context.function_name().IDENTIFIER().GetText();
            var parameterDeclarationList =
                parameterList ?? new ParameterDeclarationList(new List<ParameterDeclaration>());
            var typename = context.result_type.GetText();
            return new FunctionDefinition(name, parameterDeclarationList, body, typename, null);
        }

        public override IAstNode VisitIfElseStmt([NotNull] FifthParser.IfElseStmtContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var ifExpList = VisitBlock(context.ifpart) as ExpressionList;
            var result = new IfElseExp(null);
            var ifBlock = new Block(result, ifExpList);
            var elseBlock = new Block(result, VisitBlock(context.elsepart) as ExpressionList);
            result.Construct(condNode, ifBlock, elseBlock);
            return result;
        }

        public override IAstNode VisitIri([NotNull] FifthParser.IriContext context) => base.VisitIri(context);

        public override IAstNode VisitIri_query_param([NotNull] FifthParser.Iri_query_paramContext context) =>
            base.VisitIri_query_param(context);

        public override IAstNode VisitModule_import([NotNull] FifthParser.Module_importContext context) =>
            base.VisitModule_import(context);

        public override IAstNode VisitModule_name([NotNull] FifthParser.Module_nameContext context) =>
            base.VisitModule_name(context);

        public override IAstNode VisitPackagename([NotNull] FifthParser.PackagenameContext context) =>
            base.VisitPackagename(context);

        public override IAstNode VisitParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context)
        {
            var type = context.parameter_type().IDENTIFIER().GetText();
            var name = context.parameter_name().IDENTIFIER().GetText();
            return new ParameterDeclaration(name, type, null);
        }

        public override IAstNode VisitParameter_name([NotNull] FifthParser.Parameter_nameContext context) =>
            base.VisitParameter_name(context);

        public override IAstNode VisitParameter_type([NotNull] FifthParser.Parameter_typeContext context) =>
            base.VisitParameter_type(context);

        public override IAstNode VisitTerminal(ITerminalNode node) => base.VisitTerminal(node);

        public override IAstNode VisitType_initialiser([NotNull] FifthParser.Type_initialiserContext context) =>
            base.VisitType_initialiser(context);

        public override IAstNode VisitType_name([NotNull] FifthParser.Type_nameContext context) =>
            new Identifier(context.IDENTIFIER().GetText());

        public override IAstNode VisitType_property_init([NotNull] FifthParser.Type_property_initContext context) =>
            base.VisitType_property_init(context);

        public override IAstNode VisitVar_decl([NotNull] FifthParser.Var_declContext context)
        {
            var nameId = base.Visit(context.var_name()) as Identifier;
            var typename = context.type_name().GetText();
            var varType = TypeHelpers.LookupType(typename);

            return new VariableDeclarationStatement(nameId, default, varType);
        }

        public override IAstNode VisitVar_name([NotNull] FifthParser.Var_nameContext context) =>
            new Identifier(context.IDENTIFIER().GetText());

        public override IAstNode VisitVarDeclStmt([NotNull] FifthParser.VarDeclStmtContext context)
        {
            var decl = VisitVar_decl(context.decl) as VariableDeclarationStatement;
            if (context.exp() != null)
            {
                var exp = base.Visit(context.exp());
                decl.Expression = (Expression)exp;
            }

            return decl;
        }

        public override IAstNode VisitWithStmt([NotNull] FifthParser.WithStmtContext context) =>
            base.VisitWithStmt(context);

        protected override IAstNode AggregateResult(IAstNode aggregate, IAstNode nextResult) =>
            base.AggregateResult(aggregate, nextResult);

        protected override bool ShouldVisitNextChild(IRuleNode node, IAstNode currentResult) =>
            base.ShouldVisitNextChild(node, currentResult);

        private BinaryExpression BinExp(FifthParser.ExpContext left, FifthParser.ExpContext right, Operator op)
        {
            var astLeft = (Expression)Visit(left);
            var astRight = (Expression)Visit(right);
            return new BinaryExpression(astLeft, astRight, op, null);
        }

        private UnaryExpression UnExp(FifthParser.ExpContext operand, Operator op)
        {
            var astOperand = (Expression)Visit(operand);
            var result = new UnaryExpression(astOperand, op, null);
            // var resultType = TypeChecker.Infer(result.NearestScope(), result);
            // result.TypeId = resultType;
            return result;
        }
    }
}
