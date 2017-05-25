using strange.extensions.command.impl;
using UnityEngine.VR;

namespace VisualiseR.Main
{
    public class MainStartCommand : Command
    {
        public override void Execute()
        {
            VRSettings.enabled = false;
        }
    }
}