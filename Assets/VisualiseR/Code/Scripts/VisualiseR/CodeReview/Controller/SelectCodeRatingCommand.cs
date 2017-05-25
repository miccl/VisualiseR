using System;
using strange.extensions.command.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.CodeReview
{
    public class SelectCodeRatingCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(SelectCodeRatingCommand));

        [Inject]
        public Code _code { get; set; }

        [Inject]
        public Rate _rate { get; set; }

        [Inject]
        public CodeRatingChangedSignal CodeRatingChangedSignal { get; set; }

        public override void Execute()
        {
            Rate prevRate = _code.Rate;
            if (!prevRate.Equals(_rate))
            {
                _code.Rate = _rate;
                Logger.InfoFormat("Code {0} was rated with {1} (previous {2})", _code, _code.Rate, prevRate);
                MoveFileToRate(_code);

                CodeRatingChangedSignal.Dispatch(_code);
            }
        }

        private void MoveFileToRate(Code code)
        {
            string mainDir = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.MAIN_DIR);
            if (!String.IsNullOrEmpty(mainDir))
            {
                var rateDirInfo = DirectoryUtil.GetRatingDirectory(mainDir, code.Rate);
                if (rateDirInfo != null && rateDirInfo.Exists)
                {
                    string destFilePath = FileUtil.MoveFile(code.Pic.Path, rateDirInfo.FullName);
                    if (destFilePath != null)
                    {
                        code.Pic.Path = destFilePath;
                    }
                }
            }
            else
            {
                Logger.ErrorFormat("Main dir not set");
            }
        }
    }
}