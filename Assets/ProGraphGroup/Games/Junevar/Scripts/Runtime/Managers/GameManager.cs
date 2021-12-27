using System;
using Infinite8.Winigames.Packages.Utility;
using Nakama.TinyJson;
using ProGraphGroup.Games.Junevar.Server;
using ProGraphGroup.General.Utility;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private Log _logger;

        private void Awake()
        {
            _logger = new Log("ServerManager");
        }

        private async void Start()
        {
            await ServerManager.Instance.Init();
            var aa = await ProfileManager.Instance.GetMyAccountAsync();
            _logger.Info("User Id: " , aa.User.Id);            
            _logger.Info("Username: " , aa.User.Username);            
            var bb = await AxieManager.Instance.GetMyAxieAsync();
        }
    }
}