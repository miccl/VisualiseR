using strange.extensions.mediation.impl;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Mediator for the <see cref="PresentationPlayerView"/>
    /// </summary>
    public class SimplePresentationScreenMediator : Mediator
    {
        [Inject]
        public SimplePresentationScreenView _view { get; set; }

        [Inject]
        public SlideSelectedSignal SlideSelectedSignal { get; set; }

        public override void OnRegister()
        {
            _view.SlideClickedSignal.AddListener(OnSlideClicked);
            SlideSelectedSignal.AddListener(OnSlideSelected);
        }

        public override void OnRemove()
        {
            _view.SlideClickedSignal.RemoveListener(OnSlideClicked);
            SlideSelectedSignal.RemoveListener(OnSlideSelected);

        }

        private void OnSlideClicked(Slide slide)
        {
            SlideSelectedSignal.Dispatch(slide);
        }

        private void OnSlideSelected(Slide slide)
        {
            Destroy(gameObject);
        }
    }
}