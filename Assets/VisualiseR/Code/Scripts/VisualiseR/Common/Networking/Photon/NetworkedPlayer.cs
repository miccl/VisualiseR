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
        internal Signal<bool> InstantiatePlayer = new Signal<bool>();

        internal IPlayer _player;

        private Transform _playerGlobal;
        private Transform _playerLocal;

        private GameObject _avatar;
        private GameObject _head;
        private TextMesh _name;

        protected override void Awake()
        {
            base.Awake();
            _avatar = transform.Find("Avatar").gameObject;
            _head = _avatar.transform.Find("Head").gameObject;
            _name = _avatar.transform.Find("Name").GetComponent<TextMesh>();
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

                _avatar.SetActive(false);

                InstantiatePlayer.Dispatch(PhotonNetwork.isMasterClient);
            }
        }

        internal void InitPlayer(Player player)
        {
            _player = player;

            Color color = _player.GetAvatarColor();
            DyeAvatar(color);

            InitPlayerName();
        }

        public Color GetAvatarColor(AvatarType type)
        {
            switch (type)
            {
                case AvatarType.Green:
                    return Color.green;
                case AvatarType.Red:
                    return Color.red;
                case AvatarType.Yellow:
                    return Color.yellow;
                default:
                    return Color.blue;
            }
        }

        private void DyeAvatar(Color color)
        {
            _head.GetComponent<MeshRenderer>().material.color = color;
            _name.color = color;
        }

        private void InitPlayerName()
        {
            _name.text = _player.Name;
        }

        /// <summary>
        /// Synchronise with other players.
        /// </summary>
        /// <param name="stream"></param>
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(_playerGlobal.position);
                stream.SendNext(_playerGlobal.rotation);
                stream.SendNext(_playerLocal.localPosition);
                stream.SendNext(_playerLocal.localRotation);
                stream.SendNext((int) _player.Avatar);
                stream.SendNext(_player.Name);
            }
            else
            {
                transform.position = (Vector3) stream.ReceiveNext();
                transform.rotation = (Quaternion) stream.ReceiveNext();
                _avatar.transform.localPosition = (Vector3) stream.ReceiveNext();
                _avatar.transform.localRotation = (Quaternion) stream.ReceiveNext();
                _head.GetComponent<Material>().color = GetAvatarColor((AvatarType) (int) stream.ReceiveNext());
                _name.text = (string) stream.ReceiveNext();
            }
        }
    }
}