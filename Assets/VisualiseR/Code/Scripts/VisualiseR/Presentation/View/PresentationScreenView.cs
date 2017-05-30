using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PresentationScreenView : View
    {
        private static JCsLogger Logger;

        private const string FILE_PREFIX = "file:///";

        internal Signal<IPlayer, ISlideMedium> NextSlideSignal = new Signal<IPlayer, ISlideMedium>();
        internal Signal<IPlayer, ISlideMedium> PrevSlideSignal = new Signal<IPlayer, ISlideMedium>();
        internal Signal<IPlayer> ShowPresentationContextMenuSignal = new Signal<IPlayer>();
        internal Signal<IPlayer> ShowSceneMenuSignal = new Signal<IPlayer>();
        private List<byte[]> _images = new List<byte[]>();
        private int _currentPos;
        private bool _isLoading;

        internal ISlideMedium _medium { get; set; }
        internal IPlayer _player { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(PresentationScreenView));
        }

        public void Init(ISlideMedium slideMedium, List<byte[]> images)
        {
            _medium = slideMedium;
            _images = images;

            SetupMedium();
            
        }


        private void SetupMedium()
        {
            if (_player == null)
            {
                return;
            }

            if (_player.Type == PlayerType.Host)
            {
                LoadCurrentSlide();
            }
        }

        internal void RequestDataFromMaster()
        {
            RequestDataFromMaster(PhotonNetwork.player.ID, 0);
            _isLoading = true;
        }

        internal void RequestDataFromMaster(int playerId, int pos)
        {
            GetComponent<PhotonView>().RPC("OnDataRequest",
                PhotonTargets.MasterClient,
                playerId, pos);
            Logger.DebugFormat("Player (id '{0}'): Reqested data (pos '{1}') from master", playerId, pos);
        }

        [PunRPC]
        void OnDataRequest(int playerId, int pos)
        {
            int Pos = -1;
            byte[] Image = null;

            if (_images.Count - 1 >= pos)
            {
                Pos = pos;
                Image = _images[Pos];
            }
            GetComponent<PhotonView>().RPC("OnDataReceived",
                PhotonPlayer.Find(playerId),
                Pos, Image);
            Logger.DebugFormat("Master: Send data (pos '{1}') to player (id '{0}')", playerId, pos);            
            //TODO interesting other alternative
//            GetComponent<PhotonView>().RPC("OnDataReceived",
//                PhotonTargets.OthersBuffered,
//                Pos, Image);
        }

        [PunRPC]
        void OnDataReceived(int pos, byte[] image)
        {
            if (pos >= 0)
            {
                _images.Insert(pos, image);

                Logger.DebugFormat("(id '{0}'): Received images from master (image: {1}, pos: {2})",
                    PhotonNetwork.player.ID,
                    image.Length, pos);
                RequestDataFromMaster(PhotonNetwork.player.ID, ++pos);
                return;
            }

            _isLoading = false;
            Logger.DebugFormat("Player (id '{0}'): Received all images from master", PhotonNetwork.player.ID);
            LoadImageIntoTexture(_images[_currentPos]);
        }

        private void NextSlide()
        {
            if (_player != null && _medium != null)
            {
                NextSlideSignal.Dispatch(_player, _medium);
            }
        }

        private void PrevSlide()
        {
            if (_player != null && _medium != null)
            {
                PrevSlideSignal.Dispatch(_player, _medium);
            }
        }

        internal void LoadCurrentSlide()
        {
            var currentPos = _medium.CurrentPos;
            photonView.RPC("OnSlidePosChanged",
                PhotonTargets.All,
                currentPos);
        }

        [PunRPC]
        void OnSlidePosChanged(int pos)
        {
            if (_images == null)
            {
                Logger.Error("Image is null");
                return;
            }
            if (_isLoading)
            {
                Logger.Error("User is still loading");
                return;
            }

            _currentPos = pos;
            LoadImageIntoTexture(_images[pos]);
        }

        void LoadImageIntoTexture(byte[] bytes)
        {
            Texture2D tex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            tex.LoadImage(bytes);
            GetComponent<Renderer>().material.mainTexture = tex;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                NextSlide();

//                ShowSceneMenuSignal.Dispatch(_player);
                if (_player != null && !_player.IsEmpty())
                {
                    ShowPresentationContextMenuSignal.Dispatch(_player);
                }
            }

//            if (Input.GetButtonDown("Fire2"))
//            {
////                PrevSlide();
//                ShowContextMenu();
//            }
            if (photonView.isMine)
            {
            }
        }

        private void ShowContextMenu()
        {
            if (_player != null && !_player.IsEmpty())
            {
                ShowPresentationContextMenuSignal.Dispatch(_player);
            }
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }
    }
}