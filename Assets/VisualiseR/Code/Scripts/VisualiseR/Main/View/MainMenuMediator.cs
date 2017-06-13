using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class MainMenuMediator : Mediator
    {
        [Inject]
        public MainMenuView _view { get; set; }
        
        [Inject]
        public ShowMessageSignal ShowMessageSignal { get; set; }

        public override void OnRegister()
        {
            _view.MessageSignal.AddListener(OnMessage);
        }

        public override void OnRemove()
        {
            _view.MessageSignal.RemoveListener(OnMessage);
        }

        private void OnMessage(Message msg)
        {
            ShowMessageSignal.Dispatch(msg);
        }
    }
}