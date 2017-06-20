using strange.extensions.context.impl;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Root for the presentation scene.
    /// </summary>
    public class PresentationRoot : ContextView
    {
        void Awake()
        {
            context = new PresentationContext(this);
        }
    }
}