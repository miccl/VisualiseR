﻿using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public interface ICodeMedium : IMedium
    {
        List<ICode> CodeFragments { get; set; }

        void AddCodeFragment(ICode code);

        void RemoveCodeFragment(ICode code);

        ICode GetCodeFragment(int pos);

        bool IsEmpty();
    }
}