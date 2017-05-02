using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class StartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(StartCommand));

        [Inject]
        public LoadAndConvertFilesSignal _loadAndConvertFilesSignal { get; set; }

        [Inject]
        public SelectFileSignal _selectFileSignal { get; set; }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }


        public override void Execute()
        {
            InitView();
            SetupPath();
            InitFileBrowser();
        }

        private void InitFileBrowser()
        {
            _selectFileSignal.Dispatch();
        }

        private void InitView()
        {
            InitStand();
        }

        private void InitStand()
        {
            GameObject go = GameObject.Instantiate(Resources.Load("Common_Screen") as GameObject);
            go.name = "Screen";
            go.transform.parent = contextView.transform;
        }

        private void SetupPath()
        {
//            string path = "D:/VisualiseR_Test/FullDirectory";
//            string path = "/storage/emulated/0/Pictures/aviary-sample";
//            var url = "https://www.planwallpaper.com/static/images/desktop-year-of-the-tiger-images-wallpaper.jpg";
//            _loadAndConvertFilesSignal.Dispatch(url);
        }
    }
}