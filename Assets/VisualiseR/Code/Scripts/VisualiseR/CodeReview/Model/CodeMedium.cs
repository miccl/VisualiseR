using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Represents a medium for code fragments.
    /// </summary>
    [Serializable]
    public class CodeMedium : ICodeMedium
    {
        /// <summary>
        /// Name of the code fragment.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of <see cref="ICode"/>.
        /// </summary>
        public List<ICode> CodeFragments { get; set; }

        /// <summary>
        /// Ctor of the code medium.
        /// </summary>
        public CodeMedium()
        {
            CodeFragments = new List<ICode>();
        }
        
        /// <summary>
        /// Adds a code fragment from the medium.
        /// </summary>
        /// <param name="code"></param>
        public void AddCodeFragment(ICode picture)
        {
            CodeFragments.Add(picture);
        }
        
        /// <summary>
        /// Remmoves a code fragment from the medium.
        /// </summary>
        /// <param name="code"></param>
        public void RemoveCodeFragment(ICode picture)
        {
            CodeFragments.Remove(picture);
        }

        /// <summary>
        /// Returns the code fragment on given position.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public ICode GetCodeFragment(int pos)
        {
            return CodeFragments.ElementAt(pos);
        }

        /// <summary>
        /// Returns the position of the code fragment.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetCodeFragmentPos(ICode code)
        {
            return CodeFragments.IndexOf(code);
        }

        /// <summary>
        /// Returns all <see cref="ICode"/> with given rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public List<ICode> GetCodeFragmentsWithRate(Rate rate)
        {
            return CodeFragments.Where(item => item.Rate.Equals(rate)).ToList();
        }

        /// <summary>
        /// Prints the <see cref="CodeMedium"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CodeMedium[Name: {0}, CodeFragments: [", Name);
            foreach (ICode pic in CodeFragments)
            {
                sb.Append(pic + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Returns <c>true</c>, if it is not initialised yet.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || CodeFragments.Count == 0;
        }
    }
}