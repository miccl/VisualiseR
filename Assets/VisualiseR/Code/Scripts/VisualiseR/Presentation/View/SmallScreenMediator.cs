﻿using System.Collections.Generic;
using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Mediator for the <see cref="SmallScreenView"/>
    /// </summary>
    public class SmallScreenMediator : Mediator
    {
        [Inject]
        public SmallScreenView view { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }

        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }

        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }

        [Inject]
        public FilesLoadedSignal FilesLoadedSignal { get; set; }
                
        [Inject]
        public ShowLoadingAnimationSignal ShowLoadingAnimationSignal { get; set; }
        
        [Inject]
        public PresentationSceneMenuIsShownSignal PresentationSceneMenuIsShownSignal { get; set; }


        public override void OnRegister()
        {
            SlidePositionChangedSignal.AddListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            FilesLoadedSignal.AddListener(OnFilesLoaded);
        }

        public override void OnRemove()
        {
            SlidePositionChangedSignal.RemoveListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            FilesLoadedSignal.RemoveListener(OnFilesLoaded);

        }

        private void OnFilesLoaded(SlideMedium medium, List<byte[]> images)
        {
            view.Init(medium, images);
        }
        private void OnSlidePositionChanged()
        {
            if (view._player.IsHost())
            {
                view.LoadCurrentSlide();
            }
        }

        private void OnPlayerInstantiated(Player player)
        {
            view._player = player;
        }

    }
}