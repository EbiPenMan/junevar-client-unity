using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class AxieStats
    {
        [JsonProperty("health")] public int Health;
        [JsonProperty("speed")] public int Speed;
        [JsonProperty("skill")] public int Skill;
        [JsonProperty("morale")] public int Morale;
    }
}