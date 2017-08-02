using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Common
{
    /// <summary>
    /// Handles connecting to the photon network and instantiating the player.
    /// </summary>
    public class NetworkController : View
    {
        private JCsLogger Logger;
        
        /// <summary>
        /// Max number of players that can join the room at the same time.
        /// In free cost model of photon, the limit is 20.
        /// </summary>
        private static readonly int MAX_PLAYER_COUNT = 20;

        internal string _roomName = null;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(NetworkController));
        }

        /// <summary>
        /// Connects to the photon network.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            PhotonNetwork.ConnectUsingSettings("0.1");
        }
        
        /// <summary>
        /// Initialises network controller.
        /// </summary>
        /// <param name="roomName"></param>
        public void Init(string roomName)
        {
            _roomName = roomName;
        }

        /// <summary>
        /// Called when the player enters the lobby. 
        /// Creates or joins a room with room options.
        /// </summary>
        void OnJoinedLobby()
        {
            Logger.Info("Joined lobby");
            if (!string.IsNullOrEmpty(_roomName))
            {
                //TODO abfangen, wenn ein Raum gejoined wurde, der nicht existiert...
                RoomOptions roomOptions = InitRoomOptions();

                PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
            }
        }

        /// <summary>
        /// Called when the player sucessfully joined the room. 
        /// Initalises the <see cref="NetworkedPlayer"/> into the scene.
        /// </summary>
        void OnJoinedRoom()
        {
            Logger.InfoFormat("Joined room '{0}'", _roomName);
            PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        }
        
        /// <summary>
        /// Called when the player sucessfully create the room.
        /// </summary>
        void OnCreatedRoom()
        {
            Logger.InfoFormat("Created room '{0}'", _roomName);
        }
        
        /// <summary>
        /// Initialises the room options.
        /// </summary>
        /// <returns></returns>
        private static RoomOptions InitRoomOptions()
        {
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = false,
                MaxPlayers = (byte) MAX_PLAYER_COUNT
            };
            return roomOptions;
        }
    }
}