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
        public ShowPresentationContextMenuSignal ShowPresentationContextMenuSignal { get; set; }

        [Inject]
        public ShowSceneMenuSignal ShowSceneMenuSignal { get; set; }

        [Inject]
        public PlayerInstantiatedSignal PlayerInstantiatedSignal { get; set; }

        [Inject]
        public LoadFilesSignal LoadFilesSignal { get; set; }

        [Inject]
        public FilesLoadedSignal FilesLoadedSignal { get; set; }

        public override void OnRegister()
        {
            view.NextSlideSignal.AddListener(OnNextSlide);
            view.PrevSlideSignal.AddListener(OnPrevSlide);
            view.ShowPresentationContextMenuSignal.AddListener(OnShowContextMenu);
            view.ShowSceneMenuSignal.AddListener(OnShowScenMenu);
            SlidePositionChangedSignal.AddListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.AddListener(OnPlayerInstantiated);
            FilesLoadedSignal.AddListener(OnFilesLoaded);
        }

        private void OnFilesLoaded(SlideMedium medium, List<byte[]> images)
        {
            view.Init(medium, images);
        }

        public override void OnRemove()
        {
            view.NextSlideSignal.RemoveListener(OnNextSlide);
            view.PrevSlideSignal.RemoveListener(OnPrevSlide);
            view.ShowPresentationContextMenuSignal.RemoveListener(OnShowContextMenu);
            view.ShowSceneMenuSignal.RemoveListener(OnShowScenMenu);
            SlidePositionChangedSignal.RemoveListener(OnSlidePositionChanged);
            PlayerInstantiatedSignal.RemoveListener(OnPlayerInstantiated);
            FilesLoadedSignal.RemoveListener(OnFilesLoaded);
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

        private void OnShowContextMenu(IPlayer player, GameObject screen)
        {
            ShowPresentationContextMenuSignal.Dispatch((Player) player, screen);
        }

        private void OnShowScenMenu()
        {
            ShowSceneMenuSignal.Dispatch();
        }

        private void OnPlayerInstantiated(Player player)
        {
            view._player = player;
            if (player.Type.Equals(PlayerType.Host))
            {
                LoadFilesSignal.Dispatch(player);
            }
            else
            {
                view.RequestDataFromMaster();
            }
        }
    }
}