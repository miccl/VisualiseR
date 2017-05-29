using System;

namespace VisualiseR.Common
{
    public interface IPlayer
    {
        string Name { get; set; }
        PlayerType Type { get; set; }
        DateTime JoinDate { get;}

        bool IsEmpty();
    }
}