using strange.extensions.mediation.impl;
using VisualiseR.Common;
using VisualiseR.Presentation;

namespace Networking.Photon
{
    public class NetworkedPlayerMediator : Mediator
    {
        [Inject]
        public NetworkedPlayerView _view { get; set; }
        
        [Inject]
        public IPlayer _player { get; set; }
        
        [Inject]
        public InstantiatePlayerSignal InstantiatePlayerSignal { get; set; }

        public override void OnRegister()
        {
            _view.UserInstantiatedSignal.AddListener(OnPlayerInstantiated);
        }

        public override void OnRemove()
        {
            _view.UserInstantiatedSignal.AddListener(OnPlayerInstantiated);
        }

        private void OnPlayerInstantiated(bool isMasterClient)
        {
            InstantiatePlayerSignal.Dispatch(isMasterClient);
        }
    }
}