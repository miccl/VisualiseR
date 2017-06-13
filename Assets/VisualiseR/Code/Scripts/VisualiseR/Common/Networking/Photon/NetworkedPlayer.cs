using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace VisualiseR.Common
{
    /// <summary>
    /// Handles the synchronisation of the players.
    /// Uses <see cref="OnPhotonSerializeView"/> to synchronize with the other players.
    /// </summary>
    public class NetworkedPlayer : View
    {
        private static JCsLogger Logger;

        internal Signal<bool> InstantiatePlayer = new Signal<bool>();

        internal IPlayer _player;

        private Transform _playerGlobal;
        private Transform _playerLocal;
        private GameObject _avatar;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(NetworkedPlayer));
            _avatar = transform.Find("Avatar").gameObject;
        }

        protected override void Start()
        {
            base.Start();

            if (photonView.isMine)
            {
                _playerGlobal = GameObject.Find("GvrNetworkedPlayer").transform;
                _playerLocal = _playerGlobal.Find("GvrFPSController/FirstPersonCharacter");

                transform.SetParent(_playerLocal);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

//                _avatar.SetActive(false);

                InstantiatePlayer.Dispatch(PhotonNetwork.isMasterClient);
            }
        }

        internal void InitPlayer(Player player)
        {
            _player = player;
            
            InitAvatar();
            //TODO avatar und n
        }

        private void InitAvatar()
        {
            Color color;
            switch (_player.Avatar)
            {
                case AvatarType.Blue:
                    color = Color.blue;
                    break;
                case AvatarType.Green:
                    color = Color.green;
                    break;
                case AvatarType.Red:
                    color = Color.red;
                    break;
                case AvatarType.Yellow:
                    color = Color.yellow;
                    break;
                default:
                    color = Color.blue;
                    break;
            }
            DyeAvatar(color);
        }

        private void DyeAvatar(Color color)
        {
            _avatar.transform.Find("Head").GetComponent<MeshRenderer>().material.color = color;
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
                _avatar.transform.localPosition = (Vector3) stream.ReceiveNext();
                _avatar.transform.localRotation = (Quaternion) stream.ReceiveNext();
            }
        }
    }
}