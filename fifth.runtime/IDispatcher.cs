namespace Fifth.Runtime
{
    public interface IDispatcher
    {
        ActivationFrame Frame { get; set; }

        void Dispatch();

        object Resolve();
        T Resolve<T>() where T : class;
        void DispatchWhileNotEmpty();
        void DispatchWhileOperationIsAtTopOfStack();
    }
}
