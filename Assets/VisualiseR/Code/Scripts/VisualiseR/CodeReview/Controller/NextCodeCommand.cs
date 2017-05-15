using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class NextCodeCommand : Command
    {
        [Inject]
        public Player _player { get; set; }

        [Inject]
        public PictureMedium _medium { get; set; }

        [Inject]
        public int position { get; set; }

        [Inject]
        public CodePositionChangedSignal _codePositionChangedSignal { get; set; }

        public override void Execute()
        {
            if (AcessList.NavigateCodeRight.Contains(_player.Type))
            {
                if (_medium.Pictures.Count > 0)
                {
                    position = (position + 1) % _medium.Pictures.Count;
                    _codePositionChangedSignal.Dispatch(position);
                }
            }
        }
    }
}