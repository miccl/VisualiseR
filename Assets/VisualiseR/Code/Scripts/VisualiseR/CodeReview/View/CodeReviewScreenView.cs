using System.Collections;
using System.IO;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenView : View, DragDropHandler
    {
        private const string FILE_PREFIX = "file:///";

        public IMedium _medium;
        public IPlayer _player;

        internal int _currPicturePos;

        public Signal<IPlayer, IMedium, int> NextCodeSignal = new Signal<IPlayer, IMedium, int>();
        public Signal<IPlayer, IMedium, int> PrevCodeSignal = new Signal<IPlayer, IMedium, int>();
        private bool _isHeld;
        private GameObject _gvrReticlePointer;

        protected override void Awake()
        {
            _isHeld = false;
            _gvrReticlePointer = GameObject.Find("GvrReticlePointer");
            _player = new Player
            {
                Name = "Test",
                Type = PlayerType.Host
            };
            SetupMedium();
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
            IMedium medium = new Medium
            {
                Name = "test"
            };

            for (int i = 0; i < 3; i++)
            {
                var pic = "pic" + i;
                Texture2D tex = Resources.Load<Texture2D>(pic);
                string filePath = Application.persistentDataPath + pic + ".png";
                File.WriteAllBytes(filePath, tex.EncodeToPNG());
                medium.AddPicture(new Picture
                {
                    Title = pic,
                    Path = filePath
                });
            }
            return medium;
        }

        private void NextPicture()
        {
            NextCodeSignal.Dispatch(_player, _medium, _currPicturePos);
        }

        private void PrevPicture()
        {
            PrevCodeSignal.Dispatch(_player, _medium, _currPicturePos);
        }

        internal void LoadPictureIntoTexture(int picturePos)
        {
            IPicture currPicture = _medium.GetPicture(picturePos);
            StartCoroutine(LoadImageIntoTexture(currPicture.Path));
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
            HandleDragAndDrop();
            HandleInputs();
        }

        private void HandleInputs()
        {
            if (!_isHeld)
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

        private void HandleDragAndDrop()
        {
            if (_isHeld)
            {
                Ray ray = new Ray(_gvrReticlePointer.transform.position, _gvrReticlePointer.transform.forward);
                float distance = Vector3.Distance(_gvrReticlePointer.transform.position, transform.position);
                transform.position = ray.GetPoint(distance);

// Fix rotation
                transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
                transform.Rotate(-270, 180, 180);

//TODO Screen soll immer über dem Boden schweben, wenn per DD dies drunter soll es drüber gezogen werden.
            }
        }

        public void HandleGazeTriggerStart()
        {
            _isHeld = true;
        }

        public void HandleGazeTriggerEnd()
        {
            _isHeld = false;
//            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}