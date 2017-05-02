using strange.extensions.mediation.impl;
using VisualiseR.CodeReview;

namespace VisualiseR.Main
{
    public class CreateRoomMediator : Mediator
    {
        [Inject]
        public CreateRoomView _view { get; set; }

        [Inject]
        public SelectFileSignal _selectFileSignal { get; set; }

        public override void OnRegister()
        {
            _view._chooseDiskFileButtonClickedSignal.AddListener(OnChooseDiskFileButtonClicked);
            _view.Init();
        }

        public override void OnRemove()
        {
            _view._chooseDiskFileButtonClickedSignal.RemoveListener(OnChooseDiskFileButtonClicked);
        }

        private void OnChooseDiskFileButtonClicked()
        {
            _selectFileSignal.Dispatch();
        }
    }
}