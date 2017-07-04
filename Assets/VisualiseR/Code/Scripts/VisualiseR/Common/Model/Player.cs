using System;
using System.Collections.Generic;
using VisualiseR.CodeReview;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    [Serializable]
    public class Player : IPlayer
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(Player));

        
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
            return string.Format("Name: {0} (Type: {1}, Avatar: {2}, JoinDate: {3})", Name, Type, Avatar, JoinDate);
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