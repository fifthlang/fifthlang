namespace Fifth;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class GeneralHelpers
{
    public static void ExecOnUnix(string cmd)
    {
        if (Environment.OSVersion.Platform != PlatformID.Unix)
        {
            return;
        }
        var escapedArgs = cmd.Replace("\"", "\\\"");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "/bin/bash",
                Arguments = $"-c \"{escapedArgs}\""
            }
        };

        process.Start();
        process.WaitForExit();
    }

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
        // if (Environment.OSVersion.Platform == PlatformID.Unix)
        // {
        //     ExecOnUnix($"{executable} {string.Join(", ", args)}");
        //     return (0, new List<string>(), new List<string>());
        // }

        return RunProcessVerbosely(executable, true, args);
    }

    public static (int errorCode, List<string> outputs, List<string> errors) RunProcessVerbosely(string executable, bool relayToStdOutErr, params string[] args)
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
                RedirectStandardOutput = relayToStdOutErr,
                RedirectStandardError = relayToStdOutErr,
                Arguments = string.Join(" ", args)
            };

            if (proc.Start())
            {
                proc.WaitForExit();
                if (relayToStdOutErr)
                {
                    stdOutputs.Add(proc.StandardOutput.ReadToEnd());
                    stdErrors.Add(proc.StandardError.ReadToEnd());
                }

                result = proc.ExitCode;
            }
            else
            {
                Console.Write("Error running process");
            }
        }

        return (result, stdOutputs, stdErrors);
    }
}
