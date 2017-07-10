using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    /// Helper class for player pref.
    /// </summary>
    public static class PlayerPrefsUtil
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(PlayerPrefsUtil));

        /// <summary>
        /// PlayerPrefs key for avatar.
        /// </summary>
        public static readonly string AVATAR_KEY = "Avatar_Key";
        /// <summary>
        /// PlayerPrefs key for room.
        /// </summary>
        public static readonly string ROOM_KEY = "Room_Key";
        /// <summary>
        /// PlayerPrefs key for player name.
        /// </summary>
        public static readonly string PLAYER_NAME_KEY = "Player_Name_Key";
        /// <summary>
        /// PlayerPrefs key for main directory.
        /// </summary>
        public static readonly string MAIN_DIR = "Main_Dir";
        /// <summary>
        /// PlayerPrefs key for audio mute.
        /// </summary>
        public static readonly string AUDIO_MUTED = "Audio_Muted";


        /// <summary>
        /// Saves the object in <see cref="PlayerPrefs"/> under given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="o"></param>
        public static void saveObject(string key, object o)
        {
            var m = new MemoryStream();
            var b = new BinaryFormatter();
            b.Serialize(m, o);
            SaveValue(key, Convert.ToBase64String(m.GetBuffer()));
        }

        /// <summary>
        /// Retrevies an object from <see cref="PlayerPrefs"/> with given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object RetrieveObject(string key)
        {
            var m = new MemoryStream(Convert.FromBase64String(RetrieveValue(key)));
            var b = new BinaryFormatter();
            return b.Deserialize(m);
        }

        /// <summary>
        /// Saves  value in <see cref="PlayerPrefs"/> identified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveValue(string key, string value)
        {
            Logger.DebugFormat("Saved value '{0}' under key '{1}' in player prefs", value, key);
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        /// Saves value in <see cref="PlayerPrefs"/> identified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveValue(string key, int value)
        {
            Logger.DebugFormat("Saved value '{0}' under key '{1}' in player prefs", value, key);
            PlayerPrefs.SetInt(key, value);
        }
        
        /// <summary>
        /// Retrieve value in <see cref="PlayerPrefs"/> identified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static string RetrieveValue(string key)
        {
            var value = PlayerPrefs.GetString(key);
            Logger.DebugFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }
        
        /// <summary>
        /// Retrieve value in <see cref="PlayerPrefs"/> identified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static string RetrieveValue(string key, string defaultValue)
        {
            var value = PlayerPrefs.GetString(key, defaultValue);
            Logger.DebugFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }
        
        /// <summary>
        /// Retrieve value in <see cref="PlayerPrefs"/> identified by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static int RetrieveValue(string key, int defaultValue)
        {
            var value = PlayerPrefs.GetInt(key, defaultValue);
            Logger.DebugFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }



    }
}