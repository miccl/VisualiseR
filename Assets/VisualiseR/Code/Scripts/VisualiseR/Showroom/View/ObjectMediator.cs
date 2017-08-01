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
        
        [Inject]
        public ColorObjectSignal ColorObjectSignal { get; set; }

        
        public override void OnRegister()
        {
            _view.RotateObjectSignal.AddListener(OnRotateObject);
            _view.ColorObjectSignal.AddListener(OnColorObject);
            ChangeEditModeSignal.AddListener(OnChangedEditMode);
        }

        public override void OnRemove()
        {
            _view.RotateObjectSignal.RemoveListener(OnRotateObject);
            _view.ColorObjectSignal.RemoveListener(OnColorObject);
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

        private void OnColorObject(GameObject go, float colorValue)
        {
            ColorObjectSignal.Dispatch(go, colorValue);
        }
    }
}