using System;
using UnityEngine;

namespace VisualiseR.Common
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public DateTime JoinDate { get; set; }

        public Player()
        {
            JoinDate = DateTime.Now;
        }

        public Player(string name, PlayerType type) : this(name, type, DateTime.Now)
        {
        }

        public Player(string name, PlayerType type, DateTime joinDate)
        {
            Name = name;
            Type = type;
            JoinDate = joinDate;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} (Type: {1}, JoinDate: {2})", Name, Type, JoinDate);
        }
    }
}