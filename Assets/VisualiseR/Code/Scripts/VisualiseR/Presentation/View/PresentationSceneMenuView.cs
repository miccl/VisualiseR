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
        private static JCsLogger Logger;
        
        internal GameObject _contextView;
        private GameObject _mainPanel;
        private GameObject _timerPanel;
        private GameObject _showPanel;

        private TimerView _timerView;
        
        private Text _startStopButtonText;

        public Signal OnContextMenuCanceled = new Signal();
        public Signal<TimerTypes> ChangeTimerStatusSignal = new Signal<TimerTypes>();
        public Signal<bool> ShowTimerSignal = new Signal<bool>();
        public Signal<float> SetTimerSignal = new Signal<float>();
        public Signal<IPlayer, ISlideMedium> ShowPreviousSignal = new Signal<IPlayer, ISlideMedium>();
        public Signal ShowAllSignal = new Signal();
        private ISlideMedium _medium;
        private IPlayer _player;


        protected override void Awake()
        {
            Logger = new JCsLogger(typeof(PresentationSceneMenuView));
            
            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _timerPanel = gameObject.transform.FindChild("TimerPanel").gameObject;
            _showPanel = gameObject.transform.FindChild("ShowPanel").gameObject;

            _startStopButtonText = _timerPanel.transform.FindChild("CenterPanel").transform.FindChild("ButtonPanel").transform.FindChild("StartStopButton").GetComponentInChildren<Text>();

        }

        protected override void Start()
        {
            base.Start();
            RefreshStartStopButtonText();
        }
        
        internal void Init(GameObject contextView, IPlayer player, ISlideMedium medium)
        {
            _contextView = contextView;
            _timerView = _contextView.transform.Find("TimerCanvas").GetComponent<TimerView>();
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

        public void OnShowCancelButton(BaseEventData data)
        {
            _showPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        private void Hide()
        {
            OnContextMenuCanceled.Dispatch();
            gameObject.SetActive(false);
        }
    }
}