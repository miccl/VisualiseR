using UnityEngine;

namespace VisualiseR.Showroom
{
    public class Object : IObject
    {
        public ObjectType Type { get; set; }
        public Vector3 Position { get; set; }
        public Color Color { get; set; }
        public Quaternion Rotation { get; set; }
    }
}