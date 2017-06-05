using strange.extensions.mediation.impl;
using VisualiseR.Common;
using VisualiseR.Presentation;

namespace Networking.Photon
{
    public class NetworkedPlayerMediator : Mediator
    {
        [Inject]
        public NetworkedPlayer _view { get; set; }
        
        [Inject]
        public IPlayer _player { get; set; }
                
        [Inject]
        public InstantiatePlayerSignal InstantiatePlayerSignal { get; set; }
        
        [Inject]
        public PlayerInstantiatedSignal InstantiatedPlayerSignal { get; set; }


        public override void OnRegister()
        {
            _view.PlayerInstantiated.AddListener(OnPlayerInstantiated);
            InstantiatedPlayerSignal.AddListener(OnPlayerInstantiated);
        }

        public override void OnRemove()
        {
            _view.PlayerInstantiated.RemoveListener(OnPlayerInstantiated);
            InstantiatedPlayerSignal.RemoveListener(OnPlayerInstantiated);
        }

        private void OnPlayerInstantiated(bool isMasterClient)
        {
            InstantiatePlayerSignal.Dispatch(isMasterClient);
        }

        private void OnPlayerInstantiated(Player player)
        {
            _view.InitPlayer(player);
        }
    }
}