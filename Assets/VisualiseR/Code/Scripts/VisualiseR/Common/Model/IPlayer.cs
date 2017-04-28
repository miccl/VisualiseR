using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    public interface IPlayer
    {
        string Name { get; set; }
        DateTime JoinDate { get; set; }
        PlayerType Type { get; set; }
    }
}