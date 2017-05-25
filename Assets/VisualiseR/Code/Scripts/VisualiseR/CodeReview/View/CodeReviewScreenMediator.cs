using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenMediator : Mediator
    {
        [Inject]
        public CodeReviewScreenView _view { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }

        [Inject]
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }

        [Inject]
        public RemoveCodeSignal RemoveCodeSignal { get; set; }


        public override void OnRegister()
        {
            _view.NextCodeSignal.AddListener(OnNextCodeSignal);
            ContextMenuCanceledSignal.AddListener(OnContextMenuCanceled);
            CodeRatingChangedSignal.AddListener(OnCodeRatingChanged);
        }

        public override void OnRemove()
        {
            _view.NextCodeSignal.RemoveListener(OnNextCodeSignal);
            ContextMenuCanceledSignal.RemoveListener(OnContextMenuCanceled);
            CodeRatingChangedSignal.RemoveListener(OnCodeRatingChanged);

        }

        private void OnNextCodeSignal(Code code)
        {
            NextCodeSignal.Dispatch(code);
        }

        public void OnCodeChanged(ICode code)
        {
            _view.ChangeCode(code);
        }

        private void OnCodeRatingChanged(Code code)
        {
            if (_view._code.Equals(code))
            {
//                RemoveCodeSignal.Dispatch(code);
//                TODO verstecken und dem entsprechenden Stapel zuweisen
            }
        }


        private void OnContextMenuCanceled()
        {
            _view.IsContextMenuShown = false;
        }
    }
}