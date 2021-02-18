namespace Fifth.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Fifth.Runtime;

    public static class CodeGenerationHelpers
    {
        public static bool Matches(this ActivationStack stack, params StackElement[] elementsToCompareAgainst)
        {
            var elements = stack.Export();
            var elementsFromStack = elements as List<StackElement> ?? elements.ToList();
            if (elementsToCompareAgainst.Length != elementsFromStack.Count())
            {
                return false;
            }

            return elementsFromStack.Zip(elementsToCompareAgainst).All(x => x.First.Equals(x.Second));
        }
    }
}
