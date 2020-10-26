namespace Fifth.Runtime
{
    public interface IDispatcher
    {
        IFuncStack Stack { get; }

        void Dispatch();
    }
}
