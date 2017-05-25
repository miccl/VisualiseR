using System;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class Code : ICode
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IPicture Pic { get; set; }
        public Rate Rate { get; set; }
        public string Comment { get; set; }

        public Code()
        {
            Rate = Rate.Unrated;
        }

        public void saveCommentToTxt()
        {
            throw new NotImplementedException();
        }
    }
}