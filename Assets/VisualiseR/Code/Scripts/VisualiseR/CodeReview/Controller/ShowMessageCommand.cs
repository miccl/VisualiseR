using System.Net.Mime;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class ShowMessageCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowMessageCommand));
        private GameObject _messageCanvas;

        [Inject]
        public string _message { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            Logger.InfoFormat("Showing message");
            ShowMessage();
        }

        private void ShowMessage()
        {
            _messageCanvas = Camera.main.transform.Find("Message").gameObject;
            _messageCanvas.SetActive(true);
            _messageCanvas.GetComponentInChildren<Text>().text = _message;
            _messageCanvas.GetComponent<Animation>().Play();
        }
    }
}