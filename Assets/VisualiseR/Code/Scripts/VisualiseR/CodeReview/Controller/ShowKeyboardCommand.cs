using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Command to show the virtual keyboard.
    /// </summary>
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
            if (keyboard.activeSelf == show) return;
            
            keyboard.SetActive(show);
            if (show)
            {
                Logger.InfoFormat("Keyboard is shown");
            }
        }
    }
}