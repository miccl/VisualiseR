using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.VR;

namespace VisualiseR.CodeReview
{
    public class CodeReviewStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CodeReviewStartCommand));

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }


        public override void Execute()
        {
            Logger.Info("Starting code review scene");
            VRSettings.enabled = true;
        }
    }
}