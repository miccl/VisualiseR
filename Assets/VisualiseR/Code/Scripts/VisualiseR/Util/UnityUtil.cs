using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for useful methods for unity.
    /// </summary>
    public static class UnityUtil
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(UnityUtil));

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
                Logger.ErrorFormat("Expected to find component of type {0} but found none: {1}", typeof(T), obj);
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
                Logger.Error("Component " + typeof(T).ToString() + " not found on " + mono.name);
            }
        }

        /// <summary>
        /// Defensive <see cref="GameObject.Find"/> alternative.
        /// Checks if the retrieved game object is null.
        /// </summary>
        /// <param name="gameObjectName"></param>
        public static GameObject FindGameObject(string gameObjectName)
        {
            GameObject go = GameObject.Find(gameObjectName);
            if (go == null)
            {
                Logger.ErrorFormat("Cannot find '{0}' script", gameObjectName);
            }
            return go;
        }

        //TODO vielleicht kann man hier was sichereres machen
        public static GameObject FindGameObjectInChild(string gameObjectName)
        {
            GameObject go = GameObject.Find(gameObjectName);
            if (go == null)
            {
                Logger.ErrorFormat("Cannot find '{0}' script", gameObjectName);
            }
            return go;
        }

        public static void LoadScene(string sceneName)
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                Logger.ErrorFormat("Scene '{0}' could not be loaded", sceneName, e);
            }
        }

        /// <summary>
        /// Sets up a event trigger programmatically.
        /// </summary>
        /// <see href="http://answers.unity3d.com/questions/854251/how-do-you-add-an-ui-eventtrigger-by-script.html">Source</see>
        /// <param name="trigger"></param>
        /// <param name="eventType"></param>
        /// <param name="callback"></param>
        public static void AddEventTriggerListener(EventTrigger trigger,
            EventTriggerType eventType,
            Action<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(new UnityAction<BaseEventData>(callback));
            trigger.triggers.Add(entry);
        }
    }
}