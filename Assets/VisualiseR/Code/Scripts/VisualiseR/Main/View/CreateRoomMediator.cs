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
            _view.SelectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            MediumChangedSignal.AddListener(OnMediumChanged);
        }

        public override void OnRemove()
        {
            _view.SelectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
            MediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        private void OnSelectDiskFileButtonClicked()
        {
            SelectDiskFileSignal.Dispatch();
        }

        private void OnMediumChanged(Medium medium)
        {
            //TODO davor könnte beispielsweise eine Laderad kommen, bis dieser Aufruf getätigt wird
            _view.ChoosenMedium = medium;
            _view.ChooseMediumDropdown.captionText.text = medium.Name;

        }
    }
}