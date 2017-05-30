using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Presentation;

namespace VisualiseR.Presentation
{
    public class TimerMediator : Mediator
    {
        [Inject]
        public TimerView _view { get; set; }

        [Inject]
        public ChangeTimerStatusSignal ChangeTimerStatusSignal { get; set; }

        [Inject]
        public SetTimerSignal SetTimerSignal { get; set; }

        [Inject]
        public TimerRunDownSignal TimerRunDownSignal { get; set; }

        [Inject]
        public ShowAllSignal ShowAllSignal { get; set; }
        
        [Inject]
        public ShowTimeSignal ShowTimeSignal { get; set; }


        public override void OnRegister()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            _view.ShowTimerSignal.AddListener(OnShowTimerSignal);
            ChangeTimerStatusSignal.AddListener(OnChangedTimerStatus);
            SetTimerSignal.AddListener(OnSetTime);
            ShowAllSignal.AddListener(OnShowAll);
        }


        public override void OnRemove()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            _view.ShowTimerSignal.RemoveListener(OnShowTimerSignal);
            ChangeTimerStatusSignal.RemoveListener(OnChangedTimerStatus);
            SetTimerSignal.RemoveListener(OnSetTime);
            ShowAllSignal.RemoveListener(OnShowAll);
        }

        private void OnChangedTimerStatus(TimerTypes timerType)
        {
            switch (timerType)
            {
                case TimerTypes.Start:
                    _view.StartTimer();
                    break;
                case TimerTypes.Stop:
                    _view.StopTimer();
                    break;
                case TimerTypes.Reset:
                    _view.ResetTimer();
                    break;
                default:
                    break;
            }
        }

        private void OnShowTimerSignal(bool show)
        {
            ShowTimeSignal.Dispatch(show);
        }

        private void OnSetTime(float differenceInSeconds)
        {
            Debug.Log("SET TIME " + differenceInSeconds);
            _view.SetTimer(_view._timeFrom + differenceInSeconds);
        }

        private void OnTimerRunDown()
        {
            TimerRunDownSignal.Dispatch();
        }

        private void OnShowAll()
        {
            _view.Show(false);
        }
    }
}