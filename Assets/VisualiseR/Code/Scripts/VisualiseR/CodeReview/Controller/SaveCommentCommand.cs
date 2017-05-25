using System;
using strange.extensions.command.impl;

namespace VisualiseR.CodeReview
{
    public class SaveCommentCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SaveCommentSignal));

        [Inject]
        public Code code { get; set; }

        [Inject]
        public string text { get; set; }

        [Inject]
        public CommentChangedSignal CommentChangedSignal { get; set; }

        public override void Execute()
        {
            code.Comment = text;
            Logger.InfoFormat("Comment {0} was added  to code {1}", text, code);
            CommentChangedSignal.Dispatch(code);
        }
    }
}