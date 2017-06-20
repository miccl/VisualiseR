using strange.extensions.mediation.impl;
using VisualiseR.Presentation;

namespace VisualiseR.Common
{
    /// <summary>
    /// Signal to instantiate the <see cref="NetworkController"/>
    /// </summary>
    public class NetworkControllerMediator : Mediator
    {
        [Inject]
        public NetworkController _view { get; set; }

        [Inject]
        public CreateOrJoinSignal CreateOrJoinSignal { get; set; }

        public override void OnRegister()
        {
            CreateOrJoinSignal.AddListener(OnCreateOrJoinSignal);
        }

        public override void OnRemove()
        {
            CreateOrJoinSignal.RemoveListener(OnCreateOrJoinSignal);
        }

        private void OnCreateOrJoinSignal(string roomName)
        {
            _view.Init(roomName);
        }
    }
}