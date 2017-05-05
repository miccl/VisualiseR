using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class ScreenMediator : Mediator
    {
        [Inject]
        public PresentationScreenView view { get; set; }

        [Inject]
        public MediumChangedSignal mediumChangedSignal { get; set; }

        [Inject]
        public LoadAndConvertFilesSignal LoadAndConvertFilesSignal { get; set; }


        public override void OnRegister()
        {
            mediumChangedSignal.AddListener(OnMediumChanged);
        }


        public override void OnRemove()
        {
            mediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        public void OnMediumChanged(Medium medium)
        {
            view._medium = medium;
            view.SetupMedium();
        }
    }
}