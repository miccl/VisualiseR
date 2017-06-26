using System;
using System.IO;
using NUnit.Framework;
using VisualiseR.Util;

namespace VisualiseR.Test.Util
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
            string unratedDirName = mediumName +  Path.DirectorySeparatorChar + "Unrated";
            string uncriticalDirName = mediumName +  Path.DirectorySeparatorChar +  "Uncritical";
            string minorDirName = mediumName +  Path.DirectorySeparatorChar +  "Minor";
            string criticalDirName = mediumName +  Path.DirectorySeparatorChar +  "Critical";
            
            Assert.IsFalse(Directory.Exists(mediumName), "Directory '{0}' should not exist ", mediumName);
            Assert.IsFalse(Directory.Exists(unratedDirName), "Directory '{0}' should not exist ", unratedDirName);
            Assert.IsFalse(Directory.Exists(uncriticalDirName), "Directory '{0}' should not exist ", uncriticalDirName);
            Assert.IsFalse(Directory.Exists(minorDirName), "Directory '{0}' should not exist ", minorDirName);
            Assert.IsFalse(Directory.Exists(criticalDirName), "Directory '{0}' should not exist ", criticalDirName);
            
            //when
            var mainDir = DirectoryUtil.CreateDirectorysForCodeReview(mediumName);
            
            //then
            Assert.IsTrue(Directory.Exists(mediumName), "Directory '{0}' should exist ", mediumName);
            Assert.IsTrue(Directory.Exists(unratedDirName), "Directory '{0}' should exist ", unratedDirName);
            Assert.IsTrue(Directory.Exists(uncriticalDirName), "Directory '{0}' should exist ", uncriticalDirName);
            Assert.IsTrue(Directory.Exists(minorDirName), "Directory '{0}' should exist ", minorDirName);
            Assert.IsTrue(Directory.Exists(criticalDirName), "Directory '{0}' should exist ", criticalDirName);

            DirectoryUtil.DeleteDirectory(unratedDirName);
            DirectoryUtil.DeleteDirectory(uncriticalDirName);
            DirectoryUtil.DeleteDirectory(minorDirName);
            DirectoryUtil.DeleteDirectory(criticalDirName);
            DirectoryUtil.DeleteDirectory(mediumName);

            Assert.IsFalse(Directory.Exists(mediumName), "Directory '{0}' should not exist ", mediumName);
            Assert.IsFalse(Directory.Exists(unratedDirName), "Directory '{0}' should not exist ", unratedDirName);
            Assert.IsFalse(Directory.Exists(uncriticalDirName), "Directory '{0}' should not exist ", uncriticalDirName);
            Assert.IsFalse(Directory.Exists(minorDirName), "Directory '{0}' should not exist ", minorDirName);
            Assert.IsFalse(Directory.Exists(criticalDirName), "Directory '{0}' should not exist ", criticalDirName);

        }
    }
}