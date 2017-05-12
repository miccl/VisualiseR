using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    [Serializable]
    public class Room : IRoom
    {
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public IMedium Medium { get; set; }
        public List<IPlayer> Players { get; set; }
        public DateTime CreationDate { get; private set; }

        public Room()
        {
            CreationDate = DateTime.Now;
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            Players.Remove(player);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}, Players: {2}, CreationDate: {3}", Name, Type, Players,
                CreationDate);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Type.ToString()) || Players.Count == 0;
        }
    }
}