namespace fifth.metamodel.metadata;

public static class ASTModel
{
    public static readonly AstNodeSpec[] AstNodeSpecs = new AstNodeSpec[]
{
new AstNodeSpec()
{
    Name = "Assembly",
    Parent = "AstNode",
    CustomCode=@"
            public Assembly(string name, string strongNameKey, string versionNumber)
            {
                Name = name;
                PublicKeyToken = strongNameKey;
                Version = versionNumber;
                References = new List<AssemblyRef>();
            }
        ",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "PublicKeyToken", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Version", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Program", type: "FifthProgram"),
        new PropertySpec(name: "References", type: "AssemblyRef", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "AssemblyRef",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "PublicKeyToken", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Version", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "ClassDefinition",
    Parent = "ScopeAstNode, ITypedAstNode, IFunctionCollection",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Properties", type: "PropertyDefinition", isCollection: true),
        new PropertySpec(name: "Functions", type: "FunctionDefinition", isCollection: true, ignoreDuringVisit: false, interfaceName: "IFunctionDefinition")
    }
},
new AstNodeSpec()
{
    Name = "PropertyDefinition",
    Parent = "TypedAstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "TypeCast",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "SubExpression", type: "Expression"),
        new PropertySpec(name: "TargetTid", type: "TypeId", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "ReturnStatement",
    Parent = "Statement",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "SubExpression", type: "Expression"),
        new PropertySpec(name: "TargetTid", type: "TypeId", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "StatementList",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Statements", type: "Statement", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "AbsoluteIri",
    Parent = "TypedAstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Uri", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "AliasDeclaration",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "IRI", type: "AbsoluteIri"),
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "AssignmentStmt",
    Parent = "Statement",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Expression", type: "Expression"),
        new PropertySpec(name: "VariableRef", type: "BaseVarReference")
    }
},
new AstNodeSpec()
{
    Name = "BinaryExpression",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Left", type: "Expression"),
        new PropertySpec(name: "Op", type: "Operator?", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Right", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "Block",
    Parent = "ScopeAstNode",
    CustomCode=@"
        public Block(StatementList sl):this(sl.Statements){}
        ",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Statements", type: "Statement", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "BoolValueExpression",
    Parent = "LiteralExpression<bool>",
    PostCtor=": base(TheValue, PrimitiveBool.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "bool", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "ShortValueExpression",
    Parent = "LiteralExpression<short>",
    PostCtor=": base(TheValue, PrimitiveShort.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "short", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "IntValueExpression",
    Parent = "LiteralExpression<int>",
    PostCtor=": base(TheValue, PrimitiveInteger.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "int", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "LongValueExpression",
    Parent = "LiteralExpression<long>",
    PostCtor=": base(TheValue, PrimitiveLong.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "long", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "FloatValueExpression",
    Parent = "LiteralExpression<float>",
    PostCtor=": base(TheValue, PrimitiveFloat.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "float", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "DoubleValueExpression",
    Parent = "LiteralExpression<double>",
    PostCtor=": base(TheValue, PrimitiveDouble.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "double", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "DecimalValueExpression",
    Parent = "LiteralExpression<decimal>",
    PostCtor=": base(TheValue, PrimitiveDecimal.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "decimal", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "StringValueExpression",
    Parent = "LiteralExpression<string>",
    PostCtor=": base(TheValue, PrimitiveString.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "DateValueExpression",
    Parent = "LiteralExpression<DateTimeOffset>",
    PostCtor=": base(TheValue, PrimitiveDate.Default.TypeId)",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TheValue", type: "DateTimeOffset", isCollection: false, ignoreDuringVisit: true)
    }
},

new AstNodeSpec()
{
    Name = "ExpressionList",
    Parent = "TypedAstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Expressions", type: "Expression", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "FifthProgram",
    Parent = "ScopeAstNode, IFunctionCollection",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Aliases", type: "AliasDeclaration", isCollection: true),
        new PropertySpec(name: "Classes", type: "ClassDefinition", isCollection: true),
        new PropertySpec(name: "Functions", type: "FunctionDefinition", isCollection: true, interfaceName: "IFunctionDefinition")
    }
},
new AstNodeSpec()
{
    Name = "FuncCallExpression",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ActualParameters", type: "ExpressionList"),
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "FunctionDefinition",
    Parent = "ScopeAstNode, IFunctionDefinition",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterDeclarations", type: "ParameterDeclarationList", isCollection: false, ignoreDuringVisit: false),
        new PropertySpec(name: "Body", type: "Block", isCollection: false, ignoreDuringVisit: false),
        new PropertySpec(name: "Typename", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "IsEntryPoint", type: "bool", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "ReturnType", type: "TypeId", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "BuiltinFunctionDefinition",
    Parent = "AstNode, IFunctionDefinition",
    CustomCode=@"
            public ParameterDeclarationList ParameterDeclarations { get; set; }
            public string Typename { get; set; }
            public string Name { get; set; }
            public bool IsEntryPoint { get; set; }
            public TypeId ReturnType { get; set; }

        public BuiltinFunctionDefinition(string name, string typename, params (string, string)[] parameters)
            {
                Name = name;
                Typename = typename;
                var list = new List<IParameterListItem>();

                foreach (var (pname, ptypename) in parameters)
                {
                    var paramDef = new ParameterDeclaration(new Identifier(pname), ptypename, null);
                    list.Add(paramDef);
                }

                var paramDeclList = new ParameterDeclarationList(list);

                ParameterDeclarations = paramDeclList;
                IsEntryPoint = false;
            }
        ",
    Props = new PropertySpec[]
    {
    }
},
new AstNodeSpec()
{
    Name = "OverloadedFunctionDefinition",
    Parent = "ScopeAstNode, IFunctionDefinition, ITypedAstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "OverloadClauses", type: "FunctionDefinition", isCollection: true, ignoreDuringVisit: false, interfaceName: "IFunctionDefinition"),
        new PropertySpec(name: "Signature", type: "IFunctionSignature", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "Identifier",
    Parent = "TypedAstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Value", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "IdentifierExpression",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Identifier", type: "Identifier")
    }
},
new AstNodeSpec()
{
    Name = "IfElseStatement",
    Parent = "Statement",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "IfBlock", type: "Block"),
        new PropertySpec(name: "ElseBlock", type: "Block"),
        new PropertySpec(name: "Condition", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "ModuleImport",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ModuleName", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "DestructuringParamDecl",
    Parent = "ParameterDeclaration, IParameterListItem",
    CustomCode=@"",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "PropertyBindings", type: "PropertyBinding", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "ParameterDeclaration",
    Parent = "TypedAstNode, IParameterListItem",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterName", type: "Identifier"),
        new PropertySpec(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Constraint", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "ParameterDeclarationList",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ParameterDeclarations", type: "ParameterDeclaration", isCollection: true, ignoreDuringVisit: false, interfaceName: "IParameterListItem")
    }
},
new AstNodeSpec()
{
    Name = "TypeCreateInstExpression",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
    }
},
new AstNodeSpec()
{
    Name = "TypeInitialiser",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "PropertyInitialisers", type: "TypePropertyInit", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "PropertyBinding",
    Parent = "AstNode",
    CustomCode=@"
            public PropertyDefinition BoundProperty { get; set; }
        ",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "BoundPropertyName", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "BoundVariableName", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Constraint", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "TypePropertyInit",
    Parent = "AstNode",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
        new PropertySpec(name: "Value", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "UnaryExpression",
    Parent = "Expression",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Operand", type: "Expression"),
        new PropertySpec(name: "Op", type: "Operator", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "VariableDeclarationStatement",
    Parent = "Statement, ITypedAstNode",
    CustomCode=@"
            private string typeName;
            public string TypeName
            {
                get
                {
                    if (TypeId != null)
                    {
                        return TypeId.Lookup().Name;
                    }
                    return typeName;
                }
                set
                {
                    if (!TypeRegistry.DefaultRegistry.TryGetTypeByName(value, out var type))
                    {
                        throw new TypeCheckingException(""Setting unrecognised type for variable"");
                    }

                    typeName = type.Name; // in case we want to use some sort of mapping onto a canonical name
                    TypeId = type.TypeId;
                }
            }
            public TypeId TypeId { get; set; }

        ",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Expression", type: "Expression"),
        new PropertySpec(name: "Name", type: "Identifier")
    }
},
new AstNodeSpec()
{
    Name = "VariableReference",
    Parent = "BaseVarReference",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "CompoundVariableReference",
    Parent = "BaseVarReference",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "ComponentReferences", type: "VariableReference", isCollection: true)
    }
},
new AstNodeSpec()
{
    Name = "WhileExp",
    Parent = "Statement",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Condition", type: "Expression"),
        new PropertySpec(name: "LoopBlock", type: "Block", isCollection: false, ignoreDuringVisit: true)
    }
},
new AstNodeSpec()
{
    Name = "ExpressionStatement",
    Parent = "Statement",
    Props = new PropertySpec[]
    {
        new PropertySpec(name: "Expression", type: "Expression")
    }
},
new AstNodeSpec()
{
    Name = "Expression",
    Parent = "TypedAstNode",
    Props = new PropertySpec[]
    {
    }
}
};
}
