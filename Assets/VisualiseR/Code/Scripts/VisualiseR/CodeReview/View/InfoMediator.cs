using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class InfoMediator : Mediator
    {
        [Inject]
        public InfoView View { get; set; }


        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }
    }
}