using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    public class ObjectView : DdObject
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
    }
}