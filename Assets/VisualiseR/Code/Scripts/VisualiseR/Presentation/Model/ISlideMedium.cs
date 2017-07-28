using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Medium for the presentation scene.
    /// </summary>
    public interface ISlideMedium : IMedium
    {
        /// <summary>
        /// List of <see cref="ISlide"/>
        /// </summary>
        List<ISlide> Slides { get; set; }
        
        /// <summary>
        /// Current position of the slide, when navigating through the slides.
        /// </summary>
        int CurrentPos { get; }
        
        /// <summary>
        /// Adds a single slide.
        /// </summary>
        /// <param name="slide"></param>
        void AddSlide(ISlide slide);

        /// <summary>
        /// Removes a single slide.
        /// </summary>
        /// <param name="slide"></param>
        void RemoveSlide(ISlide slide);

        /// <summary>
        /// Retrieve the slide at the given position of the <see cref="Slides"/>.
        /// </summary>
        /// <param name="pos">position of the slide in the <see cref="Slides"/>.</param>
        /// <returns></returns>
        ISlide GetSlide(int pos);

        /// <summary>
        /// Navigate to next slide.
        /// </summary>
        /// <returns></returns>
        ISlide NextSlide();

        /// <summary>
        /// Navigate to previous slide.
        /// </summary>
        /// <returns>Returns the previous slide.</returns>
        ISlide PrevSlide();

        /// <summary>
        /// Returns the first slide.
        /// </summary>
        /// <returns></returns>
        ISlide FirstSlide();

        /// <summary>
        /// Returns the last slide.
        /// </summary>
        /// <returns></returns>
        ISlide LastSlide();

        /// <summary>
        /// Returns the current selected slide.
        /// </summary>
        /// <returns></returns>
        ISlide CurrentSlide();

        /// <summary>
        /// Sets current slide to the given slide.
        /// Returns <code>true</code> if the current slide could be set to the current slide.
        /// Otherwise <returns>false</returns>.
        /// </summary>
        /// <param name="slide"></param>
        bool SetCurrentSlide(ISlide slide);

        /// <summary>
        /// Returns <c>true</c> if this list contains no elements.
        /// Otherwise returns <c>false</c>s
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}