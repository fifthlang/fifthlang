namespace Fifth.AST
{
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

        public T CopyMetadataInto<T>(T node)
            where T : AstNode
        {
            // copy properties into node
            node.ParentNode = ParentNode;
            node.Column = Column;
            node.Filename = Filename;
            node.Line = Line;
            node.OriginalText = OriginalText;
            return CopyAnnotationsInto(node);
        }
    }
}
