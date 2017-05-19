using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class PileView : View
    {
        internal Rate _rate { get; set; }

        internal List<ICode> _codes;


        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            UpdateView();
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
        }

        private void UpdateView()
        {

        }

        public void Init(Rate rate, List<ICode> codes)
        {
            _rate = rate;
            _codes = codes;
            UpdateCode();
        }
    }
}