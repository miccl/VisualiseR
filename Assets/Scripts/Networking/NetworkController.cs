using UnityEngine;

    /// <summary>
    /// Handles connecting to the photon network and instantiating the player.
    ///
    /// </summary>
    public class NetworkController : MonoBehaviour {
        string _room = "MyRoom";

        /// <summary>
        /// Connects to the photon network.
        /// </summary>
        private void Start() {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        /// <summary>
        /// Enters the lobby room.
        /// </summary>
        void OnJoinedLobby() {
            Debug.Log("joined lobby");

            RoomOptions roomOptions = new RoomOptions() { };
            PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
        }

        /// <summary>
        ///
        /// </summary>
        void OnJoinedRoom() {
            PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        }
    }