namespace Fifth.AST
{
    using Visitors;

    public interface IParameterListItem : IVisitable
    {
        Identifier ParameterName { get; set; }
        string TypeName { get; set; }
    }

    public partial class TypeInitParamDecl
    {
        public Identifier ParameterName { get; set; }

        public string TypeName
        {
            get => Pattern.TypeName;
            set => Pattern.TypeName = value;
        }
    }
}
