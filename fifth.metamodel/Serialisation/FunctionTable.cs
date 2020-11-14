namespace Fifth.Serialisation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class FunctionTable
    {
        private readonly Dictionary<uint, FunctionTableEntry> table = new Dictionary<uint, FunctionTableEntry>();

        public FunctionTableEntry this[uint index]
        {
            get => table[index];
            set => table[index] = value;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            // See the full list of guidelines at http://go.microsoft.com/fwlink/?LinkID=85237 and
            // also the guidance for operator== at http://go.microsoft.com/fwlink/?LinkId=85238

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var rhs = (FunctionTable)obj;

            return table.SequenceEqual(rhs.table);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// The representation in memory of a function, in a form that can be pushed to and from disk
    /// and memory.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The point of this class is to provide a place to store a REPRESENTATION of a function at
    /// runtime so that it can be loaded and executed when the function is invoked at runtime. That
    /// means, not only the generated IL of the function, but also the related AST and type
    /// information and annotations needed to make sense of the data flowing into and out of a
    /// function scope.
    /// </para>
    /// <para>
    /// That is, we need to track: the IL of the function, the AST from which the IL was generated,
    /// the parmaters and return types of the function and so on.
    /// </para>
    /// <para>
    /// WHAT THIS IS NOT is a representation of the runtime state of a function invocation.
    /// Therefore it does not store information about the values of actual parameters, captured
    /// variables, runtime stacks, or links to sub function invocations etc,
    /// </para>
    /// </remarks>
    [Serializable]
    public class RuntimeFunction
    {
    }
}
