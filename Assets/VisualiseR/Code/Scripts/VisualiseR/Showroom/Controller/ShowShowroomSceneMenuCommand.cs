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
            Logger.InfoFormat("Showing scene menu");
            ShowSceneMenu();
            ShowObjects();
            ShowroomSceneMenuIsShownSignal.Dispatch(_isShown);
        }

        private void ShowObjects()
        {
            var objects = _contextView.transform.Find("Objects").gameObject;
            objects.SetActive(!_isShown);
        }

        private void ShowSceneMenu()
        {
            var sceneMenu = GameObject.Find("Menus").transform.Find("ShowroomSceneMenuCanvas").gameObject;
            sceneMenu.SetActive(_isShown);
            if (_isShown)
            {
                AdjustPositionAndRotation(sceneMenu);
            }
        }

        private static void AdjustPositionAndRotation(GameObject sceneMenu)
        {
            Vector3 shift = Camera.main.transform.forward.normalized * 4;
            Vector3 pos = Camera.main.transform.position + shift;
            sceneMenu.transform.position = pos;
            if (sceneMenu.transform.localPosition.y < 0)
            {
                sceneMenu.transform.localPosition = new Vector3(sceneMenu.transform.localPosition.x, 0, sceneMenu.transform.localPosition.z);
            }

            Vector3 relativePos = Camera.main.transform.position - pos;
            sceneMenu.transform.rotation = Quaternion.LookRotation(relativePos);
            sceneMenu.transform.Rotate(0, 180, 0);
            SetXAxisToZero(sceneMenu);
        }

        private static void SetXAxisToZero(GameObject sceneMenu)
        {
            var rot = sceneMenu.transform.eulerAngles;
            rot.x = 0;
            sceneMenu.transform.eulerAngles = rot;
        }
    }
}