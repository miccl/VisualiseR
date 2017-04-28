using NUnit.Framework;

namespace VisualiseR.Common
{
    [TestFixture]
    public class DownloadUtilTest
    {
        [Test]
        public void testCheckUrlValid()
        {
            //given
            string invalidUrl = "asd.de";
            string validUrl = "http://www.google.de";

            //then
            Assert.IsFalse(DownloadUtil.CheckUrlValid(invalidUrl));
            Assert.IsTrue(DownloadUtil.CheckUrlValid(validUrl));
        }


    }
}