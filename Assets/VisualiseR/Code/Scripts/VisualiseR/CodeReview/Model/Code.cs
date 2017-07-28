using System;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Represents a code fragment.
    /// </summary>
    public class Code : ICode
    {
        /// <summary>
        /// Code name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// File path of the original file.
        /// </summary>
        public string OldPath { get; set; }
        /// <summary>
        /// Picture of the code fragment.
        /// </summary>
        public IPicture Pic { get; set; }
        /// <summary>
        /// Rate of the code fragment.
        /// </summary>
        public Rate Rate { get; set; }
        /// <summary>
        /// Comment of the code fragment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Ctor of the <see cref="Code"/>.
        /// </summary>
        public Code()
        {
            Rate = Rate.Unrated;
        }
        
        /// <summary>
        /// Saves the comment and rating to a text file.
        /// </summary>
        /// <returns></returns>
        public string SaveToTxt()
        {
            string text = "";
            text += string.Format("Name: {0} {1}", Name, Environment.NewLine);
            text += string.Format(" - Rating: {0} {1}", Rate, Environment.NewLine);
            text += string.Format(" - Comment: {0} {1}", Comment, Environment.NewLine);
            return text;
        }

        /// <summary>
        /// Prints a <see cref="Code"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Code [Name: {0}, Rate: {1}, Comment: {2}]", Name, Rate, Comment);
        }
    }
}