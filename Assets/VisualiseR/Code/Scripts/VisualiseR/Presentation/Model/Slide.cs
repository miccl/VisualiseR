using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class Slide : ISlide
    {
        public string Name { get; set; }
        public IPicture Pic { get; set; }
        public string Note { get; set; }
    }
}