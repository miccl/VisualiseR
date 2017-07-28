using strange.extensions.context.impl;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Root for the presentation scene.
    /// </summary>
    public class ShowroomRoot : ContextView
    {
        void Awake()
        {
            context = new ShowroomContext(this);
        }
    }
}