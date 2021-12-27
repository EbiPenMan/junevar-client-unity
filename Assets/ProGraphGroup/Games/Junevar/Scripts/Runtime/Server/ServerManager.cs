using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infinite8.Winigames.Packages.Utility;
using Nakama;
using Newtonsoft.Json;
using ProGraphGroup.Games.Junevar.Server.Configs;
using ProGraphGroup.Games.Junevar.Server.Models;
using ProGraphGroup.Games.Junevar.Server.Models.Response;
using ProGraphGroup.General.Models;
using ProGraphGroup.General.Utility;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar.Server
{
    public class ServerManager : MonoSingleton<ServerManager>
    {
        public ServerClientConfigs serverClientConfigs;
        public ServerUiController uiController;
        public LoginController loginController;

        private Log _logger;

        [HideInInspector] public Client client;
        [HideInInspector] public string uniqueIdentifier;
        [HideInInspector] public ISession session;

        [HideInInspector] public ServerConfigsModel serverConfigs;
        [HideInInspector] public List<AxieClassConfigsModel> axieClassConfigs;
        [HideInInspector] public List<PartClassAdditionalStatsConfigsModel> partClassAdditionalStatsConfigs;


        private void Awake()
        {
            _logger = new Log("ServerManager");
        }

        public async UniTask<bool> Init()
        {
            _logger.Info("Start Init.");
            await CreateClient();
            await SetUserUniqueIdentifier();
            await CreateSession();
            await getInitData();
            return true;
        }

        private async UniTask<Client> CreateClient()
        {
            _logger.Info("Start.", tags: new[] {"CreateClient"});
            client = new Client(serverClientConfigs.scheme, serverClientConfigs.host, serverClientConfigs.port,
                serverClientConfigs.serverKey);
            _logger.Info("Client created.", tags: new[] {"CreateClient"});
            return client;
        }

        private async UniTask<string> SetUserUniqueIdentifier()
        {
            uniqueIdentifier = SystemInfo.deviceUniqueIdentifier + serverClientConfigs.deviceId;
            _logger.Info("User Unique Identifier set done.", "uniqueIdentifier : " + uniqueIdentifier,
                new[] {"SetUserUniqueIdentifier"});
            return uniqueIdentifier;
        }

        private async UniTask<ISession> CreateSession()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(serverClientConfigs.loginSessionTimeout)); // 5sec timeout.
            try
            {
                session = await client.AuthenticateDeviceAsync(uniqueIdentifier, null, true, null, null, cts);
                _logger.Info("Session created.", "Username : " + session.Username, new[] {"CreateSession"});
                return session;
            }
            catch (Exception e)
            {
                _logger.Error("Session not created.", "error : " + e.Message, new[] {"CreateSession"});
                return null;
            }
        }

        private async UniTask<bool> getInitData()
        {
            serverConfigs = await getServerCollection<ServerConfigsModel>("public_configuration", "server_configs");

            AxieClassConfigsResponse axieClassConfigsResponse =
                await getServerCollection<AxieClassConfigsResponse>("public_configuration", "axie_class_configs");
            axieClassConfigs = axieClassConfigsResponse.List;

            PartClassAdditionalStatsConfigsResponse partClassAdditionalStatsConfigsResponse =
                await getServerCollection<PartClassAdditionalStatsConfigsResponse>("public_configuration",
                    "part_class_additional_stats_configs");
            partClassAdditionalStatsConfigs = partClassAdditionalStatsConfigsResponse.List;

            return true;
        }


        public async UniTask<IApiAccount> GetMyAccount()
        {
            return await client.GetAccountAsync(session);
        }

        public async UniTask<IApiUsers> GetUsers(IEnumerable<string> ids)
        {
            return await client.GetUsersAsync(session, ids);
        }

        public async UniTask<IApiUser> GetUser(string id)
        {
            IApiUsers users = await GetUsers(new[] {id});
            if (users != null && users.Users != null && users.Users.Any())
                return users.Users.First();
            return null;
        }

        public async UniTask<BaseResponseVerifyModel> ChangeDisplayName(string newName)
        {
            var response = await client.RpcAsync(session, "rpcChangeOwnDisplayName", newName);
            return JsonConvert.DeserializeObject<BaseResponseVerifyModel>(response.Payload);
        }


        private async UniTask<T> getServerCollection<T>(string collection, string key, string userId = null,
            string version = null) where T : class
        {
            var storageObjectId = new StorageObjectId();
            storageObjectId.Collection = collection;
            storageObjectId.Key = key;
            if (userId != null)
            {
                storageObjectId.UserId = userId;
            }

            if (version != null)
            {
                storageObjectId.Version = version;
            }

            var result = await client.ReadStorageObjectsAsync(session,
                new IApiReadStorageObjectId[] {storageObjectId});

            return result.Objects.Any()
                ? JsonConvert.DeserializeObject<T>(result.Objects.First().Value)
                : null;
        }

        private async UniTask<List<T>> getServerCollections<T>(string collection, string key, string userId = null,
            string version = null) where T : class
        {
            var storageObjectId = new StorageObjectId();
            storageObjectId.Collection = collection;
            storageObjectId.Key = key;
            if (userId != null)
            {
                storageObjectId.UserId = userId;
            }

            if (version != null)
            {
                storageObjectId.Version = version;
            }

            var result = await client.ReadStorageObjectsAsync(session,
                new IApiReadStorageObjectId[] {storageObjectId});

            List<T> results = new List<T>();

            if (result.Objects.Any())
            {
                foreach (var apiStorageObject in result.Objects)
                {
                    results.Add(JsonConvert.DeserializeObject<T>(apiStorageObject.Value));
                }
            }

            return results;
        }
    }
}