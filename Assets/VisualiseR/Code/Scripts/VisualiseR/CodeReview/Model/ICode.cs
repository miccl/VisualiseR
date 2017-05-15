﻿using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public interface ICode
    {
        string Name { get; set; }
        Picture Pic { get; set; }
        Rate Rate { get; set; }
        string Comment { get; set; }

        void saveCommentToTxt();
    }
}