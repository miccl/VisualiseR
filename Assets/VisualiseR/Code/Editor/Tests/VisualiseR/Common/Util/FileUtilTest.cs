using System.IO;
using NUnit.Framework;
using VisualiseR.Util;

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
                Assert.That(FileUtil.GetDirectoryPath(jpgFile), Is.EqualTo(expectedDirectory));
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
                Assert.That(FileUtil.GetFileNameWithExtension(jpgFile), Is.EqualTo(expectedFileName));
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
                const string expectedFileName = "C:\\directory\\image.jpeg";
                //when

                //then
                Assert.That(FileUtil.GetPathWithExtension(jpgFile, fileExtension), Is.EqualTo(expectedFileName));
            }
        }

        [TestFixture]
        public class CopyMethod
        {
            [Test]
            public void testSimple()
            {
                //given
                const string sourceFilePath = "D:\\Downloads\\VisualiseR_Test\\Copy\\test.txt";
                const string destDirPath = "D:\\Downloads\\VisualiseR_Test\\Copy\\testerino";
                const string destFilePath = "D:\\Downloads\\VisualiseR_Test\\Copy\\testerino\\test.txt";

                //when
                Assert.IsFalse(File.Exists(destDirPath));
                FileUtil.CopyFile(sourceFilePath, destDirPath);

                //then
                Assert.IsTrue(File.Exists(destFilePath));
                FileUtil.DeleleFile(destFilePath);
                Assert.IsFalse(File.Exists(destFilePath));

            }
        }

        [TestFixture]
        public class MoveFileMethod
        {
            [Test]
            public void HappyPath()
            {
                //given
                const string sourceFilePath = "D:\\Downloads\\VisualiseR_Test\\Move\\test.txt";
                FileUtil.CreateFileIfNotExists(sourceFilePath);
                const string destDirectoryPath = "D:\\Downloads\\VisualiseR_Test\\Move\\testerino";
                const string destFilePath= @"D:\Downloads\VisualiseR_Test\Move\testerino\test.txt";

                //when
                 Assert.IsTrue(File.Exists(sourceFilePath), "source file should exist before moving");
                 Assert.IsFalse(File.Exists(destFilePath), "dest file should not exist before moving");
                FileUtil.MoveFile(sourceFilePath, destDirectoryPath);

                //then
                Assert.IsFalse(File.Exists(sourceFilePath), "source file should not exist after moving");
                Assert.IsTrue(File.Exists(destFilePath), "dest file should exist after moving");
                FileUtil.DeleleFile(destFilePath);
                FileUtil.DeleleFile(sourceFilePath);
                Assert.IsFalse(File.Exists(destFilePath), "dest file should not exist");

            }
        }
    }
}