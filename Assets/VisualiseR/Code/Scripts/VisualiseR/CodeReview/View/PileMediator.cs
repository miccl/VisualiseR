using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Mediator for the <see cref="PileView"/>
    /// </summary>
    public class PileMediator : Mediator
    {
        [Inject]
        public PileView _view { get; set; }

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
            _view.RatePileSelectedSignal.AddListener(OnRatePileSelected);
            PileSelectedSignal.AddListener(OnPileSelected);
        }

        private void OnPileSelected(Rate rate)
        {
            _view.RatePileSelected(rate.Equals(_view._rate));
        }


        public override void OnRemove()
        {
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChanged);
            RemoveCodeSignal.RemoveListener(OnRemoveCode);
            _view.RatePileSelectedSignal.RemoveListener(OnRatePileSelected);
        }

        private void OnRatePileSelected(Rate rate)
        {
            PileSelectedSignal.Dispatch(rate);
        }

        private void OnCodeRatingChanged(Code code)
        {
            if (_view._rate.Equals(code.Rate))
            {
                _view.AddCode(code);
            }
            else if (_view._codes.Contains(code))
            {
                _view.RemoveCode(code);
            }
        }

        private void OnRemoveCode(Code code)
        {
            if (_view._rate.Equals(code.Rate))
            {
                _view.RemoveCode(code);
            }
        }
    }
}