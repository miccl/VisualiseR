using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class PresentationSceneMenuView : View
    {
        private JCsLogger Logger;

        internal GameObject _contextView;
        private GameObject _mainPanel;
        private GameObject _timerPanel;
        private GameObject _showPanel;

        private TimerView _timerView;

        private Text _startStopButtonText;

        public Signal OnContextMenuCanceled = new Signal();
        public Signal<TimerType> ChangeTimerStatusSignal = new Signal<TimerType>();
        public Signal<ClockType> ChangeClockTypesSignal = new Signal<ClockType>();
        public Signal<bool> ShowTimerSignal = new Signal<bool>();
        public Signal<float> SetTimerSignal = new Signal<float>();
        public Signal<bool> ShowLaserSignal = new Signal<bool>();
        
        public Signal<IPlayer, ISlideMedium> ShowPreviousSignal = new Signal<IPlayer, ISlideMedium>();
        public Signal ShowAllSignal = new Signal();
        private ISlideMedium _medium;
        private IPlayer _player;
        
        internal bool _isLaserShown = false;
        private Text _showLaserButtonText;
        private Text _clockTypeButtonText;


        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(PresentationSceneMenuView));

            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _timerPanel = gameObject.transform.FindChild("TimerPanel").gameObject;
            _showPanel = gameObject.transform.FindChild("ShowPanel").gameObject;

            _startStopButtonText = _timerPanel.transform.Find("CenterPanel").Find("ButtonPanel")
                .Find("StartStopButton").GetComponentInChildren<Text>();
            _clockTypeButtonText = _timerPanel.transform.Find("BottomPanel").Find("TimerTypeButton").GetComponentInChildren<Text>();
            _showLaserButtonText = _showPanel.transform.Find("CenterPanel").
                Find("ShowLaserButton").GetComponentInChildren<Text>();

        }


        internal void Init(GameObject contextView, IPlayer player, ISlideMedium medium)
        {
            _contextView = contextView;
            _timerView = _contextView.transform.Find("Timer").GetComponent<TimerView>();
            _player = player;
            _medium = medium;
        }

        void Update()
        {
            if (Input.GetButtonDown(ButtonUtil.CANCEL))
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            Logger.InfoFormat("Cancel button clicked");
            if (_mainPanel.activeSelf)
            {
                Hide();
                return;
            }

            if (_timerPanel.activeSelf)
            {
                OnTimerCancelButton(null);
                return;
            }

            if (_showPanel.activeSelf)
            {
                OnShowCancelButton(null);
                return;
            }
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

        public void OnQuitButtonClick(BaseEventData data)
        {
            PhotonNetwork.LeaveRoom();
            UnityUtil.LoadScene("Main");
        }

        public void OnCancelButtonClick(BaseEventData data)
        {
            Hide();
        }

        public void OnClockTypeButton(BaseEventData data)
        {
            if (_clockTypeButtonText.text.Equals(ClockType.Countdown.ToString()))
            {
                _clockTypeButtonText.text = ClockType.Stopwatch.ToString();
            } else if (_clockTypeButtonText.text.Equals(ClockType.Stopwatch.ToString()))
            {
                _clockTypeButtonText.text = ClockType.Countdown.ToString();
            }
            ChangeClockTypesSignal.Dispatch(_clockTypeButtonText.text.ToEnum<ClockType>());

        }

        public void OnTimerStartStopButton(BaseEventData data)
        {
            if (_timerView.stop)
            {
                ChangeTimerStatusSignal.Dispatch(TimerType.Start);
            }
            else
            {
                ChangeTimerStatusSignal.Dispatch(TimerType.Stop);
            }
            RefreshStartStopButtonText();
        }

        public void OnTimerResetButton(BaseEventData data)
        {
            ChangeTimerStatusSignal.Dispatch(TimerType.Reset);
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

        public void OnShowPreviousButton(BaseEventData data)
        {
            ShowPreviousSignal.Dispatch(_player, _medium);
            Hide();
        }


        public void OnShowAllButton(BaseEventData data)
        {
            ShowAllSignal.Dispatch();
            Hide();
        }
        
        public void OnShowLaserButtonClick(BaseEventData data)
        {
            _isLaserShown = !_isLaserShown;
//            ChangeLaserText();
            ShowLaserSignal.Dispatch(_isLaserShown);
            Hide();
        }

        internal void ChangeLaserText()
        {
            _showLaserButtonText.text = _isLaserShown ? "Hide Laser" : "Show Laser";
        }


        public void OnShowCancelButton(BaseEventData data)
        {
            _showPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        private void Hide()
        {
            _mainPanel.SetActive(true);
            _showPanel.SetActive(false);
            _timerPanel.SetActive(false);

            OnContextMenuCanceled.Dispatch();
            gameObject.SetActive(false);
        }
    }
}