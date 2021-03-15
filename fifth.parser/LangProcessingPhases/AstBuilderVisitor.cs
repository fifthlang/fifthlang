namespace Fifth.Parser.LangProcessingPhases
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using AST;
    using log4net;

    public class AstBuilderVisitor : FifthBaseVisitor<IAstNode>
    {
        public override IAstNode Visit(IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override IAstNode VisitAlias([NotNull] FifthParser.AliasContext context)
        {
            return base.VisitAlias(context);
        }

        public override IAstNode VisitAssignmentStmt([NotNull] FifthParser.AssignmentStmtContext context)
        {
            return base.VisitAssignmentStmt(context);
        }

        public override IAstNode VisitBlock([NotNull] FifthParser.BlockContext context)
        {
            return base.VisitBlock(context);
        }

        public override IAstNode VisitBoolean(FifthParser.BooleanContext context)
            => new BooleanExpression(bool.Parse(context.value.Text));

        public override IAstNode VisitChildren(IRuleNode node) => base.VisitChildren(node);

        public override IAstNode VisitEAdd([NotNull] FifthParser.EAddContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.Add
        };

        public override IAstNode VisitEAnd([NotNull] FifthParser.EAndContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.And
        };

        public override IAstNode VisitEArithNegation([NotNull] FifthParser.EArithNegationContext context) =>
            new UnaryExpression {Operand = (Expression)Visit(context.operand), Op = Operator.Subtract};

        public override IAstNode VisitEDiv([NotNull] FifthParser.EDivContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.Divide
        };

        public override IAstNode VisitEDouble([NotNull] FifthParser.EDoubleContext context) =>
            new FloatValueExpression(float.Parse(context.value.Text));

        public override IAstNode VisitEFuncCall([NotNull] FifthParser.EFuncCallContext context)
        {
            var name = context.funcname.GetText();
            var actualParams = VisitExplist(context.args);
            return new FuncCallExpression {Name = name, ActualParameters = (ExpressionList)actualParams};
        }

        public override IAstNode VisitEGEQ([NotNull] FifthParser.EGEQContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left),
            Right = (Expression)Visit(context.right),
            Op = Operator.GreaterThanOrEqual
        };

        public override IAstNode VisitEGT([NotNull] FifthParser.EGTContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left),
            Right = (Expression)Visit(context.right),
            Op = Operator.GreaterThan
        };

        public override IAstNode VisitEInt([NotNull] FifthParser.EIntContext context) =>
            new IntValueExpression(int.Parse(context.value.Text));

        public override IAstNode VisitELEQ([NotNull] FifthParser.ELEQContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left),
            Right = (Expression)Visit(context.right),
            Op = Operator.LessThanOrEqual
        };

        public override IAstNode VisitELogicNegation([NotNull] FifthParser.ELogicNegationContext context) =>
            new UnaryExpression {Operand = (Expression)Visit(context.operand), Op = Operator.Not};

        public override IAstNode VisitELT([NotNull] FifthParser.ELTContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.LessThan
        };

        public override IAstNode VisitEMul([NotNull] FifthParser.EMulContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.Multiply
        };

        public override IAstNode VisitEParen([NotNull] FifthParser.EParenContext context) => Visit(context.innerexp);

        public override IAstNode VisitErrorNode(IErrorNode node) => base.VisitErrorNode(node);

        public override IAstNode VisitEString([NotNull] FifthParser.EStringContext context)
        {
            var s = context.value.Text;
            if ((s.StartsWith("\'") && s.EndsWith("\'")) || (s.StartsWith("\"") && s.EndsWith("\"")))
            {
                s = s.Substring(1, s.Length - 2);
            }

            return new StringValueExpression(s);
        }

        public override IAstNode VisitESub([NotNull] FifthParser.ESubContext context) => new BinaryExpression
        {
            Left = (Expression)Visit(context.left), Right = (Expression)Visit(context.right), Op = Operator.Subtract
        };

        //public override IAstNode VisitETypeCreate([NotNull] FifthParser.ETypeCreateContext context)
        //{
        //    return base.VisitETypeCreate(context);
        //}

        public override IAstNode VisitEVarname([NotNull] FifthParser.EVarnameContext context)
            => new IdentifierExpression {Identifier = (Identifier)Visit(context.var_name())};

        public override IAstNode VisitExplist([NotNull] FifthParser.ExplistContext context)
        {
            var exps = new List<Expression>();
            foreach (var e in context.exp())
            {
                exps.Add((Expression)base.Visit(e));
            }

            return new ExpressionList {Expressions = exps};
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
            return new FifthProgram {Functions = functionDeclarations, Aliases = aliasDeclarations};
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
            return new ParameterDeclarationList {ParameterDeclarations = parameters};
        }

        public override IAstNode VisitFunction_args([NotNull] FifthParser.Function_argsContext context)
        {
            return base.VisitFunction_args(context);
        }

        public override IAstNode VisitFunction_body([NotNull] FifthParser.Function_bodyContext context)
        {
            return VisitExplist(context.explist());
        }

        public override IAstNode VisitFunction_call([NotNull] FifthParser.Function_callContext context)
        {
            return base.VisitFunction_call(context);
        }

        public override IAstNode VisitFunction_declaration([NotNull] FifthParser.Function_declarationContext context)
        {
            var formals = context.function_args().formal_parameters();
            var parameterList = VisitFormal_parameters(formals);
            var body = VisitFunction_body(context.function_body());
            var name = context.function_name().IDENTIFIER().GetText();
            return new FunctionDefinition
            {
                Body = body as ExpressionList,
                ParameterDeclarations = parameterList as ParameterDeclarationList,
                ReturnType = TypeHelpers.LookupType(context.result_type.GetText()),
                Name = name
            };
        }

        public override IAstNode VisitIfElseStmt([NotNull] FifthParser.IfElseStmtContext context)
        {
            return base.VisitIfElseStmt(context);
        }

        public override IAstNode VisitIri([NotNull] FifthParser.IriContext context)
        {
            return base.VisitIri(context);
        }

        public override IAstNode VisitIri_query_param([NotNull] FifthParser.Iri_query_paramContext context)
        {
            return base.VisitIri_query_param(context);
        }

        public override IAstNode VisitModule_import([NotNull] FifthParser.Module_importContext context)
        {
            return base.VisitModule_import(context);
        }

        public override IAstNode VisitModule_name([NotNull] FifthParser.Module_nameContext context)
        {
            return base.VisitModule_name(context);
        }

        public override IAstNode VisitPackagename([NotNull] FifthParser.PackagenameContext context)
        {
            return base.VisitPackagename(context);
        }

        public override IAstNode VisitParameter_declaration([NotNull] FifthParser.Parameter_declarationContext context)
        {
            var type = context.parameter_type().IDENTIFIER().GetText();
            var name = context.parameter_name().IDENTIFIER().GetText();
            return new ParameterDeclaration {ParameterName = name, ParameterType = TypeHelpers.LookupBuiltinType(type)};
        }

        public override IAstNode VisitParameter_name([NotNull] FifthParser.Parameter_nameContext context)
        {
            return base.VisitParameter_name(context);
        }

        public override IAstNode VisitParameter_type([NotNull] FifthParser.Parameter_typeContext context)
        {
            return base.VisitParameter_type(context);
        }

        public override IAstNode VisitTerminal(ITerminalNode node) => base.VisitTerminal(node);

        public override IAstNode VisitType_initialiser([NotNull] FifthParser.Type_initialiserContext context)
        {
            return base.VisitType_initialiser(context);
        }

        public override IAstNode VisitType_name([NotNull] FifthParser.Type_nameContext context)
        {
            return new Identifier {Value = context.IDENTIFIER().GetText()};
        }

        public override IAstNode VisitType_property_init([NotNull] FifthParser.Type_property_initContext context)
        {
            return base.VisitType_property_init(context);
        }

        public override IAstNode VisitVar_name([NotNull] FifthParser.Var_nameContext context)
        {
            return new Identifier {Value = context.IDENTIFIER().GetText()};
        }

        public virtual IAstNode VisitVar_decl([NotNull] FifthParser.Var_declContext context)
        {
            var nameId = base.Visit(context.var_name());
            var typename = context.type_name().GetText();
            var builtinType = TypeHelpers.LookupBuiltinType(typename);

            return new VariableDeclarationStatement
            {
                Expression = default,
                Name = (Identifier)nameId,
                TypeName = typename,
                FifthType = builtinType.GetType() // dodgy
            };
        }


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

        public override IAstNode VisitWithStmt([NotNull] FifthParser.WithStmtContext context)
        {
            return base.VisitWithStmt(context);
        }

        protected override IAstNode AggregateResult(IAstNode aggregate, IAstNode nextResult) =>
            base.AggregateResult(aggregate, nextResult);

        protected override bool ShouldVisitNextChild(IRuleNode node, IAstNode currentResult) =>
            base.ShouldVisitNextChild(node, currentResult);
    }
}
