using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class SimpleCodeReviewScreenView : View
    {
        public Signal<ICode, IPlayer> CodeSelectedSignal = new Signal<ICode, IPlayer>();

        internal ICode _code;
        internal IPlayer _player;

        public void Init(ICode code, IPlayer player)
        {
            _code = code;
            _player = player;
            LoadCode();
        }

        internal void LoadCode()
        {
            if (_code != null)
            {
                LoadPictureIntoTexture(_code.Pic);
            }
        }

        internal void LoadPictureIntoTexture(IPicture pic)
        {
            if (pic != null)
            {
                StartCoroutine(LoadImageIntoTexture(pic.Path));
            }
            else
            {
                Debug.LogError("No Picture to load");
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

        public void OnScreenClick()
        {
            CodeSelectedSignal.Dispatch(_code, _player);
        }
    }
}