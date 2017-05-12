using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class JoinRoomMediator : Mediator
    {
        [Inject]
        public JoinRoomView _view { get; set; }


        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}