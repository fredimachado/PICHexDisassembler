using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class CliTests
    {
        private static string GetCliExecutablePath()
        {
            // Resolve the CLI executable relative to the solution root so tests are stable
            var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", ".."));
            var configuration = "Debug";
            var targetFramework = "net10.0";
            var projectDir = Path.Combine(solutionRoot, "src", "PICHexDisassembler.Cli", "bin", configuration, targetFramework);
            var exeName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "PICHexDisassembler.Cli.exe"
                : "PICHexDisassembler.Cli";

            return Path.Combine(projectDir, exeName);
        }

        private static async Task<(int ExitCode, string StdOut, string StdErr)> RunCliAsync(string arguments)
        {
            var exePath = GetCliExecutablePath();

            if (!File.Exists(exePath))
            {
                throw new InvalidOperationException($"CLI executable not found at '{exePath}'. Run 'dotnet build src/PICHexDisassembler.Cli/PICHexDisassembler.Cli.csproj' before executing these tests.");
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();

            var stdOutTask = process.StandardOutput.ReadToEndAsync();
            var stdErrTask = process.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdOutTask, stdErrTask);
            process.WaitForExit();

            return (process.ExitCode, stdOutTask.Result.TrimEnd('\r', '\n'), stdErrTask.Result.TrimEnd('\r', '\n'));
        }

        [Fact]
        public async Task Run_WithoutArguments_ShowsUsageAndReturnsError()
        {
            var result = await RunCliAsync(string.Empty);

            Assert.Equal(1, result.ExitCode);
            Assert.Equal("Usage: PICHexDisassembler.Cli <firmware.hex>", result.StdErr);
        }

        [Fact]
        public async Task Run_WithTooManyArguments_ShowsUsageAndReturnsError()
        {
            var result = await RunCliAsync("a.hex b.hex");

            Assert.Equal(1, result.ExitCode);
            Assert.Equal("Usage: PICHexDisassembler.Cli <firmware.hex>", result.StdErr);
        }

        [Fact]
        public async Task Run_WithMissingFile_ShowsErrorAndReturnsErrorCode()
        {
            var missingPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".hex");

            var result = await RunCliAsync($"\"{missingPath}\"");

            Assert.Equal(1, result.ExitCode);
            Assert.Equal($"File not found: {missingPath}", result.StdErr);
        }

        [Fact(Skip = "Disabled in automated environment; enable locally to validate CLI output.")]
        public async Task Run_WithValidHexFile_WritesExpectedAssemblyToStdOut()
        {
            var tempFile = Path.GetTempFileName();

            try
            {
                var hex = """
                          :020000001228C4
                          :08000800A000030E8312A10009
                          """;
                File.WriteAllText(tempFile, hex.Replace("\n", Environment.NewLine));

                var result = await RunCliAsync($"\"{tempFile}\"");

                var expectedAsm = """
                                  GOTO 0x12
                                  MOVWF 0x20
                                  SWAPF STATUS, W
                                  BCF STATUS, RP0
                                  MOVWF 0x21
                                  """;

                Assert.Equal(expectedAsm, result.StdOut);
                Assert.Equal(string.Empty, result.StdErr);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}

