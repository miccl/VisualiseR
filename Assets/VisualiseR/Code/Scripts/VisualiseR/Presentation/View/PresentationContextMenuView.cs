using System;
using System.ComponentModel;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class PresentationContextMenuView : View
    {        
        public Signal OnContextMenuCanceled = new Signal();
        public Signal<TimerTypes> ChangeTimerStatusSignal = new Signal<TimerTypes>();
        public Signal<bool> ShowTimerSignal = new Signal<bool>();
        public Signal<float> SetTimerSignal = new Signal<float>();
        public Signal ShowAllSignal = new Signal();

        private GameObject _mainPanel;
        private GameObject _timerPanel;
        private GameObject _showPanel;

        private Text _startStopButtonText;
        private Text _timerText;

        internal GameObject _contextView;
        private TimerView _timerView;

        protected override void Awake()
        {
            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _timerPanel = gameObject.transform.FindChild("TimerPanel").gameObject;
            _showPanel = gameObject.transform.FindChild("ShowPanel").gameObject;

            _startStopButtonText = _timerPanel.transform.FindChild("CenterPanel").transform.FindChild("ButtonPanel").transform.FindChild("StartStopButton").GetComponentInChildren<Text>();

        }

        protected override void Start()
        {
            base.Start();
            RefreshStartStopButtonText();
            SetTimerText(_timerView._timeFrom);
        }

        internal void Init(GameObject contextView)
        {
            _contextView = contextView;
            _timerView = _contextView.transform.Find("TimerCanvas").GetComponent<TimerView>();
        } 

        private void SetTimerText(float timeInSeconds)
        {
//            _timerText.text = TimeUtil.FormatTime(timeInSeconds);
        }

        internal void RefreshStartStopButtonText()
        {
            if (_timerView.stop)
            {
                _startStopButtonText.text = "Start";
            }
            else
            {
                _startStopButtonText.text = "Stop";
            }
        }

        public void OnTimerButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _timerPanel.SetActive(true);
            ShowTimerSignal.Dispatch(true);
        }

        public void OnShowButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _showPanel.SetActive(true);
        }


        public void OnCancelButtonClick(BaseEventData data)
        {
            OnContextMenuCanceled.Dispatch();
                        
            gameObject.SetActive(false);
        }

        public void OnTimerStartStopButton(BaseEventData data)
        {
            if (_timerView.stop)
            {
                ChangeTimerStatusSignal.Dispatch(TimerTypes.Start);
            }
            else
            {
                ChangeTimerStatusSignal.Dispatch(TimerTypes.Stop);
            }
            RefreshStartStopButtonText();
        }

        public void OnTimerResetButton(BaseEventData data)
        {
            ChangeTimerStatusSignal.Dispatch(TimerTypes.Reset);
            RefreshStartStopButtonText();
        }


        public void OnTimerUpButton(BaseEventData data)
        {
            SetTimerSignal.Dispatch(60);
        }

        public void OnTimerDownButton(BaseEventData data)
        {
            SetTimerSignal.Dispatch(-60);
        }

        public void OnTimerCancelButton(BaseEventData data)
        {
            _timerPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        public void OnShowAllButton(BaseEventData data)
        {
            ShowAllSignal.Dispatch();
            Destroy(gameObject);
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