using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public static class AcessList
    {
        public static List<PlayerType> NavigateCodeRight = new List<PlayerType> {PlayerType.Host};
        public static List<PlayerType> SceneMenu = new List<PlayerType> {PlayerType.Host, PlayerType.Client};
        public static List<PlayerType> ContextMenu =  new List<PlayerType> {PlayerType.Host, PlayerType.Client};

        public static string errorMessageFormat = "Player {0} has no rights for command '{1}'";
    }
}