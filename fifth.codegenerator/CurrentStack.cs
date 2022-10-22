namespace Fifth.CodeGeneration;

using System.Collections.Generic;

public class CurrentStack<T> : Stack<T>
{
    public T Current => Peek();
    public bool Empty => Count == 0;
}
