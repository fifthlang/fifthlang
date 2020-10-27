namespace Fifth.AST
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IAstNode : IAnnotated, IVisitable, IFromSourceFile
    {
    }
}
