using strange.extensions.mediation.impl;
using UnityEngine.EventSystems;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class PresentationSceneMenuView : View
    {

        public void OnQuitButtonClick(BaseEventData data)
        {
            PhotonNetwork.LeaveRoom();
            UnityUtil.LoadScene("Main");
        }

        public void OnCancelButtonClick(BaseEventData data)
        {
            gameObject.SetActive(false);
        }
    }
}