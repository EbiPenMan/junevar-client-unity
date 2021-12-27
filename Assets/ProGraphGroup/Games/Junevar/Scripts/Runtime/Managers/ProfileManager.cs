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
    public class ProfileManager : MonoSingleton<ProfileManager>
    {
        private Log _logger;
        private Dictionary<string, IApiUser> _users = new Dictionary<string, IApiUser>();
        private IApiAccount _myAccount;

        public UnityEvent onSynceMyAccount;

        private void Awake()
        {
            _logger = new Log("ServerManager");
        }


        public IApiAccount GetMyAccount()
        {
            return _myAccount;
        }

        public async UniTask<IApiAccount> GetMyAccountAsync()
        {
            _myAccount = await ServerManager.Instance.GetMyAccount();
            _users.Add(_myAccount.User.Id, _myAccount.User);
            onSynceMyAccount?.Invoke();
            return _myAccount;
        }

        public IApiUser GetUser(string id)
        {
            return _users[id];
        }

        public async UniTask<IApiUser> GetUserAsync(string id, bool isForceUpdate = false)
        {
            var user = _users[id];
            if (!isForceUpdate && user != null) return user;
            user = await ServerManager.Instance.GetUser(id);
            if (user == null) return null;
            _users.Add(user.Id, user);
            return user;
        }

        public async UniTask<bool> ChangeMyName(string newName)
        {
            BaseResponseVerifyModel res = await ServerManager.Instance.ChangeDisplayName(newName);
            await GetMyAccountAsync();
            return res.Result.Status;
        }

        public UserWallet GetMyWallet()
        {
            return JsonConvert.DeserializeObject<UserWallet>(_myAccount.Wallet);
        }
        public async UniTask<UserWallet> GetMyWalletAsync()
        {
            await GetMyAccountAsync();
            return JsonConvert.DeserializeObject<UserWallet>(_myAccount.Wallet);
        }
    }
}