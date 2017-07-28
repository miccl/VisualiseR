using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Represents a medium for code fragments.
    /// </summary>
    public interface ICodeMedium : IMedium
    {
        /// <summary>
        /// List of <see cref="ICode"/>.
        /// </summary>
        List<ICode> CodeFragments { get; set; }

        /// <summary>
        /// Adds a code fragment from the medium.
        /// </summary>
        /// <param name="code"></param>
        void AddCodeFragment(ICode code);

        /// <summary>
        /// Remmoves a code fragment from the medium.
        /// </summary>
        /// <param name="code"></param>
        void RemoveCodeFragment(ICode code);

        /// <summary>
        /// Returns the code fragment on given position.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        ICode GetCodeFragment(int pos);

        /// <summary>
        /// Returns the position of the code fragment.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetCodeFragmentPos(ICode code);

        /// <summary>
        /// Returns all <see cref="ICode"/> with given rate.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        List<ICode> GetCodeFragmentsWithRate(Rate rate);

        /// <summary>
        /// Returns <c>true</c>, if it is not initialised yet.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}