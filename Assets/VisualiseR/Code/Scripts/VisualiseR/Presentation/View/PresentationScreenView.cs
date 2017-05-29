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
        internal Signal<IPlayer, GameObject> ShowPresentationContextMenuSignal = new Signal<IPlayer, GameObject>();
        internal Signal ShowSceneMenuSignal = new Signal();
        private bool _IsSlideChanged;
        private byte[] _globalTexture;
        private byte[] _globalBytes;
        private List<byte[]> _images = new List<byte[]>();
        private int _currentPos;

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
                Debug.Log("Is Host");
                LoadCurrentSlide();
            }
        }

        internal void RequestDataFromMaster()
        {
            Debug.Log("RequestDataFromMaster");
            GetComponent<PhotonView>().RPC("OnDataRequest",
                PhotonTargets.MasterClient,
                PhotonNetwork.player.ID);
            
        }

        [PunRPC]
        void OnDataRequest(int playerId)
        {
            Debug.Log("OnDataRequest");
//            GetComponent<PhotonView>().RPC("OnDataReceived",
//                PhotonPlayer.Find(playerId),
//                _currentPos, _images.ToArray());
//            foreach (var image in _images)
//            {
                GetComponent<PhotonView>().RPC("OnDataReceived",
                    PhotonPlayer.Find(playerId),
                     _images[0]);
            //TODO
//            }
        }

        [PunRPC]
        void OnDataReceived(byte[] image)
        {
            Debug.Log("OnDataReceived");
            Debug.Log(image.Length);
            _images.Add(image);            
            Debug.Log("Received images from master");
//            OnSlidePosChanged(currPos);
        }

        public void test()
        {
            test2(_images.ToArray());
        }

        private void test2(params byte[][] images)
        {
            
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

//                ShowSceneMenuSignal.Dispatch();
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
                ShowPresentationContextMenuSignal.Dispatch(_player, gameObject);
            }
        }


// synchronsize with the others
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                if (PhotonNetwork.isMasterClient)
                {
//                    if (PhotonNetwork.room)
                }
//                PhotonNetwork
//                if (_IsSlideChanged)
//                {
//                    Debug.Log("IsWriting");
//                    stream.SendNext(_globalBytes);
//                    _IsSlideChanged = false;
//                }
            }
            else
            {
//                Debug.Log("IsReading");
//                ISlideMedium medium = (ISlideMedium) stream.ReceiveNext();
//                ShowContextMenu();
                Debug.Log("IsReading");
                byte[] foo = (byte[]) stream.ReceiveNext();
                Debug.Log(foo.Length);
                LoadImageIntoTexture(foo);
                Debug.Log("IsRFinished");
            }
//                LoadImageIntoTexture(foo);
        }
    }
}