﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace VisualiseR.Util
{
    public static class PlayerPrefsUtil
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(PlayerPrefsUtil));


        public static readonly string AVATAR_KEY = "Avatar_Key";
        public static readonly string ROOM_KEY = "Room_Key";
        public static readonly string PLAYER_NAME_KEY = "Player_Name_Key";
        public static readonly string MAIN_DIR = "Main_Dir";
        public static readonly string AUDIO_MUTED = "Audio_Muted";


        public static void saveObject(string key, object o)
        {
            var m = new MemoryStream();
            var b = new BinaryFormatter();
            b.Serialize(m, o);
            SaveValue(key, Convert.ToBase64String(m.GetBuffer()));
        }

        public static object RetrieveObject(string key)
        {
            var m = new MemoryStream(Convert.FromBase64String(RetrieveValue(key)));
            var b = new BinaryFormatter();
            return b.Deserialize(m);
        }

        public static void SaveValue(string key, string value)
        {
            Logger.InfoFormat("Saved value '{0}' under key '{1}' in player prefs", value, key);
            PlayerPrefs.SetString(key, value);
        }

        public static void SaveValue(string key, int value)
        {
            Logger.InfoFormat("Saved value '{0}' under key '{1}' in player prefs", value, key);
            PlayerPrefs.SetInt(key, value);
        }
        

        public static string RetrieveValue(string key)
        {
            var value = PlayerPrefs.GetString(key);
            Logger.InfoFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }

        public static string RetrieveValue(string key, string defaultValue)
        {
            var value = PlayerPrefs.GetString(key, defaultValue);
            Logger.InfoFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }

        public static int RetrieveValue(string key, int defaultValue)
        {
            var value = PlayerPrefs.GetInt(key, defaultValue);
            Logger.InfoFormat("Retrieved value '{0}' under key '{1}' from player prefs", value, key);
            return value;
        }



    }
}