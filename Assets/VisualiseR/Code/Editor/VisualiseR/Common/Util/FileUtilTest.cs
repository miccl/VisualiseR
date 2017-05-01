using NUnit.Framework;

namespace VisualiseR.Common
{
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
                Assert.IsTrue(FileUtil.IsPdfFile(pdfFile));
            }

            [Test]
            public void NoPdfFile()
            {
                //given
                const string jpgFile = "C:/image.jpg";
                const string javaCode = "C:/code.java";

                //when


                //then
                Assert.IsFalse(FileUtil.IsPdfFile(jpgFile));
                Assert.IsFalse(FileUtil.IsPdfFile(javaCode));

            }
        }

        [TestFixture]
        public class GetDirectoryMethod
        {
            [Test]
            public void TestGetDirectory()
            {
                //given
                const string jpgFile = "C:/directory/image.jpg";
                const string expectedDirectory = "C:/directory";
                //when

                //then
                Assert.That(FileUtil.GetDirectoryName(jpgFile), Is.EqualTo(expectedDirectory));
            }
        }

        [TestFixture]
        public class GetFileNameMethod
        {
            [Test]
            public void TestGetFileName()
            {
                //given
                const string jpgFile = "C:/directory/image.jpg";
                const string expectedFileName = "image.jpg";
                //when

                //then
                Assert.That(FileUtil.GetFileName(jpgFile), Is.EqualTo(expectedFileName));
            }
        }

        [TestFixture]
        public class GetFileNameWithExtensionMethod
        {
            [Test]
            public void TestGetFileNameWithExtension()
            {
                //given
                const string jpgFile = "C:/directory/image.java";
                const string fileExtension = "jpeg";
                const string expectedFileName = "C:/directory/image.java";
                //when

                //then
                Assert.That(FileUtil.GetPathWithExtension(jpgFile, fileExtension), Is.EqualTo(expectedFileName));
            }
        }
    }
}