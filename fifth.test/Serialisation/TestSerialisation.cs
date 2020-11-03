namespace Fifth.Test.Serialisation
{
    using System.IO;
    using Fifth.Serialisation;
    using FluentAssertions;
    using FluentAssertions.Common;
    using NUnit.Framework;

    [TestFixture]
    public class TestSerialisation
    {
        [Test]
        public void TestCanRoundtripAFunctionTableEntry()
        {
            var expected = new FunctionTableEntry(1, FunctionTableEntryType.ILOffset, "eric".ToCharArray(), 212763);
            byte[] buf;
            using (var s = new MemoryStream())
            {
                s.Serialise(expected);
                buf = s.ReadToEnd();
                buf.Should().NotBeNull().And.NotBeEmpty();
            }
            var actual = buf.Deserialise<FunctionTableEntry>();
            actual.Should().NotBeNull();
            actual.Equals(expected).Should().BeTrue();
        }
    }
}
