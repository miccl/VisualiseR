using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// View of the scene menu.
    /// </summary>
    public class PresentationSceneMenuView : View
    {
        private JCsLogger Logger;

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

        internal GameObject _contextView;
        private GameObject _mainPanel;
        private GameObject _timerPanel;
        private GameObject _showPanel;

        private Text _startStopButtonText;
        private Text _showLaserButtonText;
        private Text _clockTypeButtonText;

        private TimerView _timerView;

        internal bool _isLaserShown = false;
        private Selectable _timeUpButton;
        private Selectable _timeDownButton;

        /// <summary>
        /// Initialises the variables.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(PresentationSceneMenuView));

            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _timerPanel = gameObject.transform.FindChild("TimerPanel").gameObject;
            _showPanel = gameObject.transform.FindChild("ShowPanel").gameObject;

            var buttonPanel = _timerPanel.transform.Find("CenterPanel").Find("ButtonPanel");
            _startStopButtonText = buttonPanel.Find("StartStopButton").GetComponentInChildren<Text>();
            _clockTypeButtonText = _timerPanel.transform.Find("BottomPanel").Find("TimerTypeButton")
                .GetComponentInChildren<Text>();
            _timeUpButton = buttonPanel.Find("UpButton").GetComponent<Selectable>();
            _timeDownButton = buttonPanel.Find("DownButton").GetComponent<Selectable>();
            
            _showLaserButtonText = _showPanel.transform.Find("CenterPanel").Find("ShowLaserButton")
                .GetComponentInChildren<Text>();
        }


        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="contextView"></param>
        /// <param name="player"></param>
        /// <param name="medium"></param>
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

        /// <summary>
        /// Called when the timer button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnTimerButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _timerPanel.SetActive(true);
            ShowTimerSignal.Dispatch(true);
        }

        /// <summary>
        /// Called when the show button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnShowButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _showPanel.SetActive(true);
        }

        /// <summary>
        /// Called when the quit button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnQuitButtonClick(BaseEventData data)
        {
            PhotonNetwork.LeaveRoom();
            UnityUtil.LoadScene("Main");
        }

        /// <summary>
        /// Called when the cancel button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnCancelButtonClick(BaseEventData data)
        {
            Hide();
        }

        public void OnClockTypeButton(BaseEventData data)
        {
            if (_clockTypeButtonText.text.Equals(ClockType.Countdown.ToString()))
            {
                _clockTypeButtonText.text = ClockType.Stopwatch.ToString();
                _timeUpButton.interactable = false;
                _timeDownButton.interactable = false;
            }
            else if (_clockTypeButtonText.text.Equals(ClockType.Stopwatch.ToString()))
            {
                _clockTypeButtonText.text = ClockType.Countdown.ToString();
                _timeUpButton.interactable = true;
                _timeDownButton.interactable = true;
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
            if (_timeUpButton.interactable)
            {
                SetTimerSignal.Dispatch(60);
            }
        }

        public void OnTimerDownButton(BaseEventData data)
        {
            if (_timeDownButton.interactable)
            {
                SetTimerSignal.Dispatch(-60);
            }
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

        /// <summary>
        /// Hides the view.
        /// </summary>
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