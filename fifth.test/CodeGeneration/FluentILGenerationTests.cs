namespace Fifth.Test.CodeGeneration;
using Fifth.CodeGeneration.IL;
using fifth.metamodel.metadata.il;

[TestFixture]
[Category("CIL")]
public class FluentILGenerationTests
{
    [Test]
    public void CanCreateAssembly()
    {
        var sut = AssemblyDeclarationBuilder.Create()
                                            .WithName("Foo")
                                            .WithPrimeModule(ModuleDeclarationBuilder.Create().WithFileName("helloworld").New())
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
        var sut = ModuleDeclarationBuilder.Create().WithFileName("Foo").Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("module", "Foo");
    }

    [Test]
    public void CanCreateFieldDefinition()
    {
        var barType = TypeReferenceBuilder.Create()
                            .WithModuleName("mymodule")
                            .WithNamespace("some.namespace")
                            .WithName("Field1Type")
                            .New();
        var sut = FieldDefinitionBuilder.Create().WithName("Foo").WithTheType(barType).Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("field", "Foo", "Bar");
    }

    [Test]
    public void CanCreatePropertyDefinition()
    {
        var sut = PropertyDefinitionBuilder.Create().WithName("Foo").WithTypeName("Bar").Build();
        sut.Should().NotBeNullOrWhiteSpace()
           .And.ContainAll("property", "Foo", "Bar");
    }

    [Test]
    public void CanCreateClassDefinition()
    {
        var sut = ClassDefinitionBuilder.Create()
                                        .WithName("Foo")
                                        .Build();
        sut.Should().NotBeNullOrWhiteSpace();
        sut.Should().ContainAll("class", "Foo", "extends", "Object");
    }

    [Test]
    public void CanCreateExtendedClassDescription()
    {
        var f1Type = TypeReferenceBuilder.Create()
                            .WithModuleName("mymodule")
                            .WithNamespace("some.namespace")
                            .WithName("Field1Type")
                            .New();
        var fooType = TypeReferenceBuilder.Create()
                            .WithModuleName("mymodule")
                            .WithNamespace("some.namespace")
                            .WithName("Foo")
                            .New();
        var cb = ClassDefinitionBuilder.Create()
                                       .WithName("MyClass")
                                       .AddingItemToFields(FieldDefinitionBuilder.Create()
                                           .WithName("Field1")
                                           .WithTheType(f1Type)
                                           .WithTypeOfMember(MemberType.Field)
                                           .New())
                                       .AddingItemToProperties(PropertyDefinitionBuilder.Create()
                                           .WithName("Prop1")
                                           .WithTypeName("Prop1Type")
                                           .New())
                                       .AddingItemToMethods(MethodDefinitionBuilder.Create()
                                           .WithName("FooMethod")
                                           .WithTheType(fooType)
                                           .WithTypeOfMember(MemberType.Field)
                                           .WithImpl(
                                               MethodImplBuilder.Create()
                                                                .WithBody(BlockBuilder.Create()
                                                                 .AddingItemToStatements(StatementBuilder.Create()
                                                                     .WithVariableAssignment("someVar",
                                                                         ExpressionBuilder.Create()
                                                                             .WithLiteral(5.0)
                                                                             .New(), 0)
                                                                     .New()
                                                                 ).AddingItemToStatements(StatementBuilder.Create()
                                                                     .WithReturnStatement(ExpressionBuilder.Create()
                                                                         .WithFunctionCall("fooBar", "int")
                                                                         .New())
                                                                     .New())
                                                                 .New())
                                                                .New()
                                               ) //impl
                                           .New())
                                       .Build();
        cb.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void CanGenerateIfStatement()
    {
        var sut = IfStatementBuilder.Create()
                                    .WithConditional(ExpressionBuilder.Create().WithLiteral(true).New())
                                    .WithIfBlock(
                                        BlockBuilder.Create()
                                                    .AddingItemToStatements(StatementBuilder.Create()
                                                        .WithReturnStatement(ExpressionBuilder.Create()
                                                            .WithFunctionCall("fooBar", "int")
                                                            .New()).New()
                                                    )
                                                    .AddingItemToStatements(StatementBuilder.Create()
                                                        .WithReturnStatement(ExpressionBuilder.Create()
                                                            .WithFunctionCall("fooBar", "int")
                                                            .New()).New())
                                                    .New())
                                    .WithElseBlock(
                                        BlockBuilder.Create()
                                                    .AddingItemToStatements(StatementBuilder.Create()
                                                        .WithReturnStatement(ExpressionBuilder.Create()
                                                            .WithFunctionCall("fooBar", "int")
                                                            .New()).New()
                                                    )
                                                    .New())
                                    .Build();
        sut.Should().NotBeNullOrWhiteSpace();
        sut.Should().ContainAll("true", "fooBar");
    }

    [Test]
    public void CanGenerateWhileStatement()
    {
        var sut = WhileStmtBuilder.Create()
                                  .WithConditional(ExpressionBuilder.Create().WithLiteral(true).New())
                                  .WithStatement(StatementBuilder.Create()
                                                                 .WithReturnStatement(ExpressionBuilder.Create()
                                                                     .WithFunctionCall("fooBar", "int")
                                                                     .New())
                                                                 .New())
                                  .WithStatement(StatementBuilder.Create()
                                                                 .WithReturnStatement(ExpressionBuilder.Create()
                                                                     .WithFunctionCall("fooBar", "int")
                                                                     .New()).New())
                                  .Build();
        sut.Should().NotBeNullOrWhiteSpace();
        sut.Should().ContainAll("true", "fooBar", "LBL_START", "LBL_END");
    }
}