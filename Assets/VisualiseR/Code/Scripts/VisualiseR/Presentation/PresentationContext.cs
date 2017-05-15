using strange.examples.signals;
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
            injectionBinder.Bind<IRoom>().To<Room>().ToSingleton();
        }

        private void BindServices()
        {
            // TODO
        }

        private void BindMediators()
        {
            mediationBinder.Bind<PresentationScreenView>().To<PresentationScreenMediator>();
            mediationBinder.Bind<SelectDiskFileView>().To<SelectDiskFileMediator>();
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

            commandBinder.Bind<LoadFilesSignal>().To<LoadFilesCommand>();
            commandBinder.Bind<SelectDiskFileSignal>().To<SelectDiskFileCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
            injectionBinder.Bind<MediumChangedSignal>().ToSingleton();
        }
    }
}