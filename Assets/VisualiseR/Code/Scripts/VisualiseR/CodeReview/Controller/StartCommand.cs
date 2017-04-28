using strange.extensions.command.impl;
using VisualiseR.Code.Scripts.VisualiseR.Common.Controller;

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
            //            string path = "D:/VisualiseR_Test/EmptyDirectory";
            //            string path = "D:/VisualiseR_Test/imgres.jpg";

            LoadDiskDataSignal.Dispatch(path);
        }
    }
}