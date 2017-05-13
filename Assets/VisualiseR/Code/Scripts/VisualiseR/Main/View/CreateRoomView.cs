using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main

{
    public class CreateRoomView : View
    {
        private static readonly string CHOOSE_MEDIUM_TEXT = " Choose medium...";
        private static readonly string SELECT_DISK_FILE = "Choose disk file";
        private static readonly string SELECT_WEB_FILE = "Choose web file";

        public Signal SelectDiskFileButtonClickedSignal = new Signal();

        public Signal<string, RoomType, IMedium> CreateRoomButtonClickedSignal =
            new Signal<string, RoomType, IMedium>();


        internal Dropdown RoomTypeDropdown;
        internal InputField RoomNameInputField;
        internal Dropdown ChooseMediumDropdown;
        internal IMedium ChoosenMedium;

        private readonly List<string> _roomTypes = CSharpUtil.EnumToList<RoomType>();
        private readonly List<string> _chooseMediumTypes = new List<string> {SELECT_WEB_FILE, SELECT_DISK_FILE};

        private GameObject _mainMenuPanelView;

        protected override void Awake()
        {
            base.Awake();
            RoomTypeDropdown = UnityUtil.FindGameObjectInChild("RoomTypePanel").GetComponentInChildren<Dropdown>();
            RoomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();
            ChooseMediumDropdown = UnityUtil.FindGameObjectInChild("ChooseMediumPanel")
                .GetComponentInChildren<Dropdown>();

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
            RoomTypeDropdown.ClearOptions();
            RoomTypeDropdown.AddOptions(_roomTypes);
        }

        private void PopulateChooseMediumDropdown()
        {
            ChooseMediumDropdown.ClearOptions();
            ChooseMediumDropdown.AddOptions(_chooseMediumTypes);
            ChooseMediumDropdown.captionText.text = CHOOSE_MEDIUM_TEXT;
        }

        public void OnCreateRoomButtonClick()
        {
            CreateRoomButtonClickedSignal.Dispatch(RoomNameInputField.text,
                _roomTypes[RoomTypeDropdown.value].ToEnum<RoomType>(), ChoosenMedium);
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }

        //TODO vielleicht überlegen, das woanders hin zu verlagern..., denn die view soll ja möglichst ohne logik bleiben.
        public void OnChooseRoomIndexChange(int index)
        {
            if (index == _chooseMediumTypes.IndexOf(SELECT_DISK_FILE))
            {
                SelectDiskFileButtonClickedSignal.Dispatch();
            }
            else if (index == _chooseMediumTypes.IndexOf(SELECT_WEB_FILE))
            {
                ChoosenMedium = null;
                ChooseMediumDropdown.captionText.text = CHOOSE_MEDIUM_TEXT;
                throw new NotImplementedException();
            }
        }

        public void DisplayErrorMessage(string msg)
        {
            //TODO
        }
    }
}