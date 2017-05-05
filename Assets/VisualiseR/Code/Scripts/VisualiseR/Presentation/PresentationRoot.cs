using strange.extensions.context.impl;

namespace VisualiseR.Presentation
{
    public class PresentationRoot : ContextView
    {
        void Awake()
        {
            context = new PresentationContext(this);
        }
    }
}