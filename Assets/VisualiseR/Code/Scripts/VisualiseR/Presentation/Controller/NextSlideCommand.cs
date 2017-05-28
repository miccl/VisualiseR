using strange.extensions.command.impl;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Command to get the next slide.
    /// Tests if the player has the needed rights.
    ///    
    /// </summary>
    public class NextSlideCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(NextSlideCommand));

        [Inject]
        public Player _player { get; set; }

        [Inject]
        public SlideMedium _medium { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }

        public override void Execute()
        {
            if (AcessList.NavigateCodeRight.Contains(_player.Type))
            {
                if (_medium.Slides.Count > 0)
                {
                    _medium.NextSlide();
                    Logger.InfoFormat("Next slide (Curr pos: {0})", _medium.CurrentPos);
                    SlidePositionChangedSignal.Dispatch();
                }
            }
            else
            {
                Logger.InfoFormat("Player {0} has no rights for command '{1}'", _player, typeof(NextSlideCommand));
            }
        }
    }
}