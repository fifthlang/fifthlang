using System.Collections.Generic;

namespace fifth
{
    public interface IFuncStack
    {
        bool IsEmpty { get; }
        Stack<FuncWrapper> Stack { get; }

        FuncWrapper Pop();

        void Push(FuncWrapper funcWrapper);
    }
}