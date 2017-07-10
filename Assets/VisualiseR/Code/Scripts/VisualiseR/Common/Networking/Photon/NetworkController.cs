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
                
                if (PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default))
                {
                    Logger.InfoFormat("Created or joined room '{0}'", _roomName);
                    return;
                }
                
                Logger.ErrorFormat("Couldn't create or join the room '{0}'", _roomName);
                UnityUtil.LoadScene("Main");
            }
        }

        /// <summary>
        /// Called when the player joined the room. 
        /// Initalises the <see cref="NetworkedPlayer"/> into the scene.
        /// </summary>
        void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        }

        /// <summary>
        /// Initialises the room options.
        /// </summary>
        /// <returns></returns>
        private static RoomOptions InitRoomOptions()
        {
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = true,
                MaxPlayers = (byte) MAX_PLAYER_COUNT
            };
            return roomOptions;
        }
    }
}