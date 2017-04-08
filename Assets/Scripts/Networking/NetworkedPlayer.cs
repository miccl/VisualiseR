using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

namespace Networking.Photon
{
    /// <summary>
    /// Handles the synchronisation of the players.
    /// Uses <see cref="OnPhotonSerializeView"/> to synchronize with the other players.
    /// </summary>
    public class NetworkedPlayer : MonoBehaviour
    {
        public GameObject Avatar;

        private Transform _playerGlobal;
        private Transform _playerLocal;

        void Start()
        {
            Debug.Log("i'm instantiated");
            if (photonView.isMine)
            {
                Debug.Log("player is mine");

                _playerGlobal = GameObject.Find("GvrViewerMain/FPSController").transform;
                _playerLocal = _playerGlobal.Find("FirstPersonCharacter");

                transform.SetParent(_playerLocal);
                transform.localPosition = Vector3.zero;

                // avatar.SetActive(false);
            }
        }

        // synchronsize with the others
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(_playerGlobal.position);
                stream.SendNext(_playerGlobal.rotation);
                stream.SendNext(_playerLocal.localPosition);
                stream.SendNext(_playerLocal.localRotation);
            }
            else
            {
                transform.position = (Vector3) stream.ReceiveNext();
                transform.rotation = (Quaternion) stream.ReceiveNext();
                Avatar.transform.localPosition = (Vector3) stream.ReceiveNext();
                Avatar.transform.localRotation = (Quaternion) stream.ReceiveNext();
            }
        }
    }
}