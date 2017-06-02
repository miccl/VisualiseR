using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class InfoMediator : Mediator
    {
        [Inject]
        public InfoView _view { get; set; }

        [Inject]
        public NextCodeSignal NextCodeSignal { get; set; }

        [Inject]
        public CommentChangedSignal CommentChangedSignal { get; set; }


        public override void OnRegister()
        {
            NextCodeSignal.AddListener(OnNextCode);
            CommentChangedSignal.AddListener(OnCommentChanged);
        }

        public override void OnRemove()
        {
            NextCodeSignal.RemoveListener(OnNextCode);
            CommentChangedSignal.RemoveListener(OnCommentChanged);
        }

        private void OnCommentChanged(Code code)
        {
            _view.UpdateComment(code.Comment);

        }

        private void OnNextCode(Code code)
        {
            _view.UpdateView(code);
        }
    }
}