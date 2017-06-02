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

        private void InstantiateContextMenu()
        {
            var position = GetContextMenuPosition();
            var rotation = GetContextMenuRotation();
            GameObject contextMenu = Instantiate(Resources.Load("ContextMenuCanvas"), position, rotation) as GameObject;
            contextMenu.transform.Rotate(90, -180, 0);
            contextMenu.transform.SetParent(transform);

            //TODO direkte Verdrahtung entfernen
            CodeReviewContextMenuView codeReviewContextMenuView = contextMenu.GetComponent<CodeReviewContextMenuView>();
            codeReviewContextMenuView._code = _code;
        }

        private Quaternion GetContextMenuRotation()
        {
            //TODO überarbeiten
            return transform.rotation;
        }

        private Vector3 GetContextMenuPosition()
        {
            //TODO irgendwann nochmal verbessern, derzeit schwankt das immer hin und her
            Vector3 cameraBack = -Camera.main.transform.forward * 12;
            Vector3 shift = new Vector3(0, 0, cameraBack.z);
            Vector3 pos = transform.position + shift;
            pos.y = 2;

            return pos;
        }

        public void ChangeCode(ICode code)
        {
            _code = code;
            LoadCode();
        }
    }
}