namespace Fifth.AST
{
    using System.Collections.Generic;
    using Symbols;
    using TypeSystem;
    using Visitors;

    public interface ITypedAstNode
    {
        TypeId TypeId { get; set; }
    }


    public interface IFromSourceFile
    {
        int Column { get; set; }

        string Filename { get; set; }

        int Line { get; set; }
    }

    public interface IVisitable
    {
        void Accept(IAstVisitor visitor);
    }

    public interface IAstNode : IAnnotated, IVisitable, IFromSourceFile
    {
    }

    public interface IAnnotated
    {
        object this[string index]
        {
            get;
            set;
        }

        bool HasAnnotation(string key);

        bool TryGetAnnotation<T>(string name, out T result);
    }

    public abstract class AnnotatedThing : IAnnotated
    {
        private readonly IDictionary<string, object> annotations = new Dictionary<string, object>();

        public object this[string index]
        {
            get
            {
                if (annotations.TryGetValue(index, out var result))
                {
                    return result;
                }

                return default;
            }
            set => annotations[index] = value;
        }

        public bool HasAnnotation(string key)
            => annotations.ContainsKey(key);

        public bool TryGetAnnotation<T>(string name, out T result)
        {
            if (annotations.TryGetValue(name, out var uncastResult))
            {
                result = (T)uncastResult;
                return true;
            }

            result = default;
            return false;
        }
    }

    public abstract class AstNode : AnnotatedThing, IAstNode
    {
        protected AstNode() => ParentNode = default;

        protected AstNode(AstNode parentNode) => ParentNode = parentNode;

        public AstNode ParentNode { get; set; }
        public int Column { get; set; }
        public string Filename { get; set; }
        public int Line { get; set; }

        public abstract void Accept(IAstVisitor visitor);
    }

    public abstract class TypedAstNode : AstNode, ITypedAstNode
    {
        protected TypedAstNode()
        {
        }

        protected TypedAstNode(TypeId fifthType) => TypeId = fifthType;

        public TypeId TypeId { get; set; }
    }

    public abstract class ScopeAstNode : AstNode, IScope
    {
        protected ScopeAstNode()
        {
            SymbolTable = new SymbolTable();
            EnclosingScope = default;
        }

        public IScope EnclosingScope { get; set; }
        public ISymbolTable SymbolTable { get; set; }

        public void Declare(string name, SymbolKind kind, IAstNode ctx, params (string, object)[] properties)
        {
            var symTabEntry = new SymTabEntry {Name = name, SymbolKind = kind, Line = Line, Context = ctx};
            foreach (var (x, y) in properties)
            {
                symTabEntry[x] = y;
            }

            SymbolTable[name] = symTabEntry;
        }

        public bool TryResolve(string name, out ISymbolTableEntry result)
        {
            result = null;
            var tmp = SymbolTable.Resolve(name);
            if (tmp != null)
            {
                result = tmp;
                return true;
            }

            return this?.ParentNode.NearestScope()?.TryResolve(name, out result) ?? false;
        }

        public ISymbolTableEntry Resolve(string name)
        {
            if (TryResolve(name, out var ste))
            {
                return ste;
            }

            throw new CompilationException($"Unable to resolve symbol {name}");
        }
    }

    public abstract class Statement : AstNode
    {
    }
}
