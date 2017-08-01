using UnityEngine;

namespace VisualiseR.Showroom
{
    public interface IObject
    {
        ObjectType Type { get; set; }
        Vector3 Position { get; set; }
        Color Color { get; set; }
        Quaternion Rotation { get; set; }
        
    }
}