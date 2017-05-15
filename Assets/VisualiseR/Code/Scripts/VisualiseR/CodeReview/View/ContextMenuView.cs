﻿using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class ContextMenuView : View, DragDropHandler
    {
        public Signal<Code, Rate> CodeRatingSelected = new Signal<Code, Rate>();
        public Signal<Code, string> CommentSaveButtonClickedSignal = new Signal<Code, string>();
        public Signal<Code> RemoveCodeSignal = new Signal<Code>();
        public Signal OnContextMenuCanceled = new Signal();

        private GameObject _mainPanel;
        private GameObject _ratePanel;
        private GameObject _editPanel;
        private GameObject _commentPanel;
        private GameObject _removePanel;
        private InputField _commentInputField;
        private bool _isHeld;
        private GameObject _gvrReticlePointer;

        protected override void Awake()
        {
            GameObject contextMenuCanvas = UnityUtil.FindGameObject("ContextMenuCanvas(Clone)");
            _mainPanel = contextMenuCanvas.transform.FindChild("MainPanel").gameObject;
            Debug.Log("MainPanel: " + _mainPanel);
            _ratePanel = contextMenuCanvas.transform.FindChild("RatePanel").gameObject;
            _editPanel = contextMenuCanvas.transform.FindChild("EditPanel").gameObject;
            _commentPanel = contextMenuCanvas.transform.FindChild("CommentPanel").gameObject;
            _removePanel = contextMenuCanvas.transform.FindChild("RemovePanel").gameObject;
            _commentInputField = _commentPanel.GetComponentInChildren<InputField>();

//            UnityUtil.AddEventTriggerListener(_editPanel.transform.FindChild("CommentButton"));

            _gvrReticlePointer = GameObject.Find("GvrReticlePointer");
        }

        void Update()
        {
            HandleDragAndDrop();
        }

        public void OnRateButtonClick()
        {
            _mainPanel.SetActive(false);
            _ratePanel.SetActive(true);
        }

        public void OnEditButtonClick()
        {
            _mainPanel.SetActive(false);
            _editPanel.SetActive(true);
        }

        public void OnCancelButtonClick()
        {
            OnContextMenuCanceled.Dispatch();
            Destroy(gameObject);
        }

        public void OnRateGoodButtonClick()
        {
            //TODO Rating des mommentanen Fragments auf Gut setzen
            CodeRatingSelected.Dispatch(new Code(), Rate.Uncritical);
        }

        public void OnRateOkButtonClick()
        {
            //TODO Rating des Code-Fragments auf Gut setzen
            CodeRatingSelected.Dispatch(new Code(), Rate.Minor);
        }

        public void OnRateBadButtonClick()
        {
            //TODO Rating des Code-Fragments auf Gut setzen
            CodeRatingSelected.Dispatch(new Code(), Rate.Criticial);
        }

        public void OnRateCancelButtonClick()
        {
            _ratePanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        public void OnCommentButtonClick()
        {
            _editPanel.SetActive(false);
            _commentPanel.SetActive(true);
        }

        public void OnRemoveButtonClick()
        {
            _editPanel.SetActive(false);
            _removePanel.SetActive(true);
        }

        public void OnEditCancelButtonClick()
        {
            _editPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        public void OnCommentSaveButtonClick()
        {
            CommentSaveButtonClickedSignal.Dispatch(new Code(), _commentInputField.text);
        }

        public void OnCommentCancelButtonClick()
        {
            _commentPanel.SetActive(false);
            _editPanel.SetActive(true);
        }

        public void OnRemoveYesButtonClick()
        {
            RemoveCodeSignal.Dispatch(new Code());

        }

        public void OnRemoveNoButtonClick()
        {
            _removePanel.SetActive(false);
            _editPanel.SetActive(true);
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