using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Mediator for the <see cref="PresentationPlayerView"/>
    /// </summary>
    public class PresentationPlayerMediator : Mediator
    {
        [Inject]
        public PresentationPlayerView _view { get; set; }
        
        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }
        
        [Inject]
        public ShowLaserSignal ShowLaserSignal { get; set; }
        
        [Inject]
        public ShowLoadingAnimationSignal ShowLoadingAnimationSignal { get; set; }
                
        public override void OnRegister()
        {
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            ShowLaserSignal.AddListener(OnShowLaser);
            ShowLoadingAnimationSignal.AddListener(OnShowLoadingAnimation);
            _view._contextView = contextView;
        }

        public override void OnRemove()
        {
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            ShowLaserSignal.RemoveListener(OnShowLaser);
        }

        private void OnPlayerInstantiated(Player player)
        {
            _view.Init(player);
        }

        private void OnShowLaser(bool show)
        {
            _view.ShowLaser(show);
        }

        private void OnShowLoadingAnimation(bool show, string text)
        {
            if (!show)
            {
                _view.AdjustPosition();
            }
        }
    }
}