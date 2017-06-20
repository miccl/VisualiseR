using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Command to show the scene menu.
    /// </summary>
    public class ShowCodeReviewSceneMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowCodeReviewSceneMenuCommand));

        [Inject]
        public CodeMedium _codeMedium { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }
        
        [Inject]
        public CodeReviewSceneMenuIsShownSignal CodeReviewSceneMenuIsShownSignal { get; set; }

        public override void Execute()
        {
            ShowSceneMenu();
        }

        private void ShowSceneMenu()
        {
            Logger.InfoFormat("Showing scene menu");
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("CodeReviewSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(true);
            var sceneMenuView = sceneMenu.GetComponent<CodeReviewSceneMenuView>();
            sceneMenuView.Init(_codeMedium);
            CodeReviewSceneMenuIsShownSignal.Dispatch(true);
        }
    }
}