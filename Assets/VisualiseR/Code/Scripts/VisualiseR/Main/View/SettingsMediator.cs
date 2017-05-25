using System;
using strange.extensions.mediation.impl;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class SettingsMediator : Mediator
    {
        [Inject]
        public SettingsView _view { get; set; }


        public override void OnRegister()
        {
            Init();
        }

        public override void OnRemove()
        {
        }

        private void Init()
        {
            RetrievePlayerPrefs();
        }

        private void RetrievePlayerPrefs()
        {
            RetrievePlayerName();
            RetrieveAvatarType();
        }

        private void RetrievePlayerName()
        {
            var playerName = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            if (!String.IsNullOrEmpty(playerName))
            {
                _view._playerNameInputField.text = playerName;
            }
        }

        private void RetrieveAvatarType()
        {
            var avatarType = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.AVATAR_KEY);
            if (!String.IsNullOrEmpty(avatarType))
            {
                _view._avatarDropdown.captionText.text = avatarType;
            }
        }
    }
}