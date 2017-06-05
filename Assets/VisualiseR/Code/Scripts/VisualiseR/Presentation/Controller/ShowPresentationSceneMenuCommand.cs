﻿using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class ShowPresentationSceneMenuCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowPresentationSceneMenuCommand));

        [Inject]
        public Player _player { get; set; }
        
        [Inject]
        public SlideMedium _medium { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        [Inject]
        public PresentationSceneMenuIsShownSignal PresentationSceneMenuIsShownSignal { get; set; }
        public override void Execute()
        {
            if (_player.HasRight(AcessList.SceneMenu))
            {
                ShowSceneMenu();
            }
        }

        private void ShowSceneMenu()
        {
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("PresentationSceneMenuCanvas")
                .gameObject;
            sceneMenu.SetActive(true);
            var sceneView = sceneMenu.GetComponent<PresentationSceneMenuView>();
            sceneView.Init(_contextView, _player, _medium);
            PresentationSceneMenuIsShownSignal.Dispatch(true);
            Logger.InfoFormat("Scene menu is shown");
        }
    }
}