using strange.extensions.command.impl;
using UnityEngine.VR;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    /// <summary>
    /// Command to initialise the presentation scene.
    /// </summary>
    public class PresentationStartCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(PresentationStartCommand));

        [Inject]
        public CreateOrJoinSignal CreateOrJoinSignal { get; set; }

        public override void Execute()
        {
            Logger.Info("Starting presentation scene");
            VRSettings.enabled = true;
            CreateOrJoinRoom();
        }

        private void CreateOrJoinRoom()
        {
            object o = PlayerPrefsUtil.RetrieveObject(PlayerPrefsUtil.ROOM_KEY);
            if (o != null)
            {
                Common.Room room = (Common.Room) o;
                CreateOrJoinSignal.Dispatch(room.Name);
            }
        }
    }
}