using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APILayer.Entities.ForumService
{
    public class ForumMessage
    {
        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
