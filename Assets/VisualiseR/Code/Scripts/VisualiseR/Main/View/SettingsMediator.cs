using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class SettingsMediator : Mediator
    {
        [Inject]
        private SettingsView _view { get; set; }


        public override void OnRegister()
        {
            _view.Init();
        }

        public override void OnRemove()
        {
        }
    }
}