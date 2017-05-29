using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Networking.Photon
{
    /// <summary>
    /// Handles the synchronisation of the players.
    /// Uses <see cref="OnPhotonSerializeView"/> to synchronize with the other players.
    /// </summary>
    public class NetworkedPlayerView : View
    {
        private static JCsLogger Logger;

        internal Signal<bool> UserInstantiatedSignal = new Signal<bool>();
        
        public GameObject Avatar;

        private Transform _playerGlobal;
        private Transform _playerLocal;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(NetworkedPlayerView));
        }

        protected override void Start()
        {
            base.Start();

            Logger.Info("User is instantiated");
            if (PhotonNetwork.isMasterClient)
            {
                Logger.Info("User is master");
            }
            else
            {
                Logger.Info("User is client");
            }

            if (photonView.isMine)
            {
                UserInstantiatedSignal.Dispatch(PhotonNetwork.isMasterClient);
                Logger.Info("Player is mine");

                _playerGlobal = GameObject.Find("GvrNetworkedPlayer").transform;
                _playerLocal = _playerGlobal.Find("GvrFPSController/FirstPersonCharacter");

                transform.SetParent(_playerLocal);
                transform.localPosition = Vector3.zero;

                Avatar.SetActive(false);
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