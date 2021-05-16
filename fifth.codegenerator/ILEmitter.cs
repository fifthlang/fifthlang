namespace Fifth.CodeGeneration
{
    using System.Collections.Generic;
    using System.IO;

    public interface ICilEmitter
    {
        public ICilEmitter ParentEmitter { get; set; }
        ICilEmitter Emit();
    }

    public abstract class BaseCilEmitter : ICilEmitter
    {
        public BaseCilEmitter(StreamWriter sw, ICilEmitter parentEmitter)
        {
            SW = sw;
            ParentEmitter = parentEmitter;
        }

        protected StreamWriter SW { get; }

        public ICilEmitter ParentEmitter { get; set; }
        public abstract ICilEmitter Emit();

        protected void w(string s)
            => SW.WriteLine(s);
    }

    public class CilEmitter : BaseCilEmitter
    {
        public CilEmitter(StreamWriter sw, ICilEmitter parentEmitter) : base(sw, parentEmitter)
        {
        }

        public AssemblyEmitter Assembly(string name)
            => new(SW, name);

        public override ICilEmitter Emit()
            // nothing to do in this emitter
            => ParentEmitter;
    }

    public class ClassEmitter : BaseCilEmitter
    {
        public ClassEmitter(StreamWriter sw, ICilEmitter parent) : base(sw, parent)
        {
        }

        public string Name { get; set; }

        public override ICilEmitter Emit()
        {
            w($".class public  {Name} extends [System.Runtime]System.Object {{");
            return this;
        }

        public ClassEmitter WithName(string name)
        {
            Name = name;
            return this;
        }
    }

    public class AssemblyEmitter : BaseCilEmitter
    {
        private const string ClrV5PublicKey = "B0 3F 5F 7F 11 D5 0A 3A";
        private const string ClrV5Version = "5:0:0:0";

        public AssemblyEmitter(StreamWriter sw, string name) : base(sw, null)
        {
            Name = name;
            WithReference("mscorlib", ClrV5PublicKey, ClrV5Version);
            WithReference("System", ClrV5PublicKey, ClrV5Version);
            WithReference("System.Runtime", ClrV5PublicKey, ClrV5Version);
            WithReference("System.Console", ClrV5PublicKey, ClrV5Version);
        }

        public List<AssemblyReference> AssemblyReferences { get; } = new();
        public string Name { get; }

        public override ICilEmitter Emit()
        {
            foreach (var reference in AssemblyReferences)
            {
                w(
                    $".assembly extern {reference.Name} {{.publickeytoken = ( {reference.PublicKeyToken} ) .ver {reference.Version} }}");
            }

            w($".assembly {Name} {{ }}");
            SW.WriteLine();
            return ParentEmitter;
        }

        public ClassEmitter WithClass(string name)
            => new ClassEmitter(SW, this).WithName(name);

        public AssemblyEmitter WithReference(string name, string pubKey = null, string ver = null)
        {
            AssemblyReferences.Add(new AssemblyReference(name, pubKey, ver));
            return this;
        }

        public record AssemblyReference(string Name, string PublicKeyToken, string Version);
    }
}
