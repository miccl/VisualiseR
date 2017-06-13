using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PresentationScreenMediator : Mediator
    {
        [Inject]
        public PresentationScreenView view { get; set; }

        [Inject]
        public NextSlideSignal NextSlideSignal { get; set; }

        [Inject]
        public PrevSlideSignal PrevSlideSignal { get; set; }

        [Inject]
        public SlidePositionChangedSignal SlidePositionChangedSignal { get; set; }

        [Inject]
        public ShowSceneMenuSignal ShowSceneMenuSignal { get; set; }

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
            view.NextSlideSignal.AddListener(OnNextSlide);
            view.PrevSlideSignal.AddListener(OnPrevSlide);
            view.ShowSceneMenuSignal.AddListener(OnShowSceneMenu);
            view.ShowLoadingAnimationSignal.AddListener(OnShowAnimation);
            SlidePositionChangedSignal.AddListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            FilesLoadedSignal.AddListener(OnFilesLoaded);
            PresentationSceneMenuIsShownSignal.AddListener(OnSceneMenuIsShown);
        }

        public override void OnRemove()
        {
            view.NextSlideSignal.RemoveListener(OnNextSlide);
            view.PrevSlideSignal.RemoveListener(OnPrevSlide);
            view.ShowSceneMenuSignal.RemoveListener(OnShowSceneMenu);
            view.ShowLoadingAnimationSignal.RemoveListener(OnShowAnimation);
            SlidePositionChangedSignal.RemoveListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            FilesLoadedSignal.RemoveListener(OnFilesLoaded);
            PresentationSceneMenuIsShownSignal.RemoveListener(OnSceneMenuIsShown);

        }

        private void OnShowAnimation(bool show, string text)
        {
            ShowLoadingAnimationSignal.Dispatch(show, text);
        }

        private void OnFilesLoaded(SlideMedium medium, List<byte[]> images)
        {
            view.Init(medium, images);
        }

        private void OnNextSlide(IPlayer player, ISlideMedium medium)
        {
            NextSlideSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        private void OnPrevSlide(IPlayer player, ISlideMedium medium)
        {
            PrevSlideSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        private void OnSlidePositionChanged()
        {
            view.LoadCurrentSlide();
        }

        private void OnShowSceneMenu(IPlayer player, ISlideMedium medium)
        {
            ShowSceneMenuSignal.Dispatch((Player) player, (SlideMedium) medium);
        }

        private void OnPlayerInstantiated(Player player)
        {
            view._player = player;
            if (player.IsHost())
            {
                LoadFilesSignal.Dispatch(player);
            }
            else
            {
                view.RequestDataFromMaster();
            }
        }

        private void OnSceneMenuIsShown(bool isShown)
        {
            view._isSceneMenuShown = isShown;
        }
    }
}