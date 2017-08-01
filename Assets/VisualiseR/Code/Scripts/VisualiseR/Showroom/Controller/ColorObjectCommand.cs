using System;
using strange.extensions.command.impl;
using UnityEngine;
using VisualiseR.Util;

namespace VisualiseR.Showroom
{
    /// <summary>
    /// Command to dye a object in an other color.
    /// </summary>
    public class ColorObjectCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(RotateObjectCommand));

        [Inject]
        public Object _object { get; set; }
        
        [Inject]
        public GameObject _gameObject { get; set; }

        [Inject]
        public float _colorValue { get; set; }


        public override void Execute()
        {
            NormalizeNumber();
            ColorObject();
        }

        /// <summary>
        /// Returns the values after the decimal point.
        /// E.g. 1.2 = 0.2, 66.7 = 0.7.
        /// </summary>
        private void NormalizeNumber()
        {
            _colorValue = (float) (_colorValue - Math.Truncate(_colorValue));
        }

        private void ColorObject()
        {
            var color = HSBColor.ToColor(new HSBColor(_colorValue, 1, 1));
            _gameObject.GetComponent<Renderer>().material.color = color;
            _object.Color = color;
            Logger.DebugFormat("Changing color of object '{0}' to {1}", _gameObject, color);
        }
    }
}