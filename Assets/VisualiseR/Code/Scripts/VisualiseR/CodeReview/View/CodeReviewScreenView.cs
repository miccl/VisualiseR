using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenView : View
    {
        public Signal<Code> NextCodeSignal = new Signal<Code>();
        public Signal<GameObject, Code> ShowContextMenuSignal = new Signal<GameObject, Code>();

        private const string FILE_PREFIX = "file:///";

        internal ICode _code;
        internal IPlayer _player;

        public bool IsFirst { get; set; }
        private Text _infoText;


        internal bool _isContextMenuShown = false;
        internal bool _isSceneMenuShown = false;

        protected override void Start()
        {
            base.Start();
            LoadCode();
        }


        public void Init(bool isFirst)
        {
            IsFirst = isFirst;
        }

        internal void LoadCode()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            
            if (_code == null)
            {
                return;
            }
            
            LoadPictureIntoTexture(_code.Pic);
        }

        internal void LoadPictureIntoTexture(IPicture pic)
        {
            if (pic == null)
            {
                Debug.LogError("No Picture to load");
                return;
            }
            StartCoroutine(LoadImageIntoTexture(pic.Path));
        }

        IEnumerator LoadImageIntoTexture(string path)
        {
            WWW www = new WWW(FILE_PREFIX + path);
            yield return www;
            Texture2D tex = new Texture2D(6, 4, TextureFormat.RGBA32, false);
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
            www.Dispose();
        }

        public void OnScreenClick()
        {
            if (_isContextMenuShown || _isSceneMenuShown)
            {
                return;
            }

            if (IsFirst)
            {
                ShowContextMenu();
            }
            else
            {
                NextCodeSignal.Dispatch((Code) _code);
            }
        }

        public void ShowContextMenu()
        {
            if (_isContextMenuShown)
            {
                return;
            }
            
            ShowContextMenuSignal.Dispatch(gameObject, (Code) _code);
        }

        public void ChangeCode(ICode code)
        {
            _code = code;
            LoadCode();
        }
    }
}