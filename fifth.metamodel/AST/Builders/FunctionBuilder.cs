namespace Fifth.AST.Builders
{
    using System.Collections.Generic;
    using System.Linq;

    public class BlockBuilder
    {
        private List<Statement> statements = new();

        private BlockBuilder()
        {
        }
        public static BlockBuilder NewBlock()
            => new BlockBuilder();

        public BlockBuilder WithStatement(Statement s)
        {
            statements.Add(s);
            return this;
        }

        public Block AsAstNode()
            => new Block(statements);
    }
    public class IfElseBuilder
    {
        private Expression condition;
        private Block ifBlock;
        private Block elseBlock;

        private IfElseBuilder()
        {
        }
        public static IfElseBuilder NewIfElse()
            => new IfElseBuilder();

        public IfElseBuilder WithCondition(Expression condition)
        {
            this.condition = condition;
            return this;
        }

        public IfElseStatement AsAstNode()
            => new IfElseStatement(ifBlock, elseBlock, condition);

        public IfElseBuilder WithIfBlock(Block block)
        {
            ifBlock = block;
            return this;
        }
    }
    public class FunctionBuilder
    {
        private string name;
        private string returnTypeName;
        private TypeId returnType;
        private List<(string, string)> parameters = new();
        private Block body;
        private AstNode parentNode;

        private FunctionBuilder()
        {
        }

        public static FunctionBuilder NewFunction()
            => new FunctionBuilder();

        public FunctionBuilder Called(string funcName)
        {
            name = funcName;
            return this;
        }

        public FunctionBuilder WithReturnType(string typename)
        {
            returnTypeName = typename;
            return this;
        }

        public FunctionBuilder WithReturnType(TypeId tid)
        {
            returnType = tid;
            return this;
        }

        public FunctionBuilder WithParam(string name, string typename)
        {
            parameters.Add((typename, name));
            return this;
        }

        public FunctionBuilder WithBody(Block body)
        {
            this.body = body;
            return this;
        }

        public IFunctionDefinition AsAstNode()
        {
            var pds = parameters.Select(x => new ParameterDeclaration(new Identifier(x.Item2), x.Item1, null)).Cast<IParameterListItem>().ToList();
            var paramDecls = new ParameterDeclarationList(pds);
            var result = new FunctionDefinition(paramDecls, body, returnTypeName, name, name == "main", returnType);
            result.ParentNode = parentNode;
            return result;
        }

        public FunctionBuilder WithSameParentAs(AstNode ctx)
        {
            this.parentNode = ctx.ParentNode;
            return this;
        }
    }
}
