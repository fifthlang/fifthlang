namespace Fifth
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class GeneralHelpers
    {
        public static void ForEach<T>(this IEnumerable<T> seq, Action<T> job)
        {
            foreach (var t in seq)
            {
                job(t);
            }
        }

        public static string Join<T>(this IEnumerable<T> seq, Func<T, string> accessor, string separator = ", ")
            => string.Join(separator, seq.Select(accessor).ToArray());

        public static IEnumerable<T> Map<T>(this IEnumerable<T> seq, Func<T, T> fn)
        {
            foreach (var t in seq)
            {
                yield return fn(t);
            }
        }

        public static (int errorCode, List<string> outputs, List<string> errors) RunProcess(string executable,
            params string[] args)
        {
            var result = 0;
            var stdErrors = new List<string>();
            var stdOutputs = new List<string>();
            using (var proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo(executable)
                {
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Arguments = string.Join(" ", args)
                };

                if (proc.Start())
                {
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        var line = proc.StandardOutput.ReadLine();
                        stdOutputs.Add(line);
                    }

                    while (!proc.StandardError.EndOfStream)
                    {
                        var line = proc.StandardError.ReadLine();
                        stdErrors.Add(line);
                    }

                    proc.WaitForExit();
                    result = proc.ExitCode;
                }
                else
                {
                    Console.Write("Error running ilasm");
                }
            }

            return (result, stdOutputs, stdErrors);
        }
    }
}
