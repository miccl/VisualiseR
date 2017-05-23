using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class InfoMediator : Mediator
    {
        [Inject]
        public InfoView _view { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }


        public override void OnRegister()
        {
            NextCodeSignal.AddListener(OnNextCode);
        }

        public override void OnRemove()
        {
            NextCodeSignal.RemoveListener(OnNextCode);
        }

        private void OnNextCode(Code code)
        {
            _view.UpdateView(code);
        }
    }
}