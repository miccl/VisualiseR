using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public interface ISlide
    {
        /// <summary>
        /// Name of the slide.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Picture of the slide.
        /// </summary>
        IPicture Pic { get; set; }
        /// <summary>
        /// Information note.
        /// </summary>
        string Note { get; set; }
    }
}