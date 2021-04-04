namespace Fifth.AST
{
    using System;
    using TypeSystem;
    using Visitors;

    public class FunctionDefinition : ScopeAstNode
    {
        public ExpressionList Body { get; set; }
        public string Typename { get; }
        public string Name { get; set; }
        public bool IsEntryPoint { get; set; }
        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public TypeId ReturnType => TypeId;

        public override void Accept(IAstVisitor visitor)
        {
            visitor.EnterFunctionDefinition(this);
            ParameterDeclarations.Accept(visitor);
            Body.Accept(visitor);
            visitor.LeaveFunctionDefinition(this);
        }

        public FunctionDefinition(string name, ParameterDeclarationList parameterDeclarations, ExpressionList body,
            string typename, TypeId fifthType) : base(fifthType)
        {
            Name = name;
            ParameterDeclarations = parameterDeclarations;
            Body = body;
            Typename = typename;
            IsEntryPoint = name.Equals("main", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
