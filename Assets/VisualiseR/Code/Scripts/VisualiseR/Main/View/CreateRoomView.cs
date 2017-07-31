using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main

{
    /// <summary>
    /// View for the create room menu.
    /// </summary>
    public class CreateRoomView : View
    {
        private JCsLogger Logger;
        
        private static readonly string CHOOSE_MEDIUM_TEXT = " Choose pictureMedium...";
        private static readonly string SELECT_DISK_FILE = "Choose disk file";
        private static readonly string SELECT_WEB_FILE = "Choose web file";

        public Signal SelectDiskFileButtonClickedSignal = new Signal();
        public Signal SelectWebFileButtonClickedSignal = new Signal();
        public Signal<string, RoomType, IPictureMedium> CreateRoomButtonClickedSignal =
            new Signal<string, RoomType, IPictureMedium>();
        internal Signal<Message> ShowMessageSignal = new Signal<Message>();


        internal Dropdown _roomTypeDropdown;
        internal InputField _roomNameInputField;
        internal Dropdown _chooseMediumDropdown;
        internal IPictureMedium _choosenMedium;

        private readonly List<string> _roomTypes = EnumUtil.EnumToList<RoomType>();
        private readonly List<string> _chooseMediumTypes = new List<string> {"None", SELECT_DISK_FILE, SELECT_WEB_FILE};

        private GameObject _mainMenuPanelView;
        private Button _backButton;
        private Button _createRoomButton;
        private bool _onJoinedLobby;
        private string _roomName;
        private Selectable _chooseMediumDropdownSelectable;

        protected override void Awake()
        {
            base.Awake();
            
            Logger = new JCsLogger(typeof(JoinRoomView));

            
            var createRoomPanel = UnityUtil.FindGameObject("CreateRoomPanel").transform.Find("CenterPanel");
            _roomTypeDropdown = createRoomPanel.Find("RoomTypePanel").GetComponentInChildren<Dropdown>();
            _roomNameInputField = createRoomPanel.Find("RoomNamePanel").GetComponentInChildren<InputField>();
            _chooseMediumDropdown = createRoomPanel.Find("ChooseMediumPanel").
                GetComponentInChildren<Dropdown>();
            _chooseMediumDropdownSelectable = createRoomPanel.Find("ChooseMediumPanel").
                GetComponentInChildren<Selectable>();
            var buttonPanel = createRoomPanel.Find("ButtonPanel");
            _backButton = buttonPanel.Find("BackButton").GetComponent<Button>();
            _createRoomButton = buttonPanel.Find("CreateRoomButton").GetComponent<Button>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        protected override void Start()
        {
            base.Start();
            PopulateRoomTypeDropdown();
            PopulateChooseMediumDropdown();
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        private void PopulateRoomTypeDropdown()
        {
            _roomTypeDropdown.ClearOptions();
            _roomTypeDropdown.AddOptions(_roomTypes);
        }

        private void PopulateChooseMediumDropdown()
        {
            _chooseMediumDropdown.ClearOptions();
            _chooseMediumDropdown.AddOptions(_chooseMediumTypes);
            _chooseMediumDropdown.captionText.text = CHOOSE_MEDIUM_TEXT;
        }

        public void OnCreateRoomButtonClick()
        {
            if (!_onJoinedLobby)
            {
                return;
            }
            _roomName = _roomNameInputField.text;
            PhotonNetwork.CreateRoom(_roomName);
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnRoomTypeIndexChange(int index)
        {
            if (index == _roomTypes.IndexOf(RoomType.CodeReview.ToString()))
            {
                _chooseMediumDropdownSelectable.interactable = true;
                return;
            }
            
            if (index == _roomTypes.IndexOf(RoomType.Presentation.ToString()))
            {
                _chooseMediumDropdownSelectable.interactable = true;
                return;
            }
            
            if (index == _roomTypes.IndexOf(RoomType.Showroom.ToString()))
            {
                _chooseMediumDropdownSelectable.interactable = false;
                return;
            }

        }

        public void OnChooseRoomIndexChange(int index)
        {
            if (index == _chooseMediumTypes.IndexOf(SELECT_WEB_FILE))
            {
                SelectWebFileButtonClickedSignal.Dispatch();
                return;
            }

            if (index == _chooseMediumTypes.IndexOf(SELECT_DISK_FILE))
            {
                SelectDiskFileButtonClickedSignal.Dispatch();
                ChangeInteractibilityOfButtons(false);
            }
        }

        public void DisplayErrorMessage(string msg)
        {
            //TODO
        }

        public void ChangeInteractibilityOfButtons(bool interactable)
        {
            _roomNameInputField.interactable = interactable;
            _chooseMediumDropdown.interactable = interactable;
            _backButton.interactable = interactable;
            _createRoomButton.interactable = interactable;
        }

        public void SelectionCanceled()
        {
            _chooseMediumDropdown.value = 0;
        }

        void OnJoinedLobby()
        {
            _onJoinedLobby = true;
        }

        void OnCreatedRoom()
        {
            Logger.InfoFormat("Room '{0}' exists. Initiating joining room", _roomName);
            PhotonNetwork.LeaveRoom();
            CreateRoomButtonClickedSignal.Dispatch(_roomName,
                _roomTypes[_roomTypeDropdown.value].ToEnum<RoomType>(), _choosenMedium);
        }

        void OnPhotonCreateRoomFailed()
        {
            string errorMessage = string.Format("Room with name {0} already exist", _roomName);
            Logger.InfoFormat("Error: {0}", errorMessage);
            ShowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));

        }
    }
}