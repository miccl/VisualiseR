using System.Collections;
using System.IO;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenView : View
    {
        private const string FILE_PREFIX = "file:///";

        public IMedium _medium;
        public IPlayer _player;

        internal int _currPicturePos;

        public Signal<IPlayer, IMedium, int> nextCodeSignal = new Signal<IPlayer, IMedium, int>();
        public Signal<IPlayer, IMedium, int> prevCodeSignal = new Signal<IPlayer, IMedium, int>();

        protected override void Awake()
        {
            _player = new Player("Test", PlayerType.Host);
            SetupMedium();
            Debug.Log("asd: " + _medium.Pictures.Count);
        }

        internal void SetupMedium()
        {
            if (_medium == null)
            {
                _medium = CreateMockMedium();
            }
            _currPicturePos = 0;
            LoadPictureIntoTexture(_currPicturePos);
        }

        private IMedium CreateMockMedium()
        {
            IMedium medium = new Medium("test");

            for (int i = 0; i < 3; i++)
            {
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                medium.AddPicture(new Picture(pic, filePath));
                Debug.Log(filePath);
            }

            return medium;
        }

        private void NextPicture()
        {
            nextCodeSignal.Dispatch(_player, _medium, _currPicturePos);
        }

        private void PrevPicture()
        {
            prevCodeSignal.Dispatch(_player, _medium, _currPicturePos);
        }

        internal void LoadPictureIntoTexture(int picturePos)
        {
            IPicture currPicture = _medium.GetPicture(picturePos);
            StartCoroutine(LoadImageIntoTexture(currPicture.Path));
            Debug.Log(picturePos);
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

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                NextPicture();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                PrevPicture();
            }
        }
    }
}