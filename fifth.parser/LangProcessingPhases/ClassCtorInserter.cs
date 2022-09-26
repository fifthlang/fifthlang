namespace Fifth.LangProcessingPhases;

using System.Collections.Generic;
using AST;
using AST.Builders;
using AST.Visitors;

public class ClassCtorInserter : BaseAstVisitor
{
    public override void EnterClassDefinition(ClassDefinition ctx)
    {
        var f = FunctionDefinitionBuilder.CreateFunctionDefinition()
                                         .WithName("ctor")
                                         .WithFunctionKind(FunctionKind.Ctor)
                                         .WithParameterDeclarations(new ParameterDeclarationList(new List<IParameterListItem>()))
                                         .WithBody(new Block(new List<Statement>()))
                                         .Build();
        f.ParentNode = ctx;
        ctx.Functions.Add(f);
    }

}