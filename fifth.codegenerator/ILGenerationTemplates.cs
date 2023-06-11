namespace Fifth.CodeGeneration;

using System;
using System.Reflection;
using System.Threading.Tasks;
using RazorLight;

// https://github.com/toddams/RazorLight https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-7.0#Razor%20Syntax

public class ILGenerationTemplates
{
    public ILGenerationTemplates()
    {
        //Engine = new RazorLightEngineBuilder()
        //             .UseEmbeddedResourcesProject(typeof(TypeMappings))
        //             .UseMemoryCachingProvider()
        //             .SetOperatingAssembly(Assembly.GetExecutingAssembly())
        //             .Build();
        Engine = new RazorLightEngineBuilder()
           .UseEmbeddedResourcesProject(typeof(ILGenerationTemplates).Assembly, "Fifth.CodeGeneration.ILTemplates")
           .UseMemoryCachingProvider()
           .Build();
    }

    public RazorLightEngine Engine { get; }

    public async Task<string> ProcessTemplateAsync(AssemblyDeclaration asm)
    {
        return await Engine.CompileRenderAsync("assembly.cshtml", asm);
    }
}
