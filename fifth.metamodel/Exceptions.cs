namespace Fifth
{
    [System.Serializable]
    public class Exception : System.Exception
    {
        public Exception()
        {
        }

        public Exception(string message) : base(message)
        {
        }

        public Exception(string message, Exception inner) : base(message, inner)
        {
        }

        protected Exception(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class RuntimeException : Exception
    {
        public RuntimeException()
        {
        }

        public RuntimeException(string message) : base(message)
        {
        }

        public RuntimeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RuntimeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class InvalidVariableReferenceException : RuntimeException
    {
        public InvalidVariableReferenceException()
        {
        }

        public InvalidVariableReferenceException(string message) : base(message)
        {
        }

        public InvalidVariableReferenceException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidVariableReferenceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class TypeCheckingException : Fifth.Exception
    {
        public TypeCheckingException()
        {
        }

        public TypeCheckingException(string message) : base(message)
        {
        }

        public TypeCheckingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TypeCheckingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
