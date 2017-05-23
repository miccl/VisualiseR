using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class MessageView : View
    {
        internal Text _infoText;
        internal Text _titleLabel;
        private GameObject _centerPanel;
        private Image _centerPanelImage;
        private GameObject _menuCanvas;
        private Image _titleImage;

        protected override void Awake()
        {
            base.Awake();
            _infoText = UnityUtil.FindGameObjectInChild("InfoText").GetComponent<Text>();
            _titleLabel = transform.FindChild("Title").GetComponentInChildren<Text>();
            _titleImage = transform.FindChild("Title").GetComponent<Image>();
            _centerPanel = transform.FindChild("CenterPanel").gameObject;

            _menuCanvas = UnityUtil.FindGameObject("MenuCanvas");

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

        }

        public void OnOkButtonClick()
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}