using strange.extensions.command.impl;
using VisualiseR.CodeReview;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Command to get the previous slide.
    /// </summary>
    public class PrevSlideCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(PrevSlideCommand));

        [Inject]
        public Player _player { get; set; }

        [Inject]
        public SlideMedium _medium { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }

        public override void Execute()
        {
            if (!_player.HasRight(AcessList.NAVIGATE_MEDIUM))
            {
                Logger.InfoFormat(AcessList.ERROR_MESSAGE, _player, typeof(PrevSlideCommand));
                return;
            }

            if (_medium.Slides.Count > 0)
            {
                _medium.PrevSlide();
                Logger.InfoFormat("Prev slide (Curr pos: {0})", _medium.CurrentPos);
                SlidePositionChangedSignal.Dispatch();
            }
        }
    }
}