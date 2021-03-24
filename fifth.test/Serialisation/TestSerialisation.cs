#pragma warning disable IDE0058 // Expression value is never used

namespace Fifth.Test.Serialisation
{
    using System.IO;
    using Fifth.Serialisation;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture, Category("Serialisation")]
    public class TestSerialisation
    {
        [Test]
        public void TestCanRoundtripAFunctionTable()
        {
            var table = new FunctionTable();
            table[1u] = new FunctionTableEntry(1u, FunctionTableEntryType.ILOffset, "eric".ToCharArray(), 212763u);
            table[2u] = new FunctionTableEntry(2u, FunctionTableEntryType.ILOffset, "ernie".ToCharArray(), 4663635u);
            table[3u] = new FunctionTableEntry(3u, FunctionTableEntryType.ILOffset, "little".ToCharArray(), 35631u);
            table[4u] = new FunctionTableEntry(3u, FunctionTableEntryType.ILOffset, "large".ToCharArray(), 465421324u);
            var buf = table.Serialise();
            buf.Should().NotBeNull().And.NotBeEmpty();
            var actual = buf.Deserialise<FunctionTable>();
            actual.Should().NotBeNull();
            actual.Equals(table).Should().BeTrue();
        }

        [Test]
        public void TestCanRoundtripAFunctionTableEntryUsingHelperFunctions()
        {
            var expected = new FunctionTableEntry(1, FunctionTableEntryType.ILOffset, "eric".ToCharArray(), 212763);
            var buf = expected.Serialise();
            buf.Should().NotBeNull().And.NotBeEmpty();
            var actual = buf.Deserialise<FunctionTableEntry>();
            actual.Should().NotBeNull();
            actual.Equals(expected).Should().BeTrue();
        }

        [Test]
        public void TestCanRoundtripAFunctionTableEntryUsingStreams()
        {
            var expected = new FunctionTableEntry(1, FunctionTableEntryType.ILOffset, "eric".ToCharArray(), 212763);
            byte[] buf;
            using (var s = new MemoryStream())
            {
                s.Serialise(expected);
                buf = s.ReadToEnd();
                buf.Should().NotBeNull().And.NotBeEmpty();
            }
            using (var s = new MemoryStream(buf, false))
            {
                var actual = s.Deserialise<FunctionTableEntry>();
                actual.Should().NotBeNull();
                actual.Equals(expected).Should().BeTrue();
            }
        }
    }
}

#pragma warning restore IDE0058 // Expression value is never used
