using NUnit.Framework;

namespace VisualiseR.Common
{
    public class WebUtilTest
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
                Assert.IsFalse(WebUtil.IsValidUrl(invalidUrl));
            }

            [Test]
            public void checkValidUrls()
            {
                //given
                string validUrl = "http://www.google.de";

                //then
                Assert.IsTrue(WebUtil.IsValidUrl(validUrl));
            }
        }

        [TestFixture]
        public class GetFileNameMethod
        {
            [Test]
            public void testGetFileName()
            {
                //given
                var url = "https://www.planwallpaper.com/static/images/desktop-year-of-the-tiger-images-wallpaper.jpg";
                var expectedFileName = "desktop-year-of-the-tiger-images-wallpaper.jpg";
                //then
                Assert.That(WebUtil.GetFileName(url), Is.EqualTo(expectedFileName));
            }
        }
    }
}