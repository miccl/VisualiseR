using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Presentation
{
    public class ShowLoadingAnimationCommand : Command
    {
        [Inject]
        public bool _show { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            ShowLoadingAnimation();
        }

        private void ShowLoadingAnimation()
        {
            var loadingCanvas = _contextView.transform.Find("LoadingCanvas");
            loadingCanvas.gameObject.SetActive(_show);
        }
    }
}