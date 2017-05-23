using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public interface ICode
    {
        string Name { get; set; }
        string Path { get; set; }
        IPicture Pic { get; set; }
        Rate Rate { get; set; }
        string Comment { get; set; }

        void saveCommentToTxt();
    }
}