using System;
using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    /// <summary>
    /// Mediator for the <see cref="CreateRoomView"/>
    /// </summary>
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

        [Inject]
        public SelectionCanceledSignal SelectionCanceledSignal { get; set; }

        [Inject]
        public ShowWindowMessageSignal ShowWindowMessageSignal { get; set; }

        public override void OnRegister()
        {
            _view.SelectDiskFileButtonClickedSignal.AddListener(OnSelectDiskFileButtonClicked);
            _view.SelectWebFileButtonClickedSignal.AddListener(OnSelectWebFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.AddListener(OnCreateRoomButtonClick);
            _view.ShowMessageSignal.AddListener(OnShowMessage);
            MediumChangedSignal.AddListener(OnMediumChanged);
            SelectionCanceledSignal.AddListener(OnSelectionCanceled);
            _view._choosenMedium = Medium;
        }

        public override void OnRemove()
        {
            _view.SelectDiskFileButtonClickedSignal.RemoveListener(OnSelectDiskFileButtonClicked);
            _view.SelectWebFileButtonClickedSignal.RemoveListener(OnSelectWebFileButtonClicked);
            _view.CreateRoomButtonClickedSignal.RemoveListener(OnCreateRoomButtonClick);
            _view.ShowMessageSignal.RemoveListener(OnShowMessage);
            SelectionCanceledSignal.RemoveListener(OnSelectionCanceled);
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
            if (pictureMedium == null)
            {
                OnSelectionCanceled();
                return;
            }
            _view._choosenMedium = pictureMedium;
            _view._chooseMediumDropdown.captionText.text = pictureMedium.Name;
            _view.ChangeInteractibilityOfButtons(true);
        }

        private void OnSelectWebFileButtonClicked()
        {
            SelectWebFileSignal.Dispatch();
        }

        private void OnSelectionCanceled()
        {
            _view.ChangeInteractibilityOfButtons(true);
            _view.SelectionCanceled();
        }

        private void OnShowMessage(Message msg)
        {
            ShowWindowMessageSignal.Dispatch(msg);
        }
    }
}