using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeSelectedCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CodeSelectedCommand));

        [Inject]
        public Code _code { get; set; }

        [Inject]
        public Player _player { get; set; }

        [Inject]
        public PileSelectedSignal PileSelectedSignal { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            Logger.InfoFormat("Selected code '{0}'", _code);
            DestroySimpleScreens();
            ActivateObjects();
            ShowSelectedCodeScreen();
        }

        private void ShowSelectedCodeScreen()
        {
            PileSelectedSignal.Dispatch(_code.Rate);

            NextCodeSignal.Dispatch(_code);
        }

        private void ActivateObjects()
        {
            var screens = _contextView.transform.Find("Screens").gameObject;
            screens.SetActive(true);

            var infoCanvas = _contextView.transform.Find("InfoCanvas").gameObject;
            infoCanvas.SetActive(true);

            GameObject piles = _contextView.transform.Find("Piles").gameObject;
            piles.SetActive(true);
        }

        private void DestroySimpleScreens()
        {
            var simpleScreens = _contextView.transform.Find("SimpleScreens").gameObject;
            GameObject.Destroy(simpleScreens);
        }
    }
}