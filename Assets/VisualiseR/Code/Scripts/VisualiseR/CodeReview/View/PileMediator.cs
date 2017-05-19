using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class PileMediator : Mediator
    {
        [Inject]
        public PileView View { get; set; }

        [Inject]
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        public override void OnRegister()
        {
            CodeRatingChangedSignal.AddListener(OnCodeRatingChangedSignal);
        }


        public override void OnRemove()
        {
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChangedSignal);
        }

        private void OnCodeRatingChangedSignal(Code code)
        {
            if (View._rate.Equals(code.Rate))
            {
                View.AddCode(code);
            }
        }
    }
}