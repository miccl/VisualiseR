using strange.extensions.mediation.impl;

namespace VisualiseR.Presentation
{
    public class PresentationSceneMenuMediator : Mediator
    {
        [Inject]
        public PresentationSceneMenuView _view { get; set; }

        public override void OnRegister()
        {

        }

        public override void OnRemove()
        {

        }
    }
}