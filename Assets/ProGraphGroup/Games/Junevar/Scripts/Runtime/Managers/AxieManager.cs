using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infinite8.Winigames.Packages.Utility;
using Nakama;
using Newtonsoft.Json;
using ProGraphGroup.Games.Junevar.Server;
using ProGraphGroup.Games.Junevar.Server.Models;
using ProGraphGroup.Games.Junevar.Server.Models.Response;
using ProGraphGroup.Games.Junevar.UiManagers;
using ProGraphGroup.General.Utility;
using UnityEngine.Events;

namespace ProGraphGroup.Games.Junevar.Managers
{
    public class AxieManager : MonoSingleton<AxieManager>
    {
        private Log _logger;

        public UnityEvent onSynceMyAxies;

        private void Awake()
        {
            _logger = new Log("ServerManager");
        }

        public async UniTask<bool> GetMyAxieAsync()
        {
            onSynceMyAxies?.Invoke();
            return transform;
        }
        
     
    }
}