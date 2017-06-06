using strange.extensions.mediation.impl;

namespace VisualiseR.CodeReview
{
    public class CodeReviewSceneMenuMediator : Mediator
    {
        [Inject]
        public CodeReviewSceneMenuView _view { get; set; }

        [Inject]
        public ExportToTxtSignal ExportToTxtSignal { get; set; }
        
        [Inject]
        public CodeReviewSceneMenuIsShownSignal CodeReviewSceneMenuIsShownSignal { get; set; }
        
        [Inject]
        public ShowAllCodeSignal ShowAllCodeSignal { get; set; }


        public override void OnRegister()
        {
            _view.ExportButtonClickSignal.AddListener(OnExportButtonClick);
            _view.SceneMenuCanceledSignal.AddListener(OnSceneMenuCanceled);
            _view.ShowAllCodeSignal.AddListener(OnShowAllCode);
        }

        public override void OnRemove()

        {
            _view.ExportButtonClickSignal.RemoveListener(OnExportButtonClick);
            _view.SceneMenuCanceledSignal.AddListener(OnSceneMenuCanceled);
            _view.ShowAllCodeSignal.RemoveListener(OnShowAllCode);
        }

        private void OnExportButtonClick()
        {
            ExportToTxtSignal.Dispatch((CodeMedium) _view._medium);
        }

        private void OnSceneMenuCanceled()
        {
            CodeReviewSceneMenuIsShownSignal.Dispatch(false);
        }

        private void OnShowAllCode()
        {
            ShowAllCodeSignal.Dispatch();
        }
    }
 
}