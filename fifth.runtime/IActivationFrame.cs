namespace Fifth.Runtime
{
    public interface IActivationFrame
    {
        Environment Environment { get; set; }
        IKnowledgeGraph KnowledgeGraph { get; set; }
        ActivationFrame ParentFrame { get; set; }
        FuncStack Stack { get; set; }

        ActivationFrame CreateChildFrame();
    }
}
