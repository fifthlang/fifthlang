namespace Fifth.CodeGeneration.ILGeneration;

using System.IO;

public interface IBuilder<TModel>
{
    TModel Model { get; set; }
    string Build();
    string Build(TModel model);
    void Build(StreamWriter writer);
}
