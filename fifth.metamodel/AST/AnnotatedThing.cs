namespace Fifth.AST
{
    using System.Collections.Generic;

    public abstract class AnnotatedThing : IAnnotated
    {
        private readonly IDictionary<string, object> annotations = new Dictionary<string, object>();
        public IDictionary<string, object> Annotations => annotations;

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
            set
            {
                annotations[index] = value;
            }
        }

        public T CopyAnnotationsInto<T>(T thing)
            where T : AnnotatedThing
        {
            foreach (var item in annotations)
            {
                thing[item.Key] = item.Value;
            }
            return thing;
        }

        public bool HasAnnotation(string key)
        {
            return annotations.ContainsKey(key);
        }

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
