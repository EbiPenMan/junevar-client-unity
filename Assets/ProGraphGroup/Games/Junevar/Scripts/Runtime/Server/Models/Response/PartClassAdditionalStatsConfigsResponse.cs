using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models.Response
{
    public class PartClassAdditionalStatsConfigsResponse
    {
        [JsonProperty("list")] public List<PartClassAdditionalStatsConfigsModel> List;
    }
}