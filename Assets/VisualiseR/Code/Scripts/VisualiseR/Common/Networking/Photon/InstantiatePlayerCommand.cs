using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    /// <summary>
    /// Command to instantiate the <see cref="Player"/>
    /// </summary>
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
            InitiatePlayer();
            Logger.InfoFormat("Instantiated player '{0}'", Player);
            PlayerInstantiatedSignal.Dispatch((Player) Player);

        }

        /// <summary>
        /// Initialises the player.
        /// </summary>
        private void InitiatePlayer()
        {
            Player.Name = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            Player.Type = _isMasterClient ? PlayerType.Host : PlayerType.Client;
            Player.Avatar = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.AVATAR_KEY).ToEnum<AvatarType>();
        }
    }
}