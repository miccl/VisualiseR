using NUnit.Framework;

namespace VisualiseR.Common
{
    [TestFixture]
    public class DownloadUtilTest
    {
        [TestFixture]
        public class CheckUrlValidMethod
        {
            [Test]
            public void checkInvalidUrls()
            {
                //given
                string invalidUrl = "asd.de";

                //then
                Assert.IsFalse(DownloadUtil.CheckUrlValid(invalidUrl));
            }

            [Test]
            public void checkValidUrls()
            {
                //given
                string validUrl = "http://www.google.de";

                //then
                Assert.IsTrue(DownloadUtil.CheckUrlValid(validUrl));
            }

        }


    }
}