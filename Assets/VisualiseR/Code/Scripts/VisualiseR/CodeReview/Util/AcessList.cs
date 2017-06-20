using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Acess list for the different types of players.
    /// </summary>
    public static class AcessList
    {
        public static readonly List<PlayerType> NAVIGATE_CODE = new List<PlayerType> {PlayerType.Host};
        public static readonly List<PlayerType> SCENE_MENU = new List<PlayerType> {PlayerType.Host, PlayerType.Client};
        public static readonly List<PlayerType> CONTEXT_MENU =  new List<PlayerType> {PlayerType.Host, PlayerType.Client};

        public static readonly string ERROR_MESSAGE = "Player {0} has no rights for command '{1}'";
    }
}