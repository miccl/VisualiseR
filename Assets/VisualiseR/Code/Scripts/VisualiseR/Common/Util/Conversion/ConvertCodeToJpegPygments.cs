using System;
using System.Diagnostics;
using System.IO;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    public class ConvertCodeToJpegPygments : ImageConversionStrategy
    {
        private const string OUTPUT_FORMAT = "jpeg";


        public override string Convert(string filePath)
        {
//            runCmd();
            return "";
        }


        private string convertASdasdasd()
        {
            return "";
        }


        private void run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "Cmd.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        public void runCmd(string filePath)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            var outputFormat = OUTPUT_FORMAT;
            var outputFile = FileUtil.GetPathWithExtension(filePath, OUTPUT_FORMAT);
            var inputFile = filePath;

            var pygmentizeCommand = String.Format("pygmentize -f {0} -o {1} {2}", outputFormat, outputFile, inputFile);

            cmd.StandardInput.WriteLine(pygmentizeCommand);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
}