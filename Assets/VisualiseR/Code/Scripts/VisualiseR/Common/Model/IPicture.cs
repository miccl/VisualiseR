namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a picture.
    /// </summary>
    public interface IPicture
    {
        /// <summary>
        /// Title of the picture.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// File path of the picture.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Returns <c>true</c>, if the object is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        string ToString();

    }
}