using System.Collections;
using System.IO;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PresentationScreenView : View
    {
        private const string FILE_PREFIX = "file:///";

        internal IPictureMedium _medium { get; set; }

        private IPicture _currPicture;
        private int _currPicturePos;

        protected override void Awake()
        {
            SetupMedium();
        }

        internal void SetupMedium()
        {
            if (_medium == null)
            {
                _medium = CreateMockMedium();
            }

            _currPicturePos = 0;
            _currPicture = _medium.GetPicture(0);

            StartCoroutine(LoadImageIntoTexture(_currPicture.Path));
        }

        //TODO vielleicht auslagern in einen Command
        private IPictureMedium CreateMockMedium()
        {
            IPictureMedium medium = new PictureMedium
            {
                Name = "Test"
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
                Debug.Log(filePath);
            }

            return medium;
        }

        private void NextPicture()
        {
            _currPicturePos = (_currPicturePos + 1) % _medium.Pictures.Count;
            LoadPictureIntoTexture(_currPicturePos);
        }

        private void PrevPicture()
        {
            _currPicturePos = _currPicturePos - 1;
            if (_currPicturePos == -1)
            {
                _currPicturePos = _medium.Pictures.Count - 1;
            }
            LoadPictureIntoTexture(_currPicturePos);
        }

        private void LoadPictureIntoTexture(int picturePos)
        {
            _currPicture = _medium.GetPicture(picturePos);
            StartCoroutine(LoadImageIntoTexture(_currPicture.Path));
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