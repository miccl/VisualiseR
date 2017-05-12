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

        [Inject]
        public ErrorSignal ErrorSignal { get; set; }

        [Inject]
        public CreateRoomSignal CreateRoomSignal { get; set; }

        [Inject]
        public IMedium Medium { get; set; }

        public override void OnRegister()
        {
            _view.SelectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.AddListener(OnCreateRoomButtonClick);
            MediumChangedSignal.AddListener(OnMediumChanged);
            ErrorSignal.AddListener(OnError);
            _view.ChoosenMedium = Medium;
        }

        private void OnError(string msg)
        {
            _view.DisplayErrorMessage(msg);
        }

        public override void OnRemove()
        {
            _view.SelectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.RemoveListener(OnCreateRoomButtonClick);
            MediumChangedSignal.RemoveListener(OnMediumChanged);
            ErrorSignal.RemoveListener(OnError);
        }

        private void OnCreateRoomButtonClick(string roomName, RoomType roomType, IMedium medium)
        {
            CreateRoomSignal.Dispatch(roomName, roomType, (Medium) medium);
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