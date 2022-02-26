namespace Fifth.AST
{
    using System.IO;
    using Visitors;

    public abstract class AstNode : AnnotatedThing, IAstNode
    {
        protected AstNode() => ParentNode = default;

        protected AstNode(AstNode parentNode) => ParentNode = parentNode;

        public int Column { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }
        public string OriginalText { get; set; }
        public AstNode ParentNode { get; set; }

        public abstract void Accept(IAstVisitor visitor);
    }
}
