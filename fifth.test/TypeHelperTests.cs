namespace Fifth.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;
    using PrimitiveTypes;
    using TypeSystem;

    [TestFixture]
    [Category("Helper Code")]
    [Category("Type Checking")]
    public class TypeHelperTests
    {
        [Test]
        public void TestLoadsBuiltinOperations()
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes().Should().BeTrue();
            InbuiltOperatorRegistry.DefaultRegistry.LoadBuiltinOperators();
        }

        [Test]
        public void TestCanGetPrimitiveType()
        {
            TypeRegistry.DefaultRegistry.LoadPrimitiveTypes().Should().BeTrue();
            var cases = new Dictionary<string, IType>
            {
                {"char", PrimitiveChar.Default},
                {"string", PrimitiveString.Default},
                {"int", PrimitiveInteger.Default},
                {"float", PrimitiveFloat.Default}
            };
            foreach (var kvp in cases)
            {
                var typeId = TypeHelpers.LookupBuiltinType(kvp.Key);
                _ = typeId.Should().NotBeNull().And.Be(kvp.Value.TypeId);
            }
        }
    }
}
