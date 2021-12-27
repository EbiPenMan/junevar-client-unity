using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models.Response
{
    public class AxieClassConfigsResponse
    {
        [JsonProperty("list")]  public List<AxieClassConfigsModel> List;
    }
}