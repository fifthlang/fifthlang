using System.Collections.Generic;
using System.Linq;

namespace Fifth.AST.Builders
{
    /// <summary>
    /// A fluent API for building the AST node definitions of Functions
    /// </summary>
    public class ProgramBuilder : IBuilder<ProgramDefinition>
    {
        public ProgramBuilder() => this.FunctionDefinitions = new List<FunctionDefinition>();

        public List<FunctionDefinition> FunctionDefinitions { get; set; }

        public static ProgramBuilder Start() => new ProgramBuilder();

        public ProgramDefinition Build()
        {
            if (!this.IsValid())
                throw new System.Exception("Invalid program definition");
            return new ProgramDefinition
            {
                FunctionDefinitions = FunctionDefinitions
            };
        }

        public bool IsValid() =>
            // there must be at least one function defined, called 'main'
            this.FunctionDefinitions.Any(fd => fd.Name == "main");

        public ProgramBuilder WithFunction(FunctionDefinition functionDefinition)
        {
            this.FunctionDefinitions.Add(functionDefinition);
            return this;
        }
    }
}
