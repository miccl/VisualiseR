using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public interface ISlide
    {
        string Name { get; set; }
        IPicture Pic { get; set; }
        string Note { get; set; }
    }
}