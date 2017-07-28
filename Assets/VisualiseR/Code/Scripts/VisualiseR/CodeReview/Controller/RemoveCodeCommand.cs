using strange.extensions.command.impl;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Command to remove code from the disk.
    /// </summary>
    public class RemoveCodeCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(RemoveCodeCommand));


        [Inject]
        public Code code { get; set; }

        public override void Execute()
        {
            FileUtil.DeleleFile(code.Pic.Path);
            Logger.InfoFormat("Code '{0}' was removed", code);
        }
    }
}