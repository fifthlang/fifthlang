namespace Fifth.Runtime
{
    public class KnowledgeGraph : IKnowledgeGraph
    {
        public KnowledgeGraph(KnowledgeGraph parent)
            => ParentGraph = parent;

        public IKnowledgeGraph ParentGraph { get; set; }
    }
}
