using strange.examples.multiplecontexts.common;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.CodeReview;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class MainContext : MVCSContext
    {
        public MainContext(MonoBehaviour view) : base(view)
        {
        }

        public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the StartSignal
        override public IContext Start()
        {
            base.Start();
            StartSignal startSignal = injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
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
            injectionBinder.Bind<IMedium>().To<Medium>().ToSingleton();
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
            mediationBinder.Bind<CreateRoomView>().To<CreateRoomMediator>();
            mediationBinder.Bind<JoinRoomView>().To<JoinRoomMediator>();
            mediationBinder.Bind<SettingsView>().To<SettingsMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<StartSignal>().To<StartCommand>().Once ();
            }
            else
            {
                commandBinder.Bind<StartSignal>()
                    .To<KillAudioListenerCommand>()
                    .To<StartCommand>()
                    .Once();
            }

            commandBinder.Bind<SelectDiskFileSignal>().To<SelectDiskFileCommand>();

        }

        private void BindSignals()
        {
        }
    }
}