namespace Fifth.AST
{
    public interface IFromSourceFile
    {
        int Column { get; set; }

        string Filename { get; set; }

        int Line { get; set; }
    }
}
