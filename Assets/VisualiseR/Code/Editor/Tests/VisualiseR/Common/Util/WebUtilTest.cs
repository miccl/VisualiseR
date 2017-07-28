using NUnit.Framework;
using VisualiseR.Util;

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
    }
}