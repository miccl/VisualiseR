using System;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Presentation slide.
    /// </summary>
    [Serializable]
    public class Slide : ISlide
    {
        /// <summary>
        /// Name of the slide.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Picture of the slide.
        /// </summary>
        public IPicture Pic { get; set; }
        /// <summary>
        /// Information note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Prints <see cref="Slide"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Slide[Name: {0}, Pic: {1}, Note: {2}]", Name, Pic, Note);
        }
    }
}