using strange.extensions.mediation.impl;
using UnityEngine;
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


        public override void OnRegister()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            ChangeTimerStatusSignal.AddListener(OnChangedTimerStatus);
            SetTimerSignal.AddListener(OnSetTime);
        }


        public override void OnRemove()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            ChangeTimerStatusSignal.RemoveListener(OnChangedTimerStatus);
            SetTimerSignal.RemoveListener(OnSetTime);

        }

        private void OnChangedTimerStatus(TimerTypes timerType)
        {
            Debug.Log("WARUM??? " + timerType);
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

        private void OnSetTime(float timeInSeconds)
        {
            _view.SetTimer(timeInSeconds);
        }

        private void OnTimerRunDown()
        {
            TimerRunDownSignal.Dispatch();
        }
    }
}