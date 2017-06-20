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
        private static readonly string CHOOSE_MEDIUM_TEXT = " Choose pictureMedium...";
        private static readonly string SELECT_DISK_FILE = "Choose disk file";
        private static readonly string SELECT_WEB_FILE = "Choose web file";

        public Signal SelectDiskFileButtonClickedSignal = new Signal();
        public Signal SelectWebFileButtonClickedSignal = new Signal();
        public Signal<string, RoomType, IPictureMedium> CreateRoomButtonClickedSignal =
            new Signal<string, RoomType, IPictureMedium>();

        internal Dropdown _roomTypeDropdown;
        internal InputField _roomNameInputField;
        internal Dropdown _chooseMediumDropdown;
        internal IPictureMedium _choosenMedium;

        private readonly List<string> _roomTypes = EnumUtil.EnumToList<RoomType>();
        private readonly List<string> _chooseMediumTypes = new List<string> {"None", SELECT_DISK_FILE, SELECT_WEB_FILE};

        private GameObject _mainMenuPanelView;
        private Button _backButton;
        private Button _createRoomButton;

        protected override void Awake()
        {
            base.Awake();
            var createRoomPanel = UnityUtil.FindGameObject("CreateRoomPanel").transform.Find("CenterPanel");
            _roomTypeDropdown = createRoomPanel.Find("RoomTypePanel").GetComponentInChildren<Dropdown>();
            _roomNameInputField = createRoomPanel.Find("RoomNamePanel").GetComponentInChildren<InputField>();
            _chooseMediumDropdown = createRoomPanel.Find("ChooseMediumPanel").
                GetComponentInChildren<Dropdown>();
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
            CreateRoomButtonClickedSignal.Dispatch(_roomNameInputField.text,
                _roomTypes[_roomTypeDropdown.value].ToEnum<RoomType>(), _choosenMedium);
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }

        //TODO vielleicht überlegen, das woanders hin zu verlagern..., denn die view soll ja möglichst ohne logik bleiben.
        public void OnChooseRoomIndexChange(int index)
        {
            if (index == _chooseMediumTypes.IndexOf(SELECT_WEB_FILE))
            {
                SelectWebFileButtonClickedSignal.Dispatch();
            }

            if (index == _chooseMediumTypes.IndexOf(SELECT_DISK_FILE))
            {
                SelectDiskFileButtonClickedSignal.Dispatch();
            }
            ChangeInteractibilityOfButtons(false);
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
    }
}