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
            }
        }

        private void ChangeObjectColor()
        {
            GetComponent<Renderer>().material.color = HSBColor.ToColor(new HSBColor(ColorValue, 1, 1));
            ColorValue += 0.1f;
        }
    }
}