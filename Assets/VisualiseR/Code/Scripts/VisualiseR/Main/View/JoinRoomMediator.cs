using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class JoinRoomMediator : Mediator
    {
        [Inject]
        public JoinRoomView _view { get; set; }

        [Inject]
        public JoinRoomSignal JoinRoomSignal { get; set; }

        public override void OnRegister()
        {
            _view.JoinRoomButtonClickSignal.AddListener(OnJoinRoomButtonClicked);
        }

        public override void OnRemove()
        {
            _view.JoinRoomButtonClickSignal.RemoveListener(OnJoinRoomButtonClicked);
        }

        private void OnJoinRoomButtonClicked(string roomName)
        {
            JoinRoomSignal.Dispatch(roomName);
        }
    }
}