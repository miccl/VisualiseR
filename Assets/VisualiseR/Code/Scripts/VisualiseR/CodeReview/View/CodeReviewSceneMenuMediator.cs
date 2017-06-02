using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class CodeReviewSceneMenuMediator : Mediator
    {
        [Inject]
        public CodeReviewSceneMenuView _view { get; set; }

        [Inject]
        public ExportToTxtSignal ExportToTxtSignal { get; set; }

        public override void OnRegister()
        {
            _view.ExportButtonClickSignal.AddListener(OnExportButtonClick);
        }

        public override void OnRemove()

        {
            _view.ExportButtonClickSignal.RemoveListener(OnExportButtonClick);
        }

        private void OnExportButtonClick()
        {
            ExportToTxtSignal.Dispatch((CodeMedium) _view._medium);
        }
    }
 
}