using Newtonsoft.Json;

namespace APILayer.Entities.MessageService
{
    public class PrivateMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
