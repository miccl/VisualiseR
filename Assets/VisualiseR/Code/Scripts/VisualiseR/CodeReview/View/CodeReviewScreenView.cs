using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenView : View, DragDropHandler
    {
        public Signal<Code> NextCodeSignal = new Signal<Code>();

        private const string FILE_PREFIX = "file:///";

        internal ICode _code;
        internal IPlayer _player;

        private bool _isHeld;
        public bool IsFirst { get; set; }
        private GameObject _gvrReticlePointer;
        private Text _infoText;
        private GvrPointerGraphicRaycaster pointerScript;
        internal GameObject contextMenu;


        internal bool IsContextMenuShown;

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

        void Update()
        {
            HandleDragAndDrop();
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
            if (IsFirst)
            {
                ShowContextMenu();
            }
            else
            {
                NextCodeSignal.Dispatch((Code) _code);
            }
        }


        private void HandleDragAndDrop()
        {
            if (_isHeld)
            {
//                Ray ray = new Ray(_gvrReticlePointer.transform.position, _gvrReticlePointer.transform.forward);
//                float distance = Vector3.Distance(_gvrReticlePointer.transform.position, transform.position);
//                transform.position = ray.GetPoint(distance);
//
//                // Fix rotation
//                transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
//                transform.Rotate(-270, 180, 180);
//
//                //TODO Screen soll immer über dem Boden schweben, wenn per DD dies drunter soll es drüber gezogen werden.
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


        public void ShowContextMenu()
        {
            if (!IsContextMenuShown)
            {
                InstantiateContextMenu();
                IsContextMenuShown = true;
            }
        }

        private void InstantiateContextMenu()
        {
            var position = GetContextMenuPosition();
            var rotation = GetContextMenuRotation();
            contextMenu = Instantiate(Resources.Load("ContextMenuCanvas"), position, rotation) as GameObject;
            contextMenu.transform.Rotate(90, -180, 0);
            contextMenu.transform.SetParent(transform);

            //TODO direkte Verdrahtung entfernen
            ContextMenuView contextMenuView = contextMenu.GetComponent<ContextMenuView>();
            contextMenuView._code = _code;
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