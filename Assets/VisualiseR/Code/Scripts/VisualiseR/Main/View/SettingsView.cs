using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class SettingsView : View
    {
        private GameObject _mainMenuPanelView;

        protected override void Start()
        {
            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}