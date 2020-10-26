using System.Collections.Generic;

namespace Fifth.AST.Builders
{
    public class ParameterListBuilder
    {
        public ParameterListBuilder() => this.ParameterDeclarations = new List<ParameterDeclaration>();

        public List<ParameterDeclaration> ParameterDeclarations { get; private set; }

        public static ParameterListBuilder Start() => new ParameterListBuilder();

        public ParameterDeclarationList Build() => new ParameterDeclarationList
        {
            ParameterDeclarations = this.ParameterDeclarations
        };

        public ParameterListBuilder WithParameter(ParameterDeclaration parameterDeclaration)
        {
            this.ParameterDeclarations.Add(parameterDeclaration);
            return this;
        }
    }
}