using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    public class InstantiatePlayerCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(InstantiatePlayerCommand));

        [Inject]
        public bool _isMasterClient { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }

        public override void Execute()
        {
            Player.Name = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            Player.Type = _isMasterClient ? PlayerType.Host : PlayerType.Client;
            if (Player.Type.Equals(PlayerType.Host))
            {
                
            }
            Logger.InfoFormat("Instantiated player '{0}'", Player);
            PlayerInstantiatedSignal.Dispatch((Player) Player);
        }
    }
}