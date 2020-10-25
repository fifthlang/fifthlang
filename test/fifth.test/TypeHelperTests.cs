using System;
using System.Linq;
using Fifth.VirtualMachine;
using Fifth.VirtualMachine.PrimitiveTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Fifth.Test
{
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
