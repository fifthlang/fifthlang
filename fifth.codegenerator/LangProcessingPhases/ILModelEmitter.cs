namespace Fifth.CodeGeneration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using fifth.metamodel.metadata.AST;
using fifth.metamodel.metadata;
using Fifth.TypeSystem.PrimitiveTypes;

/// <summary>
/// The final iteration of IL code generation using the new .NET 9 persisted Assembly Builder
/// </summary>
/// <seealso cref="https://learn.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder.save?view=net-9.0"/>
/// <seealso cref="https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview#reflection"/>
public class ILModelEmitter
{
    public AssemblyBuilder GenerateAssembly(AssemblyDef ctx)
    {
        AssemblyName asmName = new AssemblyName(ctx.Name);
        var assemblyBuilder = AssemblyBuilder.DefinePersistedAssembly(asmName, typeof(object).Assembly);
        var mb = assemblyBuilder.DefineDynamicModule(asmName.Name + "Module");
        foreach (var c in ctx.ClassDefs)
        {
            var cdb = CreateClass(mb, c);
        }
        return assemblyBuilder;
    }
    public TypeBuilder CreateClass(ModuleBuilder mb, ClassDef ctx)
    {
        var tb = mb.DefineType(ctx.Name, TypeAttributes.Public | TypeAttributes.Class, typeof(object));
        foreach (var memberDef in ctx.MemberDefs)
        {
            switch (memberDef)
            {
                case FieldDef fd:
                    var fdb = CreateField(tb, fd);
                    break;
                case PropertyDef pd:
                    var pdb = CreateProperty(tb, pd);
                    break;
                case MethodDef md:
                    var mdb = CreateMethod(tb, md);
                    break;
                default:
                    break;
            }
        }
        return tb;
    }
    private FieldBuilder CreateField(TypeBuilder tb, FieldDef ctx)
    {
        FieldBuilder fb = tb.DefineField(
            ctx.Name,
            LookupDotnetType(ctx.Type),
            FieldAttributes.Private);
        return fb;
    }
    private PropertyBuilder CreateProperty(TypeBuilder tb, PropertyDef ctx)
    {
        var propertyAttributes = PropertyAttributes.None;
        if (ctx.IsReadOnly)
        {
            propertyAttributes |= PropertyAttributes.HasDefault;
        }

        PropertyBuilder propertyBuilder = tb.DefineProperty(
            ctx.Name,
            propertyAttributes,
            LookupDotnetType(ctx.Type),
            null);
        if (ctx.IsReadOnly)
        {
            propertyBuilder.SetGetMethod(tb.DefineMethod("get_" + ctx.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual));
        }
        if (ctx.IsWriteOnly)
        {
            propertyBuilder.SetSetMethod(tb.DefineMethod("set_" + ctx.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual));
        }
        return propertyBuilder;
    }

    private MethodBuilder CreateMethod(TypeBuilder tb, MethodDef ctx)
    {
        MethodBuilder methodBuilder = tb.DefineMethod(
            ctx.Name,
            GetMethodAttributes(ctx),
            LookupDotnetType(ctx.Type),
            ctx.Params.Select(pd => LookupDotnetType(pd.Type)).ToArray());

        ILGenerator methIL = methodBuilder.GetILGenerator();
        GenerateMethodBody(methIL, ctx);
        return methodBuilder;
    }
    void GenerateMethodBody(ILGenerator ilGenerator, MethodDef ctx)
    {
        foreach (var statement in ctx.Body.Statements)
        {
            GenerateILForStatement(ilGenerator, statement);
        }
    }
    void GenerateILForStatement(ILGenerator ilGenerator, Statement statement)
    {
    }

        MethodAttributes GetMethodAttributes(MethodDef ctx)
        {
            return ctx.Visibility switch
            {
                Visibility.Public => MethodAttributes.Public,
                Visibility.Protected => MethodAttributes.Family,
                Visibility.Private => MethodAttributes.Private,
                Visibility.Internal => MethodAttributes.Assembly,
                _ => MethodAttributes.Private,
            };
        }
        private Type LookupDotnetType(TypeMetadata type)
        {
            var typeDef = type.TypeId.Lookup();
            if (typeDef is UserDefinedType udt)
            {
                return LookupDotnetTypeForUDT(udt.TypeId);
            }
            else if (typeDef is PrimitiveAny pt)
            {
                return TypeMappings.DotnetPrimitiveTypes[pt.TypeId];
            }
            else
            {
                throw new NotImplementedException($"Unhandled type definition kind: {typeDef.GetType().Name}");
            }
        }

    private readonly Dictionary<TypeId, Type> _typeRegistry = new Dictionary<TypeId, Type>();

    Type LookupDotnetTypeForUDT(TypeId tid)
    {
        if (_typeRegistry.TryGetValue(tid, out var type))
        {
            return type;
        }
        else
        {
            throw new InvalidOperationException($"Type {tid} has not been built yet");
        }
    }



}
