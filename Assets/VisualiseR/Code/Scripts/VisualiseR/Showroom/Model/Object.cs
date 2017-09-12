using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Represents a 3d object in the scene.
    /// </summary>
    public class Object : IObject
    {
        /// <summary>
        /// Type of the object.
        /// </summary>
        public ObjectType Type { get; set; }
        /// <summary>
        /// Current position of the object.
        /// </summary>
        public Vector3 Position { get; set; }
        /// <summary>
        /// Color of the object.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Rotation of the object.
        /// </summary>
        public Quaternion Rotation { get; set; }
    }
}