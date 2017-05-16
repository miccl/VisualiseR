using System.IO;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenMediator : Mediator
    {
        [Inject]
        public CodeReviewScreenView _view { get; set; }


        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }



        public override void OnRegister()
        {
            ContextMenuCanceledSignal.AddListener(OnContextMenuCanceled);
        }


        public override void OnRemove()
        {
            ContextMenuCanceledSignal.RemoveListener(OnContextMenuCanceled);
        }

        public void OnCodeChanged(ICode code)
        {
            _view.ChangeCode(code);
        }




        private void OnContextMenuCanceled()
        {
            _view.IsContextMenuShown = false;
        }
    }
}