using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace VisualiseR.Presentation
{
    public class ShowTimeCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowTimeCommand));

        [Inject]
        public bool _show { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            ShowTime();
        }

        private void ShowTime()
        {
            var timerView = _contextView.transform.Find("TimerCanvas").gameObject;
            timerView.SetActive(_show);
            Logger.InfoFormat("Time Canvas shown: {0}", _show);
        }
    }
}