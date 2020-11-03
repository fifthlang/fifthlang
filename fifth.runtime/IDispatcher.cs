namespace Fifth.Runtime
{
    public interface IDispatcher
    {
        ActivationFrame Frame { get; }

        void Dispatch();

        object Resolve();
    }
}
