using System;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class ShowCodeReviewContextMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowCodeReviewContextMenuCommand));
        
        [Inject]
        public GameObject _gameObject { get; set; }

        [Inject]
        public Code _code { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }
        
        [Inject]
        public CodeReviewContextMenuIsShownSignal CodeReviewContextMenuIsShownSignal { get; set; }

        public override void Execute()
        {
            Logger.Info("Showing context menu");
            InstantiateContextMenu();
        }
        
        private void InstantiateContextMenu()
        {
            var contextMenu = _contextView.transform.Find("Menus").transform.Find("CodeReviewContextMenuCanvas").gameObject;
            contextMenu.SetActive(true);
            CodeReviewContextMenuView codeReviewContextMenuView = contextMenu.GetComponent<CodeReviewContextMenuView>();
            codeReviewContextMenuView.Init(_code);
            
            CodeReviewContextMenuIsShownSignal.Dispatch(true);
        }
    }
}