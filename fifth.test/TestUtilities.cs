namespace Fifth.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class TestUtilities
    {
        public static Task<List<string>> BuildRunAndTestProgram(string sourceFile)
        {
            List<string> programOutputs = new();

            if (Program.TryCompile(sourceFile, out var assemblyFilename))
            {
                try
                {
                    if (File.Exists(assemblyFilename))
                    {
                        GeneralHelpers.ExecOnUnix($"chmod 777 {assemblyFilename}");
                    }

                    var (result, stdOutputs, stdErrors) = GeneralHelpers.RunProcessVerbosely(assemblyFilename, true);
                    if (result == 0)
                    {
                        programOutputs.AddRange(stdOutputs ?? new List<string>());
                    }
                    else
                    {
                        programOutputs.AddRange(stdErrors ?? new List<string>());
                    }
                }
                finally
                {
                    Program.DeleteFile(Path.ChangeExtension(assemblyFilename, "pdb"));
                    Program.DeleteFile(Path.ChangeExtension(assemblyFilename, "dll"));
                    Program.DeleteFile(assemblyFilename);
                }
            }

            return Task.FromResult(programOutputs);
        }

        public static async Task<List<string>> BuildRunAndTestProgramInResource(string resourceName)
        {
            using var f = LoadTestResource(resourceName);
            return await BuildRunAndTestProgram(f.Path);
        }

        public static async Task<List<string>> BuildRunAndTestProgramInString(string source)
        {
            string sourcePath = null;
            try
            {
                sourcePath = await CopyStringToTempFile(source);
                return await BuildRunAndTestProgram(sourcePath);
            }
            finally
            {
                Program.DeleteFile(sourcePath);
            }
        }

        public static async Task<string> CopyEmbeddedResourceToTempFile(Type t, string fullName)
        {
            var content = await LoadEmbeddedResourceToString(t, fullName);
            return await CopyStringToTempFile(content);
        }

        public static async Task<string> CopyStringToTempFile(string content)
        {
            var p = Path.GetTempFileName();
            using var s = File.OpenWrite(p);
            using var sw = new StreamWriter(s);
            await sw.WriteAsync(content.ToCharArray());
            return p;
        }

        public static Task<string> CopyTestResourceToTempFile(string fullName)
            => CopyEmbeddedResourceToTempFile(typeof(TestUtilities), fullName);

        public static Task<string> LoadEmbeddedResourceToString(Type t, string fullName)
        {
            var resourceStream = t.Assembly.GetManifestResourceStream(fullName);
            using var reader = new StreamReader(resourceStream, Encoding.UTF8);
            return reader.ReadToEndAsync();
        }

        public static TempFile LoadTestResource(string fullName)
        {
            var stream = typeof(TestUtilities).Assembly
                                              .GetManifestResourceStream(fullName);
            var sr = new StreamReader(stream);
            return new TempFile(sr);
        }
    }
}
