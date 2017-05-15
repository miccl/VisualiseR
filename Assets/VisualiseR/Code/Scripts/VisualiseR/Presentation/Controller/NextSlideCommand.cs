using strange.extensions.command.impl;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class NextSlideCommand : Command
    {
        [Inject]
        public Player _player { get; set; }

        [Inject]
        public CodeMedium _medium { get; set; }

        [Inject]
        public int position { get; set; }

        [Inject]
        public CodePositionChangedSignal _codePositionChangedSignal { get; set; }

        public override void Execute()
        {
            if (AcessList.NavigateCodeRight.Contains(_player.Type))
            {
                if (_medium.CodeFragments.Count > 0)
                {
                    position = (position + 1) % _medium.CodeFragments.Count;
                    _codePositionChangedSignal.Dispatch(position);
                }
            }
        }
    }
}