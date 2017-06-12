using strange.extensions.mediation.impl;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.Presentation
{
    public class PlayerView : View
    {
        private static JCsLogger Logger;
        
        private GameObject _reticlePointer;
        private GameObject _laser;
        private bool _isLaserShown;
        private bool _firstFalse;
        private Player _player;
        
        private GameObject _globalReticlePointer;
        private GameObject _globalLaser;
        private GameObject _globalElements;
        public GameObject _contextView;

        protected override void Awake()
        {
            base.Awake();
            Logger = new JCsLogger(typeof(PlayerView));
        }
        
        public void Init(Player player)
        {
            _player = player;
            InitView();
        }

        private void InitView()
        {
            _laser = transform.Find("GvrLaser").gameObject;
            _reticlePointer = _laser.transform.Find("GvrReticlePointer").gameObject;
            _globalElements = _contextView.transform.Find("GlobalElements").gameObject;
            _globalReticlePointer = _globalElements.transform.Find("GvrReticlePointer").gameObject;

            if (_player.IsHost())
            {
                _laser.SetActive(true);
                ShowLaser(false);
            }
            else
            {
                _laser.SetActive(false);
            }
        }

        internal void ShowLaser(bool show)
        {
            if (show == _isLaserShown)
            {
                return;
            }
            
            _isLaserShown = show;
            if (_player.IsHost())
            {
                _laser.GetComponent<LineRenderer>().enabled = show;   
                _reticlePointer.GetComponent<MeshRenderer>().material.color = _isLaserShown ? Color.red : Color.white;
            }
            else
            {
                _globalElements.SetActive(_isLaserShown);
            }

            Logger.InfoFormat("Logger is {0}", _isLaserShown ? "shown" : "hidden");
        }
        
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                if (_isLaserShown)
                {
                    _firstFalse = true;
                    
                    stream.SendNext(true);
                    stream.SendNext(_reticlePointer.transform.position);
                }
                else if (_firstFalse)
                {
                    stream.SendNext(false);
                    _firstFalse = false;
                }
            }
            else
            {
                var isLaserShown = (bool) stream.ReceiveNext();
                ShowLaser(isLaserShown);
                if (isLaserShown)
                {
                    _globalReticlePointer.transform.position = (Vector3) stream.ReceiveNext();
                }
            }

        }

    }
}