using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        string JoinDate { get; set; }
        bool IsHost { get; set; }
    }
}