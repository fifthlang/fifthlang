namespace Fifth
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using AST;
    using CodeGeneration;
    using CodeGeneration.LangProcessingPhases;

    // ReSharper disable once UnusedType.Global
    public class Program
    {
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

        public static async Task<int> Main(string fileName, string[] args)
        {
            if (TryCompile(fileName, out var assemblyFilename))
            {
                return await ExecuteAssemblyAsync(assemblyFilename);
            }

            return 1;
        }

        public static bool TryCompile(string sourceFilename, out string assemblyFilename)
        {
            var ilFilename = Path.ChangeExtension(sourceFilename, ".il");
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
    }
}
/*
 Usage: ilasm [Options] <sourcefile> [Options]

Options:
/NOLOGO         Don't type the logo
/QUIET          Don't report assembly progress
/NOAUTOINHERIT  Disable inheriting from System.Object by default
/DLL            Compile to .dll
/EXE            Compile to .exe (default)
/PDB            Create the PDB file without enabling debug info tracking
/APPCONTAINER   Create an AppContainer exe or dll
/DEBUG          Disable JIT optimization, create PDB file, use sequence points from PDB
/DEBUG=IMPL     Disable JIT optimization, create PDB file, use implicit sequence points
/DEBUG=OPT      Enable JIT optimization, create PDB file, use implicit sequence points
/OPTIMIZE       Optimize long instructions to short
/FOLD           Fold the identical method bodies into one
/CLOCK          Measure and report compilation times
/RESOURCE=<res_file>    Link the specified resource file (*.res)
                        into resulting .exe or .dll
/OUTPUT=<targetfile>    Compile to file with specified name
                        (user must provide extension, if any)
/KEY=<keyfile>      Compile with strong signature
                        (<keyfile> contains private key)
/KEY=@<keysource>   Compile with strong signature
                        (<keysource> is the private key source name)
/INCLUDE=<path>     Set path to search for #include'd files
/SUBSYSTEM=<int>    Set Subsystem value in the NT Optional header
/SSVER=<int>.<int>  Set Subsystem version number in the NT Optional header
/FLAGS=<int>        Set CLR ImageFlags value in the CLR header
/ALIGNMENT=<int>    Set FileAlignment value in the NT Optional header
/BASE=<int>     Set ImageBase value in the NT Optional header (max 2GB for 32-bit images)
/STACK=<int>    Set SizeOfStackReserve value in the NT Optional header
/MDV=<version_string>   Set Metadata version string
/MSV=<int>.<int>   Set Metadata stream version (<major>.<minor>)
/PE64           Create a 64bit image (PE32+)
/HIGHENTROPYVA  Set High Entropy Virtual Address capable PE32+ images (default for /APPCONTAINER)
/NOCORSTUB      Suppress generation of CORExeMain stub
/STRIPRELOC     Indicate that no base relocations are needed
/ITANIUM        Target processor: Intel Itanium
/X64            Target processor: 64bit AMD processor
/ARM            Target processor: ARM processor
/32BITPREFERRED Create a 32BitPreferred image (PE32)
/ENC=<file>     Create Edit-and-Continue deltas from specified source file

Key may be '-' or '/'
Options are recognized by first 3 characters
Default source file extension is .il

Target defaults:
/PE64      => /PE64 /ITANIUM
/ITANIUM   => /PE64 /ITANIUM
/X64       => /PE64 /X64
 */
