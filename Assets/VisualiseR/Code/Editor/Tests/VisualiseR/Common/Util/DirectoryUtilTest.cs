using System;
using System.IO;
using NUnit.Framework;

namespace VisualiseR.Common
{
    public class DirectoryUtilTest
    {
        [TestFixture]
        public class IsValidDirectoryMethod
        {
            [Test]
            public void InValidDirectory()
            {
                //given
                string invalidDirectoryPath = "asdasdaca";

                //when

                //then
                Assert.IsFalse(DirectoryUtil.IsValidNotEmptyDirectory(invalidDirectoryPath));
            }

            [Test]
            public void EmptyDirectory()
            {
                //given
                string invalidDirectoryPath = "D:/VisualiseR_Test/EmptyDirectory";

                //when

                //then
                Assert.IsFalse(DirectoryUtil.IsValidNotEmptyDirectory(invalidDirectoryPath));
            }

            [Test]
            public void ValidDirectory()
            {
                //given
                string invalidDirectoryPath = "D:/Downloads/VisualiseR_Test/FullDirectory";

                //when

                //then
                Assert.IsTrue(DirectoryUtil.IsValidNotEmptyDirectory(invalidDirectoryPath));
            }
        }
    }

    [TestFixture]
    public class IsDirectoryEmptyMethod
    {
        [Test]
        public void EmptyDirectory()
        {
            //given
            string directoryPath = "D:/Downloads/VisualiseR_Test/EmptyDirectory";

            //when

            //then
            Assert.IsTrue(DirectoryUtil.IsDirectoryEmpty(directoryPath));
        }

        [Test]
        public void FullDirectory()
        {
            //given
            string directoryPath = "D:/Downloads/VisualiseR_Test/FullDirectory";

            //when

            //then
            Assert.IsFalse(DirectoryUtil.IsDirectoryEmpty(directoryPath));
        }
    }

    [TestFixture]
    public class CreateDirectorysForCodeReview
    {
        [Test]
        public void HappyPath()
        {
             //given
            string mediumName = "D:/Downloads/VisualiseR_Test/CodeReview" + DateTime.Now.Ticks;
            string unratedDirName = "D:/Downloads/VisualiseR_Test/CodeReview/Unrated";
            string uncriticalDirName = "D:/Downloads/VisualiseR_Test/CodeReview/Uncritical";
            string minorDirName = "D:/Downloads/VisualiseR_Test/CodeReview/Minor";
            string criticalDirName = "D:/Downloads/VisualiseR_Test/CodeReview/Critical";

            //when
            var mainDirInfo = DirectoryUtil.CreateDirectorysForCodeReview(mediumName);

            Assert.IsTrue(Directory.Exists(mediumName), "Directory '{0}' should exist ", mediumName);
            Assert.IsTrue(Directory.Exists(unratedDirName), "Directory '{0}' should exist ", unratedDirName);
            Assert.IsTrue(Directory.Exists(uncriticalDirName), "Directory '{0}' should exist ", uncriticalDirName);
            Assert.IsTrue(Directory.Exists(minorDirName), "Directory '{0}' should exist ", minorDirName);
            Assert.IsTrue(Directory.Exists(criticalDirName), "Directory '{0}' should exist ", criticalDirName);
            //then

        }

    }
}