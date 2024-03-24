namespace fifth.metamodel.metadata;

public static class ASTModel
{
    public static readonly AstNodeSpec[] AstNodeSpecs = {
        /*Assembly*/new()
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
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "PublicKeyToken", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Version", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Program", type: "FifthProgram"),
                new(name: "References", type: "AssemblyRef", isCollection: true)
            }
        },
        /*AssemblyRef*/new()
        {
            Name = "AssemblyRef",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "PublicKeyToken", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Version", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ClassDefinition*/new()
        {
            Name = "ClassDefinition",
            Parent = "ScopeAstNode, ITypedAstNode, IFunctionCollection",
            Props = new PropertySpec[]
            {
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Namespace", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Fields", type: "FieldDefinition", isCollection: true),
                new(name: "Properties", type: "PropertyDefinition", isCollection: true),
                new(name: "Functions", type: "FunctionDefinition", isCollection: true, ignoreDuringVisit: false, interfaceName: "IFunctionDefinition")
            }
        },
        /*FieldDefinition*/new()
        {
            Name = "FieldDefinition",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "BackingFieldFor", type: "PropertyDefinition?", isCollection: false, ignoreDuringVisit: true),
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*PropertyDefinition*/new()
        {
            Name = "PropertyDefinition",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "BackingField", type: "FieldDefinition?", isCollection: false, ignoreDuringVisit: true),
                new(name: "GetAccessor", type: "FunctionDefinition?", isCollection: false, ignoreDuringVisit: false),
                new(name: "SetAccessor", type: "FunctionDefinition?", isCollection: false, ignoreDuringVisit: false),
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*TypeCast*/new()
        {
            Name = "TypeCast",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "SubExpression", type: "Expression"),
                new(name: "TargetTid", type: "TypeId", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ReturnStatement*/new()
        {
            Name = "ReturnStatement",
            Parent = "Statement",
            Props = new PropertySpec[]
            {
                new(name: "SubExpression", type: "Expression"),
                new(name: "TargetTid", type: "TypeId?", isCollection: false, isNullable: true, ignoreDuringVisit: true)
            }
        },
        /*StatementList*/new()
        {
            Name = "StatementList",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "Statements", type: "Statement", isCollection: true)
            }
        },
        /*AbsoluteIri*/new()
        {
            Name = "AbsoluteIri",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "Uri", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*AliasDeclaration*/new()
        {
            Name = "AliasDeclaration",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "IRI", type: "AbsoluteIri"),
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*AssignmentStmt*/new()
        {
            Name = "AssignmentStmt",
            Parent = "Statement",
            Props = new PropertySpec[]
            {
                new(name: "Expression", type: "Expression"),
                new(name: "VariableRef", type: "BaseVarReference")
            }
        },
        /*BinaryExpression*/new()
        {
            Name = "BinaryExpression",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "Left", type: "Expression"),
                new(name: "Op", type: "Operator?", isCollection: false, ignoreDuringVisit: true),
                new(name: "Right", type: "Expression")
            }
        },
        /*Block*/new()
        {
            Name = "Block",
            Parent = "ScopeAstNode",
            CustomCode=@"
                public Block(StatementList sl):this(sl.Statements){}
                ",
            Props = new PropertySpec[]
            {
                new(name: "Statements", type: "Statement", isCollection: true)
            }
        },
        /*BoolValueExpression*/new()
        {
            Name = "BoolValueExpression",
            Parent = "LiteralExpression<bool>",
            PostCtor=": base(TheValue, PrimitiveBool.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "bool", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ShortValueExpression*/new()
        {
            Name = "ShortValueExpression",
            Parent = "LiteralExpression<short>",
            PostCtor=": base(TheValue, PrimitiveShort.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "short", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*IntValueExpression*/new()
        {
            Name = "IntValueExpression",
            Parent = "LiteralExpression<int>",
            PostCtor=": base(TheValue, PrimitiveInteger.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "int", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*LongValueExpression*/new()
        {
            Name = "LongValueExpression",
            Parent = "LiteralExpression<long>",
            PostCtor=": base(TheValue, PrimitiveLong.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "long", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*FloatValueExpression*/new()
        {
            Name = "FloatValueExpression",
            Parent = "LiteralExpression<float>",
            PostCtor=": base(TheValue, PrimitiveFloat.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "float", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*DoubleValueExpression*/new()
        {
            Name = "DoubleValueExpression",
            Parent = "LiteralExpression<double>",
            PostCtor=": base(TheValue, PrimitiveDouble.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "double", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*DecimalValueExpression*/new()
        {
            Name = "DecimalValueExpression",
            Parent = "LiteralExpression<decimal>",
            PostCtor=": base(TheValue, PrimitiveDecimal.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "decimal", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*StringValueExpression*/new()
        {
            Name = "StringValueExpression",
            Parent = "LiteralExpression<string>",
            PostCtor=": base(TheValue, PrimitiveString.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*DateValueExpression*/new()
        {
            Name = "DateValueExpression",
            Parent = "LiteralExpression<DateTimeOffset>",
            PostCtor=": base(TheValue, PrimitiveDate.Default.TypeId)",
            Props = new PropertySpec[]
            {
                new(name: "TheValue", type: "DateTimeOffset", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ExpressionList*/new()
        {
            Name = "ExpressionList",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "Expressions", type: "Expression", isCollection: true)
            }
        },
        /*FifthProgram*/new()
        {
            Name = "FifthProgram",
            Parent = "ScopeAstNode, IFunctionCollection",
            Props = new PropertySpec[]
            {
                new(name: "Aliases", type: "AliasDeclaration", isCollection: true),
                new(name: "Classes", type: "ClassDefinition", isCollection: true),
                new(name: "Functions", type: "FunctionDefinition", isCollection: true, interfaceName: "IFunctionDefinition")
            }
        },
        /*FuncCallExpression*/new()
        {
            Name = "FuncCallExpression",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "ActualParameters", type: "ExpressionList"),
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*FunctionDefinition*/new()
        {
            Name = "FunctionDefinition",
            Parent = "ScopeAstNode, IFunctionDefinition",
            Props = new PropertySpec[]
            {
                new(name: "ParameterDeclarations", type: "ParameterDeclarationList", isCollection: false, ignoreDuringVisit: false),
                new(name: "Body", type: "Block?", isCollection: false, ignoreDuringVisit: false),
                new(name: "Typename", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "IsEntryPoint", type: "bool", isCollection: false, ignoreDuringVisit: true),
                new(name: "IsInstanceFunction", type: "bool", isCollection: false, ignoreDuringVisit: true),
                new(name: "FunctionKind", type: "FunctionKind", isCollection: false, ignoreDuringVisit: true),
                new(name: "ReturnType", type: "TypeId?", isCollection: false, ignoreDuringVisit: true, isNullable: true)
            }
        },
        /*OverloadedFunctionDefinition*/new()
        {
            Name = "OverloadedFunctionDefinition",
            Parent = "ScopeAstNode, IFunctionDefinition, ITypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "OverloadClauses", type: "FunctionDefinition", isCollection: true, ignoreDuringVisit: false, interfaceName: "IFunctionDefinition"),
                new(name: "Signature", type: "IFunctionSignature", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*Identifier*/new()
        {
            Name = "Identifier",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "Value", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*IdentifierExpression*/new()
        {
            Name = "IdentifierExpression",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "Identifier", type: "Identifier")
            }
        },
        /*IfElseStatement*/new()
        {
            Name = "IfElseStatement",
            Parent = "Statement",
            Props = new PropertySpec[]
            {
                new(name: "IfBlock", type: "Block"),
                new(name: "ElseBlock", type: "Block"),
                new(name: "Condition", type: "Expression")
            }
        },
        /*ModuleImport*/new()
        {
            Name = "ModuleImport",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "ModuleName", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ParameterDeclarationList*/new()
        {
            Name = "ParameterDeclarationList",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "ParameterDeclarations", type: "ParameterDeclaration", isCollection: true, ignoreDuringVisit: false, interfaceName: "IParameterListItem")
            }
        },
        /*ParameterDeclaration*/new()
        {
            Name = "ParameterDeclaration",
            Parent = "TypedAstNode, IParameterListItem",
            Props = new PropertySpec[]
            {
                new(name: "ParameterName", type: "Identifier"),
                new(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Constraint", type: "Expression"),
                new(name: "DestructuringDecl", type: "DestructuringDeclaration")
            }
        },
        /*DestructuringDeclaration*/new()
        {
            Name = "DestructuringDeclaration",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "Bindings", type: "DestructuringBinding", isCollection: true, ignoreDuringVisit: false)
            }
        },
        /*DestructuringBinding*/new()
        {
            Name = "DestructuringBinding",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
                new(name: "Varname", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Propname", type: "string", isCollection: false, ignoreDuringVisit: true),
                // propdecl gets filled in at a later date by a visitor after resolution
                new(name: "PropDecl", type: "PropertyDefinition", isCollection: false, ignoreDuringVisit: true),
                new(name: "Constraint", type: "Expression"),
                new(name: "DestructuringDecl", type: "DestructuringDeclaration")
            }
        },
        /*TypeCreateInstExpression*/new()
        {
            Name = "TypeCreateInstExpression",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
            }
        },
        /*TypeInitialiser*/new()
        {
            Name = "TypeInitialiser",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "TypeName", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "PropertyInitialisers", type: "TypePropertyInit", isCollection: true)
            }
        },
        /*TypePropertyInit*/new()
        {
            Name = "TypePropertyInit",
            Parent = "AstNode",
            Props = new PropertySpec[]
            {
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true),
                new(name: "Value", type: "Expression")
            }
        },
        /*UnaryExpression*/new()
        {
            Name = "UnaryExpression",
            Parent = "Expression",
            Props = new PropertySpec[]
            {
                new(name: "Operand", type: "Expression"),
                new(name: "Op", type: "Operator", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*VariableDeclarationStatement*/new()
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
                new(name: "Expression", type: "Expression"),
                new(name: "Name", type: "string", ignoreDuringVisit: true),
                new(name: "UnresolvedTypeName", type: "string", ignoreDuringVisit: true)
            }
        },
        /*VariableReference*/new()
        {
            Name = "VariableReference",
            Parent = "BaseVarReference",
            Props = new PropertySpec[]
            {
                new(name: "Name", type: "string", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*MemberAccessExpression*/new()
        {
          Name  = "MemberAccessExpression",
          Parent = "Expression",
          Props = new []
          {
                new PropertySpec(name: "LHS", type: "Expression"),
                new PropertySpec(name: "RHS", type: "Expression")

          },
          Commentary = new string[]{
             "Supersedes the old Compound Variable reference, which assumed ",
             "that the only elements in a call chain were variables"
          }
        },
        /*WhileExp*/new()
        {
            Name = "WhileExp",
            Parent = "Statement",
            Props = new PropertySpec[]
            {
                new(name: "Condition", type: "Expression"),
                new(name: "LoopBlock", type: "Block", isCollection: false, ignoreDuringVisit: true)
            }
        },
        /*ExpressionStatement*/new()
        {
            Name = "ExpressionStatement",
            Parent = "Statement",
            Props = new PropertySpec[]
            {
                new(name: "Expression", type: "Expression")
            }
        },
        /*Expression*/new()
        {
            Name = "Expression",
            Parent = "TypedAstNode",
            Props = new PropertySpec[]
            {
            }
        }
    };
}
