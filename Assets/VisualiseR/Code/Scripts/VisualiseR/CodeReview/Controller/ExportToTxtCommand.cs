using System.IO;
using System.Text;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Command to export all ratings and comments from the code fragements to txt.
    /// </summary>
    public class ExportToTxtCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ExportToTxtCommand));

        private static readonly string FILE_NAME = "notes.txt";

        [Inject]
        public CodeMedium _medium { get; set; }

        [Inject]
        public ShowScreenMessageSignal ShowScreenMessageSignal { get; set; }

        [Inject]
        public CodeReviewSceneMenuIsShownSignal CodeReviewSceneMenuIsShownSignal { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void Execute()
        {
            string dirPath = GetDirPath();
            string text = GetText();
            FileUtil.WriteFile(dirPath, text);
            
            var logMsg = string.Format("Exported informations in file '{0}'", dirPath);
            Logger.InfoFormat(logMsg);
            ShowScreenMessageSignal.Dispatch(logMsg);
            HideSceneMenu();
        }

        private void HideSceneMenu()
        {
            var sceneMenu = _contextView.transform.Find("Menus").transform.Find("CodeReviewSceneMenuCanvas")
                .gameObject;
            sceneMenu.SetActive(false);
            CodeReviewSceneMenuIsShownSignal.Dispatch(false);
        }

        private string GetDirPath()
        {
            string dirPath = FileUtil.GetParentDirectory(_medium.GetCodeFragment(0).Pic.Path);
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
                sb.AppendLine(code.SaveToTxt());
            }
            return sb.ToString();
        }
    }
}