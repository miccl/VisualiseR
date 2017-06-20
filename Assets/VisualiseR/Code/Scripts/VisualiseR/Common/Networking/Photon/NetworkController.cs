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

        public void Init(string roomName)
        {
            _roomName = roomName;
        }

        /// <summary>
        /// Enters the lobby room.
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

        void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        }

        private static RoomOptions InitRoomOptions()
        {
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = true,
                MaxPlayers = 21
            };
            return roomOptions;
        }
    }
}