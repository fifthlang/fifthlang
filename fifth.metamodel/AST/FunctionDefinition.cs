namespace Fifth.AST
{
    using System;
    using Visitors;

    public class FunctionDefinition : ScopeAstNode
    {
        public FunctionDefinition(string name, ParameterDeclarationList parameterDeclarations, Block body,
            string typename, TypeId fifthType)
        {
            Name = name;
            ParameterDeclarations = parameterDeclarations;
            Body = body;
            Typename = typename;
            IsEntryPoint = name.Equals("main", StringComparison.InvariantCultureIgnoreCase);
            ReturnType = fifthType;
        }

        public Block Body { get; set; }
        public string Typename { get; }
        public string Name { get; set; }
        public bool IsEntryPoint { get; set; }
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public TypeId ReturnType { get; set; }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFunctionDefinition(this);
            ParameterDeclarations.Accept(visitor);
            Body.Accept(visitor);
            visitor.LeaveFunctionDefinition(this);
        }
    }
}
