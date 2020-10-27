namespace Fifth.Tests.Runtime
{
    using System;
    using System.Collections.Generic;
    using Fifth.PrimitiveTypes;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture()]
    public class EnvironmentTests
    {
        public static IEnumerable<Tuple<IFifthType, object>> Cases()
        {
            yield return new Tuple<IFifthType, object>(PrimitiveChar.Default, 'a');
            yield return new Tuple<IFifthType, object>(PrimitiveString.Default, "shine on you crazy diamond");
            yield return new Tuple<IFifthType, object>(PrimitiveInteger.Default, 1);
            yield return new Tuple<IFifthType, object>(PrimitiveLong.Default, 1L);
            yield return new Tuple<IFifthType, object>(PrimitiveFloat.Default, 1.0);
            yield return new Tuple<IFifthType, object>(PrimitiveDouble.Default, 1.0D);
        }

        [TestCaseSource("Cases")]
        public void EnvironmentTest(Tuple<IFifthType, object> a)
        {
            (var t, var o) = a;
            var sut = new Fifth.Runtime.Environment(null);
            _ = sut.Should().NotBeNull();
            _ = sut.IsEmpty.Should().BeTrue();
            sut[new VariableReference("hello")] = new VariableAssignment(t, o);
            _ = sut.IsEmpty.Should().BeFalse();
            var x = sut[new VariableReference("hello")];
            _ = x.Value.Should().Be(o);
            _ = x.FifthType.Should().Be(t);
        }
    }
}
