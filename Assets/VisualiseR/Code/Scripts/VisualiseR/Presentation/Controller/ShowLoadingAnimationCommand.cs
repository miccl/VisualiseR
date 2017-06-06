using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.UI;

namespace VisualiseR.Presentation
{
    public class ShowLoadingAnimationCommand : Command
    {
        [Inject]
        public bool _show { get; set; }
        
        [Inject]
        public string _text { get; set; }
        
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
            if (_show)
            {
                var label = loadingCanvas.GetComponentInChildren<Text>();
                label.text = _text;
            }
        }
    }
}