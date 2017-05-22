using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class SelectWebFileMediator : Mediator
    {
        [Inject]
        public SelectWebFileView _view { get; set; }


        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }


        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}