﻿using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Context for the presentation scene.
    /// </summary>
    public class PresentationContext : MVCSContext
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="view"></param>
        public PresentationContext(MonoBehaviour view) : base(view)
        {
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="flags"></param>
        public PresentationContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        /// <summary>
        /// Unbinds the default EventCommandBinder and rebinds to the SignalCommandBinder.
        /// </summary>
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        /// <summary>
        /// Override Start so that we can fire the PresentationStartSignal
        /// </summary>
        /// <returns></returns>
        public override IContext Start()
        {
            base.Start();
            PresentationStartSignal presentationStartSignal = injectionBinder.GetInstance<PresentationStartSignal>();
            presentationStartSignal.Dispatch();
            return this;
        }

        protected override void mapBindings()
        {
            BindModels();
            BindServices();
            BindMediators();
            BindCommands();
            BindSignals();
        }


        /// <summary>
        /// BInd Models
        /// </summary>
        private void BindModels()
        {
            injectionBinder.Bind<IPictureMedium>().To<PictureMedium>().ToSingleton();
            injectionBinder.Bind<IPicture>().To<Picture>().ToSingleton();
            injectionBinder.Bind<IPlayer>().To<Player>().ToSingleton();
            injectionBinder.Bind<IRoom>().To<Common.Room>().ToSingleton();
            injectionBinder.Bind<ISlide>().To<Slide>().ToSingleton();
            injectionBinder.Bind<ISlideMedium>().To<SlideMedium>().ToSingleton();
        }

        private void BindServices()
        {
        }

        private void BindMediators()
        {
            mediationBinder.Bind<PresentationScreenView>().To<PresentationScreenMediator>();
            mediationBinder.Bind<PresentationSceneMenuView>().To<PresentationSceneMenuMediator>();
            mediationBinder.Bind<SimplePresentationScreenView>().To<SimplePresentationScreenMediator>();
            mediationBinder.Bind<TimerView>().To<TimerMediator>();
            mediationBinder.Bind<NetworkController>().To<NetworkControllerMediator>();
            mediationBinder.Bind<NetworkedPlayer>().To<NetworkedPlayerMediator>();
            mediationBinder.Bind<PresentationPlayerView>().To<PresentationPlayerMediator>();
            mediationBinder.Bind<SmallScreenView>().To<SmallScreenMediator>();
        }

        private void BindCommands()
        {
            commandBinder.Bind<PresentationStartSignal>().To<PresentationStartCommand>().Once();
            commandBinder.Bind<NextSlideSignal>().To<NextSlideCommand>();
            commandBinder.Bind<PrevSlideSignal>().To<PrevSlideCommand>();
            commandBinder.Bind<ShowAllSignal>().To<ShowAllSlidesCommand>();
            commandBinder.Bind<SlideSelectedSignal>().To<SlideSelectedCommand>();
            commandBinder.Bind<ShowSceneMenuSignal>().To<ShowPresentationSceneMenuCommand>();
            commandBinder.Bind<InstantiatePlayerSignal>().To<InstantiatePlayerCommand>();
            commandBinder.Bind<LoadFilesSignal>().To<LoadFilesCommand>();
            commandBinder.Bind<ShowTimeSignal>().To<ShowTimeCommand>();
            commandBinder.Bind<ShowLoadingAnimationSignal>().To<ShowLoadingAnimationCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<SlidePositionChangedSignal>().ToSingleton();
            injectionBinder.Bind<ChangeTimerStatusSignal>().ToSingleton();
            injectionBinder.Bind<SetTimerSignal>().ToSingleton();
            injectionBinder.Bind<TimerRunDownSignal>().ToSingleton();
            injectionBinder.Bind<PlayerInstantiatedSignal>().ToSingleton();
            injectionBinder.Bind<FilesLoadedSignal>().ToSingleton();
            injectionBinder.Bind<CreateOrJoinSignal>().ToSingleton();
            injectionBinder.Bind<PresentationSceneMenuIsShownSignal>().ToSingleton();
            injectionBinder.Bind<ShowLaserSignal>().ToSingleton();
            injectionBinder.Bind<ChangeClockTypeSignal>().ToSingleton();
        }
    }
}