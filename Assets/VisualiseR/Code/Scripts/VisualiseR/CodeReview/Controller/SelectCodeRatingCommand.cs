using strange.extensions.command.impl;

namespace VisualiseR.CodeReview
{
    public class SelectCodeRatingCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SelectCodeRatingCommand));

        [Inject]
        public Code code { get; set; }

        [Inject]
        public Rate rate { get; set; }

        [Inject]
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        public override void Execute()
        {
            Rate prevRate = code.Rate;
            if (!prevRate.Equals(rate))
            {
                code.Rate = rate;
                Logger.InfoFormat("Code {0} was rated with {1} (previous {2})", code, code.Rate, prevRate);
                CodeRatingChangedSignal.Dispatch(code);
            }
        }
    }
}