using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PlayerMediator : Mediator
    {
        [Inject]
        public PlayerView _view { get; set; }
        
        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }
        
        [Inject]
        public ShowLaserSignal ShowLaserSignal { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void OnRegister()
        {
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            ShowLaserSignal.AddListener(OnShowLaser);
            _view._contextView = _contextView;
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
    }
}