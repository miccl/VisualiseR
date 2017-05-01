using NUnit.Framework;
using VisualiseR.Common;

namespace Test.Common
{
    [TestFixture]
    public class ConvertCodeToJpegTest
    {
        [Test]
        public void runCmdTest()
        {
            ConvertCodeToJpegPygments convert = new ConvertCodeToJpegPygments();
            convert.runCmd("D:/VisualiseR_Test/FullDirectory/test.py");
        }

        [Test]
        public void testConvert()
        {
            string filePath = "D:/VisualiseR_Test/FullDirectory/test.py";
            ConvertCodeToJpeg convert = new ConvertCodeToJpeg();
            convert.Convert(filePath);
        }

    }
}