using System;
using System.Collections.Generic;
using System.Linq;
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
        public Signal _selectDiskFileButtonClickedSignal = new Signal();

        internal Dropdown RoomTypeDropdown;
        internal InputField RoomNameInputField;
        internal Dropdown ChooseMediumDropdown;

        private static readonly string CHOOSE_MEDIUM_TEXT = "Choose medium...";
        private static readonly string SELECT_DISK_FILE = "Choose disk file";
        private static readonly string SELECT_WEB_FILE = "Choose web file";

        private List<string> roomTypes = Enum.GetNames(typeof(RoomType)).ToList();
        private List<string> chooseMediumTypes = new List<string> {SELECT_WEB_FILE, SELECT_DISK_FILE};

        internal Medium ChoosenMedium;

        protected override void Awake()
        {
            base.Awake();
            RoomTypeDropdown = UnityUtil.FindGameObjectInChild("RoomTypePanel").GetComponentInChildren<Dropdown>();
            RoomNameInputField = UnityUtil.FindGameObjectInChild("RoomNamePanel").GetComponentInChildren<InputField>();
            ChooseMediumDropdown = UnityUtil.FindGameObjectInChild("ChooseMediumPanel")
                .GetComponentInChildren<Dropdown>();
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
            RoomTypeDropdown.AddOptions(roomTypes);
        }

        private void PopulateChooseMediumDropdown()
        {
            ChooseMediumDropdown.ClearOptions();
            ChooseMediumDropdown.AddOptions(chooseMediumTypes);
            ChooseMediumDropdown.captionText.text = CHOOSE_MEDIUM_TEXT;
        }

        public void OnChooseDiskFileButtonClick()
        {
            _selectDiskFileButtonClickedSignal.Dispatch();
        }

        public void OnCreateRoomButtonClick()
        {
            if (IsValidInput())
            {
                Room room = new Room
                {
                    Name = RoomNameInputField.text,
                    Type = roomTypes[RoomTypeDropdown.value].ToEnum<RoomType>()
                };
                Debug.Log(room);
            }
        }

        /// <summary>
        /// Test, if all needed information are choosen.
        /// If not, a error message is displayed with
        /// </summary>
        private bool IsValidInput()
        {
            //TODO Fehlernachricht anzeigen
            //TODO vielleicht andere Validierungen hinzufügen (keine Sonderzeichen...)
            //TODO ist der Raumname schon vergeben???
            if (RoomNameInputField.text == null)
            {
                Debug.LogFormat("Room name wasn't choosen yet ({0})", RoomNameInputField.text);
                return false;
            }

            if (ChoosenMedium == null)
            {
                Debug.Log("Medium wasn't choosen yet");
                return false;
            }

            return true;
        }


        //TODO vielleicht überlegen, das woanders hin zu verlagern..., denn die view soll ja möglichst ohne logik bleiben.
        public void OnChooseRoomIndexChange(int index)
        {
            if (index == chooseMediumTypes.IndexOf(SELECT_DISK_FILE))
            {
                _selectDiskFileButtonClickedSignal.Dispatch();
            }
            else if (index == chooseMediumTypes.IndexOf(SELECT_WEB_FILE))
            {
                ChoosenMedium = null;
                ChooseMediumDropdown.captionText.text = CHOOSE_MEDIUM_TEXT;
                throw new NotImplementedException();
            }
        }

        public void OnMediumLoadFinished(Medium medium)
        {
            //TODO davor könnte beispielsweise eine Laderad kommen, bis dieser Aufruf getätigt wird
            ChoosenMedium = medium;
            ChooseMediumDropdown.captionText.text = medium.Name;
        }
    }
}