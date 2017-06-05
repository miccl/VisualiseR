using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class SmallScreenView : View
    {
        private static JCsLogger Logger;

        private const string FILE_PREFIX = "file:///";

        private List<byte[]> _images = new List<byte[]>();
        internal bool _isSceneMenuShown = false;

        internal ISlideMedium _medium { get; set; }
        internal IPlayer _player { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(SmallScreenView));
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
                LoadImageIntoTexture(_images[0]);
                return;
            }

            if (_player.Type == PlayerType.Host)
            {
                LoadCurrentSlide();
            }
        }

        internal void LoadCurrentSlide()
        {
            var currentPos = _medium.CurrentPos;
            if (_images == null)
            {
                Logger.Error("Image is null");
                return;
            }
            
            LoadImageIntoTexture(_images[currentPos]);
        }


        void LoadImageIntoTexture(byte[] bytes)
        {
            Texture2D tex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            tex.LoadImage(bytes);
            GetComponent<Renderer>().material.mainTexture = tex;
        }

    }
}