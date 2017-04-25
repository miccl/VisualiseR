using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public List<Player> Players { get; set; }
        public DateTime CreationDate { get; set; }

        public Room(string name, RoomType type) : this(name, type, DateTime.Now)
        {
        }

        public Room(string name, RoomType type, DateTime creationDate)
        {
            Name = name;
            Type = type;
            CreationDate = creationDate;
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}, Players: {2}, CreationDate: {3}", Name, Type, Players,
                CreationDate);
        }
    }

    public enum RoomType
    {
        CodeReview,
        LiveCoding,
        ArchitecturalReview,
        Presentation,
        Showroom
    };
}