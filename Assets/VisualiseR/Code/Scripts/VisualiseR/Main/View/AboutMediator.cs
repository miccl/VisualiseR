using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    /// <summary>
    /// Mediator for the <see cref="AboutView"/>
    /// </summary>
    public class AboutMediator : Mediator
    {
        [Inject]
        public AboutView _view { get; set; }

        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}