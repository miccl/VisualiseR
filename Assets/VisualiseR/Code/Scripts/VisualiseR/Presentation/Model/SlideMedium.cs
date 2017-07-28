using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualiseR.Presentation
{
    [Serializable]
    public class SlideMedium : ISlideMedium
    {
        /// <summary>
        /// Name of the <see cref="SlideMedium"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of <see cref="ISlide"/>
        /// </summary>
        public List<ISlide> Slides { get; set; }

        /// <summary>
        /// Current position of the slide, when navigating through the slides.
        /// </summary>
        public int CurrentPos { private set; get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public SlideMedium()
        {
            Slides = new List<ISlide>();
            CurrentPos = 0;
        }

        /// <summary>
        /// Adds a single slide.
        /// </summary>
        /// <param name="slide"></param>
        public void AddSlide(ISlide slide)
        {
            Slides.Add(slide);
        }

        /// <summary>
        /// Removes a single slide.
        /// </summary>
        /// <param name="slide"></param>
        public void RemoveSlide(ISlide slide)
        {
            Slides.Remove(slide);
        }

        /// <summary>
        /// Retrieve the slide at the given position of the <see cref="Slides"/>.
        /// </summary>
        /// <param name="pos">position of the slide in the <see cref="Slides"/>.</param>
        /// <returns></returns>
        public ISlide GetSlide(int pos)
        {
            return Slides.ElementAt(pos);
        }

        /// <summary>
        /// Navigate to next slide.
        /// </summary>
        /// <returns></returns>
        public ISlide NextSlide()
        {
            CurrentPos = (CurrentPos + 1) % Slides.Count;
            return GetSlide(CurrentPos);
        }

        /// <summary>
        /// Navigate to previous slide.
        /// </summary>
        /// <returns>Returns the previous slide.</returns>
        public ISlide PrevSlide()
        {
            CurrentPos = CurrentPos - 1;
            if (CurrentPos == -1)
            {
                CurrentPos = Slides.Count - 1;
            }
            return GetSlide(CurrentPos);
        }

        /// <summary>
        /// Returns the first slide.
        /// </summary>
        /// <returns></returns>
        public ISlide FirstSlide()
        {
            CurrentPos = 0;
            return GetSlide(CurrentPos);
        }

        /// <summary>
        /// Returns the last slide.
        /// </summary>
        /// <returns></returns>
        public ISlide LastSlide()
        {
            CurrentPos = Slides.Count - 1;
            return GetSlide(CurrentPos);
        }

        /// <summary>
        /// Returns the current selected slide.
        /// </summary>
        /// <returns></returns>
        public ISlide CurrentSlide()
        {
            return Slides[CurrentPos];
        }

        /// <summary>
        /// Sets current slide to the given slide.
        /// Returns <code>true</code> if the current slide could be set to the current slide.
        /// Otherwise <returns>false</returns>.
        /// </summary>
        /// <param name="slide"></param>
        public bool SetCurrentSlide(ISlide slide)
        {
            var pos = Slides.IndexOf(slide);
            if (pos == -1) return false;
            
            CurrentPos = pos;
            return false;
        }

        /// <summary>
        /// Returns <c>true</c> if this list contains no elements.
        /// Otherwise returns <c>false</c>s
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) && Slides.Count == 0;
        }
    }
}