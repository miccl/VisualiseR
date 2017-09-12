using strange.extensions.command.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to teleport a player to an other position.
    /// The position ist based on the players current position and his view angle (<see cref="Camera.main"/>)
    /// </summary>
    public class TeleportPlayerCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(ShowShowroomSceneMenuCommand));
                
        public override void Execute()
        {
            var tpPosition = AdjustPlayerPosition();
            Logger.DebugFormat("Teleporting player to pos {0}", tpPosition);
        }

        private Vector3 AdjustPlayerPosition()
        {
            Vector3 tpPosition = Camera.main.transform.forward * 5;
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = player.transform.position +  new Vector3(tpPosition.x, 0, tpPosition.z);

            return player.transform.position;
        }
    }
}