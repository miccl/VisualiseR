using System;
using strange.extensions.command.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Showroom
{
    public class CaptureScreenshotCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CaptureScreenshotCommand));
        
        [Inject]
        public ShowScreenMessageSignal ShowScreenMessageSignal { get; set; }

        public override void Execute()
        {
            var name = ScreenShotName();
            Application.CaptureScreenshot(name);
            var logMsg = string.Format("Captured screenshot '{0}",
                name);
            Logger.DebugFormat(logMsg);
            ShowScreenMessageSignal.Dispatch(logMsg);

        }

        public static string ScreenShotName()
        {
            return string.Format("screen_{0}.png",
                DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }
    }
}