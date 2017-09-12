using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    /// <summary>
    /// Mediator for the <see cref="JoinRoomView"/>
    /// </summary>
    public class JoinRoomMediator : Mediator
    {
        [Inject]
        public JoinRoomView _view { get; set; }

        [Inject]
        public JoinRoomSignal JoinRoomSignal { get; set; }
        
        [Inject]
        public ShowWindowMessageSignal ShowWindowMessageSignal { get; set; }

        public override void OnRegister()
        {
            _view.JoinRoomButtonClickSignal.AddListener(OnJoinRoomButtonClicked);
            _view.ShowMessageSignal.AddListener(OnShowMessage);
        }

        public override void OnRemove()
        {
            _view.JoinRoomButtonClickSignal.RemoveListener(OnJoinRoomButtonClicked);
            _view.ShowMessageSignal.RemoveListener(OnShowMessage);
        }

        private void OnJoinRoomButtonClicked(string roomName)
        {
            JoinRoomSignal.Dispatch(roomName);
        }

        private void OnShowMessage(Message msg)
        {
            ShowWindowMessageSignal.Dispatch(msg);
        }
    }
}