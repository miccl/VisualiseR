using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to show the scene menu.
    /// </summary>
    public class ShowShowroomSceneMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowShowroomSceneMenuCommand));
        
        [Inject]
        public bool _isShown { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }
        
        [Inject]
        public ShowroomSceneMenuIsShownSignal ShowroomSceneMenuIsShownSignal { get; set; }

        public override void Execute()
        {
            ShowSceneMenu();
        }

        private void ShowSceneMenu()
        {
            Logger.InfoFormat("Showing scene menu");
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("ShowroomSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(_isShown);
            ShowroomSceneMenuIsShownSignal.Dispatch(_isShown);
        }
    }
}