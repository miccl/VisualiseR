using strange.examples.multiplecontexts.common;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
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

        // Override Start so that we can fire the PresentationStartSignal
        override public IContext Start()
        {
            base.Start();
            MainStartSignal mainStartSignal = injectionBinder.GetInstance<MainStartSignal>();
            mainStartSignal.Dispatch();
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
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
            mediationBinder.Bind<CreateRoomView>().To<CreateRoomMediator>();
            mediationBinder.Bind<JoinRoomView>().To<JoinRoomMediator>();
            mediationBinder.Bind<SettingsView>().To<SettingsMediator>();

            mediationBinder.Bind<SelectDiskFileView>().To<SelectDiskFileMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<MainStartSignal>().To<MainStartCommand>().Once();
            }
            else
            {
                commandBinder.Bind<MainStartSignal>()
                    .To<KillAudioListenerCommand>()
                    .To<MainStartCommand>()
                    .Once();
            }

            commandBinder.Bind<SelectDiskFileSignal>().To<SelectDiskFileCommand>();
            commandBinder.Bind<LoadFilesSignal>().To<LoadFilesCommand>();
            commandBinder.Bind<CreateRoomSignal>().To<CreateRoomCommand>();
            commandBinder.Bind<JoinRoomSignal>().To<JoinRoomSignal>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<MediumChangedSignal>().ToSingleton();
            injectionBinder.Bind<ErrorSignal>().ToSingleton();
            injectionBinder.Bind<RoomChangedSignal>().ToSingleton();
        }
    }
}