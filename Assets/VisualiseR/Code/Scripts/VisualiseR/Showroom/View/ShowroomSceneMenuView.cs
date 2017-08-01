﻿using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// View for the scene menu.
    /// </summary>
    public class ShowroomSceneMenuView : View
    {
        private JCsLogger Logger;

        private IPlayer _player;

        internal GameObject _contextView;
        private GameObject _mainPanel;
        private GameObject _objectPanel;
        private GameObject _editModePanel;

        internal Signal<ObjectType> CreateObjectSignal = new Signal<ObjectType>();
        internal Signal<EditMode> ChangeEditModeSignal = new Signal<EditMode>();
        public Signal<bool> ShowSceneMenuSignal = new Signal<bool>();
        private Selectable _dragAndDropButton;
        private Selectable _colorButton;
        private Selectable _rotationButton;

        /// <summary>
        /// Initialises the variables.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(ShowroomSceneMenuView));

            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _objectPanel = gameObject.transform.FindChild("ObjectPanel").gameObject;
            _editModePanel = gameObject.transform.FindChild("EditModePanel").gameObject;
            _dragAndDropButton = _editModePanel.transform.Find("CenterPanel").Find("DragAndDropButton")
                .GetComponent<Selectable>();
            _colorButton = _editModePanel.transform.Find("CenterPanel").Find("ColorButton")
                .GetComponent<Selectable>();
            _rotationButton = _editModePanel.transform.Find("CenterPanel").Find("RotationButton")
                .GetComponent<Selectable>();
        }

        protected override void Start()
        {
            base.Start();
            _dragAndDropButton.interactable = false;
        }


        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="contextView"></param>
        /// <param name="player"></param>
        internal void Init(GameObject contextView, IPlayer player)
        {
            _contextView = contextView;
            _player = player;
        }

        void Update()
        {
            if (Input.GetButtonDown(ButtonUtil.CANCEL))
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            Logger.InfoFormat("Cancel button clicked");
            if (_mainPanel.activeSelf)
            {
                Hide();
                return;
            }

            if (_objectPanel.activeSelf)
            {
                OnObjectCancelButton(null);
                return;
            }

            if (_editModePanel.activeSelf)
            {
                OnEditModeCancelButton(null);
                return;
            }
        }

        /// <summary>
        /// Called when the object button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnObjectButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _objectPanel.SetActive(true);
        }

        /// <summary>
        /// Called when the object button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnEditModeButtonClick(BaseEventData data)
        {
            _mainPanel.SetActive(false);
            _editModePanel.SetActive(true);
        }

        /// <summary>
        /// Called when the quit button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnQuitButtonClick(BaseEventData data)
        {
            PhotonNetwork.LeaveRoom();
            UnityUtil.LoadScene("Main");
        }

        /// <summary>
        /// Called when the cancel button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnCancelButtonClick(BaseEventData data)
        {
            Hide();
        }

        public void OnCubeObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Cube);
        }

        public void OnSphereObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Sphere);
        }

        public void OnCapsuleObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Capsule);
        }

        public void OnCylinderObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Cylinder);
        }

        public void OnPlaneObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Plane);
        }

        public void OnQuadObjectButton(BaseEventData data)
        {
            CreateObject(ObjectType.Quad);
        }

        /// <summary>
        /// Called when the object cancel button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnObjectCancelButton(BaseEventData data)
        {
            _objectPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        private void CreateObject(ObjectType type)
        {
            CreateObjectSignal.Dispatch(type);
            Hide();
        }

        public void OnDragAndDropButton(BaseEventData data)
        {
            ChangeEditMode(EditMode.DragAndDrop);
            _dragAndDropButton.interactable = false;
            _colorButton.interactable = true;
            _rotationButton.interactable = true;
        }

        public void OnColorButton(BaseEventData data)
        {
            ChangeEditMode(EditMode.Coloring);
            _dragAndDropButton.interactable = true;
            _colorButton.interactable = false;
            _rotationButton.interactable = true;
        }

        public void OnRotateButton(BaseEventData data)
        {
            ChangeEditMode(EditMode.Rotate);
            _dragAndDropButton.interactable = true;
            _colorButton.interactable = true;
            _rotationButton.interactable = false;
        }

        private void ChangeEditMode(EditMode mode)
        {
            ChangeEditModeSignal.Dispatch(mode);
            Hide();
        }

        public void OnEditModeCancelButton(BaseEventData data)
        {
            _editModePanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        /// <summary>
        /// Hides the view.
        /// </summary>
        private void Hide()
        {
            _mainPanel.SetActive(true);
            _objectPanel.SetActive(false);
            _editModePanel.SetActive(false);

            ShowSceneMenuSignal.Dispatch(false);
        }
    }
}