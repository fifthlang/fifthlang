namespace Fifth.AST.Builders
{
    using System.Collections.Generic;

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
}
