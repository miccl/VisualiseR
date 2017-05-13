using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class MainMenuMediator : Mediator
    {
        [Inject]
        public MainMenuView _view { get; set; }

        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}