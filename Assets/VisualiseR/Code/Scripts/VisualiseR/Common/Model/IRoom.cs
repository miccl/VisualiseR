using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    public interface IRoom
    {
        string Name { get; set; }
        RoomType Type { get; set; }
        List<IPlayer> Players { get; set; }
        DateTime CreationDate { get;}

        void AddPlayer(IPlayer player);

        void RemovePlayer(IPlayer player);

        bool IsEmpty();


    }
}