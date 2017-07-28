using strange.extensions.mediation.impl;
using VisualiseR.Common;

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

                                
        public override void OnRegister()
        {
            _view.CancelButtonPressedSignal.AddListener(OnCancelButtonClicked);
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            ShowroomSceneMenuIsShownSignal.AddListener(OnSceneMenuIsShown);
            _view._contextView = contextView;
        }

        public override void OnRemove()
        {
            _view.CancelButtonPressedSignal.RemoveListener(OnCancelButtonClicked);
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            ShowroomSceneMenuIsShownSignal.RemoveListener(OnSceneMenuIsShown);
        }

        private void OnCancelButtonClicked()
        {
            ShowShowroomSceneMenuSignal.Dispatch(!_view._isSceneMenuShown);
        }

        private void OnPlayerInstantiated(Player player)
        {
            _view.Init(player);
        }
        
        private void OnSceneMenuIsShown(bool isShown)
        {
            _view._isSceneMenuShown = isShown;
        }

    }
}