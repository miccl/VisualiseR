using System;
using System.IO;
using NUnit.Framework;

public class asd
{
    private string filePath = @"d:\temp\test.txt";

    [Test]
    public void SimpleAddition()
    {
        Assert.IsTrue(readFile("test.txt"));
    }


    public bool readFile(string fileName)
    {
        try
        {
            createFile();
            int count = 1;
            string readText = File.ReadAllText(filePath);
            string createText = "Hello and Welcone" + Environment.NewLine;
            File.WriteAllText(filePath, createText);
            return true;
        }
        catch (Exception e)
        {
            throw new FileNotFoundException("The file could not be read", e);
        }
    }

    private void createFile()
    {
        if (!File.Exists(filePath))
        {
            // Create a file to write to.
            string createText = "Hello and Welcome" + Environment.NewLine;
            File.WriteAllText(filePath, createText);
        }
    }
}