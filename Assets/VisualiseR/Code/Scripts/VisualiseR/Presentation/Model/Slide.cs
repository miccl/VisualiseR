using System;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    [Serializable]
    public class Slide : ISlide
    {
        public string Name { get; set; }
        public IPicture Pic { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            return string.Format("Slide[Name: {0}, Pic: {1}, Note: {2}]", Name, Pic, Note);
        }
    }
}