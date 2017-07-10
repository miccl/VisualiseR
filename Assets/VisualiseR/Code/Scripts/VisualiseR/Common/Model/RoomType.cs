using System;

namespace VisualiseR.Common
{
    /// <summary>
    /// Types of rooms.
    /// </summary>
    [Serializable]
    public enum RoomType
    {
        CodeReview,
        Presentation,
        LiveCoding,
        ArchitecturalReview,
        Showroom
    }
}