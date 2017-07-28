using strange.extensions.context.impl;

namespace VisualiseR.Main
{
    /// <summary>
    /// Root for the main scene.
    /// </summary>
    public class MainRoot : ContextView
    {
        void Awake()
        {
            context = new MainContext(this);
        }
    }
}