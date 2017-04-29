using NUnit.Framework;

namespace VisualiseR.Common
{
    [TestFixture]
    public class FileUtilTest
    {
        [TestFixture]
        public class IsImageFileMethod
        {
            [Test]
            public void ValidImageFile()
            {
                //given
                const string jpgFile = "C:/image.jpg";
                const string jpegFile = "C:/image.jpeg";
                const string pngFile = "C:/image.png";

                //when


                //then
                Assert.IsTrue(FileUtil.IsImageFile(jpgFile));
                Assert.IsTrue(FileUtil.IsImageFile(jpegFile));
                Assert.IsTrue(FileUtil.IsImageFile(pngFile));
            }

            [Test]
            public void NoImageFile()
            {
                //given
                const string javaCode = "C:/code.java";
                const string pdfFile = "C:/file.pdf";


               //when


                //then
                Assert.False(FileUtil.IsImageFile(javaCode));
                Assert.False(FileUtil.IsImageFile(pdfFile));

            }
        }

        [TestFixture]
        public class IsCodeFileMethod
        {
            [Test]
            public void ValidCodeFile()
            {
                //given
                const string javaCode = "C:/code.java";
                const string csharpCode = "C:/code.cs";
                const string pythonCode = "C:/code.py";

                //when


                //then
                Assert.IsTrue(FileUtil.IsCodeFile(javaCode));
                Assert.IsTrue(FileUtil.IsCodeFile(csharpCode));
                Assert.IsTrue(FileUtil.IsCodeFile(pythonCode));
            }

            [Test]
            public void NoCodeFile()
            {
                //given
                const string jpgFile = "C:/image.jpg";
                const string pdfFile = "C:/file.pdf";

                //when


                //then
                Assert.IsFalse(FileUtil.IsCodeFile(jpgFile));
                Assert.IsFalse(FileUtil.IsCodeFile(pdfFile));

            }
        }

        [TestFixture]
        public class IsPdfFileMethod
        {
            [Test]
            public void ValidPdfFile()
            {
                //given
                const string pdfFile = "C:/file.pdf";

                //when


                //then
                Assert.IsTrue(FileUtil.IsCodeFile(pdfFile));
            }

            [Test]
            public void NoPdfFile()
            {
                //given
                const string jpgFile = "C:/image.jpg";
                const string javaCode = "C:/code.java";

                //when


                //then
                Assert.IsFalse(FileUtil.IsCodeFile(jpgFile));
                Assert.IsFalse(FileUtil.IsCodeFile(javaCode));

            }
        }
    }
}