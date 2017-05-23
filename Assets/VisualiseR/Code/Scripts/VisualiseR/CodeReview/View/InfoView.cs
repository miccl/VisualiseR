using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class InfoView : View
    {
        internal ICode _code;
        private Text _titleText;
        private Text _nameText;
        private Text _pathText;
        private Text _positionText;
        private Text _sizeText;
        private Text _ratingText;

        protected override void Awake()
        {
            base.Awake();
            var infoPanel = gameObject.transform.FindChild("InfoPanel");
            _titleText = infoPanel.FindChild("Title").GetComponent<Text>();
            var centerPanel = infoPanel.FindChild("CenterPanel");
            _nameText = centerPanel.FindChild("NamePanel").FindChild("Text").GetComponent<Text>();
            _ratingText = centerPanel.FindChild("RatingPanel").FindChild("Text").GetComponent<Text>();
            _pathText = centerPanel.FindChild("PathPanel").FindChild("Text").GetComponent<Text>();
            _positionText = centerPanel.FindChild("PositionPanel").FindChild("Text").GetComponent<Text>();
            _sizeText = centerPanel.FindChild("SizePanel").FindChild("Text").GetComponent<Text>();
        }

        private void OnGUI()
        {
            GUI.skin.textArea.wordWrap = true;
        }

        public void UpdateView(ICode code)
        {
            if (code != null && !code.Equals(_code))
            {
                _code = code;
                SetViewValues();
            }
        }

        private void SetViewValues()
        {
            _nameText.text = _code.Name;
            _ratingText.text = _code.Rate.ToString();
            _pathText.text = _code.Pic.Path;
            _sizeText.text = FileUtil.GetSizeInBytes(_code.Pic.Path);
        }
    }
}