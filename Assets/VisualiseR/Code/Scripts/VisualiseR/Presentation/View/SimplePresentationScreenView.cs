using System;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class SimplePresentationScreenView : View
    {
        public Signal<Slide> SlideClickedSignal = new Signal<Slide>();

        internal ISlide _slide { get; set; }
        internal IPlayer _player { get; set; }

        public void Init(ISlide slide)
        {
            _slide = slide;

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
            WWW www = new WWW(FileUtil.FILE_PREFIX + path);

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