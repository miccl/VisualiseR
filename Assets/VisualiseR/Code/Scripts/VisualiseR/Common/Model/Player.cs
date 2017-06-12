using System;
using System.Collections.Generic;
using VisualiseR.CodeReview;

namespace VisualiseR.Common
{
    [Serializable]
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public AvatarType Avatar { get; set; }
        public DateTime JoinDate { get; private set; }

        public Player()
        {
            JoinDate = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} (Type: {1}, JoinDate: {2})", Name, Type, JoinDate);
        }

        public bool HasRight(List<PlayerType> command)
        {
            return command.Contains(Type);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Type.ToString());
        }

        public bool IsHost()
        {
            return Type.Equals(PlayerType.Host);
        }
    }
}