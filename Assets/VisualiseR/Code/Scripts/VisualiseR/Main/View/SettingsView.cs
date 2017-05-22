using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class SettingsView : View
    {
        private GameObject _mainMenuPanelView;
        internal InputField _playerNameInputField;
        internal Dropdown _avatarDropdown;


        private const string CHOOSE_AVATAR_TEXT = "Choose a avatar...";
        private readonly List<string> avatarTypes = EnumUtil.EnumToList<AvatarType>();


        protected override void Awake()
        {
            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;

            _playerNameInputField = UnityUtil.FindGameObjectInChild("PlayerNamePanel")
                .GetComponentInChildren<InputField>();
            _avatarDropdown = UnityUtil.FindGameObjectInChild("AvatarPanel").GetComponentInChildren<Dropdown>();
        }

        protected override void Start()
        {
            base.Start();
            PopulateAvatarDropdown();
        }

        private void PopulateAvatarDropdown()
        {
            _avatarDropdown.ClearOptions();
            _avatarDropdown.AddOptions(avatarTypes);
            if (String.IsNullOrEmpty(_avatarDropdown.captionText.text))
            {
                _avatarDropdown.captionText.text = CHOOSE_AVATAR_TEXT;
            }
        }


        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSaveButtonClick()
        {
            SavePlayerName();
            SaveAvatar();
        }

        private void SavePlayerName()
        {
            string playerName = _playerNameInputField.text;
            if (!String.IsNullOrEmpty(playerName))
            {
                PlayerPrefsUtil.SaveValue(PlayerPrefsUtil.PLAYER_NAME_KEY, playerName);
            }
        }

        private void SaveAvatar()
        {
            string avatarType = _avatarDropdown.captionText.text;
            if (!String.IsNullOrEmpty(avatarType) && !avatarType.Equals(CHOOSE_AVATAR_TEXT))
            {
                PlayerPrefsUtil.SaveValue(PlayerPrefsUtil.AVATAR_KEY, avatarType);
            }
        }
    }
}