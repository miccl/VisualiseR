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

        private bool _isHeld;
        public bool IsFirst { get; set; }
        private GameObject _gvrReticlePointer;
        private Text _infoText;
        private GvrPointerGraphicRaycaster pointerScript;


        internal bool _isContextMenuShown = false;
        internal bool _isSceneMenuShown = false;

        protected override void Awake()
        {
            base.Awake();
            _isHeld = false;
            _gvrReticlePointer = GameObject.Find("GvrReticlePointer");

            pointerScript = GetComponent<GvrPointerGraphicRaycaster>();
        }

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
            WWW www = new WWW(FILE_PREFIX + path);
            yield return www;
            Texture2D tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
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