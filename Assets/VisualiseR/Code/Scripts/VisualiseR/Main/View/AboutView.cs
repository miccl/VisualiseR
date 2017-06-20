using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main

{
    /// <summary>
    /// View for the about dialog.
    /// </summary>
    public class AboutView : View
    {
        private static readonly string ABOUT_TEXT = "This is some about!!!";

        private GameObject _mainMenuPanelView;
        internal Text _aboutText;

        protected override void Awake()
        {
            base.Awake();
            _aboutText = UnityUtil.FindGameObjectInChild("AboutText").GetComponentInChildren<Text>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;


        }

        protected override void Start()
        {
            base.Start();
            _aboutText.text = ABOUT_TEXT;
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}