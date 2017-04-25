using strange.extensions.context.impl;

namespace VisualiseR.Main
{
    public class MainRoot : ContextView
    {
        void Awake()
        {
            context = new MainContext(this);
        }
    }
}