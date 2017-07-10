using System.Collections.Generic;

namespace VisualiseR.Common
{
    /// <summary>
    /// Acess list for the different types of players.
    /// </summary>
    public static class AcessList
    {
        /// <summary>
        /// Generic error message.
        /// Example usage: <code>Logger.InfoFormat(AcessList.ERROR_MESSAGE, _player, typeof(NextSlideCommand));</code>
        /// </summary>
        public static readonly string ERROR_MESSAGE = "Player {0} has no rights for command '{1}'";

        /// <summary>
        /// Right to navigate between the mediums (e.g. slides, code fragments)
        /// </summary>
        public static readonly List<PlayerType> NAVIGATE_MEDIUM = new List<PlayerType> {PlayerType.Host};

        /// <summary>
        /// Right to show the scene menu.
        /// </summary>
        public static readonly List<PlayerType> SCENE_MENU = new List<PlayerType> {PlayerType.Host, PlayerType.Client};

        /// <summary>
        /// Right to show the context menu.
        /// </summary>
        public static readonly List<PlayerType> CONTEXT_MENU = new List<PlayerType> {PlayerType.Host};
    }
}