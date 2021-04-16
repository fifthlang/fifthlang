namespace Fifth.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using AST;
    using Parser.LangProcessingPhases;
    using TypeSystem;

    public class AstBuilderVisitor : FifthBaseVisitor<IAstNode>
    {
        public override IAstNode Visit(IParseTree tree) => base.Visit(tree);

        public override IAstNode VisitAlias([NotNull] FifthParser.AliasContext context) => base.VisitAlias(context);

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

            return new Block(new StatementList(statements)).CaptureLocation(context.Start);
        }

        public override IAstNode VisitChildren(IRuleNode node) => base.VisitChildren(node);

        public override IAstNode VisitClass_definition(FifthParser.Class_definitionContext context)
        {
            var name = context.name.Text;
            var properties = context._properties
                                    .Select(fctx => VisitProperty_declaration(fctx))
                                    .Cast<PropertyDefinition>()
                                    .ToList();

            var classDefinition = new ClassDefinition(name, properties, new List<FunctionDefinition>()).CaptureLocation(context.Start);
            TypeRegistry.DefaultRegistry.RegisterType(new UserDefinedType(classDefinition));
            return classDefinition;
        }

        public override IAstNode VisitEAdd([NotNull] FifthParser.EAddContext context)
            => BinExp(context.left, context.right, Operator.Add).CaptureLocation(context.Start);

        public override IAstNode VisitEAnd([NotNull] FifthParser.EAndContext context)
            => BinExp(context.left, context.right, Operator.And).CaptureLocation(context.Start);

        public override IAstNode VisitEArithNegation([NotNull] FifthParser.EArithNegationContext context)
            => UnExp(context.operand, Operator.Subtract).CaptureLocation(context.Start);

        public override IAstNode VisitEDiv([NotNull] FifthParser.EDivContext context)
            => BinExp(context.left, context.right, Operator.Divide).CaptureLocation(context.Start);

        public override IAstNode VisitEDouble([NotNull] FifthParser.EDoubleContext context) =>
            new FloatValueExpression(float.Parse(context.value.Text)).CaptureLocation(context.Start);

        public override IAstNode VisitEFuncCall([NotNull] FifthParser.EFuncCallContext context)
        {
            var name = context.funcname.GetText();
            var actualParams = (ExpressionList)VisitExplist(context.args);
            return new FuncCallExpression(actualParams, name)
                .CaptureLocation(context.Start); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitEGEQ([NotNull] FifthParser.EGEQContext context)
            => BinExp(context.left, context.right, Operator.GreaterThanOrEqual).CaptureLocation(context.Start);

        public override IAstNode VisitEGT([NotNull] FifthParser.EGTContext context)
            => BinExp(context.left, context.right, Operator.GreaterThan).CaptureLocation(context.Start);

        public override IAstNode VisitEInt([NotNull] FifthParser.EIntContext context) =>
            new IntValueExpression(int.Parse(context.value.Text)).CaptureLocation(context.Start);

        public override IAstNode VisitELEQ([NotNull] FifthParser.ELEQContext context)
            => BinExp(context.left, context.right, Operator.LessThanOrEqual).CaptureLocation(context.Start);

        public override IAstNode VisitELogicNegation([NotNull] FifthParser.ELogicNegationContext context)
            => UnExp(context.operand, Operator.Not).CaptureLocation(context.Start);

        public override IAstNode VisitELT([NotNull] FifthParser.ELTContext context)
            => BinExp(context.left, context.right, Operator.LessThan).CaptureLocation(context.Start);

        public override IAstNode VisitEMul([NotNull] FifthParser.EMulContext context)
            => BinExp(context.left, context.right, Operator.Multiply).CaptureLocation(context.Start);

        public override IAstNode VisitEParen([NotNull] FifthParser.EParenContext context) =>
            Visit(context.innerexp).CaptureLocation(context.Start);

        public override IAstNode VisitErrorNode(IErrorNode node) => base.VisitErrorNode(node);

        public override IAstNode VisitEString([NotNull] FifthParser.EStringContext context)
        {
            var s = context.value.Text;
            if ((s.StartsWith("\'") && s.EndsWith("\'")) || (s.StartsWith("\"") && s.EndsWith("\"")))
            {
                s = s[1..^1];
            }

            return new StringValueExpression(s).CaptureLocation(context.Start);
        }

        public override IAstNode VisitESub([NotNull] FifthParser.ESubContext context)
            => BinExp(context.left, context.right, Operator.Subtract).CaptureLocation(context.Start);

        public override IAstNode VisitETypeCast(FifthParser.ETypeCastContext context)
        {
            var sexp = Visit(context.subexp) as Expression;
            if (TypeRegistry.DefaultRegistry.TryGetTypeByName(context.type.GetText(), out var type))
            {
                return new TypeCast(sexp, type.TypeId).CaptureLocation(context.Start);
            }

            throw new TypeCheckingException("Unable to find target type for cast");
        }

        public override IAstNode VisitEVarname([NotNull] FifthParser.EVarnameContext context)
        {
            var id = context.var_name().GetText();
            return
                new VariableReference(id)
                    .CaptureLocation(context.Start); // TODO: I need to supply the type, perhaps via symtab
        }

        public override IAstNode VisitExplist([NotNull] FifthParser.ExplistContext context)
        {
            var exps = new List<Expression>();
            foreach (var e in context.exp())
            {
                exps.Add((Expression)base.Visit(e));
            }

            return new ExpressionList(exps).CaptureLocation(context.Start);
        }

        public override IAstNode VisitFifth([NotNull] FifthParser.FifthContext context)
        {
            var classDeclarations = context._classes
                                           .Select(fctx => VisitClass_definition(fctx))
                                           .Cast<ClassDefinition>()
                                           .ToList();
            var functionDeclarations = context._functions
                                              .Select(fctx => VisitFunction_declaration(fctx))
                                              .Cast<FunctionDefinition>()
                                              .ToList();
            var aliasDeclarations = context.alias()
                                           .Select(actx => VisitAlias(actx))
                                           .Cast<AliasDeclaration>()
                                           .ToList();
            var result = new FifthProgram(aliasDeclarations, classDeclarations, functionDeclarations)
                .CaptureLocation(context.Start);
            result.TargetAssemblyFileName = Path.GetFileName( Path.ChangeExtension(result.Filename, "exe"));
            return result;
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
            return new ParameterDeclarationList(parameters).CaptureLocation(context.Start);
        }

        public override IAstNode VisitFunction_args([NotNull] FifthParser.Function_argsContext context) =>
            base.VisitFunction_args(context).CaptureLocation(context.Start);

        public override IAstNode VisitFunction_body([NotNull] FifthParser.Function_bodyContext context) =>
            VisitBlock(context.block()).CaptureLocation(context.Start);

        public override IAstNode VisitFunction_call([NotNull] FifthParser.Function_callContext context) =>
            base.VisitFunction_call(context).CaptureLocation(context.Start);

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
            return result.CaptureLocation(context.Start);
        }

        public override IAstNode VisitIri([NotNull] FifthParser.IriContext context) =>
            base.VisitIri(context).CaptureLocation(context.Start);

        public override IAstNode VisitIri_query_param([NotNull] FifthParser.Iri_query_paramContext context) =>
            base.VisitIri_query_param(context).CaptureLocation(context.Start);

        public override IAstNode VisitModule_import([NotNull] FifthParser.Module_importContext context) =>
            base.VisitModule_import(context).CaptureLocation(context.Start);

        public override IAstNode VisitModule_name([NotNull] FifthParser.Module_nameContext context) =>
            base.VisitModule_name(context).CaptureLocation(context.Start);

        public override IAstNode VisitPackagename([NotNull] FifthParser.PackagenameContext context) =>
            base.VisitPackagename(context).CaptureLocation(context.Start);

        public override IAstNode VisitParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context)
        {
            var type = context.parameter_type().IDENTIFIER().GetText();
            var name = context.parameter_name().IDENTIFIER().GetText();
            return new ParameterDeclaration(new Identifier(name), type).CaptureLocation(context.Start);
        }

        public override IAstNode VisitParameter_name([NotNull] FifthParser.Parameter_nameContext context) =>
            base.VisitParameter_name(context).CaptureLocation(context.Start);

        public override IAstNode VisitParameter_type([NotNull] FifthParser.Parameter_typeContext context) =>
            base.VisitParameter_type(context).CaptureLocation(context.Start);

        public override IAstNode VisitProperty_declaration(FifthParser.Property_declarationContext context)
        {
            var name = context.name.Text;
            var type = context.type.Text;

            return new PropertyDefinition(name, type).CaptureLocation(context.Start);
        }

        public override IAstNode VisitSAssignment([NotNull] FifthParser.SAssignmentContext context)
        {
            var id = Visit(context.var_name()) as Identifier;
            var expression = Visit(context.exp()) as Expression;
            var varName = id.Value;
            var variableReference = new VariableReference(varName);
            var result = new AssignmentStmt(expression, variableReference);
            return result.CaptureLocation(context.Start);
        }

        public override IAstNode VisitSIfElse([NotNull] FifthParser.SIfElseContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var ifBlockEL = VisitBlock(context.ifpart) as StatementList;
            var elseBlockEL = VisitBlock(context.elsepart) as StatementList;
            var result = new IfElseStatement(new Block(ifBlockEL), new Block(elseBlockEL), condNode);
            return result.CaptureLocation(context.Start);
        }

        public override IAstNode VisitSReturn(FifthParser.SReturnContext context) =>
            new ReturnStatement((Expression)Visit(context.exp()), null).CaptureLocation(context.Start);

        public override IAstNode VisitSVarDecl([NotNull] FifthParser.SVarDeclContext context)
        {
            var decl = VisitVar_decl(context.decl) as VariableDeclarationStatement;
            if (context.exp() != null)
            {
                var exp = base.Visit(context.exp());
                decl.Expression = (Expression)exp;
            }

            return decl.CaptureLocation(context.Start);
        }

        public override IAstNode VisitETypeCreateInst(FifthParser.ETypeCreateInstContext context)
        {
            var typeInitialiser = context.type_initialiser();
            var propertyInits = from x in typeInitialiser._properties
                                select VisitType_property_init(x);
            
            var result = new TypeInitialiser(propertyInits.Cast<TypePropertyInit>().ToList());
            return result;
        }

        public override IAstNode VisitSWhile(FifthParser.SWhileContext context)
        {
            var condNode = Visit(context.condition) as Expression;
            var expressionList = Visit(context.looppart) as StatementList;
            var loopBlock = new Block(expressionList);
            var result = new WhileExp(condNode, loopBlock);
            return result.CaptureLocation(context.Start);
            ;
        }

        public override IAstNode VisitSWith([NotNull] FifthParser.SWithContext context) =>
            base.VisitSWith(context).CaptureLocation(context.Start);

        public override IAstNode VisitTerminal(ITerminalNode node) => base.VisitTerminal(node);

        public override IAstNode VisitTruth_value(FifthParser.Truth_valueContext context)
            => new BoolValueExpression(bool.Parse(context.value.Text)).CaptureLocation(context.Start);

        public override IAstNode VisitType_initialiser([NotNull] FifthParser.Type_initialiserContext context) =>
            base.VisitType_initialiser(context).CaptureLocation(context.Start);

        public override IAstNode VisitType_name([NotNull] FifthParser.Type_nameContext context) =>
            new Identifier(context.IDENTIFIER().GetText()).CaptureLocation(context.Start);

        public override IAstNode VisitType_property_init([NotNull] FifthParser.Type_property_initContext context)
        {
            var exp = Visit(context.exp()) as Expression;
            return new TypePropertyInit(context.var_name().GetText(), exp);
        }

        public override IAstNode VisitVar_decl([NotNull] FifthParser.Var_declContext context)
        {
            var nameId = base.Visit(context.var_name()) as Identifier;
            var typename = context.type_name().GetText();
            var decl = new VariableDeclarationStatement(null, nameId)
            {
                TypeName = typename // the setter will do the tid lookup internally
            };
            return decl.CaptureLocation(context.Start);
        }

        public override IAstNode VisitVar_name([NotNull] FifthParser.Var_nameContext context) =>
            new Identifier(context.IDENTIFIER().GetText()).CaptureLocation(context.Start);

        protected override IAstNode AggregateResult(IAstNode aggregate, IAstNode nextResult) =>
            base.AggregateResult(aggregate, nextResult);

        protected override bool ShouldVisitNextChild(IRuleNode node, IAstNode currentResult) =>
            base.ShouldVisitNextChild(node, currentResult);

        private BinaryExpression BinExp(FifthParser.ExpContext left, FifthParser.ExpContext right, Operator op)
        {
            var astLeft = (Expression)Visit(left).CaptureLocation(left.Start);
            var astRight = (Expression)Visit(right).CaptureLocation(right.Start);
            return new BinaryExpression(astLeft, op, astRight);
        }

        private UnaryExpression UnExp(FifthParser.ExpContext operand, Operator op)
        {
            var astOperand = (Expression)Visit(operand);
            var result = new UnaryExpression(astOperand, op);
            // var resultType = TypeChecker.Infer(result.NearestScope(), result);
            // result.TypeId = resultType;
            return result.CaptureLocation(operand.Start);
        }
    }
}
