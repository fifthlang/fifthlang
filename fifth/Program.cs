namespace Fifth
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using CodeGeneration.LangProcessingPhases;

    // ReSharper disable once UnusedType.Global
    /// <summary>
    /// Main CLI entry-point to the Fifth Compiler.
    /// </summary>
    public class Program
    {
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
        /// Entry-point to compiler
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<int> Main(string fileName, string[] args)
        {
            _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _ = args ?? throw new ArgumentNullException(nameof(args));
            if (TryCompile(fileName, out var assemblyFilename))
            {
                return await ExecuteAssemblyAsync(assemblyFilename);
            }

            return 1;
        }

        /// <summary>
        /// Try to compile the source file to an assembly
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="assemblyFilename"></param>
        /// <returns></returns>
        public static bool TryCompile(string sourceFilename, out string assemblyFilename)
        {
            var ilFilename = Path.ChangeExtension(sourceFilename, ".il");
            try
            {
                assemblyFilename = Path.ChangeExtension(sourceFilename, ".exe");
                if (FifthParserManager.TryParseFile<AST.Assembly>(sourceFilename, out var ast, out var errors))
                {
                    using (var writer = File.CreateText(ilFilename))
                    {
                        var codeGenVisitor = new CodeGenVisitor(writer);
                        codeGenVisitor.VisitAssembly(ast);
                    }

                    var ilasmPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe";
                    var (result, stdOutputs, stdErrors) = GeneralHelpers.RunProcess(ilasmPath, ilFilename,
                        "/DEBUG",
                        "/EXE",
                        "/NOLOGO",
                        $"/OUTPUT={assemblyFilename}");

                    if (result != 0)
                    {
                        Console.Write(stdOutputs.Join(s => s, "\n"));
                        Console.Write(stdErrors.Join(s => s, "\n"));
                    }

                    return result == 0;
                }

                return false;
            }
            finally
            {
                DeleteFile(ilFilename);
                DeleteFile(Path.ChangeExtension(sourceFilename, ".tmp"));
            }
        }

        private static void DeleteFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
