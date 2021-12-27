using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace ProGraphGroup.General.Controllers
{
    public class PGC_Update : MonoBehaviour
    {

        public List<ConfigDownloadPlatformClass> ConfigDownloadPlatforms = new List<ConfigDownloadPlatformClass>();

        public List<string> ConfigSelfHostUrls = new List<string>();
        public string ConfigGooglePlayUrl;
        public string ConfigCafeBazaarUrl;

        private Action<bool> onDone;

        private int currentConfigUrlIndex = 0;

        private void Start()
        {
            Init((res) =>
            {
                Debug.Log("OnDone");
            });
        }
        
        public void Init(Action<bool> onDone)
        {
            this.onDone = onDone;
        }

        private async UniTask GetConfigs()
        {
            try
            {
                // string txt = (await UnityWebRequest.Get(ConfigUrls[currentConfigUrlIndex]).SendWebRequest()).downloadHandler.text;
                
                onDone(true);
            }
            catch (Exception ex) when (!(ex is OperationCanceledException)) // when (ex is not OperationCanceledException) at C# 9.0
            {
                // if (currentConfigUrlIndex == ConfigUrls.Count - 1)
                // {
                //     onDone(false);
                // }
                // else
                // {
                //     currentConfigUrlIndex++;
                //     GetConfigs();
                // }
            }
        }
        
        
        [Serializable]
        public class ConfigSelfHostClass
        {

            public ServerStatusClass StatusClass;
            public List<ClientVersionsClass> ClientVersions;

            
            [Serializable]
            public class ServerStatusClass
            {
                [JsonProperty("isUpdating")]
                public bool IsUpdating;
                [JsonProperty("message")]
                public string Message;
                [JsonProperty("time")]
                public double Time;
            }
            
            [Serializable]
            public class ClientVersionsClass
            {
                [JsonProperty("version")]
                public float Version;
                [JsonProperty("updateDescription")]
                public string UpdateDescription;
                [JsonProperty("DownloadLinks")]
                public List<DownloadLinkClass> DownloadLinks;
            }
            [Serializable]
            public class DownloadLinkClass
            {
                [JsonProperty("Url")]
                public string url;
                [JsonProperty("imageUrl")]
                public string ImageUrl;
                [JsonProperty("plafromName")]
                public string PlafromName;
            }
            
        }

    
        [Serializable]
        public class ConfigDownloadPlatformClass
        {
            public ConfigDownloadPlatformTypeEn ConfigDownloadPlatformType;
            public bool IsActive;
        }
        
        [Serializable]
        public enum ConfigDownloadPlatformTypeEn
        {
            SelfHost,
            GooglePlay,
            CafeBazaar
        }
  
    }
    
    
    
    
}
