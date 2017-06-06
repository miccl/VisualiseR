using System.Collections.Generic;
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
        List<Vector3> remainingClientPositions = new List<Vector3>();

        public static readonly int AMOUNT_OF_SEATS = 5;
        static Random rnd = new Random();


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
                AdjustPosition();
                _playerLocal = _playerGlobal.Find("GvrFPSController/FirstPersonCharacter");

                transform.SetParent(_playerLocal);
                transform.localPosition = Vector3.zero;

                Avatar.SetActive(false);
            }
        }

        private void AdjustPosition()
        {
            if (PhotonNetwork.isMasterClient)
            {
                _playerGlobal.position = Positions.HOST_POS;
                _playerGlobal.rotation = Quaternion.Euler(0, -180, 0);
                remainingClientPositions = GetStandPositions();
            }
            else
            {
                RequestPositionFromMaster();
            }
        }

        private Vector3 GetRandomStandPosition()
        {
            int r = Random.Range(0, remainingClientPositions.Count);
            Vector3 pos = remainingClientPositions[r];
            remainingClientPositions.RemoveAt(r);
            return pos;
        }


        private List<Vector3> GetStandPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            
            var stand = GameObject.Find("Environment").transform.Find("Stand");
            
            List<Transform> rows = new List<Transform>();
            rows.Add(stand.transform.Find("Row1"));
            rows.Add(stand.transform.Find("Row2"));
            rows.Add(stand.transform.Find("Row3"));
            rows.Add(stand.transform.Find("Row4"));

            for (int i = 0; i < rows.Count; i++)
            {
                Vector3 firstSeatPosition = rows[i].Find("FirstSeat").position;
                Vector3 lastSeatPosition = rows[i].Find("LastSeat").position;
                List<Vector3> seatPositions =
                    Positions.GetRowSeatPositions(firstSeatPosition, lastSeatPosition, AMOUNT_OF_SEATS);
                positions.AddRange(seatPositions);
            }
            return positions;
        }

        internal void InitPlayer(Player player)
        {
            _player = player;
            //TODO avatar und n
        }

        internal void RequestPositionFromMaster()
        {
            GetComponent<PhotonView>().RPC("OnPositionRequest",
                PhotonTargets.MasterClient,
                PhotonNetwork.player.ID);
            Logger.DebugFormat("Player (id '{0}'): Reqested position from master", PhotonNetwork.player.ID);
        }

        [PunRPC]
        void OnPositionRequest(int playerId)
        {
            Vector3 playerPos = GetRandomStandPosition();
            GetComponent<PhotonView>().RPC("OnPositionReceived",
                PhotonPlayer.Find(playerId),
                playerPos);
            Logger.DebugFormat("Master: Send position (pos '{1}') to player (id '{0}')", playerId, playerPos);
        }

        [PunRPC]
        void OnPositionReceived(Vector3 playerPos)
        {
            _playerGlobal.position = GetRandomStandPosition();
            _playerGlobal.rotation = Quaternion.Euler(0, 0, 0);
            Logger.DebugFormat("Player (id '{0}'): Received position '{1}' from master", PhotonNetwork.player.ID,
                playerPos);
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