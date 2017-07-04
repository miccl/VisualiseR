using strange.extensions.command.impl;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Command to save comment of a <see cref="Code"/>.
    /// </summary>
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
            var oldComment = code.Comment;
            code.Comment = text;
            Logger.InfoFormat("Comment from code '{0}' was changed  from '{1}' to {2}", code, code.Comment, oldComment);
            CommentChangedSignal.Dispatch(code);
        }
    }
}