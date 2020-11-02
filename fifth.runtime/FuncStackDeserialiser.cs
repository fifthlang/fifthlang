using System.Text;

namespace Fifth
{
    public interface IFuncStackDeserialiser
    {
        IFuncStack Deserialise(string s);
    }

    public interface IFuncStackSerialiser
    {
        string Serialise(IFuncStack stack);
    }

    public class FuncStackDeserialiser : IFuncStackDeserialiser
    {
        public IFuncStack Deserialise(string s)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FuncStackSerialiser : IFuncStackSerialiser
    {
        public string Serialise(IFuncStack stack)
        {
            var sb = new StringBuilder();
            while (!stack.IsEmpty)
            {
                sb.Append(stack.Pop().ToString());
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
