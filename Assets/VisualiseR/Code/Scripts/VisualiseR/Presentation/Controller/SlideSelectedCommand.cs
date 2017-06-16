using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Presentation
{
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
            var screen = ActivateScreen();
            ActivateEnvironment();
            PresentationScreenView screenView = screen.GetComponent<PresentationScreenView>();
            screenView._medium.SetCurrentSlide(_slide);
            Logger.InfoFormat("Selected slide '{0}'", _slide);
            SlidePositionChangedSignal.Dispatch();
        }

        private GameObject ActivateScreen()
        {
            GameObject screen = _contextView.transform.Find("Screens").transform.gameObject;
            screen.SetActive(true);
            return screen;
        }

        private void ActivateEnvironment()
        {
            GameObject walls = _contextView.transform.Find("Environment").gameObject;
            walls.SetActive(true);
        }
    }
}