// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006
#pragma warning disable CS8625
namespace Fifth.AST.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public interface INodeBuilder { }

public abstract class BaseNodeBuilder<T, T2> : INodeBuilder
where T2 : AstNode
where T : class
{
    protected int column;
    protected string filename  = default;
    protected int line ;
    protected string originalText  = default;
    protected AstNode parentNode = default ;
    protected Dictionary<string, object> annotations = new();

    public T WithSameMetadataAs(T2 node){
        parentNode = node.ParentNode;
        column = node.Column;
        filename = node.Filename;
        line = node.Line;
        originalText = node.OriginalText;
        annotations = new Dictionary<string, object>(node.Annotations);
        return this as T;
    }

    public void CopyMetadataInto(T2 node)
    {
        node.ParentNode = parentNode;
        node.Column = column;
        node.Filename = filename;
        node.Line = line;
        node.OriginalText = originalText;
        foreach (var item in annotations)
        {
            node[item.Key] = item.Value;
        }
    }

}
