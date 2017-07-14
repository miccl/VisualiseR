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
        private JCsLogger Logger;

        internal Signal<string> JoinRoomButtonClickSignal = new Signal<string>();
        internal Signal<Message> ShowMessageSignal = new Signal<Message>();

        internal InputField _roomNameInputField;

        private GameObject _mainMenuPanelView;
        private bool _onJoinedLobby;
        private string _roomName;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(JoinRoomView));
            _roomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        protected override void Start()
        {
            base.Start();
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        public void OnJoinRoomButtonClick()
        {
            _roomName = _roomNameInputField.text;
            if (_roomName != null && _onJoinedLobby)
            {
                var joinedRoom = PhotonNetwork.JoinRoom(_roomName);
//                JoinRoomButtonClickSignal.Dispatch(roomName);
            }
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }

        void OnJoinedLobby()
        {
            _onJoinedLobby = true;
        }

        void OnJoinedRoom()
        {
            Logger.InfoFormat("Room '{0}' exists. Initiating joining room", _roomName);
            PhotonNetwork.LeaveRoom();
            JoinRoomButtonClickSignal.Dispatch(_roomName);
        }

        void OnPhotonJoinRoomFailed()
        {
            Logger.InfoFormat("Room '{0}' doesn't exists.", _roomName);
            string errorMessage = string.Format("Room with name {0} does not exist", _roomName);
            Logger.Info("Error:" + errorMessage);
            ShowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));

            //TODO Infonachricht für den Kunden
        }
    }
}