namespace Fifth.Metamodel.AST2;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using fifth.metamodel.metadata.il;

public abstract class BaseBuilder<T, TModel> : IBuilder<TModel>
    where T : IBuilder<TModel>, new()
{
    public TModel Model { get; set; }

    [DebuggerStepThrough]
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
    public TModel New()
    {
        return Model;
    }

    public static string Join<TItem>(IEnumerable<TItem> items, string sep, Func<TItem, string> renderer)
    {
        var renderedItems = from i in items
                            select renderer(i);
        return String.Join(sep, items);
    }
}
