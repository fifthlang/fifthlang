namespace Fifth.AST
{
    public partial class FifthProgram
    {
        public string TargetAssemblyFileName { get; set; }
        public void RemoveFunction(IFunctionDefinition fd)
        {
            Functions.Remove(fd);
        }

        public void AddFunction(IFunctionDefinition fd)
        {
            Functions.Add(fd);
        }
    }
}
