using strange.examples.signals;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Main;

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
        /// Override Start so that we can fire the PresentationStartSignal
        /// </summary>
        /// <returns></returns>
        public override IContext Start()
        {
            base.Start();
            CodeReviewStartSignal codeReviewStartSignal = injectionBinder.GetInstance<CodeReviewStartSignal>();
            codeReviewStartSignal.Dispatch();
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
            injectionBinder.Bind<ICodeMedium>().To<CodeMedium>().ToSingleton();
            injectionBinder.Bind<IPicture>().To<Picture>().ToSingleton();
            injectionBinder.Bind<IPlayer>().To<Player>().ToSingleton();
            injectionBinder.Bind<IRoom>().To<Common.Room>().ToSingleton();
        }

        private void BindServices()
        {
            // TODO
        }

        private void BindMediators()
        {
            mediationBinder.Bind<CodeReviewScreenView>().To<CodeReviewScreenMediator>();
            mediationBinder.Bind<CodeReviewContextMenuView>().To<CodeReviewContextMenuMediator>();
            mediationBinder.Bind<CodeReviewControllerView>().To<CodeReviewControllerMediator>();
            mediationBinder.Bind<PileView>().To<PileMediator>();
            mediationBinder.Bind<InfoView>().To<InfoMediator>();
            mediationBinder.Bind<CodeReviewSceneMenuView>().To<CodeReviewSceneMenuMediator>();
            mediationBinder.Bind<SimpleCodeReviewScreenView>().To<SimpleCodeReviewScreenMediator>();
        }

        private void BindCommands()
        {
            if (this == firstContext)
            {
                commandBinder.Bind<CodeReviewStartSignal>().To<CodeReviewStartCommand>().Once();
            }
            else
            {
                commandBinder.Bind<CodeReviewStartSignal>()
                    .To<CodeReviewStartCommand>()
                    .Once()
                    .InSequence();
            }

            commandBinder.Bind<ShowSceneMenuSignal>().To<ShowCodeReviewSceneMenuCommand>();
            commandBinder.Bind<SelectCodeRatingSignal>().To<SelectCodeRatingCommand>();
            commandBinder.Bind<SaveCommentSignal>().To<SaveCommentCommand>();
            commandBinder.Bind<RemoveCodeSignal>().To<RemoveCodeCommand>();
            commandBinder.Bind<ShowCodeReviewContextMenuSignal>().To<ShowCodeReviewContextMenuCommand>();
            commandBinder.Bind<ExportToTxtSignal>().To<ExportToTxtCommand>();
            commandBinder.Bind<ShowAllCodeSignal>().To<ShowAllCodeCommand>();
            commandBinder.Bind<CodeSelectedSignal>().To<CodeSelectedCommand>();
            commandBinder.Bind<ShowKeyboardSignal>().To<ShowKeyboardCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
            injectionBinder.Bind<CodeReviewContextMenuIsShownSignal>().ToSingleton();
            injectionBinder.Bind<CodeRatingChangedSignal>().ToSingleton();
            injectionBinder.Bind<PileSelectedSignal>().ToSingleton();
            injectionBinder.Bind<CommentChangedSignal>().ToSingleton();
            injectionBinder.Bind<CodeReviewSceneMenuIsShownSignal>().ToSingleton();
            injectionBinder.Bind<NextCodeSignal>().ToSingleton();
            injectionBinder.Bind<ShowLaserSignal>().ToSingleton();
        }
    }
}