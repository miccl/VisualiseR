using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// View for the ground.
    /// </summary>
    public class GroundView : View
    {
        public Signal OnClickSignal = new Signal();

        public void OnClick(BaseEventData data)
        {
            OnClickSignal.Dispatch();
        }
    }
}