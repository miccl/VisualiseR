using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewSceneMenuView : View
    {
        public Signal ExportButtonClickSignal = new Signal();
        public Signal SceneMenuCanceledSignal = new Signal();
        public Signal ShowAllCodeSignal = new Signal();
        public Signal<bool> ShowLaserSignal = new Signal<bool>();

        internal ICodeMedium _medium;
        private Text _showLaserButtonText;

        public void Init(ICodeMedium medium)
        {
            _medium = medium;
        }

        void Update()
        {
            if (Input.GetButtonDown(ButtonUtil.CANCEL))
            {
                OnCancelButtonClick(null);
            }
        }

        public void OnShowAllButtonClick(BaseEventData data)
        {
            ShowAllCodeSignal.Dispatch();
        }
        
        public void OnExportButtonClick(BaseEventData data)
        {
            ExportButtonClickSignal.Dispatch();
        }
        
        public void OnQuitButtonClick(BaseEventData data)
        {
            PhotonNetwork.LeaveRoom();
            UnityUtil.LoadScene("Main");
        }

        public void OnCancelButtonClick(BaseEventData data)
        {
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            SceneMenuCanceledSignal.Dispatch();
        }
    }
}