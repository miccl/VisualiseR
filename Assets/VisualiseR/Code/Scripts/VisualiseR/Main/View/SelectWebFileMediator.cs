using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class SelectWebFileMediator : Mediator
    {
        [Inject]
        public SelectWebFileView _view { get; set; }


        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }
        
        [Inject]
        public SelectionCanceledSignal SelectionCanceledSignal { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }


        public override void OnRegister()
        {
            _view.UrlSelected.AddListener(OnUrlSelected);
            _view.CanceledSignal.AddListener(OnCanceled);
            _view._contextView = _contextView;
        }

        public override void OnRemove()
        {
            _view.UrlSelected.AddListener(OnUrlSelected);
            _view.CanceledSignal.RemoveListener(OnCanceled);

        }

        private void OnUrlSelected(string url)
        {
           LoadFilesSignal.Dispatch(url);
        }

        private void OnCanceled()
        {
            SelectionCanceledSignal.Dispatch();
        }
    }
}