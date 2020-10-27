namespace Fifth.AST
{
    using System.Collections.Generic;
    using Fifth.Parser.LangProcessingPhases;

    public abstract class AstNode : BaseAnnotated, IAstNode
    {
        public int Column { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }

        public abstract void Accept(IAstVisitor visitor);
    }

    public abstract class BaseAnnotated : IAnnotated
    {
        private readonly IDictionary<string, object> annotations = new Dictionary<string, object>();

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

        public bool HasAnnotation(string key)
        => annotations.ContainsKey(key);
    }
}
