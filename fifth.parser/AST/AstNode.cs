namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;

    public abstract class AstNode : IAstNode
    {
        private readonly IDictionary<string, object> annotations = new Dictionary<string, object>();

        public int Column { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }

        public object this[string index]
        {
            get
            {
                if (annotations.TryGetValue(index, out var result))
                {
                    return result;
                }
                return default;
            }
            set => annotations[index] = value;
        }

        public abstract void Accept(IAstVisitor visitor);
    }
}
