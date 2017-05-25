using System;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class SimplePresentationScreenView : View
    {
        public Signal<Slide> SlideClickedSignal = new Signal<Slide>();

        private const string FILE_PREFIX = "file:///";

        internal ISlide _slide { get; set; }
        internal IPlayer _player { get; set; }
        internal int _pos { get; set; }

        public void Init(ISlide slide, int pos)
        {
            _slide = slide;
            _pos = pos;

            SetupMedium();
        }

        private void SetupMedium()
        {
            if (_slide != null)
            {
                LoadCurrentSlide();
            }
        }

        internal void LoadCurrentSlide()
        {
            string path = _slide.Pic.Path;
            if (!String.IsNullOrEmpty(path))
            {
                StartCoroutine(LoadImageIntoTexture(path));
            }
        }


        IEnumerator LoadImageIntoTexture(string path)
        {
            WWW www = new WWW(FILE_PREFIX + path);

            yield return www;

            Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
            www.Dispose();
        }

        public void OnClick(BaseEventData data)
        {
            SlideClickedSignal.Dispatch((Slide) _slide);
        }
    }
}