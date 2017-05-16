using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class NextCodeCommand : Command
    {
        [Inject]
        public Code code { get; set; }

        [Inject]
        public CodePositionChangedSignal _codePositionChangedSignal { get; set; }

        public override void Execute()
        {
            //TODO überlegen, wie man das integriert. ist derzeit obsolete
//            if (AcessList.NavigateCodeRight.Contains(_player.Type))
//            {
//                if (_medium.CodeFragments.Count > 0)
//                {
//                    position = (position + 1) % _medium.CodeFragments.Count;
//                    _codePositionChangedSignal.Dispatch(position);
//                }
//            }
        }
    }
}