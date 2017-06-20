using UnityEngine.EventSystems;

namespace VisualiseR.Util
{
    /// <summary>
    /// Handler for drag and drop.
    /// </summary>
    public interface DragDropHandler : IEventSystemHandler
    {
        void HandleGazeTriggerStart();
        void HandleGazeTriggerEnd();
    }
}