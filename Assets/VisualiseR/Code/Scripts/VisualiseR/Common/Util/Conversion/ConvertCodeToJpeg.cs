using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VisualiseR.Common
{
    public class ConvertCodeToJpeg : ImageConversionStrategy
    {
        public override string Convert(string filePath)
        {
            List<string> lines = ReadFile(filePath);
//            string filePath = WriteFile(lines);
            return null;
        }

        private static List<string> ReadFile(string filePath)
        {
            int counter = 1;
            string line;
            List<string> lines = new List<string>();

            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                line = String.Format("{0:000} {1}", counter, line);
                Console.WriteLine(line);
                lines.Add(line);
                counter++;
            }

            file.Close();

            return lines;
        }

        private string WriteFile(List<string> lines)
        {
            return null;
        }
    }
}