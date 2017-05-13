using strange.extensions.command.impl;
using UnityEngine.VR;
using VisualiseR.Common;

namespace VisualiseR.Main
{
	public class MainStartCommand : Command
	{

	    [Inject] public ErrorSignal errorSignal { get; set; }
        public override void Execute()
        {
            VRSettings.enabled = false;
        }
    }
}