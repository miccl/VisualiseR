using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{

    public class CodeReviewRoot : ContextView
    {
        void Awake()
        {
            context = new CodeReviewContext(this);
        }

    }
}