using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class PileMediator : Mediator
    {
        [Inject]
        public PileView View { get; set; }

        [Inject]
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        [Inject]
        public RemoveCodeSignal RemoveCodeSignal { get; set; }

        [Inject]
        public PileSelectedSignal PileSelectedSignal { get; set; }

        public override void OnRegister()
        {
            CodeRatingChangedSignal.AddListener(OnCodeRatingChanged);
            RemoveCodeSignal.AddListener(OnRemoveCode);
            View.RatePileSelectedSignal.AddListener(OnRatePileSelected);
        }


        public override void OnRemove()
        {
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChanged);
            RemoveCodeSignal.RemoveListener(OnRemoveCode);
            View.RatePileSelectedSignal.RemoveListener(OnRatePileSelected);
        }

        private void OnRatePileSelected(Rate rate)
        {
            PileSelectedSignal.Dispatch(rate);
        }

        private void OnCodeRatingChanged(Code code)
        {
            if (View._rate.Equals(code.Rate))
            {
                View.AddCode(code);
            }
            else if (View._codes.Contains(code))
            {
                View.RemoveCode(code);
            }
        }

        private void OnRemoveCode(Code code)
        {
            if (View._rate.Equals(code.Rate))
            {
                View.RemoveCode(code);
            }
        }
    }
}