using strange.extensions.command.impl;
using UnityEngine.VR;

namespace VisualiseR.Main
{
    /// <summary>
    /// Initialises the scene 'main'.
    /// </summary>
    public class MainStartCommand : Command
    {
        public override void Execute()
        {
            VRSettings.enabled = false;
        }
    }
}