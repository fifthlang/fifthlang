namespace Fifth.Tests
{
    using System;
    using Fifth.PrimitiveTypes;
    using FluentAssertions;
    using NUnit.Framework;
    using TypeSystem;

    [TestFixture, Category("Helper Code"), Category("Type Checking")]
    public class TypeHelperTests
    {

        [Test]
        public void TestLoadsBuiltinOperations()
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes().Should().BeTrue();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        }

        [TestCase("char", typeof(PrimitiveChar))]
        [TestCase("string", typeof(PrimitiveString))]
        [TestCase("int", typeof(PrimitiveInteger))]
        [TestCase("float", typeof(PrimitiveFloat))]
        public void TestCanGetPrimitiveType(string typename, Type expectedType)
        {
            var fifthType = TypeHelpers.LookupBuiltinType(typename);
            _ = fifthType.Should().NotBeNull().And.BeOfType(expectedType);
        }
    }
}
