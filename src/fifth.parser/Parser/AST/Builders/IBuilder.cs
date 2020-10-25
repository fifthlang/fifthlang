namespace Fifth.AST.Builders
{
    interface IBuilder<TypeBuilt>
    {
        TypeBuilt Build();
        bool IsValid();
    }
}
