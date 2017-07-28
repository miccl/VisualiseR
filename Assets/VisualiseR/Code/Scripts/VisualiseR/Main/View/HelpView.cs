using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// View for the help dialog.
    /// </summary>
    public class HelpView : View
    {
        private static readonly string HELP_TEXT = "Here is some help. You are welcome!";

        private GameObject _mainMenuPanelView;
        internal Text HelpText;

        protected override void Awake()
        {
            base.Awake();
            HelpText = UnityUtil.FindGameObjectInChild("HelpText").GetComponentInChildren<Text>();

            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");
            _mainMenuPanelView = menuCanvas.transform.FindChild("MainMenuPanel").gameObject;


        }

        protected override void Start()
        {
            base.Start();
            HelpText.text = HELP_TEXT;
        }

        public void OnBackButtonClick()
        {
            _mainMenuPanelView.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}