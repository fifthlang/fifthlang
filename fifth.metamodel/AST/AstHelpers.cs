namespace Fifth.AST
{
    public static class AstHelpers
    {
        public static T1 HasSameParentAs<T1, T2>(this T1 newAstNode, T2 otherAstNode)
            where T1 : IAstNode
            where T2 : IAstNode
        {
            newAstNode.ParentNode = otherAstNode.ParentNode;
            return newAstNode;
        }

        public static T1 CameFromSameSourceLocation<T1, T2>(this T1 target, T2 source)
            where T1 : IAstNode
            where T2 : IAstNode
        {
            target.Column = source.Column;
            target.Line = source.Line;
            target.Filename = source.Filename;
            target.OriginalText += source.OriginalText; // += because of overloaded functions being combined
            return target;
        }
    }
}
