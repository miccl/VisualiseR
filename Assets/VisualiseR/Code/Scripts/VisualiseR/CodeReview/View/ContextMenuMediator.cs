using System;
using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class ContextMenuMediator : Mediator
    {
        [Inject]
        public ContextMenuView _view { get; set; }

        [Inject]
        public SelectedCodeRatingSignal SelectedCodeRatingSignal { get; set; }

        [Inject]
        public SaveCommentSignal SaveCommentSignal { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }

        [Inject]
        public RemoveCodeSignal RemoveCodeSignal { get; set; }


        public override void OnRegister()
        {
            _view.CodeRatingSelected.AddListener(OnRateSelected);
            _view.CommentSaveButtonClickedSignal.AddListener(OnCommentAdded);
            _view.OnContextMenuCanceled.AddListener(OnContextMenuCanceled);
            _view.RemoveCodeSignal.AddListener(OnRemoveCode);
        }

        public override void OnRemove()
        {
            _view.CodeRatingSelected.RemoveListener(OnRateSelected);
            _view.CommentSaveButtonClickedSignal.RemoveListener(OnCommentAdded);
            _view.OnContextMenuCanceled.RemoveListener(OnContextMenuCanceled);
            _view.RemoveCodeSignal.RemoveListener(OnRemoveCode);
        }

        private void OnContextMenuCanceled()
        {
            ContextMenuCanceledSignal.Dispatch();
        }

        private void OnCommentAdded(Code code, string text)
        {
            throw new NotImplementedException();
        }

        private void OnRateSelected(Code code, Rate rate)
        {
            SelectedCodeRatingSignal.Dispatch(code, rate);
        }

        private void OnRemoveCode(Code code)
        {
            RemoveCodeSignal.Dispatch(code);
        }
    }
}