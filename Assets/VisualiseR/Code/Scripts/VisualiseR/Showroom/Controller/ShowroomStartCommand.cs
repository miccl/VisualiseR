using strange.extensions.command.impl;
using UnityEngine.VR;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to initialises the scene.
    /// </summary>
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