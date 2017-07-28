using strange.extensions.context.impl;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Root for the code review scene.
    /// </summary>
    public class CodeReviewRoot : ContextView
    {
        void Awake()
        {
            context = new CodeReviewContext(this);
        }
    }
}