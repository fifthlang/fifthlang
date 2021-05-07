namespace Fifth.AST
{
    using System.Collections.Generic;
    using Visitors;

    public partial class FunctionDefinition
    {
    }
    public class adfBuiltinFunctionDefinition : ScopeAstNode, IFunctionDefinition
    {
        public adfBuiltinFunctionDefinition(string name, string typename, params (string, string)[] parameters)
        {
            Name = name;
            Typename = typename;
            var list = new List<IParameterListItem>();

            foreach (var (pname, ptypename) in parameters)
            {
                var paramDef = new ParameterDeclaration(new Identifier(pname), ptypename);
                list.Add(paramDef);
            }
                
            var paramDeclList = new ParameterDeclarationList(list);

            ParameterDeclarations = paramDeclList;
            IsEntryPoint = false;
        }
        public override void Accept(IAstVisitor visitor){}

        public ParameterDeclarationList ParameterDeclarations { get; set; }
        public string Typename { get; set; }
        public string Name { get; set; }
        public bool IsEntryPoint { get; set; }
        public TypeId ReturnType { get; set; }
    }

}
