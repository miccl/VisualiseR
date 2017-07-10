using System;

namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a picture.
    /// </summary>
    [Serializable]
    public class Picture : IPicture
    {
        /// <summary>
        /// Title of the picture.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// File path of the picture.
        /// </summary>
        public string Path { get; set; }

        public Picture()
        {
        }

        /// <summary>
        /// Returns <c>true</c>, if the object is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Title) || String.IsNullOrEmpty(Path);
        }

        /// <summary>
        /// Returns important info of the picture.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Picture [Title: {0}, Path: {1}]", Title, Path);
        }
    }
}