namespace Fifth.Test.CodeGeneration
{
    using System.IO;
    using System.Text;
    using Fifth.CodeGeneration;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class CilEmitterTests
    {
        [Test]
        public void TestCanEmitAssembly()
        {
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, Encoding.UTF8))
            {
                new CilEmitter(sw, null)
                    .Assembly("MyAssembly")
                        .WithClass("MyClass")
                        .Emit()
                    .Emit();
                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                var s = Encoding.UTF8.GetString(ms.ToArray());
                s.Should().NotBeNullOrWhiteSpace().And.ContainAll(".assembly", "MyAssembly", "mscorlib");
            }
        }
    }
}
