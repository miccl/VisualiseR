using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    public class ShowroomSceneMenuMediator : Mediator
    {
        [Inject]
        public ShowroomSceneMenuView _view { get; set; }
        
        [Inject]
        public InstantiateObjectSignal InstantiateObjectSignal { get; set; }
        
        [Inject]
        public ChangeEditModeSignal ChangeEditModeSignal { get; set; }
        
        public override void OnRegister()
        {
            _view.CreateObjectSignal.AddListener(OnCreateObject);
            _view.ChangeEditModeSignal.AddListener(OnChangeEditMode);
        }

        public override void OnRemove()
        {
            _view.CreateObjectSignal.RemoveListener(OnCreateObject);
            _view.ChangeEditModeSignal.RemoveListener(OnChangeEditMode);
        }

        private void OnCreateObject(ObjectType type)
        {
            InstantiateObjectSignal.Dispatch(type);
        }

        private void OnChangeEditMode(EditMode mode)
        {
            ChangeEditModeSignal.Dispatch(mode);
        }
    }
}