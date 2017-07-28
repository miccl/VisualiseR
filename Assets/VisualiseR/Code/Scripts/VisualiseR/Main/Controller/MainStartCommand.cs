using strange.extensions.command.impl;
using UnityEngine.VR;

namespace VisualiseR.Main
{
    /// <summary>
    /// Command to initialise the main scene.
    /// </summary>
    public class MainStartCommand : Command
    {
        public override void Execute()
        {
            VRSettings.enabled = false;
        }
    }
}