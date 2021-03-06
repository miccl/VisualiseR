﻿using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Mediator for the <see cref="ObjectView"/>
    /// </summary>
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

        private void OnRotateObject(IObject o, GameObject go, int rotateValue)
        {
            RotateObjectSignal.Dispatch((Object) o, go, rotateValue);
        }

        private void OnColorObject(IObject o, GameObject go, float colorValue)
        {
            ColorObjectSignal.Dispatch((Object) o, go, colorValue);
        }
    }
}