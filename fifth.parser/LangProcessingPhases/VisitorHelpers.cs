namespace Fifth.Parser.LangProcessingPhases
{
    using Fifth.AST;

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
