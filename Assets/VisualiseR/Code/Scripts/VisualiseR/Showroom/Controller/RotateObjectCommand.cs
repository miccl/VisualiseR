using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to rotate a object.
    /// </summary>
    public class RotateObjectCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(RotateObjectCommand));
        
        [Inject]
        public GameObject _gameObject { get; set; }
        
        [Inject]
        public int _rotationValue { get; set; }
       
        
        public override void Execute()
        {
            _rotationValue = _rotationValue % (8+1);
            RotateObject();
        }

        private void RotateObject()
        {
            string binaryValue = Convert.ToString(_rotationValue, 2);
            int rotationX = Int32.Parse(binaryValue[binaryValue.Length - 1].ToString());
            int rotationY = binaryValue.Length >= 2 ? Int32.Parse(binaryValue[binaryValue.Length - 2].ToString()) : 0;
            int rotationZ = binaryValue.Length == 3 ? Int32.Parse(binaryValue[0].ToString()) : 0;

            Logger.DebugFormat("213 Rotating object (x: {0}, y: {1}, z: {2})", rotationX * 90, rotationY * 90,
                rotationZ * 90);
            _gameObject.transform.rotation = Quaternion.Euler(rotationX * 90, rotationY * 90, rotationZ * 90);
        }
    }
}