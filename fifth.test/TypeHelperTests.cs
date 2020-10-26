namespace Fifth.Test
{
    using System;
    using Fifth.PrimitiveTypes;
    using Fifth.Runtime;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class TypeHelperTests
    {
        [Test]
        public void TestBuiltinTypesAreFound()
        {
            var builtins = TypeHelpers.TypesHavingAttribute<TypeTraitsAttribute, IFifthType>();
            builtins.Should().NotBeEmpty();
        }

        [TestCase("char", typeof(PrimitiveChar))]
        [TestCase("string", typeof(PrimitiveString))]
        [TestCase("int", typeof(PrimitiveInteger))]
        [TestCase("float", typeof(PrimitiveFloat))]
        public void TestCanGetPrimitiveType(string typename, Type expectedType)
        {
            var fifthType = TypeHelpers.LookupBuiltinType(typename);
            fifthType.Should().NotBeNull();
        }
    }
}
