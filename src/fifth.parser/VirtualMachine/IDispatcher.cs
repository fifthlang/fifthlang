namespace fifth.VirtualMachine
{
    public interface IDispatcher
    {
        IFuncStack Stack { get; }

        void Dispatch();
    }
}