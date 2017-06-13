using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class JoinRoomView : View
    {
        internal Signal<string> JoinRoomButtonClickSignal = new Signal<string>();
        
        internal InputField RoomNameInputField;

        private GameObject _mainMenuPanelView;

        protected override void Awake()
        {
            base.Awake();
            RoomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        public void OnJoinRoomButtonClick()
        {
            var roomName = RoomNameInputField.text;
            if (roomName != null)
            {
                JoinRoomButtonClickSignal.Dispatch(roomName);
            }
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}