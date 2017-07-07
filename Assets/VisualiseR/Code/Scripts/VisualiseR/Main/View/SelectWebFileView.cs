using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// View for the web file selection dialog.
    /// </summary>
    public class SelectWebFileView : View
    {
        private JCsLogger Logger;

        public Signal<string> UrlSelected = new Signal<string>();
        public Signal CanceledSignal = new Signal();

        private InputField _urlInputField;
        public GameObject _contextView;
        private Transform _createRoomPanel;


        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(LoadFilesCommand));
            _createRoomPanel = transform.parent.Find("CreateRoomPanel");
            _urlInputField = gameObject.GetComponentInChildren<InputField>();
        }

        public void OnOkButtonClick()
        {
            var url = _urlInputField.text;
            UrlSelected.Dispatch(url);
            Hide();
        }

        public void OnCancelButtonClick()
        {
            Hide();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}