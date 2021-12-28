using System;
using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class Axie
    {
        [JsonProperty("id")] public string Id;
        [JsonProperty("display_name")] public string DisplayName;
        [JsonProperty("class")] public AxieClassType Class;
        [JsonProperty("gene")] public string Gene;
        [JsonProperty("create_time")] public DateTime CreateTime;
        [JsonProperty("update_time")] public DateTime UpdateTime;

        public Axie()
        {
        }

        public Axie(string id, string displayName, AxieClassType @class, string gene, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            DisplayName = displayName;
            Class = @class;
            Gene = gene;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}