using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Represents a code fragment.
    /// </summary>
    public interface ICode
    {
        /// <summary>
        /// Code name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// File path of the original file.
        /// </summary>
        string OldPath { get; set; }
        /// <summary>
        /// Picture of the code fragment.
        /// </summary>
        IPicture Pic { get; set; }
        /// <summary>
        /// Rate of the code fragment.
        /// </summary>
        Rate Rate { get; set; }
        /// <summary>
        /// Comment of the code fragment.
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Saves the comment and rating to a text file.
        /// </summary>
        /// <returns></returns>
        string SaveToTxt();
    }
}