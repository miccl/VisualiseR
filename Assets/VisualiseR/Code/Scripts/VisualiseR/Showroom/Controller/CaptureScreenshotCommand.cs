using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace VisualiseR.Main
{
    public class CaptureScreenshotCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CaptureScreenshotCommand));
        
        public override void Execute()
        {
            var name = ScreenShotName();
            Application.CaptureScreenshot(name);
            Logger.DebugFormat("Captured screenshot '{0}",
                name);
        }

        public static string ScreenShotName()
        {
            return string.Format("screen_{0}.png",
                DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }
    }
}