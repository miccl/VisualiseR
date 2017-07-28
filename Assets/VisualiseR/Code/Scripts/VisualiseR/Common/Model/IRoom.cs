using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Name of the room.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Type of the room.
        /// </summary>
        RoomType Type { get; set; }
        /// <summary>
        /// Medium that is presented in the room.
        /// </summary>
        IMedium Medium { get; set; }
        /// <summary>
        /// Players of the room.
        /// </summary>
        List<IPlayer> Players { get; set; }
        /// <summary>
        /// Date of the room creation.
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Adds a player to the room.
        /// </summary>
        /// <param name="player"></param>
        void AddPlayer(IPlayer player);

        /// <summary>
        /// Removes a player from the room.
        /// </summary>
        /// <param name="player"></param>
        void RemovePlayer(IPlayer player);

        /// <summary>
        /// Returns <c>true</c>, if the room is not initialised yet.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}