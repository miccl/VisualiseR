using strange.examples.signals;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeReviewContext : MVCSContext
    {
        public CodeReviewContext(MonoBehaviour view) : base(view)
        {
        }

        public CodeReviewContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        /// <summary>
        /// Unbinds the default EventCommandBinder and rebind the SignalCommandBinder.
        /// </summary>
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        /// <summary>
        /// Override Start so that we can fire the StartSignal
        /// </summary>
        /// <returns></returns>
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
            mediationBinder.Bind<ScreenView>().To<ScreenMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
            }
            else
            {
                commandBinder.Bind<StartSignal>()
//                    .To<KillAudioListenerCommand>()
                    .To<StartCommand>()
                    .Once();
            }

            commandBinder.Bind<LoadAndConvertFilesSignal>().To<LoadDiskDataCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
            injectionBinder.Bind<MediumChangedSignal>().ToSingleton();
        }
    }
}