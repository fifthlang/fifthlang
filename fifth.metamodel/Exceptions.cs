namespace Fifth
{
    using System.Collections.Generic;
    using System.Text;

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
