using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace VisualiseR.Main
{
    public class MessageView : View
    {
        private Transform _messagePanel;
        private Transform _centerPanel;
        internal Text _infoText;
        private Transform _title;
        internal Text _titleLabel;
        private Image _titleImage;
        private Button _okButton;

        protected override void Awake()
        {
            base.Awake();
            _messagePanel = transform.Find("MessagePanel");
            
            _centerPanel = _messagePanel.Find("CenterPanel");
            _infoText = _centerPanel.Find("InfoText").GetComponent<Text>();
            _okButton = _centerPanel.Find("OkButton").GetComponent<Button>();

            _title = _messagePanel.FindChild("Title");
            _titleLabel = _title.GetComponentInChildren<Text>();
            _titleImage = _title.GetComponent<Image>();
        }

        public void Init(MessageType type, string title, string message)
        {
            _titleLabel.text = title;
            _infoText.text = message;

            switch (type)
            {
                case MessageType.Error:
                    SetupErrorMessage();
                    break;
                case MessageType.Info:
                    SetupInfoMessage();
                    break;
                 default:
                     Debug.LogError("Invalid type");
                     break;
            }
        }

        private void SetupErrorMessage()
        {
            var color = Color.red;
            _titleImage.color = color;
        }

        private void SetupInfoMessage()
        {
            var color = Color.blue;
            _titleImage.color = color;
            _okButton.gameObject.SetActive(false);
            StartCoroutine(WaitAndClose());
        }

        private IEnumerator WaitAndClose()
        {
            yield return new WaitForSeconds(2f);
            Destroy();
        }

        public void OnOkButtonClick()
        {
            Destroy();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}