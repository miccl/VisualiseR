using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    public class ObjectView : DdObject, IPointerClickHandler
    {
        private JCsLogger Logger;

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
        
        private int _rotationValue = 0;
        private int RotationValue
        {
            get { return _rotationValue; }
            set
            {
                _rotationValue = value;
                if (value > 8)
                {
                    _rotationValue = 0;
                }
            }
        }


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
            GetComponent<Renderer>().material.color = HSBColor.ToColor(new HSBColor(ColorValue, 1, 1));
            Logger.DebugFormat("Changing color of object '{0}' to {1}", this, GetComponent<Renderer>().material.color);

            ColorValue += 0.1f;
        }

        private void ChangeObjectRotation()
        {
            string binaryValue = Convert.ToString(RotationValue, 2);
            int rotationX = Int32.Parse(binaryValue[binaryValue.Length - 1].ToString());
            int rotationY = binaryValue.Length >= 2 ? Int32.Parse(binaryValue[binaryValue.Length - 2].ToString()) : 0;
            int rotationZ = binaryValue.Length == 3 ? Int32.Parse(binaryValue[0].ToString()) : 0;
           
            Logger.DebugFormat("Rotating object (x: {0}, y: {1}, z: {2})", rotationX * 90, rotationY * 90, rotationZ * 90);
            transform.rotation = Quaternion.Euler(rotationX * 90, rotationY * 90, rotationZ * 90);
            
            RotationValue++;
        }
    }
}