using Newtonsoft.Json;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    public class BaseErrorModel
    {
        [JsonProperty("code")]  public int Code;
        [JsonProperty("message")] public string Message;

        public BaseErrorModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}