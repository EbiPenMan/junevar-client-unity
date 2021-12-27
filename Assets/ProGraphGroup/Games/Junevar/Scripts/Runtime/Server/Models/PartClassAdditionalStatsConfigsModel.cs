using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class PartClassAdditionalStatsConfigsModel
    {
        [JsonProperty("type")]  public AxieClassType Type;
        [JsonProperty("stats")]  public AxieStats Stats;
    }
}