using strange.extensions.mediation.impl;
using VisualiseR.CodeReview;

namespace VisualiseR.Main
{
    public class CreateRoomMediator : Mediator
    {
        [Inject]
        public CreateRoomView _view { get; set; }

        [Inject]
        public SelectDiskFileSignal SelectDiskFileSignal { get; set; }

        public override void OnRegister()
        {
            _view._selectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            _view.Init();
        }

        public override void OnRemove()
        {
            _view._selectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
        }

        private void OnSelectDiskFileButtonClicked()
        {
            SelectDiskFileSignal.Dispatch();
        }
    }
}