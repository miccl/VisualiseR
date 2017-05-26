using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Presentation
{
    public class ShowSceneMenuCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            InstantiateSceneMenu();
        }

        private void InstantiateSceneMenu()
        {
            var position = GetContextMenuPosition();
            var rotation = GetContextMenuRotation();
            GameObject contextMenu =
                GameObject.Instantiate(Resources.Load("PresentationSceneMenuCanvas"), position, rotation) as GameObject;
//            contextMenu.transform.Rotate(90, -180, 0);
            contextMenu.transform.SetParent(_contextView.transform);
        }

        private Quaternion GetContextMenuRotation()
        {
            //TODO überarbeiten

            return Quaternion.Euler(0,0,0);
        }

        private Vector3 GetContextMenuPosition()
        {
            //TODO irgendwann nochmal verbessern, derzeit schwankt das immer hin und her
            Vector3 cameraBack = -Camera.main.transform.forward * 12;
            Vector3 shift = new Vector3(0, 0, cameraBack.z);
            Vector3 pos = Camera.main.transform.position + shift;
            pos.y = 2;

            return pos;
        }
    }
}