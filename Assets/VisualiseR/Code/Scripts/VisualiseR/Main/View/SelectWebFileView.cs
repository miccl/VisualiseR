using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class SelectWebFileView : View
    {
        public Signal<string> UrlSelected = new Signal<string>();
        public Signal CanceledSignal = new Signal();

        private InputField _urlInputField;
        public GameObject _contextView;

        protected override void Awake()
        {
            base.Awake();
            var webBrowser = _contextView.transform.Find("WebBrowser");
            _urlInputField = webBrowser.GetComponentInChildren<InputField>();
        }

        public void OnOkButtonClick()
        {
            var url = _urlInputField.text;
            if (!string.IsNullOrEmpty(url))
            {
                UrlSelected.Dispatch(url);
            }
            else
            {
                Debug.Log("url was empty");
            }
            Hide();

        }

        public void OnCancelButtonClick()
        {
            CanceledSignal.Dispatch();
            Hide();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}