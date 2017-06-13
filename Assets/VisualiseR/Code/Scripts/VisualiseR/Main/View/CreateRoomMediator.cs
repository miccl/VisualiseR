using strange.extensions.mediation.impl;
using UnityEngine;
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
        public SelectWebFileSignal SelectWebFileSignal { get; set; }

        [Inject]
        public MediumChangedSignal MediumChangedSignal { get; set; }

        [Inject]
        public CreateRoomSignal CreateRoomSignal { get; set; }

        [Inject]
        public IPictureMedium Medium { get; set; }

        public override void OnRegister()
        {
            _view.SelectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            _view.SelectWebFileButtonClickedSignal.AddListener(OnSelectWebFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.AddListener(OnCreateRoomButtonClick);
            MediumChangedSignal.AddListener(OnMediumChanged);
            _view.ChoosenMedium = Medium;
        }

        public override void OnRemove()
        {
            _view.SelectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
            _view.SelectWebFileButtonClickedSignal.RemoveListener(OnSelectWebFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.RemoveListener(OnCreateRoomButtonClick);
            MediumChangedSignal.RemoveListener(OnMediumChanged);
        }

        private void OnError(string msg)
        {
            _view.DisplayErrorMessage(msg);
        }

        private void OnCreateRoomButtonClick(string roomName, RoomType roomType, IPictureMedium medium)
        {
            CreateRoomSignal.Dispatch(roomName, roomType, (PictureMedium) medium);
        }

        private void OnSelectDiskFileButtonClicked()
        {
            SelectDiskFileSignal.Dispatch();
        }

        private void OnMediumChanged(PictureMedium pictureMedium)
        {
            //TODO davor könnte beispielsweise eine Laderad kommen, bis dieser Aufruf getätigt wird
            _view.ChoosenMedium = pictureMedium;
            _view.ChooseMediumDropdown.captionText.text = pictureMedium.Name;
        }

        private void OnSelectWebFileButtonClicked()
        {
            SelectWebFileSignal.Dispatch();
        }
    }
}