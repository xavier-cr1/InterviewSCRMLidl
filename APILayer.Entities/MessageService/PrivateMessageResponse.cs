using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace APILayer.Entities.MessageService
{
    public class PrivateMessageResponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("messages")]
        public ObservableCollection<PrivateMessage> Messages { get; set; }
    }
}
