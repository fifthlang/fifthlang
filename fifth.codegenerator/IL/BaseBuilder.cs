namespace Fifth.CodeGeneration.IL;

using System.IO;
using System.Text;

public abstract class BaseBuilder<T, TModel> : IBuilder<TModel>
    where T: IBuilder<TModel>, new()
{
    public TModel Model { get; set; }
    public static T Create(TModel model)
    {
        var builder = new T();
        builder.Model = model;
        return builder;
    }
    public static T Create()
    {
        return new T();
    }
    public abstract string Build();
    public virtual void Build(StreamWriter writer)
    {
        writer.Write(Build());
    }

    public virtual string Build(TModel model)
    {
        return Create(model).Build();
    }

    public TModel New()
    {
        return Model;
    }
    public static string WriteVersion(Version v)
    {
        var sb = new StringBuilder();
        sb.Append(v.Major);
        if (v.Minor.HasValue)
        {
            sb.Append($":{v.Minor}");
        }
        if (v.Build.HasValue)
        {
            sb.Append($":{v.Build}");
        }
        if (v.Patch.HasValue)
        {
            sb.Append($":{v.Patch}");
        }

        return sb.ToString();
    }
}
