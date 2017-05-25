using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PresentationScreenMediator : Mediator
    {
        [Inject]
        public PresentationScreenView view { get; set; }

        [Inject]
        public NextSlideSignal NextSlideSignal { get; set; }

        [Inject]
        public PrevSlideSignal PrevSlideSignal { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }


        public override void OnRegister()
        {
            view.NextSlideSignal.AddListener(OnNextSlide);
            view.PrevSlideSignal.AddListener(OnPrevSlide);
            SlidePositionChangedSignal.AddListener(OnSlidePositionChanged);
        }

        public override void OnRemove()
        {
            view.NextSlideSignal.RemoveListener(OnNextSlide);
            view.PrevSlideSignal.RemoveListener(OnPrevSlide);
            SlidePositionChangedSignal.RemoveListener(OnSlidePositionChanged);
        }

        private void OnNextSlide(IPlayer player, ISlideMedium medium)
        {
            NextSlideSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        private void OnPrevSlide(IPlayer player, ISlideMedium medium)
        {
            PrevSlideSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        private void OnSlidePositionChanged()
        {
            view.LoadCurrentSlide();
        }
    }
}