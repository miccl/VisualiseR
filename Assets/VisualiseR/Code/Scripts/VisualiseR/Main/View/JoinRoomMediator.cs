using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class JoinRoomMediator : Mediator
    {
        [Inject]
        private JoinRoomView _view { get; set; }


        public override void OnRegister()
        {
            _view.Init();
        }

        public override void OnRemove()
        {
        }
    }
}