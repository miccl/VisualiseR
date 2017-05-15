using System;
using strange.extensions.command.impl;

namespace VisualiseR.CodeReview
{
    public class SelectedCodeRatingCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SelectedCodeRatingCommand));


        [Inject]
        public Code code { get; set; }

        [Inject]
        public Rate rate { get; set; }

        public override void Execute()
        {
            Logger.InfoFormat("Code {0} was rated with {1}", code, rate);
            throw new NotImplementedException();
        }
    }
}