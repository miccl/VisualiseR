using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Context for the presentation scene.
    /// </summary>
    public class ShowroomContext : MVCSContext
    {
        public ShowroomContext(MonoBehaviour view) : base(view)
        {
        }

        public ShowroomContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            ShowroomStartSignal startSignal = injectionBinder.GetInstance<ShowroomStartSignal>();
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
        /// Bind Models
        /// </summary>
        private void BindModels()
        {
            injectionBinder.Bind<IPlayer>().To<Player>().ToSingleton();
            injectionBinder.Bind<IObject>().To<Object>().ToSingleton();
        }

        private void BindServices()
        {
            // TODO
        }

        private void BindMediators()
        {
            mediationBinder.Bind<ShowroomPlayerView>().To<ShowroomPlayerMediator>();
            mediationBinder.Bind<ShowroomSceneMenuView>().To<ShowroomSceneMenuMediator>();
            mediationBinder.Bind<GroundView>().To<GroundMediator>();
            mediationBinder.Bind<ObjectView>().To<ObjectMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<ShowroomStartSignal>().To<ShowroomStartCommand>().Once();
            }
            else
            {
                commandBinder.Bind<ShowroomStartSignal>()
//                    .To<KillAudioListenerCommand>()
                    .To<ShowroomStartCommand>()
                    .Once()
                    .InSequence();
            }
            commandBinder.Bind<ShowShowroomSceneMenuSignal>().To<ShowShowroomSceneMenuCommand>();
            commandBinder.Bind<InstantiateObjectSignal>().To<InstantiateObjectCommand>();
            commandBinder.Bind<TeleportPlayerSignal>().To<TeleportPlayerCommand>();
            commandBinder.Bind<RotateObjectSignal>().To<RotateObjectCommand>();
            commandBinder.Bind<ColorObjectSignal>().To<ColorObjectCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<PlayerInstantiatedSignal>().ToSingleton();
            injectionBinder.Bind<ShowroomSceneMenuIsShownSignal>().ToSingleton();
            injectionBinder.Bind<ChangeEditModeSignal>().ToSingleton();
        }
    }
}