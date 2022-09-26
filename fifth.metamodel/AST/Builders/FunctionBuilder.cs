namespace Fifth.AST.Builders;

using System.Collections.Generic;
using System.Linq;

public partial class BlockBuilder
{
    public BlockBuilder WithStatement(Statement value)
    {
        if (_Statements == null)
        {
            _Statements = new List<Statement> { };
        }

        if (_Statements is List<Statement> ls)
        {
            ls.Add(value);
        }
        return this;
    }
}

public class FunctionBuilder
{
    private Block body;
    private string name;
    private List<(string, string)> parameters = new();
    private AstNode parentNode;
    private TypeId returnType;
    private string returnTypeName;

    private FunctionBuilder()
    {
    }

    public static FunctionBuilder NewFunction()
        => new FunctionBuilder();

    public IFunctionDefinition AsAstNode()
    {
        var pds = parameters.Select(x => new ParameterDeclaration(new Identifier(x.Item2), x.Item1, null, null)).Cast<IParameterListItem>().ToList();
        var paramDecls = new ParameterDeclarationList(pds);
        var result = new FunctionDefinition(ParameterDeclarations: paramDecls, Body: body, Typename: returnTypeName, Name: name, IsEntryPoint: name == "main", FunctionKind: FunctionKind.Normal, ReturnType: returnType)
        {
            ParentNode = parentNode
        };
        return result;
    }

    public FunctionBuilder Called(string funcName)
    {
        name = funcName;
        return this;
    }

    public FunctionBuilder WithBody(Block body)
    {
        this.body = body;
        return this;
    }

    public FunctionBuilder WithParam(string name, string typename)
    {
        parameters.Add((typename, name));
        return this;
    }

    public FunctionBuilder WithReturnType(string typename)
    {
        returnTypeName = typename;
        return this;
    }

    public FunctionBuilder WithReturnType(TypeId tid)
    {
        returnType = tid;
        return this;
    }

    public FunctionBuilder WithSameParentAs(AstNode ctx)
    {
        parentNode = ctx.ParentNode;
        return this;
    }
}
