using System;
using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class CodeReviewContextMenuMediator : Mediator
    {
        [Inject]
        public CodeReviewContextMenuView _view { get; set; }

        [Inject]
        public SelectCodeRatingSignal SelectCodeRatingSignal { get; set; }

        [Inject]
        public SaveCommentSignal SaveCommentSignal { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }

        [Inject]
        public RemoveCodeSignal RemoveCodeSignal { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }


        public override void OnRegister()
        {
            _view.CodeRatingSelected.AddListener(OnRateSelected);
            _view.CommentSaveButtonClickedSignal.AddListener(OnCommentAdded);
            _view.OnContextMenuCanceled.AddListener(OnContextMenuCanceled);
            _view.RemoveCodeSignal.AddListener(OnRemoveCode);
            NextCodeSignal.AddListener(OnNextCode);
        }

        public override void OnRemove()
        {
            _view.CodeRatingSelected.RemoveListener(OnRateSelected);
            _view.CommentSaveButtonClickedSignal.RemoveListener(OnCommentAdded);
            _view.OnContextMenuCanceled.RemoveListener(OnContextMenuCanceled);
            _view.RemoveCodeSignal.RemoveListener(OnRemoveCode);
            NextCodeSignal.RemoveListener(OnNextCode);
        }

        private void OnContextMenuCanceled()
        {
            ContextMenuCanceledSignal.Dispatch();
            Destroy(gameObject);
        }

        private void OnCommentAdded(Code code, string text)
        {
            SaveCommentSignal.Dispatch(code, text);
            OnContextMenuCanceled();
        }

        private void OnRateSelected(Code code, Rate rate)
        {
            SelectCodeRatingSignal.Dispatch(code, rate);
            OnContextMenuCanceled();
        }

        private void OnRemoveCode(Code code)
        {
            RemoveCodeSignal.Dispatch(code);
            OnContextMenuCanceled();
        }

        private void OnNextCode(Code code)
        {

            OnContextMenuCanceled();
        }
    }
}