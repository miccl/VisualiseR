using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VisualiseR.CodeReview
{
    public class PresentationContextMenuView : View
    {
        public Signal OnContextMenuCanceled = new Signal();

        public ICode _code;

        private GameObject _ratePanel;
        private GameObject _editPanel;
        private GameObject _commentPanel;
        private GameObject _removePanel;
        private InputField _commentInputField;
        private bool _isHeld;
        private GameObject _gvrReticlePointer;
        private Selectable _goodButtonSelectable;
        private Selectable _okButtonSelectable;
        private Selectable _badButtonSelectable;
        private GameObject _mainPanel;
        private GameObject _timerPanel;
        private GameObject _showPanel;

        protected override void Awake()
        {
            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _timerPanel = gameObject.transform.FindChild("TimerPanel").gameObject;
            _showPanel = gameObject.transform.FindChild("ShowPanel").gameObject;
        }

        public void OnTimerButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _timerPanel.SetActive(true);
        }

        public void OnShowButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _showPanel.SetActive(true);
        }


        public void OnCancelButtonClick(BaseEventData data)
        {
            OnContextMenuCanceled.Dispatch();
            Destroy(gameObject);
        }

        public void OnTimerStartStopButton(BaseEventData data)
        {
            throw new NotImplementedException("OnTimerStartStopButton");
        }

        public void OnTimerResetButton(BaseEventData data)
        {
            throw new NotImplementedException("OnTimerStartStopButton");
        }


        public void OnTimerUpButton(BaseEventData data)
        {
            throw new NotImplementedException("OnTimerUpButton");
        }

        public void OnTimerDownButton(BaseEventData data)
        {
            throw new NotImplementedException("OnTimerDownButton");
        }

        public void OnTimerCancelButton(BaseEventData data)
        {
            _timerPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        public void OnShowAllButton(BaseEventData data)
        {
            throw new NotImplementedException("OnShowAllButton");
        }

        public void OnShowNextNButton(BaseEventData data)
        {
            throw new NotImplementedException("OnShowNextNButton");
        }

        public void OnShowCancelButton(BaseEventData data)
        {
            _showPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

    }
}