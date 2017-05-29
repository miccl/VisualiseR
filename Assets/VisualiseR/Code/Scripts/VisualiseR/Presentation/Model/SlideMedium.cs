using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualiseR.Presentation
{
    [Serializable]
    public class SlideMedium : ISlideMedium
    {
        public string Name { get; set; }

        public List<ISlide> Slides { get; set; }

        public int CurrentPos { private set; get; }

        public SlideMedium()
        {
            Slides = new List<ISlide>();
            CurrentPos = 0;
        }

        public void AddSlide(ISlide slide)
        {
            Slides.Add(slide);
        }

        public void RemoveSlide(ISlide slide)
        {
            Slides.Remove(slide);
        }

        public ISlide GetSlide(int pos)
        {
            return Slides.ElementAt(pos);
        }

        public void SetCurrentSlide(ISlide slide)
        {
            CurrentPos = Slides.IndexOf(slide);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) && Slides.Count == 0;
        }

        public ISlide NextSlide()
        {
            CurrentPos = (CurrentPos + 1) % Slides.Count;
            return GetSlide(CurrentPos);
        }

        public ISlide PrevSlide()
        {
            CurrentPos = CurrentPos - 1;
            if (CurrentPos == -1)
            {
                CurrentPos = Slides.Count - 1;
            }
            return GetSlide(CurrentPos);
        }

        public ISlide FirstSlide()
        {
            CurrentPos = 0;
            return GetSlide(CurrentPos);
        }


        public ISlide LastSlide()
        {
            CurrentPos = Slides.Count - 1;
            return GetSlide(CurrentPos);
        }

        public ISlide CurrentSlide()
        {
            return Slides[CurrentPos];
        }
    }
}