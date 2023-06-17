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
        public string Namespace
        {
            get
            {
                return Definition.Namespace;
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
