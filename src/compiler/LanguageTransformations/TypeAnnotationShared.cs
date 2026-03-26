using ast;
using ast_model.Symbols;
using ast_model.TypeSystem.Inference;

namespace Fifth.LangProcessingPhases;

public sealed class TypeAnnotationContext
{
    private readonly Dictionary<Type, FifthType> _languageFriendlyTypes = new();
    private readonly TypeSystem _typeSystem = new();

    public List<TypeCheckingError> Errors { get; } = [];
    public ModuleDef? CurrentModule { get; set; }

    public TypeAnnotationContext()
    {
        InitializeLanguageFriendlyTypes();

        foreach (var fifthType in _languageFriendlyTypes.Values)
        {
            _typeSystem.WithType(fifthType);
        }

        _typeSystem.WithType(new FifthType.TVoidType { Name = TypeName.From("void") });

        if (_languageFriendlyTypes.TryGetValue(typeof(int), out var intType) &&
            _languageFriendlyTypes.TryGetValue(typeof(float), out var floatType))
        {
            _typeSystem.WithFunction([intType, intType], intType, "+")
                      .WithFunction([intType, intType], intType, "-")
                      .WithFunction([intType, intType], intType, "*")
                      .WithFunction([intType, intType], floatType, "/")
                      .WithFunction([floatType, floatType], floatType, "+")
                      .WithFunction([floatType, floatType], floatType, "-")
                      .WithFunction([floatType, floatType], floatType, "*")
                      .WithFunction([floatType, floatType], floatType, "/");
        }
    }

    private void InitializeLanguageFriendlyTypes()
    {
        var typeNameMapping = new Dictionary<Type, string>
        {
            [typeof(bool)] = "bool",
            [typeof(byte)] = "byte",
            [typeof(sbyte)] = "sbyte",
            [typeof(char)] = "char",
            [typeof(short)] = "short",
            [typeof(ushort)] = "ushort",
            [typeof(int)] = "int",
            [typeof(uint)] = "uint",
            [typeof(long)] = "long",
            [typeof(ulong)] = "ulong",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
            [typeof(decimal)] = "decimal",
            [typeof(string)] = "string",
            [typeof(DateTime)] = "DateTime",
            [typeof(DateTimeOffset)] = "DateTimeOffset"
        };

        foreach (var primitiveType in TypeRegistry.Primitives)
        {
            if (typeNameMapping.TryGetValue(primitiveType, out var friendlyName))
            {
                _languageFriendlyTypes[primitiveType] = new FifthType.TDotnetType(primitiveType)
                {
                    Name = TypeName.From(friendlyName)
                };
            }
            else
            {
                _languageFriendlyTypes[primitiveType] = new FifthType.TDotnetType(primitiveType)
                {
                    Name = TypeName.From(primitiveType.Name)
                };
            }
        }
    }

    public FifthType? GetLanguageFriendlyType(Type dotnetType)
    {
        return _languageFriendlyTypes.TryGetValue(dotnetType, out var fifthType) ? fifthType : null;
    }

    public FifthType? GetLanguageFriendlyTypeByName(string typeName)
    {
        return typeName switch
        {
            "int" => GetLanguageFriendlyType(typeof(int)),
            "long" => GetLanguageFriendlyType(typeof(long)),
            "float" => GetLanguageFriendlyType(typeof(float)),
            "double" => GetLanguageFriendlyType(typeof(double)),
            "bool" => GetLanguageFriendlyType(typeof(bool)),
            "string" => GetLanguageFriendlyType(typeof(string)),
            _ => null
        };
    }

    public FifthType CreateFifthType(TypeName typeName, CollectionType collectionType)
    {
        var baseType = CreateBaseType(typeName);

        return collectionType switch
        {
            CollectionType.Array => new FifthType.TArrayOf(baseType) { Name = TypeName.From($"{typeName.Value}[]") },
            CollectionType.List => new FifthType.TListOf(baseType) { Name = TypeName.From($"List<{typeName.Value}>") },
            _ => baseType
        };
    }

    public FifthType CreateBaseType(TypeName typeName)
    {
        var typeNameValue = typeName.Value;

        if (typeNameValue.StartsWith("[") && typeNameValue.EndsWith("]"))
        {
            var innerTypeName = typeNameValue[1..^1];
            var innerType = CreateBaseType(TypeName.From(innerTypeName));
            return new FifthType.TListOf(innerType) { Name = TypeName.From($"List<{innerTypeName}>") };
        }

        if (typeNameValue.EndsWith("[]"))
        {
            var innerTypeName = typeNameValue[..^2];
            var innerType = CreateBaseType(TypeName.From(innerTypeName));
            return new FifthType.TArrayOf(innerType) { Name = TypeName.From($"{innerTypeName}[]") };
        }

        var friendlyType = GetLanguageFriendlyTypeByName(typeNameValue);
        if (friendlyType != null)
        {
            return friendlyType;
        }

        if (TypeRegistry.DefaultRegistry.TryGetTypeByName(typeNameValue, out var registeredType))
        {
            return registeredType;
        }

        return new FifthType.TType { Name = typeName };
    }

