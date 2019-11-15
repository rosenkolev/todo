#load "common.csx"

using System.Diagnostics;
using System.Runtime.InteropServices;

public static partial class MM
{
    public class Command
    {
        internal StringBuilder lastStandardErrorOutput = new StringBuilder();
        internal StringBuilder lastStandardOutput = new StringBuilder();
        internal Process process = new Process();

        public Command(string commandPath, string arguments, string workingDirectory, LogLevel outputLogLevel = LogLevel.Verbose)
        {
            WriteLine($"{commandPath} {arguments}", LogLevel.Debug);

            process.StartInfo = new ProcessStartInfo(commandPath);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;

            if (!string.IsNullOrEmpty(workingDirectory))
            {
                process.StartInfo.WorkingDirectory = workingDirectory;
            }

            process.ErrorDataReceived += (s, e) => Log(lastStandardErrorOutput, e.Data, outputLogLevel);
            process.OutputDataReceived += (s, e) => Log(lastStandardOutput, e.Data, outputLogLevel);
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            WriteLine("----------------------", outputLogLevel);
        }

        public string WaitForResult()
        {
            process.WaitForExit();
            
            return lastStandardOutput.ToString().Trim();;
        }

        public int ExitCode => process.ExitCode;

        public void FailWhenExitCode(int allowedExitCode) =>
            Assert.Truthy(process.ExitCode == allowedExitCode, lastStandardErrorOutput.ToString().Trim(), "Check if exit code is " + allowedExitCode.ToString());

        public static string Execute(string commandPath, string arguments, string workingDirectory = null, LogLevel outputLogLevel = LogLevel.Verbose)
        {
            var command = new Command(commandPath, arguments, workingDirectory, outputLogLevel);

            var result = command.WaitForResult();

            WriteLine($"Exit with status code " + command.ExitCode.ToString(), LogLevel.Debug);
            command.FailWhenExitCode(0);

            return result;
        }

        public static string ExecuteCommand(string commandText, string workingDirectory = null) => Execute("CMD", $"/C {commandText}", workingDirectory);

        private static void Log(StringBuilder output, string message, LogLevel level)
        {
            var msg = message?.TrimEnd() ?? string.Empty;
            WriteLine(msg, level);
            output.AppendLine(msg);
        }
    }
    
    /// <summary> Execute a system command. Example Cmd("/bin/bash -c ls").</summary>
    public static string Cmd(string command, string workingDirectory = null)
    {
        var index = command.IndexOf(' ');
        var commandName = command.Substring(0, index);
        var commandArgs = command.Substring(index + 1);
        return Command.Execute(commandName, commandArgs, workingDirectory);
    }

    public static string Shell(string command, string workingDirectory = null)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Command.Execute("/bin/bash", "-c " + command, workingDirectory);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Command.Execute("cmd.exe", "/C " + command, workingDirectory);
        }
        
        throw new Exception("Shell is not supported!");
    }
}