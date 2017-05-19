using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public ICode _code;

        private GameObject _mainPanel;
        private GameObject _ratePanel;
        private GameObject _editPanel;
        private GameObject _commentPanel;
        private GameObject _removePanel;
        private InputField _commentInputField;
        private bool _isHeld;
        private GameObject _gvrReticlePointer;
        private Selectable _goodButtonSelectable;
        private Selectable _okButtonSelectable;
        private Selectable _badButtonSelectable;

        protected override void Awake()
        {
            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _ratePanel = gameObject.transform.FindChild("RatePanel").gameObject;
            _editPanel = gameObject.transform.FindChild("EditPanel").gameObject;
            _commentPanel = gameObject.transform.FindChild("CommentPanel").gameObject;
            _removePanel = gameObject.transform.FindChild("RemovePanel").gameObject;
            _commentInputField = _commentPanel.GetComponentInChildren<InputField>();

            _goodButtonSelectable = _ratePanel.transform.FindChild("CenterPanel").transform.FindChild("GoodButton").GetComponent<Selectable>();
            _okButtonSelectable = _ratePanel.transform.FindChild("CenterPanel").transform.FindChild("OkButton").GetComponent<Selectable>();
            _badButtonSelectable = _ratePanel.transform.FindChild("CenterPanel").transform.FindChild("BadButton").GetComponent<Selectable>();


//            UnityUtil.AddEventTriggerListener(_mainPanel.transform.FindChild("RateButton").gameObject.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnRateButtonClick);

            _gvrReticlePointer = GameObject.Find("GvrReticlePointer");
        }

        protected override void Start()
        {
            base.Start();
            SetupRateButtons();
        }

        private void SetupRateButtons()
        {
            _goodButtonSelectable.interactable = !_code.Rate.Equals(Rate.Uncritical);
            _okButtonSelectable.interactable = !_code.Rate.Equals(Rate.Minor);
            _badButtonSelectable.interactable = !_code.Rate.Equals(Rate.Criticial);
        }

        void Update()
        {
            HandleDragAndDrop();
        }

        public void OnRateButtonClick(BaseEventData eventData)
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
            DispatchRatingIfChanged(Rate.Uncritical);
        }

        public void OnRateOkButtonClick()
        {

            DispatchRatingIfChanged(Rate.Minor);
        }

        public void OnRateBadButtonClick()
        {
            DispatchRatingIfChanged(Rate.Criticial);
        }

        private void DispatchRatingIfChanged(Rate rate)
        {
            if (!_code.Rate.Equals(rate))
            {
                CodeRatingSelected.Dispatch((Code) _code, rate);
            }

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
            if (!String.IsNullOrEmpty(_code.Comment))
            {
                _commentInputField.text = _code.Comment;
            }
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
            CommentSaveButtonClickedSignal.Dispatch((Code) _code, _commentInputField.text);
        }

        public void OnCommentCancelButtonClick()
        {
            _commentPanel.SetActive(false);
            _editPanel.SetActive(true);
        }

        public void OnRemoveYesButtonClick()
        {
            RemoveCodeSignal.Dispatch((Code) _code);
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