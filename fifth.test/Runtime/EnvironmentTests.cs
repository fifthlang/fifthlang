#pragma warning disable IDE0058 // Expression value is never used

namespace Fifth.Tests.Runtime
{
    using System;
    using System.Collections.Generic;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;
    using PrimitiveTypes;
    using TypeSystem;
    using Environment = Fifth.Runtime.Environment;

    [TestFixture(Category = "Environment")]
    public class EnvironmentTests
    {
        public static IEnumerable<Tuple<IType, object>> Cases()
        {
            yield return new Tuple<IType, object>(PrimitiveChar.Default, 'a');
            yield return new Tuple<IType, object>(PrimitiveString.Default, "shine on you crazy diamond");
            yield return new Tuple<IType, object>(PrimitiveInteger.Default, 1);
            yield return new Tuple<IType, object>(PrimitiveLong.Default, 1L);
            yield return new Tuple<IType, object>(PrimitiveFloat.Default, 1.0);
            yield return new Tuple<IType, object>(PrimitiveDouble.Default, 1.0D);
        }

        [TestCaseSource("Cases")]
        public void EnvironmentTest(Tuple<IType, object> a)
        {
            var (t, o) = a;
            var sut = new Environment(null);
            _ = sut.Should().NotBeNull();
            _ = sut.IsEmpty.Should().BeTrue();
            sut["hello"] = new ValueObject(t.TypeId, t.Name, o);
            _ = sut.IsEmpty.Should().BeFalse();
            var x = sut["hello"];
            _ = x.GetValueOfValueObject().Should().Be(o);
            _ = x.ValueType.Should().Be(t.TypeId);
        }

        [Test]
        public void CanLoadBuilltinFunctions()
        {
            var e = new Environment(null);
            BuiltinFunctions.loadBuiltins(e);
            e.Should().NotBeNull();
        }
    }
}

#pragma warning restore IDE0058 // Expression value is never used
