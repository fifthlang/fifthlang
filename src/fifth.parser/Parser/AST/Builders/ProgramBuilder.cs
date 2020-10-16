using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.parser.Parser.AST.Builders
{
    /// <summary>
    /// A fluent API for building the AST node definitions of Functions
    /// </summary>
    public class ProgramBuilder
    {
        public ProgramBuilder()
        {
            this.FunctionDefinitions = new List<FunctionDefinition>();
        }

        public List<FunctionDefinition> FunctionDefinitions { get; set; }

        public static ProgramBuilder Start()
        {
            return new ProgramBuilder();
        }

        public ProgramDefinition Build()
        {
            return new ProgramDefinition
            {
                FunctionDefinitions = FunctionDefinitions
            };
        }

        public ProgramBuilder WithFunction(FunctionDefinition functionDefinition)
        {
            this.FunctionDefinitions.Add(functionDefinition);
            return this;
        }
    }
}