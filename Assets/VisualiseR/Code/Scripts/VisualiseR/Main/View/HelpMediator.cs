using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class HelpMediator : Mediator
    {
        [Inject]
        public HelpView _view { get; set; }

        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}