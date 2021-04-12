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
        public override IAstNode VisitETypeCast(FifthParser.ETypeCastContext context)
        {
            var sexp = Visit(context.subexp) as Expression;
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(context.type.GetText(), out var type))
            {
                return new TypeCast(sexp, type.TypeId);
            }

            throw new TypeCheckingException("Unable to find target type for cast");
        }

        public override IAstNode VisitAlias([NotNull] FifthParser.AliasContext context) => base.VisitAlias(context);

        public override IAstNode VisitSAssignment([NotNull] FifthParser.SAssignmentContext context)
        {
            var id = Visit(context.var_name()) as Identifier;
            var expression = Visit(context.exp()) as Expression;
            var varName = id.Value;
            var variableReference = new VariableReference(varName);
            var result = new AssignmentStmt(expression, variableReference);
            return result;
        }

        public override IAstNode VisitSReturn(FifthParser.SReturnContext context)
        {
            return new ReturnStatement((Expression)Visit(context.exp()), null);
        }

        public override IAstNode VisitBlock([NotNull] FifthParser.BlockContext context)
        {
            var statements = new List<Statement>();
            foreach (var e in context.statement())
            {
                var node = base.Visit(e);
                if (node is Expression exp)
                {
                    node = new ExpressionStatement(exp);
                }
                statements.Add((Statement)node);
            }

            return new Block(new StatementList(statements));
        }

        public override IAstNode VisitBoolean(FifthParser.BooleanContext context)
            => new BoolValueExpression(bool.Parse(context.value.Text));

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
            new FloatValueExpression(float.Parse(context.value.Text));

        public override IAstNode VisitEFuncCall([NotNull] FifthParser.EFuncCallContext context)
        {
            var name = context.funcname.GetText();
            var actualParams = (ExpressionList)VisitExplist(context.args);
            return new FuncCallExpression(actualParams, name); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitEGEQ([NotNull] FifthParser.EGEQContext context)
            => BinExp(context.left, context.right, Operator.GreaterThanOrEqual);

        public override IAstNode VisitEGT([NotNull] FifthParser.EGTContext context)
            => BinExp(context.left, context.right, Operator.GreaterThan);

        public override IAstNode VisitEInt([NotNull] FifthParser.EIntContext context) =>
            new IntValueExpression(int.Parse(context.value.Text));

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
                s = s[1..^1];
            }

            return new StringValueExpression(s);
        }

        public override IAstNode VisitESub([NotNull] FifthParser.ESubContext context)
            => BinExp(context.left, context.right, Operator.Subtract);

        public override IAstNode VisitEVarname([NotNull] FifthParser.EVarnameContext context)
        {
            var id = (Identifier)Visit(context.var_name());
            return new IdentifierExpression(id); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitSWhile(FifthParser.SWhileContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var expressionList = Visit(context.looppart) as StatementList;
            var loopBlock = new Block(expressionList);
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

            return new ExpressionList(exps);
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
            return new FifthProgram(aliasDeclarations, functionDeclarations);
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
            VisitBlock(context.block());

        public override IAstNode VisitFunction_call([NotNull] FifthParser.Function_callContext context) =>
            base.VisitFunction_call(context);

        public override IAstNode VisitFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            var formals = context.args.formal_parameters();
            var parameterList = VisitFormal_parameters(formals) as ParameterDeclarationList;
            var tmp = VisitFunction_body(context.function_body());
            var body = tmp as Block;
            var name = context.name.IDENTIFIER().GetText();
            var parameterDeclarationList =
                parameterList ?? new ParameterDeclarationList(new List<ParameterDeclaration>());
            var typename = context.result_type.GetText();
            var result = new FunctionDefinition(parameterDeclarationList, body, typename, name, name == "main", null);
            return result;
        }

        public override IAstNode VisitSIfElse([NotNull] FifthParser.SIfElseContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var ifBlockEL = VisitBlock(context.ifpart) as StatementList;
            var elseBlockEL = VisitBlock(context.elsepart) as StatementList;
            var result = new IfElseStatement(new Block(ifBlockEL), new Block(elseBlockEL), condNode);
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
            return new ParameterDeclaration(new Identifier(name), type);
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
            var decl = new VariableDeclarationStatement(null, nameId)
            {
                TypeName = typename // the setter will do the tid lookup internally
            };
            return decl;
        }

        public override IAstNode VisitVar_name([NotNull] FifthParser.Var_nameContext context) =>
            new Identifier(context.IDENTIFIER().GetText());

        public override IAstNode VisitSVarDecl([NotNull] FifthParser.SVarDeclContext context)
        {
            var decl = VisitVar_decl(context.decl) as VariableDeclarationStatement;
            if (context.exp() != null)
            {
                var exp = base.Visit(context.exp());
                decl.Expression = (Expression)exp;
            }

            return decl;
        }

        public override IAstNode VisitSWith([NotNull] FifthParser.SWithContext context) =>
            base.VisitSWith(context);

        protected override IAstNode AggregateResult(IAstNode aggregate, IAstNode nextResult) =>
            base.AggregateResult(aggregate, nextResult);

        protected override bool ShouldVisitNextChild(IRuleNode node, IAstNode currentResult) =>
            base.ShouldVisitNextChild(node, currentResult);

        private BinaryExpression BinExp(FifthParser.ExpContext left, FifthParser.ExpContext right, Operator op)
        {
            var astLeft = (Expression)Visit(left);
            var astRight = (Expression)Visit(right);
            return new BinaryExpression(astLeft, op, astRight);
        }

        private UnaryExpression UnExp(FifthParser.ExpContext operand, Operator op)
        {
            var astOperand = (Expression)Visit(operand);
            var result = new UnaryExpression(astOperand, op);
            // var resultType = TypeChecker.Infer(result.NearestScope(), result);
            // result.TypeId = resultType;
            return result;
        }
    }
}
