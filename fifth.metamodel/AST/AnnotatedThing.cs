namespace Fifth.AST
{
    using System.Collections.Generic;


    public abstract class AnnotatedThing : IAnnotated
    {
        private readonly IDictionary<string, object> annotations = new Dictionary<string, object>();

        public object this[string index]
        {
            get
            {
                if (annotations.TryGetValue(index, out var result))
                {
                    return result;
                }

                return default;
            }
            set => annotations[index] = value;
        }

        public bool HasAnnotation(string key)
            => annotations.ContainsKey(key);

        public bool TryGetAnnotation<T>(string name, out T result)
        {
            if (annotations.TryGetValue(name, out var uncastResult))
            {
                result = (T)uncastResult;
                return true;
            }

            result = default;
            return false;
        }
    }
}
