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
            ShowroomSceneMenuIsShownSignal.Dispatch(_isShown);
        }

        private void ShowSceneMenu()
        {
            Logger.InfoFormat("Showing scene menu");
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("ShowroomSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(_isShown);
            if (_isShown)
            {
////                var cameraPos = Camera.main.transform.position;
////                var rotation = GetLookAtPosition(cameraPos);
////                sceneMenu. = rotation;
//                sceneMenu.transform.LookAt(Camera.main.transform);
            }
            
        }

        private static Quaternion GetLookAtPosition(Vector3 cameraPos)
        {
            Vector3 relativePos = Camera.main.transform.position - cameraPos;
            return Quaternion.LookRotation(relativePos);
        }
    }
}