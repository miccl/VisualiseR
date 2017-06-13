using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public interface ICode
    {
        string Name { get; set; }
        string OldPath { get; set; }
        IPicture Pic { get; set; }
        Rate Rate { get; set; }
        string Comment { get; set; }

        string SaveCommentToTxt();
    }
}