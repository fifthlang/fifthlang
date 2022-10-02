namespace Fifth.TypeSystem
{
    using AST;

    public class UserDefinedType : IType
    {
        public UserDefinedType(ClassDefinition definition)
        {
            Definition = definition;
        }

        public ClassDefinition Definition { get; set; }

        public string Name
        {
            get
            {
                return Definition.Name;
            }
        }

        public TypeId TypeId
        {
            get
            {
                return Definition.TypeId;
            }
            set
            {
                Definition.TypeId = value;
            }
        }
    }
}
