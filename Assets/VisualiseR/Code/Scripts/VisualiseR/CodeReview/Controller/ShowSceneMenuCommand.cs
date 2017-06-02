using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class ShowSceneMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowSceneMenuCommand));

        [Inject]
        public CodeMedium _codeMedium { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            ShowSceneMenu();
        }

        private void ShowSceneMenu()
        {
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("CodeReviewSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(true);
            Logger.InfoFormat("Scene menu is shown");
            var sceneMenuView = sceneMenu.GetComponent<CodeReviewSceneMenuView>();
            sceneMenuView.Init(_codeMedium);
        }
    }
}