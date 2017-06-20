using strange.extensions.mediation.impl;

namespace VisualiseR.Common
{
    /// <summary>
    /// Mediator for the <see cref="NetworkedPlayer"/>
    /// </summary>
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
            _view.InstantiatePlayer.AddListener(OnInstantiatePlayer);
            InstantiatedPlayerSignal.AddListener(OnPlayerInstantiated);
        }

        public override void OnRemove()
        {
            _view.InstantiatePlayer.RemoveListener(OnInstantiatePlayer);
            InstantiatedPlayerSignal.RemoveListener(OnPlayerInstantiated);
        }

        private void OnInstantiatePlayer(bool isMasterClient)
        {
            InstantiatePlayerSignal.Dispatch(isMasterClient);
        }

        private void OnPlayerInstantiated(Player player)
        {
            _view.InitPlayer(player);
        }
    }
}