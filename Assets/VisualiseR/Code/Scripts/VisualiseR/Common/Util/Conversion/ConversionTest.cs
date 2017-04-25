using System;
using System.IO;
using UnityEngine;

namespace VisualiseR.Common
{

    public class ConversionTest
    {

        public void readFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader("Test.txt"))
                {
                    string line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }

            }
            catch (Exception e)
            {
                throw new FileNotFoundException("The file could not be read", e);
            }
        }
    }
}