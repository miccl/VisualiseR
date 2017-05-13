using System;

namespace VisualiseR.Common
{
    [Serializable]
    public class Picture : IPicture
    {
        public string Title { get; set; }
        public string Path { get; set; }

        public Picture()
        {
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Title) || String.IsNullOrEmpty(Path);
        }

        public override string ToString()
        {
            return string.Format("Picture [Title: {0}, Path: {1}]", Title, Path);
        }
    }
}