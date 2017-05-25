using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public interface ICodeMedium : IMedium
    {
        List<ICode> CodeFragments { get; set; }

        void AddCodeFragment(ICode code);

        void RemoveCodeFragment(ICode code);

        ICode GetCodeFragment(int pos);

        int GetCodeFragmentPos(ICode code);

        List<ICode> GetCodeFragmentsWithRate(Rate rate);

        bool IsEmpty();
    }
}