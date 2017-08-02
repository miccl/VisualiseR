using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    /// <summary>
    /// Mediator for the <see cref="MainMenuView"/>
    /// </summary>
    public class MainMenuMediator : Mediator
    {
        [Inject]
        public MainMenuView _view { get; set; }
        
        [Inject]
        public ShowWindowMessageSignal ShowWindowMessageSignal { get; set; }

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
            ShowWindowMessageSignal.Dispatch(msg);
        }
    }
}