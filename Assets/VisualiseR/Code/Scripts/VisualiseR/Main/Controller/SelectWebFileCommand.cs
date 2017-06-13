using System;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Main
{
    public class SelectWebFileCommand : Command
    {
        
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SelectWebFileCommand));

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            ShowWebFileView();
            Logger.InfoFormat("Web browser is shown");
        }

        private void ShowWebFileView()
        {
            var gameObject = _contextView.transform.Find("WebBrowser").gameObject;
            gameObject.SetActive(true);
        }
    }
}