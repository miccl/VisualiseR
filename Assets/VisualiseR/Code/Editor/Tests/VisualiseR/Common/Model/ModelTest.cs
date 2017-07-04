using System;
using NUnit.Framework;
using VisualiseR.CodeReview;
using VisualiseR.Util;

namespace Test.Common.Model
{
    public class ModelTest
    {
        [TestFixture]
        public class ConvertToTxtMethod
        {
            [Test]
            public void HappyPath()
            {
                //given
                Code code = new Code
                {
                    Name = "Test",
                    Rate = Rate.Critical,
                    Comment = "This is a comment"
                };

                //when
                var text = code.SaveToTxt();
                FileUtil.WriteFile(@"D:/Downloads/test.txt", text);

                //then
                StringAssert.AreEqualIgnoringCase("banane", text);
            }
        }
    }
}