namespace Fifth.Runtime
{
    /// <summary>
    ///     A class that encapsulates all the aspects of a runtime scope
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This is the primary class with which you work in the runtime to handle the runtime environment.
    ///     </para>
    ///     <para>
    ///         This brings together the different elements needed to handle the runtime scope: stack,
    ///         environment and knowledge graph. Its key task is lifecycle management around the creation of
    ///         new activation stacks. It is meant to wire together the environment to allow the resolution
    ///         of identifiers in external scopes and to handle the transfer of facts from this scope to the
    ///         parent knowledge graph.
    ///     </para>
    /// </remarks>
    public class ActivationFrame : IActivationFrame
    {
        public ActivationFrame() : this(
            new Environment(null),
            new KnowledgeGraph(null),
            new ActivationStack(),
            null)
        {
        }

        private ActivationFrame(Environment environment,
            IKnowledgeGraph knowledgeGraph,
            ActivationStack stack,
            ActivationFrame parentFrame)
        {
            Environment = environment;
            KnowledgeGraph = knowledgeGraph;
            ParentFrame = parentFrame;
            Stack = stack;
        }

        // environment
        public Environment Environment { get; set; }

        // knowledge graph
        public IKnowledgeGraph KnowledgeGraph { get; set; }

        public ActivationFrame ParentFrame { get; set; }

        // activation stack
        public ActivationStack Stack { get; set; }

        public ActivationFrame CreateChildFrame()
        {
            var result = new ActivationFrame {ParentFrame = this};
            result.Environment.Parent = Environment;
            result.KnowledgeGraph.ParentGraph = KnowledgeGraph;
            return result;
        }

        public bool ReturnResultToParentFrame()
        {
            if (ParentFrame != null && !Stack.IsEmpty)
            {
                ParentFrame?.Stack.Push(Stack.Pop());
                return true;
            }
            return false;
        }
    }
}
