using System;
using System.Collections.Generic;
using VisualiseR.CodeReview;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a player of the game.
    /// </summary>
    [Serializable]
    public class Player : IPlayer
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(Player));

        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of the player
        /// </summary>
        public PlayerType Type { get; set; }
        /// <summary>
        /// Avatar of the player.
        /// </summary>
        public AvatarType Avatar { get; set; }
        /// <summary>
        /// Date the player joined the game.
        /// </summary>
        public DateTime JoinDate { get; private set; }

        /// <summary>
        /// Ctor of the player.
        /// </summary>
        public Player()
        {
            JoinDate = DateTime.Now;
        }
        
        /// <summary>
        /// Returns <c>true</c>, if the player is empty.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name: {0} (Type: {1}, Avatar: {2}, JoinDate: {3})", Name, Type, Avatar, JoinDate);
        }

        /// <summary>
        /// Returns <c>true</c>, if the player has the command right.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool HasRight(List<PlayerType> command)
        {
            return command.Contains(Type);
        }

        /// <summary>
        /// Returns <c>true</c>, if the player is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Type.ToString());
        }
        
        /// <summary>
        /// Returns <c>true</c>, if the player is a host.
        /// </summary>
        /// <returns></returns>
        public bool IsHost()
        {
            return Type.Equals(PlayerType.Host);
        }
    }
}