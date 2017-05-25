using System.Collections.Generic;

namespace VisualiseR.Presentation
{
    public interface ISlideMedium
    {
        string Name { get; set; }
        List<ISlide> Slides { get; set; }
        int CurrentPos { get; }

        void AddSlide(ISlide slide);

        void RemoveSlide(ISlide slide);

        ISlide GetSlide(int pos);

        ISlide NextSlide();

        ISlide PrevSlide();

        ISlide FirstSlide();

        ISlide LastSlide();

        ISlide CurrentSlide();

        void SetCurrentSlide(ISlide slide);

        bool IsEmpty();
    }
}