namespace Fifth.AST
{
    public interface IAnnotated
    {
        object this[string index]
        {
            get;
            set;
        }

        bool HasAnnotation(string key);

        bool TryGetAnnotation<T>(string name, out T result);
    }
}