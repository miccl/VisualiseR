﻿using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// View for the information screen.
    /// </summary>
    public class InfoView : View
    {
        internal ICode _code;
        private Text _nameText;
        private Text _pathText;
        private Text _sizeText;
        private Text _ratingText;
        private Text _commentText;
        private Text _ownerText;
        private Text _lastModifiedText;

        protected override void Awake()
        {
            base.Awake();
            var infoPanel = gameObject.transform.FindChild("InfoPanel");
            var centerPanel = infoPanel.FindChild("CenterPanel");
            _nameText = centerPanel.FindChild("NamePanel").FindChild("Text").GetComponent<Text>();
            _ratingText = centerPanel.FindChild("RatingPanel").FindChild("Text").GetComponent<Text>();
            _pathText = centerPanel.FindChild("PathPanel").FindChild("Text").GetComponent<Text>();
            _commentText = centerPanel.FindChild("CommentPanel").FindChild("Text").GetComponent<Text>();
            _sizeText = centerPanel.FindChild("SizePanel").FindChild("Text").GetComponent<Text>();
            _ownerText = centerPanel.FindChild("OwnerPanel").FindChild("Text").GetComponent<Text>();
            _lastModifiedText = centerPanel.FindChild("LastModifiedPanel").FindChild("Text").GetComponent<Text>();
        }

        private void OnGUI()
        {
            GUI.skin.textArea.wordWrap = true;
        }

        public void UpdateView(ICode code)
        {
            if (code == null)
            {
                ResetView();
                return;
            }

            _code = code;
            SetViewValues();
        }

        private void SetViewValues()
        {
            Preconditions.CheckNotNull(_code.Name, "Name may not be null");
            _nameText.text = _code.Name;
            _ratingText.text = _code.Rate.ToString();
            _pathText.text = FileUtil.GetParentDirectory(_code.OldPath);
            _sizeText.text = FileUtil.GetSizeInBytes(_code.OldPath);
            _ownerText.text = FileUtil.GetOwner(_code.OldPath);
            _lastModifiedText.text = FileUtil.GetLastModified(_code.OldPath);
            UpdateComment(_code.Comment);
        }

        internal void UpdateComment(string comment)
        {
            var text = "None";
            if (!String.IsNullOrEmpty(comment))
            {
                text = comment;
            }
            _commentText.text = text;
        }

        public void ResetView()
        {
            _nameText.text = "";
            _ratingText.text = "";
            _pathText.text = "";
            _sizeText.text = "";
            _ownerText.text = "";
            _lastModifiedText.text = "";
        }
    }
}