using strange.extensions.mediation.impl;
using VisualiseR.Common;
using VisualiseR.Main;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Mediator for the <see cref="ShowroomPlayerView"/>
    /// </summary>
    public class ShowroomPlayerMediator : Mediator
    {
        [Inject]
        public ShowroomPlayerView _view { get; set; }
        
        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }
        
        [Inject]
        public ShowShowroomSceneMenuSignal ShowShowroomSceneMenuSignal { get; set; }
        
        [Inject]
        public ShowroomSceneMenuIsShownSignal ShowroomSceneMenuIsShownSignal { get; set; }
        
        [Inject]
        public CaptureScreenshotSignal CaptureScreenshotSignal { get; set; }
        
        [Inject]
        public ChangeEditModeSignal ChangeEditModeSignal { get; set; }

                                
        public override void OnRegister()
        {
            _view.CancelButtonPressedSignal.AddListener(OnCancelButtonClicked);
            _view.CaptureScreenshotSignal.AddListener(OnCaptureScreenshot);
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            ShowroomSceneMenuIsShownSignal.AddListener(OnSceneMenuIsShown);
            ChangeEditModeSignal.AddListener(OnEditModeChanged);
            _view._contextView = contextView;
        }

        public override void OnRemove()
        {
            _view.CancelButtonPressedSignal.RemoveListener(OnCancelButtonClicked);
            _view.CaptureScreenshotSignal.RemoveListener(OnCaptureScreenshot);
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            ChangeEditModeSignal.RemoveListener(OnEditModeChanged);
            ShowroomSceneMenuIsShownSignal.RemoveListener(OnSceneMenuIsShown);
        }

        private void OnCancelButtonClicked()
        {
            ShowShowroomSceneMenuSignal.Dispatch(true);
        }

        private void OnPlayerInstantiated(Player player)
        {
            _view.Init(player);
        }

        private void OnSceneMenuIsShown(bool isShown)
        {
            _view._isSceneMenuShown = isShown;
        }

        private void OnCaptureScreenshot()
        {
            CaptureScreenshotSignal.Dispatch();
        }

        private void OnEditModeChanged(EditMode mode)
        {
            _view._editMode = mode;
        }
    }
}