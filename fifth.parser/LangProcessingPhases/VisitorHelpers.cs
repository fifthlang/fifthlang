namespace Fifth.Parser.LangProcessingPhases
{
    using Antlr4.Runtime;
    using AST;

    public static class VisitorHelpers
    {
        public static T CaptureLocation<T>(this T n, IToken t)
        where T:IFromSourceFile
        {
            if (n == null || t == null)
            {
                return n;
            }
            n.Column = t.Column;
            n.Line = t.Line;
            n.Filename = t.TokenSource.SourceName;
            n.OriginalText = t.Text;
            return n;
        }
    }
}
