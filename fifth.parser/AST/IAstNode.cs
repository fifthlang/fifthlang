namespace Fifth.AST
{
    using System.Collections;
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;

    public interface IAnnotated
    {
        object this[string index]
        {
            get;
            set;
        }
    }

    public interface IAstNode : IAnnotated, IVisitable
    {
        int Column { get; set; }

        string Filename { get; set; }

        int Line { get; set; }
    }

    public interface IVisitable
    {
        void Accept(IAstVisitor visitor);
    }
}
