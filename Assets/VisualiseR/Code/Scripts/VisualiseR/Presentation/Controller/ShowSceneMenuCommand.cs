using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class ShowSceneMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowSceneMenuCommand));
        
        [Inject]
        public Player _player { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }
        
        public override void Execute()
        {
            if (_player.HasRight(AcessList.SceneMenu))
            {
                ShowSceneMenu();
            }
        }

        private void ShowSceneMenu()
        {
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("PresentationSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(true);
            Logger.InfoFormat("Scene menu is shown");
        }
    }
}