using strange.extensions.context.impl;

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