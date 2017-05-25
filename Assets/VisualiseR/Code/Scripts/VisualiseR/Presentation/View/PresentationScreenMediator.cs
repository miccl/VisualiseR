using strange.extensions.mediation.impl;
using VisualiseR.Common;
using VisualiseR.Main;

namespace VisualiseR.Presentation
{
    public class PresentationScreenMediator : Mediator
    {
        [Inject]
        public PresentationScreenView view { get; set; }

        [Inject]
        public MediumChangedSignal mediumChangedSignal { get; set; }

        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }


        public override void OnRegister()
        {
            mediumChangedSignal.AddListener(OnMediumChanged);
        }


        public override void OnRemove()
        {
            mediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        public void OnMediumChanged(PictureMedium pictureMedium)
        {
            view._medium = pictureMedium;
            view.SetupMedium();
        }
    }
}