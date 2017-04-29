using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class ScreenMediator : Mediator
    {
        [Inject]
        public ScreenView view { get; set; }

        [Inject]
        public MediumChangedSignal mediumChangedSignal { get; set; }

        public override void OnRegister()
        {
            mediumChangedSignal.AddListener(OnMediumChanged);
        }

        public override void OnRemove()
        {
            mediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        public void OnMediumChanged(Medium medium)
        {
            view.medium = medium;
            view.SetupMedium();
        }
    }
}