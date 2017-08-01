using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Mediator for the <see cref="GroundMediator"/>
    /// </summary>
    public class GroundMediator : Mediator
    {
        [Inject]
        public GroundView _view { get; set; }
        
        [Inject]
        public TeleportPlayerSignal TeleportPlayerSignal { get; set; }
        
        public override void OnRegister()
        {
            _view.OnClickSignal.AddListener(OnClick);
        }

        public override void OnRemove()
        {
            _view.OnClickSignal.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            TeleportPlayerSignal.Dispatch();
        }
    }
}