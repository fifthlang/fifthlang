namespace Fifth.VirtualMachine
{
    public interface IDispatcher
    {
        IFuncStack Stack { get; }

        void Dispatch();
    }
}