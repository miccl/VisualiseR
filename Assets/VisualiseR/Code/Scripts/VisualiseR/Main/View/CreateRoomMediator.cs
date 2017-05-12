using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class CreateRoomMediator : Mediator
    {
        [Inject]
        public CreateRoomView _view { get; set; }

        [Inject]
        public SelectDiskFileSignal SelectDiskFileSignal { get; set; }

        [Inject]
        public MediumChangedSignal MediumChangedSignal { get; set; }

        public override void OnRegister()
        {
            _view._selectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            MediumChangedSignal.AddListener(OnMediumChanged);
        }

        public override void OnRemove()
        {
            _view._selectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
            MediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        private void OnSelectDiskFileButtonClicked()
        {
            SelectDiskFileSignal.Dispatch();
        }

        private void OnMediumChanged(Medium medium)
        {
            _view.OnMediumLoadFinished(medium);
        }
    }
}