using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infinite8.Winigames.Packages.Utility;
using Nakama.TinyJson;
using Newtonsoft.Json;
using ProGraphGroup.Games.Junevar.Server;
using ProGraphGroup.Games.Junevar.Server.Models;
using ProGraphGroup.Games.Junevar.Server.Models.Response;
using ProGraphGroup.General.Utility;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar
{
    public class ShopManager : MonoSingleton<ShopManager>
    {
        private Log _logger;
        private void Awake()
        {
            _logger = new Log("ShopManager");
        }
        void Start()
        {
        }

        public async void GetShop(int pageNumber)
        {
            SelectQueryInputModel inputModel = new SelectQueryInputModel();
            inputModel.Limit = 10;
            inputModel.Offset = pageNumber;

            GetShopResponse res = await ServerManager.Instance.GetShopItems(inputModel);
            _logger.Info(res.Result.List[0].DisplayName);
            GetShopResponse res2 = await ServerManager.Instance.GetMyAxie();
            _logger.Info(res2.Result.List[0].DisplayName);

        }
    }
}