using NUnit.Framework;

namespace Fifth.Test.CodeGeneration;

using System.Net.WebSockets;
using Fifth.CodeGeneration.ILGeneration;

[TestFixture, Category("CIL")]
public class FluentILGenerationTests
{
    [Test]
    public void CanCreateAssembly()
    {
        var sut = AssemblyBuilder.Create()
                                 .WithName("Foo")
                                 .WithVersion(new Version(1, 2, 3, 4))
                                 .Build();
        sut.Should().NotBeNull()
           .And.NotBeEmpty()
           .And.Contain("Foo")
           .And.Contain("1:2:3:4");
    }

    [Test]
    public void CanCreateAssemblyReference()
    {
        var sut = AssemblyReferenceBuilder.Create()
                                          .WithName("Foo")
                                          .WithPublicKeyToken("Bar")
                                          .WithVersion(new Version(1, 2, 3, 4))
                                          .Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("Foo", "Bar", "1:2:3:4");
    }

    [Test]
    public void CanCreateModuleDeclaration()
    {
        var sut = ModuleBuilder.Create().WithFileName("Foo").Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("module", "Foo");
    }
    [Test]
    public void CanCreateFieldDefinition()
    {
        var sut = FieldBuilder.Create().WithName("Foo").WithType("Bar").Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("field", "Foo", "Bar");
    }
    [Test]
    public void CanCreatePropertyDefinition()
    {
        var sut = PropertyBuilder.Create().WithName("Foo").WithType("Bar").Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("property", "Foo", "Bar");
    }

    [Test]
    public void CanCreateClassDefinition()
    {
        var sut = ClassBuilder.Create()
                              .WithName("Foo")
                              .Build();
        sut.Should().NotBeNullOrWhiteSpace();
        sut.Should().ContainAll("class", "Foo", "extends", "Object");
    }

    [Test]
    public void CanCreateExtendedClassDescription()
    {
        var cb = ClassBuilder.Create()
                             .WithName("MyClass")
                             .WithField(FieldBuilder.Create()
                                                    .WithName("Field1")
                                                    .WithType("Field1Type")
                                                    .New())
                             .WithProperty(PropertyBuilder.Create()
                                                          .WithName("Prop1")
                                                          .WithType("Prop1Type")
                                                          .New())
                             .WithMethod(MethodBuilder.Create()
                                                      .WithName("FooMethod")
                                                      .WithType("FooType")
                                                      .WithStatement( StatementBuilder.Create()
                                                          .WithVariableAssignment("someVar", ExpressionBuilder.Create()
                                                              .WithLiteral(5.0)
                                                              .New() )
                                                          .New()
                                                          )
                                                      .WithStatement(StatementBuilder.Create()
                                                          .WithReturnStatement(ExpressionBuilder.Create()
                                                              .WithFunctionCall("fooBar", "int")
                                                              .New())
                                                          .New())
                                                      .New())
                             .Build();
        cb.Should().NotBeNullOrWhiteSpace();
    }
}
