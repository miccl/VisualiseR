using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// View for the join room menu.
    /// </summary>
    public class JoinRoomView : View
    {
        internal Signal<string> JoinRoomButtonClickSignal = new Signal<string>();
        
        internal InputField _roomNameInputField;

        private GameObject _mainMenuPanelView;

        protected override void Awake()
        {
            base.Awake();
            _roomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        public void OnJoinRoomButtonClick()
        {
            var roomName = _roomNameInputField.text;
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