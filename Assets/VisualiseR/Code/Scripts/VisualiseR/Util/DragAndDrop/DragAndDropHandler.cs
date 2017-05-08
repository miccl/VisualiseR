using UnityEngine.EventSystems;

namespace VisualiseR.Util
{
    public interface DragDropHandler : IEventSystemHandler
    {
        void HandleGazeTriggerStart();
        void HandleGazeTriggerEnd();
    }
}