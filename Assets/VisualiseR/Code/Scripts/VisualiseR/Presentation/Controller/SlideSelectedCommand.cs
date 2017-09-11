using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Command to show the selected slide on the main screen.
    /// </summary>
    public class SlideSelectedCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SlideSelectedCommand));

        [Inject]
        public Slide _slide { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }

        public override void Execute()
        {
            var screens = ActivateScreen();
            ActivateEnvironment();
            PresentationScreenView screenView = screens.GetComponentInChildren<PresentationScreenView>();
            screenView._medium.SetCurrentSlide(_slide);
            Logger.InfoFormat("Selected slide '{0}'", _slide);
            SlidePositionChangedSignal.Dispatch();
        }

        private GameObject ActivateScreen()
        {
            GameObject screens = _contextView.transform.Find("Screens").transform.gameObject;
            screens.SetActive(true);
            return screens;
        }

        private void ActivateEnvironment()
        {
            GameObject walls = _contextView.transform.Find("Environment").gameObject;
            walls.SetActive(true);
        }
    }
}