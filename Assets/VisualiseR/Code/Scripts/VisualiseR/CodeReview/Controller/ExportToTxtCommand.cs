using System.IO;
using System.Text;
using strange.extensions.command.impl;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class ExportToTxtCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ExportToTxtCommand));

        private static readonly string FILE_NAME = "notes.txt";

        [Inject]
        public CodeMedium _medium { get; set; }

        [Inject]
        public ShowMessageSignal ShowMessageSignal { get; set; }

        public override void Execute()
        {
            string dirPath = GetDirPath();
            string text = GetText();
            FileUtil.WriteFile(dirPath, text);
            Logger.InfoFormat("Exported informations in file '{0}'", dirPath);
            ShowMessageSignal.Dispatch(string.Format("Exported file to {0}", dirPath));
        }

        private string GetDirPath()
        {
            string dirPath = FileUtil.GetDirectoryPath(_medium.GetCodeFragment(0).Pic.Path);
            string parentDirPath = DirectoryUtil.GetParentDirectory(dirPath);
            string filePath = parentDirPath + Path.DirectorySeparatorChar + FILE_NAME;
            FileUtil.CreateFileIfNotExists(filePath);
            return filePath;
        }

        private string GetText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var code in _medium.CodeFragments)
            {
                sb.AppendLine(code.SaveCommentToTxt());
            }
            return sb.ToString();
        }
    }
}