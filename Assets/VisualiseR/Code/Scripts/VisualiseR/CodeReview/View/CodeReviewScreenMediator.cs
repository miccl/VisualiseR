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
        public CodeReviewContextMenuIsShownSignal CodeReviewContextMenuIsShownSignal { get; set; }
        
        [Inject]
        public CodeReviewSceneMenuIsShownSignal CodeReviewSceneMenuIsShownSignal { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }

        [Inject]
        public RemoveCodeSignal RemoveCodeSignal { get; set; }

        [Inject]
        public ShowCodeReviewContextMenuSignal ShowCodeReviewContextMenuSignal { get; set; }


        public override void OnRegister()
        {
            _view.NextCodeSignal.AddListener(OnNextCodeSignal);
            _view.ShowContextMenuSignal.AddListener(OnShowContextMenu);
            CodeReviewContextMenuIsShownSignal.AddListener(OnContextMenuIsShown);
            CodeReviewSceneMenuIsShownSignal.AddListener(OnSceneMenuIsShown);
        }

        public override void OnRemove()
        {
            _view.NextCodeSignal.RemoveListener(OnNextCodeSignal);
            _view.ShowContextMenuSignal.RemoveListener(OnShowContextMenu);
            CodeReviewContextMenuIsShownSignal.RemoveListener(OnContextMenuIsShown);
            CodeReviewSceneMenuIsShownSignal.RemoveListener(OnSceneMenuIsShown);

        }

        private void OnShowContextMenu(GameObject gameObject, Code code)
        {
            ShowCodeReviewContextMenuSignal.Dispatch(gameObject, code);
        }

        private void OnNextCodeSignal(Code code)
        {
            NextCodeSignal.Dispatch(code);
        }

        public void OnCodeChanged(ICode code)
        {
            _view.ChangeCode(code);
        }

        private void OnContextMenuIsShown(bool isShown)
        {
            _view._isContextMenuShown = isShown;
        }
        
        private void OnSceneMenuIsShown(bool isShown)
        {
            _view._isContextMenuShown = isShown;
        }

    }
}