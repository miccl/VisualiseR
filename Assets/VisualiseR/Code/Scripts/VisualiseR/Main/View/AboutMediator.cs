using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class AboutMediator : Mediator
    {
        [Inject]
        public AboutView _view { get; set; }

        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}