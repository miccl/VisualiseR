using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeReviewStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CodeReviewStartCommand));

        [Inject]
        public MediumChangedSignal MediumChangedSignal { get; set; }

        [Inject]
        public CodePositionChangedSignal CodePositionChangedSignal { get; set; }


        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }


        public override void Execute()
        {
            InitView();
        }


        private void InitView()
        {
            InitStand();
        }

        private void InitStand()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("CodeReview_Screen") as GameObject);
            go.name = "Screen";
            go.transform.parent = contextView.transform;
        }
    }
}