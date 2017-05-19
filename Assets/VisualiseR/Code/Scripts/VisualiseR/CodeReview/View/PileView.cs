using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace VisualiseR.CodeReview
{
    public class PileView : View
    {
        internal Rate _rate { get; set; }

        internal List<ICode> _codes;
        private GameObject _titlePanel;
        private Text _titleText;


        protected override void Awake()
        {
            base.Awake();
            _titlePanel = gameObject.transform.FindChild("TitlePanel").gameObject;
            _titleText = _titlePanel.GetComponentInChildren<Text>();


        }

        protected override void Start()
        {
            base.Start();

        }

        public void AddCode(Code code)
        {
            _codes.Add(code);
            UpdateCode();
        }

        public void RemoveCode(Code code)
        {
            _codes.Remove(code);
            UpdateCode();
        }

        private void UpdateCode()
        {
            //TODO irgendwat aktualisieren

        }

        private void SetupView()
        {
            UpdateTitle();
        }

        private void UpdateView()
        {

        }

        private void UpdateTitle()
        {
            _titleText.text = String.Format("{0} ({1})", _rate, _codes.Count);

        }
        public void Init(Rate rate, List<ICode> codes)
        {
            _rate = rate;
            _codes = codes;
            SetupView();
        }
    }
}