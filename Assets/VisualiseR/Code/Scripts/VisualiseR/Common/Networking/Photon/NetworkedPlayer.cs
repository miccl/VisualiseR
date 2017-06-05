using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Presentation;

namespace Networking.Photon
{
    /// <summary>
    /// Handles the synchronisation of the players.
    /// Uses <see cref="OnPhotonSerializeView"/> to synchronize with the other players.
    /// </summary>
    public class NetworkedPlayer : View
    {
        private static JCsLogger Logger;

        internal Signal<bool> PlayerInstantiated = new Signal<bool>();

        internal IPlayer _player;
        public GameObject Avatar;

        private Transform _playerGlobal;
        private Transform _playerLocal;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(NetworkedPlayer));
        }

        protected override void Start()
        {
            base.Start();

            if (photonView.isMine)
            {
                if (PhotonNetwork.isMasterClient)
                {
                    Logger.Info("User is master");
                }
                else
                {
                    Logger.Info("User is client");
                }


                PlayerInstantiated.Dispatch(PhotonNetwork.isMasterClient);

                _playerGlobal = GameObject.Find("GvrNetworkedPlayer").transform;
                AdjustPosition(_playerGlobal);
                _playerLocal = _playerGlobal.Find("GvrFPSController/FirstPersonCharacter");

                transform.SetParent(_playerLocal);
                transform.localPosition = Vector3.zero;

                Avatar.SetActive(false);
            }
        }

        private void AdjustPosition(Transform playerGlobal)
        {
            if (PhotonNetwork.isMasterClient)
            {
                _playerGlobal.position = Positions.HOST_POS;
                _playerGlobal.rotation = Quaternion.Euler(0,-180,0);
            }
            else
            {
                _playerGlobal.position = Positions.CLIENT_POS;
                _playerGlobal.rotation = Quaternion.Euler(0,0,0);
            }
        }

        internal void InitPlayer(Player player)
        {
            _player = player;
            //TODO avatar und n
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