    public FifthType? InferBinaryResultType(FifthType leftType, FifthType rightType, Operator op)
    {
        var operatorStr = GetOperatorString(op);
        try
        {
            var inferredType = _typeSystem.InferResultType([leftType, rightType], operatorStr);
            if (inferredType != null)
            {
                return inferredType;
            }
        }
        catch
        {
            // Fallback below
        }

        return GetSimpleOperatorResultType(leftType, rightType, op);
    }

    private FifthType? GetSimpleOperatorResultType(FifthType leftType, FifthType rightType, Operator op)
    {
        return op switch
        {
            Operator.ArithmeticAdd or Operator.ArithmeticSubtract or Operator.ArithmeticMultiply =>
                GetArithmeticResultType(leftType, rightType),
            Operator.ArithmeticDivide =>
                GetLanguageFriendlyType(typeof(float)) ?? new FifthType.TDotnetType(typeof(float)) { Name = TypeName.From("float") },
            Operator.Equal or Operator.NotEqual or
            Operator.LessThan or Operator.LessThanOrEqual or
            Operator.GreaterThan or Operator.GreaterThanOrEqual =>
                GetLanguageFriendlyType(typeof(bool)) ?? new FifthType.TDotnetType(typeof(bool)) { Name = TypeName.From("bool") },
            Operator.LogicalAnd or Operator.LogicalOr =>
                GetLanguageFriendlyType(typeof(bool)) ?? new FifthType.TDotnetType(typeof(bool)) { Name = TypeName.From("bool") },
            _ => null
        };
    }

    private FifthType GetArithmeticResultType(FifthType leftType, FifthType rightType)
    {
        if (IsFloatType(leftType) || IsFloatType(rightType))
        {
            return GetLanguageFriendlyType(typeof(float)) ?? new FifthType.TDotnetType(typeof(float)) { Name = TypeName.From("float") };
        }

        if (IsIntType(leftType) && IsIntType(rightType))
        {
            return GetLanguageFriendlyType(typeof(int)) ?? new FifthType.TDotnetType(typeof(int)) { Name = TypeName.From("int") };
        }

        return leftType;
    }

    private static bool IsFloatType(FifthType type)
    {
        return type is FifthType.TDotnetType dotnetType &&
               (dotnetType.TheType == typeof(float) || dotnetType.TheType == typeof(double));
    }

    private static bool IsIntType(FifthType type)
    {
        return type is FifthType.TDotnetType dotnetType &&
               (dotnetType.TheType == typeof(int) || dotnetType.TheType == typeof(long) ||
                dotnetType.TheType == typeof(short) || dotnetType.TheType == typeof(byte));
    }

    private static string GetOperatorString(Operator op)
    {
        return op switch
        {
            Operator.ArithmeticAdd => "+",
            Operator.ArithmeticSubtract => "-",
            Operator.ArithmeticMultiply => "*",
            Operator.ArithmeticDivide => "/",
            Operator.Equal => "==",
            Operator.NotEqual => "!=",
            Operator.LessThan => "<",
            Operator.LessThanOrEqual => "<=",
            Operator.GreaterThan => ">",
            Operator.GreaterThanOrEqual => ">=",
            Operator.LogicalAnd => "&&",
            Operator.LogicalOr => "||",
            _ => op.ToString()
        };
    }

    public bool IsPrimitiveType(FifthType type)
    {
        return type switch
        {
            FifthType.TArrayOf => false,
            FifthType.TListOf => false,
            FifthType.TDotnetType dotnetType =>
                dotnetType.TheType == typeof(int) ||
                dotnetType.TheType == typeof(long) ||
                dotnetType.TheType == typeof(float) ||
                dotnetType.TheType == typeof(double) ||
                dotnetType.TheType == typeof(bool) ||
                dotnetType.TheType == typeof(string),
            FifthType.TType ttype =>
                ttype.Name.Value == "int" ||
                ttype.Name.Value == "long" ||
                ttype.Name.Value == "float" ||
                ttype.Name.Value == "double" ||
                ttype.Name.Value == "bool" ||
                ttype.Name.Value == "string",
            _ => false
        };
    }

    public static string GetTypeName(FifthType type)
    {
        return type switch
        {
            FifthType.TDotnetType dotnetType => dotnetType.TheType?.Name ?? "unknown",
            FifthType.TType ttype => ttype.Name.Value,
            FifthType.TVoidType => "void",
            _ => type.ToString() ?? "unknown"
        };
    }

    public void OnTypeInferred(AstThing node, FifthType type)
    {
    }

    public void OnTypeMismatch(AstThing node, FifthType type1, FifthType type2)
    {
        var error = new TypeCheckingError(
            $"Type mismatch between {type1.Name} and {type2.Name}",
            node.Location?.Filename ?? "",
            node.Location?.Line ?? 0,
            node.Location?.Column ?? 0,
            [type1, type2],
            TypeCheckingSeverity.Error);

        Errors.Add(error);
    }

    public void OnTypeNotFound(AstThing node)
    {
        var error = new TypeCheckingError(
            "Unable to infer type",
            node.Location?.Filename ?? "",
            node.Location?.Line ?? 0,
            node.Location?.Column ?? 0,
            Array.Empty<FifthType>(),
            TypeCheckingSeverity.Info);

        Errors.Add(error);
    }

    public void OnTypeNotRelevant(AstThing node)
    {
    }
}
