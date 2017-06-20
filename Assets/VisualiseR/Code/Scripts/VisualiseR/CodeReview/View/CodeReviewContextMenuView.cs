using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// View of the context menu.
    /// </summary>
    public class CodeReviewContextMenuView : View
    {
        public Signal<Code, Rate> CodeRatingSelected = new Signal<Code, Rate>();
        public Signal<Code, string> CommentSaveButtonClickedSignal = new Signal<Code, string>();
        public Signal<Code> RemoveCodeSignal = new Signal<Code>();
        public Signal OnContextMenuCanceled = new Signal();
        public Signal<bool> ShowKeyboardSignal = new Signal<bool>();

        public ICode _code;

        private GameObject _mainPanel;
        private GameObject _ratePanel;
        private GameObject _editPanel;
        private GameObject _commentPanel;
        private GameObject _removePanel;
        private InputField _commentInputField;
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
        }

        protected override void Start()
        {
            base.Start();
            SetupRateButtons();
        }

        public void Init(ICode code)
        {
            _code = code;
        }

        private void SetupRateButtons()
        {
            _goodButtonSelectable.interactable = !_code.Rate.Equals(Rate.Uncritical);
            _okButtonSelectable.interactable = !_code.Rate.Equals(Rate.Minor);
            _badButtonSelectable.interactable = !_code.Rate.Equals(Rate.Critical);
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
            if (_mainPanel.activeSelf)
            {
                OnCancelButtonClick();
                return;
            }

            if (_ratePanel.activeSelf)
            {
                OnRateCancelButtonClick();
                return;
            }

            if (_editPanel.activeSelf)
            {
                OnEditCancelButtonClick();
                return;
            }

            if (_commentPanel.activeSelf)
            {
                OnCommentCancelButtonClick();
                return;
            }

            if (_removePanel.activeSelf)
            {
                OnRemoveNoButtonClick();
                return;
            }
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
            DispatchRatingIfChanged(Rate.Critical);
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
            ShowKeyboardSignal.Dispatch(true);
            _editPanel.SetActive(false);
            _commentPanel.SetActive(true);
            if (!String.IsNullOrEmpty(_code.Comment))
            {
                _commentInputField.text = _code.Comment;
            }
            else
            {
                _commentInputField.text = "";
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
            ShowKeyboardSignal.Dispatch(false);
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

        internal void HideView()
        {
            _mainPanel.SetActive(true);
            _ratePanel.SetActive(false);
            _editPanel.SetActive(false);
            _commentPanel.SetActive(false);
            _removePanel.SetActive(false);
            ShowKeyboardSignal.Dispatch(false);
            _code = null;

            gameObject.SetActive(false);
        }
    }
}