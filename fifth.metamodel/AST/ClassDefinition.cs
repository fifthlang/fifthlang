namespace Fifth.AST
{
    public partial class ClassDefinition
    {
        public TypeId TypeId { get; set; }

        public void RemoveFunction(IFunctionDefinition fd)
            => Functions.Remove(fd);

        public void AddFunction(IFunctionDefinition fd)
            => Functions.Add(fd);
    }
}
