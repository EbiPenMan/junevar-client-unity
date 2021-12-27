using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models.Response
{
    public class BaseResponseModel
    {
        [JsonProperty("result")] public object Result;
        [JsonProperty("error")] public BaseErrorModel Error;
    }
}