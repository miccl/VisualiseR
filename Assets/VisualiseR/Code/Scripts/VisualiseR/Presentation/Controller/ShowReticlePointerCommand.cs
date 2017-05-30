using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class ShowReticlePointerCommand : Command
    {
        [Inject]
        public bool show { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            ShowReticlePointer();
        }

        private void ShowReticlePointer()
        {
            var character = UnityUtil.FindGameObject("FirstPersonCharacter").transform;
            var reticlePointer = character.Find("GvrReticlePointer").gameObject;
            reticlePointer.SetActive(show);
        }
    }
}