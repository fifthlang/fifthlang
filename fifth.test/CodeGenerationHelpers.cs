namespace Fifth.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fifth.Runtime;

    public static class CodeGenerationHelpers
    {
        public static bool Matches(this ActivationStack stack, params StackElement[] elementsToCompareAgainst)
        {
            var elements = stack.Export();
            var elementsFromStack = elements as List<StackElement> ?? elements.ToList();
            elementsToCompareAgainst = elementsToCompareAgainst.Reverse().ToArray();
            return elementsFromStack.SequenceEquals(elementsToCompareAgainst);
        }

        public static bool Matches(this RuntimeFunctionDefinition fd, params StackElement[] elementsToCompareAgainst)
        {
            var elements = fd.Stack.Export();
            var elementsFromStack = elements as List<StackElement> ?? elements.ToList();
            elementsToCompareAgainst = elementsToCompareAgainst.Reverse().ToArray();
            return elementsFromStack.SequenceEquals(elementsToCompareAgainst);
        }

        public static bool SequenceEquals<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
            where T : IEquatable<T>
        {
            if ((seq1 == null && seq2 != null) || (seq2 == null && seq1 != null))
            {
                return false;
            }

            if (ReferenceEquals(seq1, seq2))
            {
                return true;
            }

            if (seq1.Count() != seq2.Count())
            {
                return false;
            }

            var en1 = seq1.GetEnumerator();
            var en2 = seq2.GetEnumerator();

            var hasData = en1.MoveNext();
            _ = en2.MoveNext();

            while (hasData)
            {
                if (!en1.Current.Equals(en2.Current))
                {
                    return false;
                }

                hasData = en1.MoveNext();
                _ = en2.MoveNext();
            }

            return true;
        }
    }
}
