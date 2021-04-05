namespace Fifth.Parser.LangProcessingPhases
{
    using AST;
    using AST.Visitors;

    public static class VisitorHelpers
    {
        public static void Accept(this IAstVisitor visitor, IAstNode node)
        {
            if (node != null)
            {
                node.Accept(visitor);
            }
        }
    }
}
