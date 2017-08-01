using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    public class ObjectMediator : Mediator
    {
        [Inject]
        public ObjectView _view { get; set; }
        
        [Inject]
        public ChangeEditModeSignal ChangeEditModeSignal { get; set; }
        
        [Inject]
        public RotateObjectSignal RotateObjectSignal { get; set; }
        
        public override void OnRegister()
        {
            _view.RotateObjectSignal.AddListener(OnRotateObject);
            ChangeEditModeSignal.AddListener(OnChangedEditMode);
        }

        public override void OnRemove()
        {
            ChangeEditModeSignal.RemoveListener(OnChangedEditMode);
        }

        private void OnChangedEditMode(EditMode editMode)
        {
            _view._editMode = editMode;
        }

        private void OnRotateObject(GameObject go, int rotateValue)
        {
            RotateObjectSignal.Dispatch(go, rotateValue);
        }
    }
}