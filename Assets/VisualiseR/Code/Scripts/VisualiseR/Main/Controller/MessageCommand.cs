using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class MessageCommand : Command
    {
        [Inject]
        public Message _message { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        public override void Execute()
        {
            GameObject menuCanvas = UnityUtil.FindGameObject("MenuCanvas");

            var messagePanel = (GameObject) GameObject.Instantiate(Resources.Load("MessageCanvas"));
            messagePanel.transform.SetParent(contextView.transform);

            MessageView messageView = messagePanel.GetComponentInChildren<MessageView>();
            messageView.Init(_message.Type, _message.Title, _message.Text);
        }
    }
}