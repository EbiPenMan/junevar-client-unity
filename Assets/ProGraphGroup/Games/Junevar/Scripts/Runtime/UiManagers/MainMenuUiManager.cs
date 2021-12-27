using System;
using Cysharp.Threading.Tasks;
using Infinite8.Winigames.Packages.Utility;
using ProGraphGroup.Games.Junevar.Managers;
using ProGraphGroup.Games.Junevar.Server;
using RTLTMPro;
using UniRx;
using UnityEngine.UI;
using Task = System.Threading.Tasks.Task;

namespace ProGraphGroup.Games.Junevar.UiManagers
{
    public class MainMenuUiManager : MonoSingleton<MainMenuUiManager>
    {
        public RTLTextMeshPro txt_playerName;
        public RTLTextMeshPro txt_wallet_current_energy;
        public RTLTextMeshPro txt_wallet_max_energy;

        private async Task Awake()
        {
            ProfileManager.Instance.onSynceMyAccount.AddListener(updateMyAccountUi);
            AxieManager.Instance.onSynceMyAxies.AddListener(updateEnergyMaxUi);
        }

        private void updateMyAccountUi()
        {
            txt_playerName.text = ProfileManager.Instance.GetMyAccount().User.DisplayName;
            txt_wallet_current_energy.text = ProfileManager.Instance.GetMyWallet().Energy.ToString();
        }
        private void updateEnergyMaxUi()
        {
            int currentAxieCount = 12; // TODO get from server
            int maxEnergy = 0;

            if (currentAxieCount >= 3 && currentAxieCount <= 9)
            {
                maxEnergy = 20;
            }
            else if (currentAxieCount >= 10 && currentAxieCount <= 19  )
            {
                maxEnergy = 40;
            }
            else if (currentAxieCount >= 20  )
            {
                maxEnergy = 60;
            }
            
            txt_wallet_max_energy.text = maxEnergy.ToString();
        }
        
    }
}