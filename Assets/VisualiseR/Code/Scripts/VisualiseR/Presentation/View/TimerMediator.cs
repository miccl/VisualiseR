using strange.extensions.mediation.impl;

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

        [Inject]
        public ChangeClockTypeSignal ChangeClockTypeSignal { get; set; }

        public override void OnRegister()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            _view.ShowTimerSignal.AddListener(OnShowTimerSignal);
            ChangeTimerStatusSignal.AddListener(OnChangedTimerStatus);
            SetTimerSignal.AddListener(OnSetTime);
            ShowAllSignal.AddListener(OnShowAll);
            ChangeClockTypeSignal.AddListener(OnClockTypeChanged);
        }


        public override void OnRemove()
        {
            _view.TimerRunDownSignal.AddListener(OnTimerRunDown);
            _view.ShowTimerSignal.RemoveListener(OnShowTimerSignal);
            ChangeTimerStatusSignal.RemoveListener(OnChangedTimerStatus);
            SetTimerSignal.RemoveListener(OnSetTime);
            ShowAllSignal.RemoveListener(OnShowAll);
            ChangeClockTypeSignal.RemoveListener(OnClockTypeChanged);
        }

        private void OnChangedTimerStatus(TimerType timerType)
        {
            switch (timerType)
            {
                case TimerType.Start:
                    _view.StartTimer();
                    break;
                case TimerType.Stop:
                    _view.StopTimer();
                    break;
                case TimerType.Reset:
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

        private void OnClockTypeChanged(ClockType type)
        {
            _view.ChangeClockType(type);
        }
    }
}