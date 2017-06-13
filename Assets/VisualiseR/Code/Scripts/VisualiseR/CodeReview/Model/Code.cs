using System;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class Code : ICode
    {
        public string Name { get; set; }
        public string OldPath { get; set; }
        public IPicture Pic { get; set; }
        public Rate Rate { get; set; }
        public string Comment { get; set; }

        public Code()
        {
            Rate = Rate.Unrated;
        }

        public string SaveCommentToTxt()
        {
            string text = "";
            text += string.Format("Name: {0} {1}", Name, Environment.NewLine);
            text += string.Format(" - Rating: {0} {1}", Rate, Environment.NewLine);
            text += string.Format(" - Comment: {0} {1}", Comment, Environment.NewLine);
            return text;
        }

        public override string ToString()
        {
            return string.Format("Code [Name: {0}, Rate: {1}, Comment: {2}]", Name, Rate, Comment);
        }
    }
}