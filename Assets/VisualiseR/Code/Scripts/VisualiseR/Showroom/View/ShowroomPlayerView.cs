using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// View of the player.
    /// Shows the laser.
    /// </summary>
    public class ShowroomPlayerView : View
    {

        private JCsLogger Logger;

        private Player _player;
        private Transform _playerGlobal;

        internal GameObject _contextView;

        internal Signal CancelButtonPressedSignal = new Signal();
        
        internal bool _isSceneMenuShown = false;



        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(ShowroomPlayerView));
        }

        public void Init(Player player)
        {
            _player = player;
        }

        private void Update()
        {
//            if (_player == null || !_player.IsHost())
//            {
//                return;
//            }

            if (ButtonUtil.IsSubmitButtonPressed())
            {
                //TODO
                return;
            }

            if (ButtonUtil.IsCancelButtonPressed())
            {
                CancelButtonPressedSignal.Dispatch();
            }

        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            {
                if (stream.isWriting)
                {
                }
            }
        }
    }
}