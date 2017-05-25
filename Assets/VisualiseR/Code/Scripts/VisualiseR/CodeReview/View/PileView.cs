using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace VisualiseR.CodeReview
{
    public class PileView : View
    {
        public Signal<Rate> RatePileSelectedSignal = new Signal<Rate>();


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

        public void Init(Rate rate, List<ICode> codes)
        {
            _rate = rate;
            _codes = codes;
            UpdateView();
        }

        public void AddCode(Code code)
        {
            _codes.Add(code);
            UpdateView();
        }

        public void RemoveCode(Code code)
        {
            _codes.Remove(code);
            UpdateView();
        }

        internal void UpdateView()
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            _titleText.text = String.Format("{0} ({1})", _rate, _codes.Count);

        }

        public void OnClick()
        {
            RatePileSelectedSignal.Dispatch(_rate);
        }
    }
}