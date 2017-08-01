using System;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    public class ObjectView : DdObject, IPointerClickHandler
    {
        private JCsLogger Logger;

        public Signal<GameObject, int> RotateObjectSignal = new Signal<GameObject, int>();
        
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
        private float ColorValue
        {
            get { return _colorValue; }
            set
            {
                _colorValue = value;
                if (value > 1)
                {
                    _colorValue = 0;
                }
            }
        }

        private int RotationValue = 0;


        private void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(ShowroomPlayerView));
        }

        private void Start()
        {
            base.Start();
            base.Init();
            _editMode = EditMode.DragAndDrop;
            ddIsActive = true;
        }

        internal void ChangeEditMode()
        {
            switch (_editMode)
            {
                case EditMode.DragAndDrop:
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
            var color = HSBColor.ToColor(new HSBColor(ColorValue, 1, 1));
            GetComponent<Renderer>().material.color = color;
            Logger.DebugFormat("Changing color of object '{0}' to {1}", this, color);

            ColorValue += 0.1f;
        }

        private void ChangeObjectRotation()
        {
            RotateObjectSignal.Dispatch(gameObject, RotationValue);
            RotationValue++;
        }
    }
}