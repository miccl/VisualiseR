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
                LoadCurrentSlide();
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

        internal void LoadCurrentSlide()
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
//                NextSlide();
                ShowSceneMenuSignal.Dispatch();
            }

            if (Input.GetButtonDown("Fire2"))
            {
//                PrevSlide();
                ShowContextMenu();
            }
        }

        private void ShowContextMenu()
        {
            ShowPresentationContextMenuSignal.Dispatch(gameObject);
        }
    }
}