﻿using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class CodeReviewSceneMenuView : View
    {
        public Signal ExportButtonClickSignal = new Signal();
        public Signal SceneMenuCanceledSignal = new Signal();

        internal ICodeMedium _medium;

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
            gameObject.SetActive(false);
            SceneMenuCanceledSignal.Dispatch();   
        }
    }
}