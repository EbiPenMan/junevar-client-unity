using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class UserWallet
    {
        [JsonProperty("energy")] public int Energy;
        [JsonProperty("axs")] public int Axs;
        [JsonProperty("slp")] public int Slp;

        public UserWallet()
        {
            Energy = 0;
            Axs = 0;
            Slp = 0;
        }

        public UserWallet(int energy, int axs, int slp)
        {
            Energy = energy;
            Axs = axs;
            Slp = slp;
        }
    }
}