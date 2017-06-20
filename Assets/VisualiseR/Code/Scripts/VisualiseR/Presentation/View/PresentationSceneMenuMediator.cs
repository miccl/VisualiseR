using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Mediator for the <see cref="PresentationSceneMenuView"/>
    /// </summary>
    public class PresentationSceneMenuMediator : Mediator
    {
        [Inject]
        public PresentationSceneMenuView _view { get; set; }

        [Inject]
        public PresentationSceneMenuIsShownSignal PresentationSceneMenuIsShownSignal { get; set; }

        [Inject]
        public ChangeTimerStatusSignal ChangeTimerStatusSignal { get; set; }

        [Inject]
        public SetTimerSignal SetTimerSignal { get; set; }

        [Inject]
        public TimerRunDownSignal TimerRunDownSignal { get; set; }

        [Inject]
        public PrevSlideSignal PrevSlideSignal { get; set; }
        
        [Inject]
        public ShowAllSignal ShowAllSignal{ get; set; }
        
        [Inject]
        public ShowTimeSignal ShowTimeSignal { get; set; }
        
        [Inject]
        public ShowLaserSignal ShowLaserSignal { get; set; }
        
        [Inject]
        public ChangeClockTypeSignal ChangeClockTypeSignal { get; set; }
       
        public override void OnRegister()
        {
            _view.OnContextMenuCanceled.AddListener(OnContextMenuCanceled);
            _view.ChangeTimerStatusSignal.AddListener(OnTimerStatusChanged);
            _view.SetTimerSignal.AddListener(SetTimer);
            _view.ShowPreviousSignal.AddListener(OnShowPrevious);
            _view.ShowAllSignal.AddListener(OnShowAll);
            _view.ShowTimerSignal.AddListener(OnShowTimerSignal);
            _view.ShowLaserSignal.AddListener(OnShowLaserClick);
            _view.ChangeClockTypesSignal.AddListener(OnClockTypeChanged);
            TimerRunDownSignal.AddListener(TimerRunDown);
            ShowLaserSignal.AddListener(OnShowLaser);
        }

        private void OnShowPrevious(IPlayer player, ISlideMedium medium)
        {
            PrevSlideSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        public override void OnRemove()
        {
            _view.OnContextMenuCanceled.RemoveListener(OnContextMenuCanceled);
            _view.ChangeTimerStatusSignal.RemoveListener(OnTimerStatusChanged);
            _view.SetTimerSignal.RemoveListener(SetTimer);
            _view.ShowPreviousSignal.RemoveListener(OnShowPrevious);
            _view.ShowAllSignal.RemoveListener(OnShowAll);
            _view.ShowTimerSignal.RemoveListener(OnShowTimerSignal);
            _view.ShowLaserSignal.AddListener(OnShowLaserClick);
            _view.ChangeClockTypesSignal.AddListener(OnClockTypeChanged);
            TimerRunDownSignal.RemoveListener(TimerRunDown);
        }


        private void OnShowTimerSignal(bool show)
        {
            ShowTimeSignal.Dispatch(show);
        }

        private void OnContextMenuCanceled()
        {
            PresentationSceneMenuIsShownSignal.Dispatch(false);
        }

        private void OnTimerStatusChanged(TimerType timerTypes)
        {
            ChangeTimerStatusSignal.Dispatch(timerTypes);
        }

        private void SetTimer(float timeInSeconds)
        {
            SetTimerSignal.Dispatch(timeInSeconds);
        }

        private void TimerRunDown()
        {
            _view.RefreshStartStopButtonText();
        }

        private void OnShowAll()
        {
            ShowAllSignal.Dispatch();
        }

        private void OnShowLaserClick(bool show)
        {
            ShowLaserSignal.Dispatch(show);
        }

        private void OnClockTypeChanged(ClockType type)
        {
            ChangeClockTypeSignal.Dispatch(type);
        }

        private void OnShowLaser(bool show)
        {
            _view._isLaserShown = show;
            _view.ChangeLaserText();
        }
    }
}