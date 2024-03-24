namespace Fifth.Test.metamodel;
using Metamodel.AST2;

[TestFixture]
public class Ast2Tests
{
    /// <summary>
    /// Tests if we can create an object graph for an assembly definition.
    /// </summary>
    /// <remarks>
    /// This test verifies if we can successfully build an assembly definition
    /// using the <see cref="AssemblyDefBuilder"/> class. The test creates an
    /// assembly with the name "MyAssembly" and adds a reference to another assembly
    /// named "bob" with a public key token and version. It also adds a class
    /// definition named "MyClass" with a field definition named "AField".
    /// </remarks>
    [Test, Category("WIP")]
    public void CanCreateObjectGraph()
    {
        // Build the assembly definition
        var assemblyDef = AssemblyDefBuilder.Create()
                          .WithName("MyAssembly") // Set the name of the assembly
                          .AddingItemToAssemblyRefs( // Add a reference to another assembly
                              AssemblyRefBuilder.Create() // Create a new assembly reference
                                                .WithName("bob") // Set the name of the referenced assembly
                                                .WithPublicKeyToken("hsgdhjgfsh") // Set the public key token
                                                .WithVersion("1.0.0.0") // Set the version
                                                .New() // Build and add the reference to the assembly
                          )
                          .AddingItemToClassDefs( // Add a class definition to the assembly
                              ClassDefBuilder.Create() // Create a new class definition
                                             .WithName("MyClass") // Set the name of the class
                                             .AddingItemToMemberDefs( // Add a field definition to the class
                                                 FieldDefBuilder.Create() // Create a new field definition
                                                                .WithName("AField") // Set the name of the field
                                                                .New() // Build and add the field to the class
                                             )
                                             .New() // Build and add the class to the assembly
                          )
                          .New(); // Build the assembly definition

        // Assert that the assembly definition is not null
        Assert.That(assemblyDef, Is.Not.Null, "The assembly definition should not be null.");
}
}
