using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class StartCommand : Command
    {
        [Inject]
        public LoadDiskDataSignal LoadDiskDataSignal { get; set; }

        public override void Execute()
        {
            SetupPath();
        }

        private void SetupPath()
        {
            string path = "D:/VisualiseR_Test/FullDirectory";

            LoadDiskDataSignal.Dispatch(path);
        }
    }
}