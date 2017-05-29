using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;

namespace VisualiseR.Presentation
{
    public class PresentationContext : MVCSContext
    {
        public PresentationContext(MonoBehaviour view) : base(view)
        {
        }

        public PresentationContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the PresentationStartSignal
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
            // TODO
        }

        private void BindMediators()
        {
            mediationBinder.Bind<PresentationScreenView>().To<PresentationScreenMediator>();
            mediationBinder.Bind<PresentationContextMenuView>().To<PresentationContextMenuMediator>();
            mediationBinder.Bind<TimerView>().To<TimerMediator>();
            mediationBinder.Bind<SimplePresentationScreenView>().To<SimplePresentationScreenMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<PresentationStartSignal>().To<PresentationStartCommand>().Once();
            }
            else
            {
                commandBinder.Bind<PresentationStartSignal>()
//                    .To<KillAudioListenerCommand>()
                    .To<PresentationStartCommand>()
                    .Once()
                    .InSequence();
            }
            commandBinder.Bind<NextSlideSignal>().To<NextSlideCommand>();
            commandBinder.Bind<PrevSlideSignal>().To<PrevSlideCommand>();
            commandBinder.Bind<ShowPresentationContextMenuSignal>().To<ShowPresentationContextMenuCommand>();
            commandBinder.Bind<ShowAllSignal>().To<ShowAllCommand>();
            commandBinder.Bind<SlideSelectedSignal>().To<SlideSelectedCommand>();
            commandBinder.Bind<ShowSceneMenuSignal>().To<ShowSceneMenuCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<SlidePositionChangedSignal>().ToSingleton();
            injectionBinder.Bind<ChangeTimerStatusSignal>().ToSingleton();
            injectionBinder.Bind<SetTimerSignal>().ToSingleton();
            injectionBinder.Bind<TimerRunDownSignal>().ToSingleton();
            injectionBinder.Bind<ContextMenuCanceledSignal>().ToSingleton();
        }
    }
}