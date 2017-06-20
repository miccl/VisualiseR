using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.VR;

namespace VisualiseR.CodeReview
{
    /// <summary>
    ///  Command to initialises the scene.
    /// </summary>
    public class CodeReviewStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CodeReviewStartCommand));

        public override void Execute()
        {
            Logger.Info("Starting code review scene");
            VRSettings.enabled = true;
        }
    }
}