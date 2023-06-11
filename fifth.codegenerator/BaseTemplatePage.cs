namespace Fifth.CodeGeneration;

public abstract class BaseTemplatePage<T> : TemplatePage<T>
{
    public string MapType(TypeId tid)
    {
        if (tid == null)
        {
            return "void";
        }

        if (TypeMappings.HasMapping(tid))
        {
            return TypeMappings.ToDotnetType(tid);
        }

        var tn = tid.Lookup().Name;
        if (tid.Lookup() is UserDefinedType)
        {
            tn = $"class {tn}";
        }

        return tn;
    }

}
