using System;
using System.Collections.Generic;

namespace VisualiseR.Common
{
    /// <summary>
    /// Represents a room.
    /// </summary>
    [Serializable]
    public class Room : IRoom
    {
        /// <summary>
        /// Name of the room.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of the room.
        /// </summary>
        public RoomType Type { get; set; }
        /// <summary>
        /// Medium that is presented in the room.
        /// </summary>
        public IMedium Medium { get; set; }
        /// <summary>
        /// Players of the room.
        /// </summary>
        public List<IPlayer> Players { get; set; }
        /// <summary>
        /// Date of the room creation.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Ctor of the <see cref="Room"/>.
        /// </summary>
        public Room()
        {
            Players = new List<IPlayer>();
            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Adds a player to the room.
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }
        
        /// <summary>
        /// Removes a player from the room.
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(IPlayer player)
        {
            Players.Remove(player);
        }

        /// <summary>
        /// Prints the <see cref="Room"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}, Players: {2}, CreationDate: {3}", Name, Type, Players,
                CreationDate);
        }
        
        /// <summary>
        /// Returns <c>true</c>, if the room is not initialised yet.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Type.ToString()) || Players.Count == 0;
        }
    }
}