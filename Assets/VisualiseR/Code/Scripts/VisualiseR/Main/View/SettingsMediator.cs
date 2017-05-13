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

        private void Init()
        {
            RetrievePlayerPrefs();
        }

        public override void OnRemove()
        {
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
            var avarType = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.AVATAR_KEY);
            if (!String.IsNullOrEmpty(avarType))
            {
                _view._avatarDropdown.captionText.text = avarType;
            }
        }
    }
}