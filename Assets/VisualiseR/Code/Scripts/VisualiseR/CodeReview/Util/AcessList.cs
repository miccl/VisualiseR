using System.Collections.Generic;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public static class AcessList
    {
        public static List<PlayerType> NavigateCodeRight = new List<PlayerType> {PlayerType.Host};

        public static string errorMessageFormat = "Player {0} has no rights for command '{1}'";
    }
}