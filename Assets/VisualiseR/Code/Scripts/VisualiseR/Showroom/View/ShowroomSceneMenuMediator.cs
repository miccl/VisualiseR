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
        
        public override void OnRegister()
        {
            _view.CreateObjectSignal.AddListener(OnCreateObject);
        }

        public override void OnRemove()
        {
            _view.CreateObjectSignal.RemoveListener(OnCreateObject);
        }

        private void OnCreateObject(ObjectType type)
        {
            InstantiateObjectSignal.Dispatch(type);
        }
    }
}