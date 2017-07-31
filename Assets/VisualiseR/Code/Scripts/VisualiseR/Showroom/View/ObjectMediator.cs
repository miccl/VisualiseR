using strange.extensions.mediation.impl;

namespace VisualiseR.Showroom
{
    public class ObjectMediator : Mediator
    {
        [Inject]
        public ObjectView _view { get; set; }
        
        [Inject]
        public ChangeEditModeSignal ChangeEditModeSignal { get; set; }
        
        public override void OnRegister()
        {
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
    }
}