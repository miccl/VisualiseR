using System;
using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class PresentationContextMenuMediator : Mediator
    {
        [Inject]
        public PresentationContextMenuView _view { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }


        public override void OnRegister()
        {
            _view.OnContextMenuCanceled.AddListener(OnContextMenuCanceled);
        }

        public override void OnRemove()
        {
            _view.OnContextMenuCanceled.RemoveListener(OnContextMenuCanceled);
        }

        private void OnContextMenuCanceled()
        {
            ContextMenuCanceledSignal.Dispatch();
            Destroy(gameObject);
        }
    }
}