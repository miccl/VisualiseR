using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to instantiate a object prefab in the scene.
    /// </summary>
    public class InstantiateObjectCommand : Command
    {
        [Inject]
        public ObjectType _objectType { get; set; }
        
        [Inject]
        public IObject Object { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            InstantiateObject();

        }

        /// <summary>
        /// Initialises an object at given position.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        private void InstantiateObject()
        {
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward.normalized * 3;

            var screen = (GameObject) GameObject.Instantiate(Resources.Load(_objectType.ToString()), pos, Quaternion.identity);
            screen.name = _objectType.ToString();
            
            screen.transform.SetParent(_contextView.transform.Find("Objects"));
        }
    }
}