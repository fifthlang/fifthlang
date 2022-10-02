namespace Fifth.AST.Builders
{
    public class IfElseBuilder
    {
        private Expression condition;
        private Block elseBlock;
        private Block ifBlock;

        private IfElseBuilder()
        {
        }

        public static IfElseBuilder NewIfElse()
        {
            return new IfElseBuilder();
        }

        public IfElseStatement AsAstNode()
        {
            if (elseBlock == null)
            {
                return new IfElseStatement(ifBlock, new Block(new List<Statement>()), condition);
            }
            return new IfElseStatement(ifBlock, elseBlock, condition);
        }

        public IfElseBuilder WithCondition(Expression condition)
        {
            this.condition = condition;
            return this;
        }

        public IfElseBuilder WithElseBlock(Block block)
        {
            elseBlock = block;
            return this;
        }

        public IfElseBuilder WithIfBlock(Block block)
        {
            ifBlock = block;
            return this;
        }
    }
}
