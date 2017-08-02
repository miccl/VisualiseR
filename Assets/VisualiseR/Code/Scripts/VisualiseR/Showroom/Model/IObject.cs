using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Represents a 3d-object in the scene.
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Type of the object.
        /// </summary>
        ObjectType Type { get; set; }
        /// <summary>
        /// Position of the object.
        /// </summary>
        Vector3 Position { get; set; }
        /// <summary>
        /// Color of the object.
        /// </summary>
        Color Color { get; set; }
        /// <summary>
        /// Rotation of the object.
        /// </summary>
        Quaternion Rotation { get; set; }
        
    }
}