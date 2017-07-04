using System;

namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a player of the game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Type of the player
        /// </summary>
        PlayerType Type { get; set; }
        /// <summary>
        /// Avatar of the player.
        /// </summary>
        AvatarType Avatar { get; set; }
        /// <summary>
        /// Date the player joined the game.
        /// </summary>
        DateTime JoinDate { get;}

        /// <summary>
        /// Returns <c>true</c>, if the player is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
        /// <summary>
        /// Returns <c>true</c>, if the player is a host.
        /// </summary>
        /// <returns></returns>
        bool IsHost();
    }
}