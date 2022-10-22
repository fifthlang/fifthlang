namespace Fifth;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeGeneration.IL;
using CodeGeneration.LangProcessingPhases;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Main CLI entry-point to the Fifth Compiler.
/// </summary>
public class Program
{
    /// <summary>
    /// Removes the file.
    /// </summary>
    /// <param name="filename">the file to delete</param>
    public static void DeleteFile(string filename)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(filename) && File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// Asynchronously execute assembly
    /// </summary>
    /// <param name="assemblyFilename"></param>
    /// <returns></returns>
    public static Task<int> ExecuteAssemblyAsync(string assemblyFilename)
    {
        var result = 0;
        using (var proc = new Process())
        {
            proc.StartInfo = new ProcessStartInfo(assemblyFilename)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(assemblyFilename)
            };
            proc.Start();
            proc.WaitForExit();
            result = proc.ExitCode;
        }

        return Task.FromResult(result);
    }

    /// <summary>
    /// Finds a local, compatible, version of ilasm to use in code generation process.
    /// </summary>
    /// <returns>path of compatible ilasm</returns>
    public static string FindIlasm()
    {
        var paths = new[]
        {
                "tools/osx-x64.ilasm",
                "ilasm.exe",
                "ilasm",
                "tools/ilasm.exe",
                "tools/ilasm",
                "runtimes/win-x64/ilasm.exe",
                "runtimes/linux-x64/ilasm",
            };

        IEnumerable<string> acceptablePaths = paths.Where(File.Exists);
        if (System.Environment.OSVersion.Platform == PlatformID.Unix)
        {
            acceptablePaths = paths.Where(p => !p.EndsWith(".exe")).Where(File.Exists);
        }
        else if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            acceptablePaths = paths.Where(p => p.EndsWith(".exe")).Where(File.Exists);
        }

        if (acceptablePaths.Any())
        {
            return acceptablePaths.First();
        }
        throw new RuntimeException("Unable to locate IL Assembler");
    }

    /// <summary>
    /// Entry-point to compiler
    /// </summary>
    /// <param name="fileName">the 5th source file to compile</param>
    /// <param name="output">the name of the executable to emit</param>
    /// <param name="cleanup">delete intermediate files after run</param>
    /// <param name="args">other args</param>
    /// <returns>exit code from entrypoint function</returns>
    /// <exception cref="ArgumentNullException">if source or args are null</exception>
    public static async Task<int> Main(string fileName, string output, bool cleanup, string[] args)
    {
        _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
        _ = args ?? throw new ArgumentNullException(nameof(args));
        var context = new CompilationContext(fileName, output, cleanup, args);
        if (TryCompile(context))
        {
            if (File.Exists(context.Output))
            {
                GeneralHelpers.ExecOnUnix($"chmod 777 {context.Output}");
                return await ExecuteAssemblyAsync(context.Output);
            }
        }

        return 1;
    }

    /// <summary>
    /// Try to compile the source file to an assembly
    /// </summary>
    /// <param name="ctx">details for coordinating the compilation process</param>
    /// <returns>true if successfully compiled, false otherwise.</returns>
    public static bool TryCompile(CompilationContext ctx)
    {
        var di = Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "build"));
        var filename = Path.GetFileName(ctx.Source);
        var ilFilename = Path.Combine(di.FullName, Path.ChangeExtension(filename, ".il"));
        //var ilFilename = Path.ChangeExtension(ctx.Source, ".il");
        try
        {
            if (string.IsNullOrWhiteSpace(ctx.Output))
            {
                ctx.Output = Path.ChangeExtension(ilFilename, ".exe");
            }

            if (FifthParserManager.TryParseFile<AST.Assembly>(ctx.Source, out var ast, out var errors))
            {
                using (var writer = File.CreateText(ilFilename))
                {
                    //var codeGenVisitor = new CodeGenVisitor(writer);
                    //codeGenVisitor.VisitAssembly(ast);
                    var mb = new ILModelBuilder();
                    _ = mb.Visit(ast);
                    var ilast = mb.CompletedAssemblies.FirstOrDefault();
                    var il = AssemblyDeclarationBuilder.Create(ilast).Build();
                    // var il = ilg.sb.ToString();
                    writer.Write(il);
                }

                var ilasmPath = FindIlasm();
                var (result, stdOutputs, stdErrors) = GeneralHelpers.RunProcess(ilasmPath,
                    ilFilename,
                    "-DEBUG", $"-OUTPUT={ctx.Output}");

                if (result != 0)
                {
                    Console.Write(stdOutputs.Join(s => s, "\n"));
                    Console.Write(stdErrors.Join(s => s, "\n"));
                }

                return result == 0;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ctx.Output = null;
            return false;
        }
        finally
        {
            if (ctx.Cleanup)
            {
                File.Delete(ilFilename);
            }
        }
    }
}

/// <summary>
/// details for coordinating the compilation process
/// </summary>
public struct CompilationContext
{
    /// <summary>
    /// create a context to corrdinate the compilation process
    /// </summary>
    /// <param name="source">the entrypoint source file to compile</param>
    /// <param name="output">the folder where output should be kept </param>
    /// <param name="cleanup">whether to cleanup intermediate files created during compilation</param>
    /// <param name="args">the CLI arg vector</param>
    public CompilationContext(string source, string output, bool cleanup, string[] args)
    {
        this.Source = source;
        this.Output = output;
        this.Cleanup = cleanup;
        this.Args = args;
    }

    /// <summary>
    /// the CLI arg vector
    /// </summary>
    public string[] Args { get; set; }

    /// <summary>
    /// whether to cleanup intermediate files created during compilation
    /// </summary>
    public bool Cleanup { get; set; }

    /// <summary>
    /// the folder where output should be kept
    /// </summary>
    public string Output { get; set; }

    /// <summary>
    /// the entrypoint source file to compile
    /// </summary>
    public string Source { get; set; }
}
