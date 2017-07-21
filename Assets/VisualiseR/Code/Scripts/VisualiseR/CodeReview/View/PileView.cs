using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    
    /// <summary>
    /// View for the rating piles.
    /// </summary>
    public class PileView : View
    {
        public Signal<Rate> RatePileSelectedSignal = new Signal<Rate>();


        internal Rate _rate { get; set; }

        internal List<ICode> _codes;
        private GameObject _titlePanel;
        private Text _titleText;
        internal Selectable _selectable;


        protected override void Awake()
        {
            base.Awake();
            _titlePanel = gameObject.transform.FindChild("TitlePanel").gameObject;
            _titleText = _titlePanel.GetComponentInChildren<Text>();
            _selectable = _titlePanel.GetComponent<Selectable>();


        }

        public void Init(Rate rate, List<ICode> codes)
        {
            _rate = rate;
            if (rate.Equals(Rate.Unrated))
            {
                RatePileSelected(true);
            }
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
            Preconditions.CheckNotNull(_rate, "Rate may not be null");
            Preconditions.CheckNotNull(_codes, "Codes may not be null");
            _titleText.text = String.Format("{0} ({1})", _rate, _codes.Count);

        }

        public void OnClick()
        {
            RatePileSelectedSignal.Dispatch(_rate);
        }

        /// <summary>
        /// Deactivates the pile, if it was selected.
        /// Otherwise it activates it.
        /// </summary>
        /// <param name="selected"></param>
        internal void RatePileSelected(bool selected)
        {
            _selectable.interactable = !selected;
        }
    }
}