using System;
using Infinite8.Winigames.Packages.Utility;
using UnityEngine;

namespace ProGraphGroup.General.Manager
{
    public class LocalStorageManager : Singleton<LocalStorageManager>
    {
        public void SaveItem(string key, string value, bool saveImmediate)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string GetItem(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public bool SaveClass<T>(string key ,T obj)
        {
            return false;
        }
    }
}