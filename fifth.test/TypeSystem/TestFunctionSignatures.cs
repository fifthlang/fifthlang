namespace Fifth.Test.TypeSystem
{
    using Fifth.TypeSystem;
    using NUnit.Framework;

    [TestFixture]
    public class TestFunctionSignatures
    {
        [Test]
        public void TestTypeIdsAreEquatable()
        {
            var tid1 = new TypeId(1);
            var tida = new TypeId(1);
            var tid2 = new TypeId(2);
            var tidb = new TypeId(2);
            Assert.That(tid1.Equals(tida));
            Assert.That(tid2.Equals(tidb));
            Assert.That(!tid1.Equals(tid2));
            Assert.That(!tid2.Equals(tida));
        }

        [Test]
        public void CanEquateSameSigs()
        {
            var intTid = new TypeId(1);
            var longTid = new TypeId(2);
            var stringTid = new TypeId(3);
            var sig1 = new FunctionSignature("foo", stringTid, new[] {intTid, longTid});

            var int2Tid = new TypeId(1);
            var long2Tid = new TypeId(2);
            var string2Tid = new TypeId(3);
            var sig2 = new FunctionSignature("foo", string2Tid, new[] {int2Tid, long2Tid});

            var sig3 = new FunctionSignature("bar", string2Tid, new[] {int2Tid, long2Tid});
            var sig4 = new FunctionSignature("foo", intTid, new[] {int2Tid, long2Tid});

            Assert.That(sig1.Equals(sig2));
            Assert.That(sig2.Equals(sig1));

            Assert.That(!sig1.Equals(sig3));
            Assert.That(!sig1.Equals(sig4));
        }

        [Test]
        public void CanEquateWithSigEq()
        {
            var intTid = new TypeId(1);
            var longTid = new TypeId(2);
            var stringTid = new TypeId(3);
            var sig1 = new FunctionSignature("foo", stringTid, new[] {intTid, longTid});

            var int2Tid = new TypeId(1);
            var long2Tid = new TypeId(2);
            var string2Tid = new TypeId(3);
            var sig2 = new FunctionSignature("foo", string2Tid, new[] {int2Tid, long2Tid});

            var sig3 = new FunctionSignature("bar", string2Tid, new[] {int2Tid, long2Tid});
            var sig4 = new FunctionSignature("foo", intTid, new[] {int2Tid, long2Tid});
            var eq = new SignaturesAreEqual();
            Assert.That(eq.Equals(sig1, sig2));
            Assert.That(eq.Equals(sig2, sig1));
            Assert.That(!eq.Equals(sig1,sig3));
            Assert.That(!eq.Equals(sig1,sig4));
        }
    }
}
