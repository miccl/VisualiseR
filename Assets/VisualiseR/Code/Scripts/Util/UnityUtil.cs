﻿using System;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for useful methods for unity.
    /// </summary>
    public static class UnityUtil
    {

        /// <summary>
        /// Defensive <see cref="Component.GetComponent{T}"/>-Alternative.
        /// Checks if the the retrieved component is null.
        /// Prints
        ///
        /// </summary>
        /// <see href="http://www.gamasutra.com/blogs/HermanTulleken/20160812/279100/50_Tips_and_Best_Practices_for_Unity_2016_Edition.php"/>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetRequiredComponent<T>(this GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError("Expected to find component of type " + typeof(T) + " but found none", obj);
            }

            return component;
        }


        /// <summary>
        ///
        /// </summary>
        ///
        /// <see href="https://forum.unity3d.com/threads/alternative-way-to-use-getcomponent.344890/">Source</see>
        /// <param name="mono"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void With<T>(this MonoBehaviour mono, Action<T> action) where T : Component
        {
            var component = mono.GetComponent<T>();
            if (component)
            {
                action(component);
            }
            else
            {
                Debug.Log("Component " + typeof(T).ToString() + " not found on " + mono.name);
            }
        }
    }
}