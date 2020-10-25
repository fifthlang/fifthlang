namespace Fifth.Parser.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.Linq;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using Fifth.AST;
    using Fifth.VirtualMachine;

    public class AstBuilderVisitor : FifthBaseVisitor<IAstNode>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override IAstNode DefaultResult => base.DefaultResult;

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => base.ToString();

        public override IAstNode Visit(IParseTree tree)
        {
            this.Log("Visit");
            return base.Visit(tree);
        }

        public override IAstNode VisitAlias([NotNull] FifthParser.AliasContext context)
        {
            this.Log("VisitAlias");
            return base.VisitAlias(context);
        }

        public override IAstNode VisitAssignmentStmt([NotNull] FifthParser.AssignmentStmtContext context)
        {
            this.Log("VisitAssignmentStmt");
            return base.VisitAssignmentStmt(context);
        }

        public override IAstNode VisitBlock([NotNull] FifthParser.BlockContext context)
        {
            this.Log("VisitBlock");
            return base.VisitBlock(context);
        }

        public override IAstNode VisitChildren(IRuleNode node) => base.VisitChildren(node);

        public override IAstNode VisitEAdd([NotNull] FifthParser.EAddContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.Plus
        };

        public override IAstNode VisitEAnd([NotNull] FifthParser.EAndContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.And
        };

        public override IAstNode VisitEDiv([NotNull] FifthParser.EDivContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.Divide
        };

        public override IAstNode VisitEDouble([NotNull] FifthParser.EDoubleContext context) => new FloatValueExpression(float.Parse(context.value.Text));

        public override IAstNode VisitEFuncCall([NotNull] FifthParser.EFuncCallContext context)
        {
            this.Log("VisitEFuncCall");
            return base.VisitEFuncCall(context);
        }

        public override IAstNode VisitEGEQ([NotNull] FifthParser.EGEQContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.GreaterThanOrEqual
        };

        public override IAstNode VisitEGT([NotNull] FifthParser.EGTContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.GreaterThan
        };

        public override IAstNode VisitEInt([NotNull] FifthParser.EIntContext context) => new IntValueExpression(int.Parse(context.value.Text));

        public override IAstNode VisitELEQ([NotNull] FifthParser.ELEQContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.LessThanOrEqual
        };

        public override IAstNode VisitELT([NotNull] FifthParser.ELTContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.LessThan
        };

        public override IAstNode VisitEMul([NotNull] FifthParser.EMulContext context) => new BinaryExpression
        {
            Left = (Expression)this.Visit(context.left),
            Right = (Expression)this.Visit(context.right),
            Op = Operator.Times
        };

        public override IAstNode VisitENegation([NotNull] FifthParser.ENegationContext context) => new UnaryExpression
        {
            Operand = (Expression)this.Visit(context.operand),
            Op = Operator.Not
        };

        public override IAstNode VisitEParen([NotNull] FifthParser.EParenContext context) => this.Visit(context.innerexp);

        public override IAstNode VisitErrorNode(IErrorNode node) => base.VisitErrorNode(node);

        public override IAstNode VisitEStatement([NotNull] FifthParser.EStatementContext context)
        {
            this.Log("VisitEStatement");
            return base.VisitEStatement(context);
        }

        public override IAstNode VisitEString([NotNull] FifthParser.EStringContext context) => new StringValueExpression(context.value.Text);

        public override IAstNode VisitESub([NotNull] FifthParser.ESubContext context)
        {
            this.Log("VisitESub");
            return base.VisitESub(context);
        }

        public override IAstNode VisitETypeCreate([NotNull] FifthParser.ETypeCreateContext context)
        {
            this.Log("VisitETypeCreate");
            return base.VisitETypeCreate(context);
        }

        public override IAstNode VisitEVarname([NotNull] FifthParser.EVarnameContext context)
            => new IdentifierExpression { Identifier = (Identifier)this.Visit(context.var_name()) };

        public override IAstNode VisitFifth([NotNull] FifthParser.FifthContext context)
        {
            this.Log("VisitFifth");
            return base.VisitFifth(context);
        }

        public override IAstNode VisitFormal_parameters([NotNull] FifthParser.Formal_parametersContext context)
        {
            this.Log("VisitFormal_parameters");
            var parameters = new List<ParameterDeclaration>();
            parameters.AddRange(
                context
                    .children
                    .Where(c => c is FifthParser.Parameter_declarationContext)
                    .Select(c => VisitParameter_declaration((FifthParser.Parameter_declarationContext)c))
                    .Cast<ParameterDeclaration>());
            return new ParameterDeclarationList
            {
                ParameterDeclarations = parameters
            };
        }

        public override IAstNode VisitFunction_args([NotNull] FifthParser.Function_argsContext context)
        {
            this.Log("VisitFunction_args");
            return base.VisitFunction_args(context);
        }

        public override IAstNode VisitFunction_body([NotNull] FifthParser.Function_bodyContext context)
        {
            this.Log("VisitFunction_body");
            return new ExpressionList
            {
                Expressions = context.explist()
                                    .children
                                    .Select(e => Visit(e))
                                    .Cast<Expression>()
                                    .ToList()
            };
        }

        public override IAstNode VisitFunction_call([NotNull] FifthParser.Function_callContext context)
        {
            this.Log("VisitFunction_call");
            return base.VisitFunction_call(context);
        }

        public override IAstNode VisitFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            this.Log("VisitFunction_declaration");
            var formals = context.function_args().formal_parameters();
            var parameterList = this.VisitFormal_parameters(formals);
            var body = this.VisitFunction_body(context.function_body());
            var name = context.function_name().IDENTIFIER().GetText();
            return new FunctionDefinition
            {
                Body = body as ExpressionList,
                ParameterDeclarations = parameterList as ParameterDeclarationList,
                ReturnType = TypeHelpers.LookupBuiltinType("int"),
                Name = name
            };
        }

        public override IAstNode VisitIfElseStmt([NotNull] FifthParser.IfElseStmtContext context)
        {
            this.Log("VisitIfElseStmt");
            return base.VisitIfElseStmt(context);
        }

        public override IAstNode VisitIri([NotNull] FifthParser.IriContext context)
        {
            this.Log("VisitIri");
            return base.VisitIri(context);
        }

        public override IAstNode VisitIri_query([NotNull] FifthParser.Iri_queryContext context)
        {
            this.Log("VisitIri_query");
            return base.VisitIri_query(context);
        }

        public override IAstNode VisitIri_query_param([NotNull] FifthParser.Iri_query_paramContext context)
        {
            this.Log("VisitIri_query_param");
            return base.VisitIri_query_param(context);
        }

        public override IAstNode VisitModule_import([NotNull] FifthParser.Module_importContext context)
        {
            this.Log("VisitModule_import");
            return base.VisitModule_import(context);
        }

        public override IAstNode VisitModule_name([NotNull] FifthParser.Module_nameContext context)
        {
            this.Log("VisitModule_name");
            return base.VisitModule_name(context);
        }

        public override IAstNode VisitPackagename([NotNull] FifthParser.PackagenameContext context)
        {
            this.Log("VisitPackagename");
            return base.VisitPackagename(context);
        }

        public override IAstNode VisitParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context)
        {
            this.Log("VisitParameter_declaration");
            var type = context.parameter_type().IDENTIFIER().GetText();
            var name = context.parameter_name().IDENTIFIER().GetText();
            return new ParameterDeclaration
            {
                ParameterName = name,
                ParameterType = TypeHelpers.LookupBuiltinType(type)
            };
        }

        public override IAstNode VisitParameter_name([NotNull] FifthParser.Parameter_nameContext context)
        {
            this.Log("VisitParameter_name");
            return base.VisitParameter_name(context);
        }

        public override IAstNode VisitParameter_type([NotNull] FifthParser.Parameter_typeContext context)
        {
            this.Log("VisitParameter_type");
            return base.VisitParameter_type(context);
        }

        public override IAstNode VisitTerminal(ITerminalNode node) => base.VisitTerminal(node);

        public override IAstNode VisitType_initialiser([NotNull] FifthParser.Type_initialiserContext context)
        {
            this.Log("VisitType_initialiser");
            return base.VisitType_initialiser(context);
        }

        public override IAstNode VisitType_name([NotNull] FifthParser.Type_nameContext context)
        {
            this.Log("VisitType_name");
            return new Identifier { Value = context.IDENTIFIER().GetText() };
        }

        public override IAstNode VisitType_property_init([NotNull] FifthParser.Type_property_initContext context)
        {
            this.Log("VisitType_property_init");
            return base.VisitType_property_init(context);
        }

        public override IAstNode VisitVar_name([NotNull] FifthParser.Var_nameContext context)
        {
            this.Log("VisitVar_name");
            return new Identifier { Value = context.IDENTIFIER().GetText() };
        }

        public override IAstNode VisitVarDeclStmt([NotNull] FifthParser.VarDeclStmtContext context)
        {
            this.Log("VisitVarDeclStmt");
            return base.VisitVarDeclStmt(context);
        }

        public override IAstNode VisitWithStmt([NotNull] FifthParser.WithStmtContext context)
        {
            this.Log("VisitWithStmt");
            return base.VisitWithStmt(context);
        }

        protected override IAstNode AggregateResult(IAstNode aggregate, IAstNode nextResult) => base.AggregateResult(aggregate, nextResult);

        protected override bool ShouldVisitNextChild(IRuleNode node, IAstNode currentResult) => base.ShouldVisitNextChild(node, currentResult);

        private void Log(string msg) => log.Info(msg);
    }
}
