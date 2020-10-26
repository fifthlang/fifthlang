namespace Fifth.AST.Builders
{
    internal interface IBuilder<TypeBuilt>
    {
        TypeBuilt Build();
        bool IsValid();
    }
}
