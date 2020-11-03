namespace Fifth.Serialisation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// A builder for serialisation and deserialisation of the IL related to an ActivationScope
    /// </summary>
    /// <remarks>
    /// <para>
    /// An ActivationScope is the thing that at runtime ties together the different aspects of the
    /// runtime state - the stack, the environment and the knowledge graph. The
    /// ActivationScopeBuilder provides the IL generation capabilities needed for creation of a
    /// scope (that is, a block of code, whether anonymous or not, that can be called into from
    /// somewhere else, and returned from. A subroutine, if you like).
    /// </para>
    /// </remarks>
    public class ActivationScopeBuilder
    {
        private readonly string name;
        private readonly SerialisationBuilder serialisationBuilder;

        private ActivationScopeBuilder(string name, SerialisationBuilder serialisationBuilder)
        {
            this.name = name;
            this.serialisationBuilder = serialisationBuilder;
        }

        public static ActivationScopeBuilder Start(string name, SerialisationBuilder serialisationBuilder)
            => new ActivationScopeBuilder(name, serialisationBuilder);

        public SerialisationBuilder Build()
        {
            return this.serialisationBuilder;
        }
    }

    /// <summary>
    /// A builder for serialisation and deserialisation of the IL related to a function
    /// </summary>
    /// <remarks>
    /// <para>
    /// For the most part a function is not so dissimilar to a subroutine. I has the need to
    /// marshall params into the activation scope and the result value, if any, back out.
    /// </para>
    /// <para>
    /// I suspect that the only real distinction is the argument handling, and the rest can be
    /// passed off to an ActivationScopeBuilder
    /// </para>
    /// </remarks>
    public class FunctionBuilder
    {
        private readonly string name;
        private readonly SerialisationBuilder serialisationBuilder;

        private FunctionBuilder(string name, SerialisationBuilder serialisationBuilder)
        {
            this.name = name;
            this.serialisationBuilder = serialisationBuilder;
        }

        public static FunctionBuilder Start(string name, SerialisationBuilder serialisationBuilder)
            => new FunctionBuilder(name, serialisationBuilder);

        public SerialisationBuilder Build()
        {
            return this.serialisationBuilder;
        }
    }

    public class SerialisationBuilder
    {
        private readonly Dictionary<uint, FunctionTableEntry> functions = new Dictionary<uint, FunctionTableEntry>();

        private SerialisationBuilder()
        {
        }

        public static SerialisationBuilder Start() => new SerialisationBuilder();

        public byte[] Build(BinaryWriter binaryWriter)
        {
            throw new NotImplementedException();
        }

        public SerialisationBuilder WithFunctionTableEntry(FunctionTableEntry entry)
        {
            functions[entry.functionIdentifier] = entry;
            return this;
        }

        public FunctionBuilder WithNewFunction(string name)
        {
            return FunctionBuilder.Start(name, this);
        }
    }
}
