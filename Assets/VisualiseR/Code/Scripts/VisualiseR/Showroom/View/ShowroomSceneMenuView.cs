using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VisualiseR.Common;
using VisualiseR.Presentation;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    public class ShowroomSceneMenuView : View
    {
        private JCsLogger Logger;

        private IPlayer _player;

        internal GameObject _contextView;
        private GameObject _mainPanel;
        private GameObject _objectPanel;

        /// <summary>
        /// Initialises the variables.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(ShowroomSceneMenuView));

            _mainPanel = gameObject.transform.FindChild("MainPanel").gameObject;
            _objectPanel = gameObject.transform.FindChild("ObjectPanel").gameObject;
        }


        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="contextView"></param>
        /// <param name="player"></param>
        /// <param name="medium"></param>
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

        /// <summary>
        /// Called when the object cancel button has been clicked.
        /// </summary>
        /// <param name="data"></param>
        public void OnObjectCancelButton(BaseEventData data)
        {
            _objectPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        /// <summary>
        /// Hides the view.
        /// </summary>
        private void Hide()
        {
            _mainPanel.SetActive(true);
            _objectPanel.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}