using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

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
            StartSignal startSignal = (StartSignal) injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
            return this;
        }

        protected override void mapBindings()
        {
            throw new System.NotImplementedException();
//            injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
//            injectionBinder.Bind<IExampleService>().To<ExampleService>().ToSingleton();
//
//
//            mediationBinder.Bind<ExampleView>().To<ExampleMediator>();
//
//
//            // Bind to
//            commandBinder.Bind<CallWebServiceSignal>().To<CallWebServiceCommand>();
//
//            //StartSignal is now fired instead of the START event.
//            //Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.
//            commandBinder.Bind<StartSignal>().To<StartCommand>().Once ();
//
//            //In MyFirstProject, there's are SCORE_CHANGE and FULFILL_SERVICE_REQUEST Events.
//            //Here we change that to a Signal. The Signal isn't bound to any Command,
//            //so we map it as an injection so a Command can fire it, and a Mediator can receive it
//            injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
//            injectionBinder.Bind<FulfillWebServiceRequestSignal>().ToSingleton();
        }


    }
}