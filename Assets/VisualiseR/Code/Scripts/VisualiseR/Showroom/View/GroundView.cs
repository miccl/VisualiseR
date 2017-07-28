using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VisualiseR.Showroom
{
    public class GroundView : View
    {
        public Signal OnClickSignal = new Signal();
        
        public void OnClick(BaseEventData data)
        {
            OnClickSignal.Dispatch();
        }
        
//        private void AdjustPlayerPosition()
//        {
//            var pos = Camera.main.transform.forward * 5;
//            GameObject player = GameObject.FindGameObjectWithTag("Player");
//            
//            player.transform.position = player.transform.position +  new Vector3(pos.x, 0, pos.z);
//            
//        }


    }
}