﻿using NUnit.Framework;
using fifth.VirtualMachine;
using FluentAssertions;
using fifth.VirtualMachine.PrimitiveTypes;
using System;
using System.Collections.Generic;

namespace fifth.VirtualMachine.Tests
{
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
            (IFifthType t, object o) = a;
            var sut = new fifth.VirtualMachine.Environment(null);
            sut.Should().NotBeNull();
            sut.IsEmpty.Should().BeTrue();
            sut[new VariableReference("hello")] = new VariableAssignment(t, o);
            sut.IsEmpty.Should().BeFalse();
            IVariableAssignment x = sut[new VariableReference("hello")];
            x.Value.Should().Be(o);
            x.FifthType.Should().Be(t);
        }
    }
}