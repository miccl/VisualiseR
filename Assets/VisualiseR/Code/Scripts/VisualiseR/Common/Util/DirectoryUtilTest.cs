using NUnit.Framework;

namespace VisualiseR.Common
{
    [TestFixture]
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
                Assert.IsFalse(DirectoryUtil.IsValidDirectory(invalidDirectoryPath));
            }

            [Test]
            public void EmptyDirectory()
            {
                //given
                string invalidDirectoryPath = "D:/VisualiseR_Test/EmptyDirectory";

                //when

                //then
                Assert.IsFalse(DirectoryUtil.IsValidDirectory(invalidDirectoryPath));
            }

            [Test]
            public void ValidDirectory()
            {
                //given
                string invalidDirectoryPath = "D:/VisualiseR_Test/FullDirectory";

                //when

                //then
                Assert.IsTrue(DirectoryUtil.IsValidDirectory(invalidDirectoryPath));
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
            string directoryPath = "D:/VisualiseR_Test/EmptyDirectory";

            //when

            //then
            Assert.IsTrue(DirectoryUtil.IsDirectoryEmpty(directoryPath));
        }

        [Test]
        public void FullDirectory()
        {
            //given
            string directoryPath = "D:/VisualiseR_Test/FullDirectory";

            //when

            //then
            Assert.IsFalse(DirectoryUtil.IsDirectoryEmpty(directoryPath));
        }
    }
}