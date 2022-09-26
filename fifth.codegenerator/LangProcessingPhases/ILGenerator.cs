namespace Fifth.CodeGeneration.LangProcessingPhases;

using System.Collections.Generic;
using AST;
using AST.Visitors;
using FluentIL;
using FluentIL.Infos;
using TypeSystem;

public class ILGenerator : BaseAstVisitor
{
    public Stack<DynamicAssemblyInfo> AssemblyBuilders { get; set; }
    public Stack<DynamicTypeInfo> ClassBuilders { get; set; }
    public override void EnterAssembly(Assembly ctx)
    {
        AssemblyBuilders.Push(IL.NewAssembly(ctx.Name));
    }

    public override void LeaveAssembly(Assembly ctx)
    {
        CurrentAsmBuilder.Save();
        AssemblyBuilders.Pop();
    }

    public override void EnterClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Push(CurrentAsmBuilder.WithType(ctx.Name));
    }

    public override void LeaveClassDefinition(ClassDefinition ctx)
    {
        ClassBuilders.Pop();
    }

    public override void EnterPropertyDefinition(PropertyDefinition ctx)
    {
        // var t = ctx.TypeId.Lookup();
        //
        // CurrentClassBuilder.WithProperty(ctx.Name, t, null, null);
    }

    public override void LeavePropertyDefinition(PropertyDefinition ctx) => base.LeavePropertyDefinition(ctx);

    public DynamicTypeInfo CurrentClassBuilder
    {
        get
        {
            return ClassBuilders.Peek();
        }
    }
    public DynamicAssemblyInfo CurrentAsmBuilder
    {
        get
        {
            return AssemblyBuilders.Peek();
        }
    }
}
