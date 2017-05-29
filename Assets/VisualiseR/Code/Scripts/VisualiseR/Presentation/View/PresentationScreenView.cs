using System;
using System.Collections;
using System.Net.Mime;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PresentationScreenView : View
    {
        private const string FILE_PREFIX = "file:///";

        internal Signal<IPlayer, ISlideMedium> NextSlideSignal = new Signal<IPlayer, ISlideMedium>();
        internal Signal<IPlayer, ISlideMedium> PrevSlideSignal = new Signal<IPlayer, ISlideMedium>();
        internal Signal<GameObject> ShowPresentationContextMenuSignal = new Signal<GameObject>();
        internal Signal ShowSceneMenuSignal = new Signal();
        private bool _IsSlideChanged;

        internal ISlideMedium _medium { get; set; }
        internal IPlayer _player { get; set; }

        public void Init(ISlideMedium slideMedium, IPlayer player)
        {
            _medium = slideMedium;
            _player = player;

            SetupMedium();
        }

        private void SetupMedium()
        {
            if (_medium != null)
            {
                LoadSlide();
            }
        }

        private void NextSlide()
        {
            NextSlideSignal.Dispatch(_player, _medium);
        }

        private void PrevSlide()
        {
            PrevSlideSignal.Dispatch(_player, _medium);
        }

        internal void LoadSlide()
        {
            _IsSlideChanged = true;
            LoadCurrentSlide();
        }

        private void LoadCurrentSlide()
        {
            string path = _medium.CurrentSlide().Pic.Path;
            if (!String.IsNullOrEmpty(path))
            {
                StartCoroutine(LoadImageIntoTexture(path));
            }
        }


        IEnumerator LoadImageIntoTexture(string path)
        {
            WWW www = new WWW(FILE_PREFIX + path);

            yield return www;

            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
            www.Dispose();
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
            ShowPresentationContextMenuSignal.Dispatch(gameObject);
        }

        // synchronsize with the others
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                if (_IsSlideChanged)
                {
                    Debug.Log("IsWriting");
                    stream.SendNext(1);
                    _IsSlideChanged = false;
                  }
//                stream.SendNext();
//                stream.SendNext(_playerGlobal.position);
//                stream.SendNext(_playerGlobal.rotation);
//                stream.SendNext(_playerLocal.localPosition);
//                stream.SendNext(_playerLocal.localRotation);
            }
            else
            {
//                Debug.Log("IsReading");
//                ISlideMedium medium = (ISlideMedium) stream.ReceiveNext();
                ShowContextMenu();
                
//                transform.position = (Vector3) stream.ReceiveNext();
//                transform.rotation = (Quaternion) stream.ReceiveNext();
//                Avatar.transform.localPosition = (Vector3) stream.ReceiveNext();
//                Avatar.transform.localRotation = (Quaternion) stream.ReceiveNext();
            }
       }

    }
}