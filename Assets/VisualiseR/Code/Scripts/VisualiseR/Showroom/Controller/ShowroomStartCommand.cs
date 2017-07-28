using strange.extensions.command.impl;
using UnityEngine.VR;

namespace VisualiseR.Showroom
{
    public class ShowroomStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowroomStartCommand));
        
        public override void Execute()
        {
            Logger.Info("Starting showroom scene");
            VRSettings.enabled = true;
        }
    }
}