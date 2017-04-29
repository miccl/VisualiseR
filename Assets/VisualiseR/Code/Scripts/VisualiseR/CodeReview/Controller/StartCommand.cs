using log4net.Config;
using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class StartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(StartCommand));

        [Inject]
        public LoadAndConvertFilesSignal LoadAndConvertFilesSignal { get; set; }

        public override void Execute()
        {
            XmlConfigurator.Configure();
            SetupPath();
        }

        private void SetupPath()
        {
            string path = "D:/VisualiseR_Test/FullDirectory";

            LoadAndConvertFilesSignal.Dispatch(path);
        }
    }
}