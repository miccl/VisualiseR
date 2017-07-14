using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// View of the player.
    /// Shows the laser.
    /// </summary>
    public class PresentationPlayerView : View
    {
        public static readonly int AMOUNT_OF_SEATS = 5;

        private JCsLogger Logger;

        private Player _player;
        private Transform _playerGlobal;

        internal GameObject _contextView;

        private GameObject _reticlePointer;

        private GameObject _laser;

        private GameObject _globalLaser;

        private GameObject _globalElements;

        private GameObject _globalReticlePointer;

        private bool _isLaserShown;

        private bool _firstFalse;


        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(PresentationPlayerView));
            _playerGlobal = GameObject.Find("GvrNetworkedPlayer").transform;
        }

        public void Init(Player player)
        {
            _player = player;
            InitView();
        }

        private void InitView()
        {
            _laser = transform.Find("GvrLaser").gameObject;
            _reticlePointer = _laser.transform.Find("GvrReticlePointer").gameObject;
            _globalElements = _contextView.transform.Find("GlobalElements").gameObject;
            _globalReticlePointer = _globalElements.transform.Find("GvrReticlePointer").gameObject;

            if (_player.IsHost())
            {
                _laser.SetActive(true);
                _globalElements.SetActive(false);
                ShowLaser(false);
            }
            else
            {
                _laser.SetActive(false);
            }
        }

        internal void AdjustPosition()
        {
            if (PhotonNetwork.isMasterClient)
            {
                _playerGlobal.position = Positions.HOST_POS;
            }
            else
            {
                PlayDoorSqueek();
                RequestPositionFromMaster();
            }
        }

        private void PlayDoorSqueek()
        {
            var door = UnityUtil.FindGameObject("Door");
            var audioSource = door.GetComponent<GvrAudioSource>();
            audioSource.Play();
        }

        internal void RequestPositionFromMaster()
        {
            photonView.RPC("OnPositionRequest",
                PhotonTargets.MasterClient);
            Logger.DebugFormat("Player (id '{0}'): Reqested position from master", PhotonNetwork.player.ID);
        }

        [PunRPC]
        void OnPositionRequest(PhotonMessageInfo info)
        {
            Vector3 playerPos = GetRandomStandPosition();
            photonView.RPC("OnPositionReceived",
                PhotonPlayer.Find(info.sender.ID),
                playerPos);
            Logger.DebugFormat("Master: Send position (pos '{1}') to player (id '{0}')", info.sender.ID, playerPos);
        }

        [PunRPC]
        void OnPositionReceived(Vector3 playerPos)
        {
            _playerGlobal.position = playerPos;
            _playerGlobal.rotation = Quaternion.Euler(0, 0, 0);
            Logger.DebugFormat("Player (id '{0}'): Received position '{1}' from master", PhotonNetwork.player.ID,
                playerPos);
        }


        private Vector3 GetRandomStandPosition()
        {
            //TODO die nachfolgende Zeile sollte eigentlich in der Initialisierung getan werden
            // Jedoch wird der initialisierte Wert aus unerfindlichen Gründen vergessen...?!
            var remainingClientPositions = GetStandPositions();
            int r = Random.Range(0, remainingClientPositions.Count);
            Debug.Log("ANGEFRAGTE POSITION = " + r);
            Debug.Log("GROOSSE = " + remainingClientPositions.Count);
            Vector3 pos = remainingClientPositions[r];
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


        internal void ShowLaser(bool show)
        {
            if (show == _isLaserShown)
            {
                return;
            }

            _isLaserShown = show;
            if (_player.IsHost())
            {
                _laser.GetComponent<LineRenderer>().enabled = show;
                _reticlePointer.GetComponent<MeshRenderer>().material.color = _isLaserShown ? Color.red : Color.white;
            }
            else
            {
                _globalElements.SetActive(_isLaserShown);
            }

            Logger.InfoFormat("Logger is {0}", _isLaserShown ? "shown" : "hidden");
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            {
                if (stream.isWriting)
                {
                    if (_isLaserShown)
                    {
                        _firstFalse = true;

                        stream.SendNext(true);
                        stream.SendNext(_reticlePointer.transform.position);
                    }
                    else if (_firstFalse)
                    {
                        stream.SendNext(false);
                        _firstFalse = false;
                    }
                }
                else
                {
                    var isLaserShown = (bool) stream.ReceiveNext();
                    ShowLaser(isLaserShown);
                    if (isLaserShown)
                    {
                        _globalReticlePointer.transform.position = (Vector3) stream.ReceiveNext();
                    }
                }
            }
        }
    }
}