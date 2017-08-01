using System;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// View for objects in the scene.
    /// </summary>
    public class ObjectView : DdObject, IPointerClickHandler
    {
        private JCsLogger Logger;

        internal IObject Object;

        public Signal<IObject, GameObject, int> RotateObjectSignal = new Signal<IObject, GameObject, int>();
        public Signal<IObject, GameObject, float> ColorObjectSignal = new Signal<IObject, GameObject, float>();
        
        private EditMode __editMode;

        internal EditMode _editMode
        {
            get { return __editMode; }
            set
            {
                if (!__editMode.Equals(value))
                {
                    Logger.DebugFormat("Changing edit mode (from: {0}, to: {1})", _editMode, value);
                    __editMode = value;
                    ChangeEditMode();
                }
            }
        }

        private float _colorValue = 0;
        private int _rotationValue = 0;


        private void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(ShowroomPlayerView));
        }

        private void Start()
        {
            base.Start();
            base.Init();
            _editMode = EditMode.Positioning;
            ddIsActive = true;
        }
        
        public void Init(IObject obj)
        {
            Object = obj;
        }

        private new void Update()
        {
            base.Update();
            if (Object == null) return;
            if (transform.hasChanged)
            {
                Object.Position = transform.position;
                transform.hasChanged = false;
            }
        }

        internal void ChangeEditMode()
        {
            switch (_editMode)
            {
                case EditMode.Positioning:
                    ddIsActive = true;
                    break;
                case EditMode.Coloring:
                    ddIsActive = false;
                    break;
                case EditMode.Rotate:
                    ddIsActive = false;
                    break;
                default:
                    Logger.Error("Invalid edit mode was choosen");
                    break;
            }
        }

        public void OnClick(BaseEventData data)
        {
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_editMode.Equals(EditMode.Coloring))
            {
                ChangeObjectColor();
                return;
            }
            
            if (_editMode.Equals(EditMode.Rotate))
            {
                ChangeObjectRotation();
                return;
            }

            
        }

        private void ChangeObjectColor()
        {
            ColorObjectSignal.Dispatch(Object, gameObject, _colorValue);
            _colorValue += 0.1f;
        }

        private void ChangeObjectRotation()
        {
            RotateObjectSignal.Dispatch(Object, gameObject, _rotationValue);
            _rotationValue++;
        }
    }
}