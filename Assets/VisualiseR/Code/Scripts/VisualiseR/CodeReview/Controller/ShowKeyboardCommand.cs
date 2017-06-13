using System;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    public class ShowKeyboardCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowKeyboardCommand));

        [Inject] public bool show { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }
        
        [Inject]
        public CodeReviewContextMenuIsShownSignal CodeReviewContextMenuIsShownSignal { get; set; }

        public override void Execute()
        {
            InstantiateContextMenu();
        }
        
        private void InstantiateContextMenu()
        {
            var keyboard = _contextView.transform.Find("Menus").transform.Find("Keyboard").gameObject;
            keyboard.SetActive(show);
            Logger.InfoFormat("Keyboard is {0}", show ? "shown" : "hidden");
        }
    }
}