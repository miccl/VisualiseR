using System.Collections;
using log4net;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

[assembly: log4net.Config.XmlConfigurator]
namespace VisualiseR.CodeReview
{
    public class ScreenView : View
    {
        private static JCsLogger log;

        public Medium medium { get; set; }
        private IPicture currPicture;
        private int currPicturePos;
        private const string FILE_PREFIX = "file:///";

        protected override void Start()
        {
            log = new JCsLogger(typeof(ScreenView));
            SetupMedium();
        }

        internal void SetupMedium()
        {
            if (medium == null)
            {
                medium = CreateMockMedium();
                Debug.Log(medium);
            }

            currPicturePos = 0;
            currPicture = medium.GetPicture(0);
            StartCoroutine(LoadImageIntoTexture(currPicture.Path));
        }

        private Medium CreateMockMedium()
        {
            Medium medium = new Medium("test");

            medium.AddPicture(new Picture("1", "D:/VisualiseR_Test/FullDirectory/url.jpg"));
            medium.AddPicture(new Picture("2", "D:/VisualiseR_Test/FullDirectory/imgres.jpg"));
            medium.AddPicture(new Picture("3", "D:/VisualiseR_Test/FullDirectory/gear-vr_kv.jpg"));

            return medium;
        }

        private void NextPicture()
        {
            currPicturePos = ((currPicturePos + 1) % medium.Pictures.Count);
            LoadPictureIntoTexture(currPicturePos);
        }

        private void PrevPicture()
        {
            currPicturePos = currPicturePos - 1;
            if (currPicturePos == -1)
            {
                currPicturePos = medium.Pictures.Count - 1;
            }
            LoadPictureIntoTexture(currPicturePos);
        }

        private void LoadPictureIntoTexture(int picturePos)
        {
            currPicture = medium.GetPicture(picturePos);
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