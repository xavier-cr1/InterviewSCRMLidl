using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace APILayer.Entities.ForumService
{
    public class ForumMessagesResponse
    {
        [JsonProperty("Automation")]
        public ObservableCollection<ForumMessage> AutomationMessage { get; set; }

        [JsonProperty("Security")]
        public ObservableCollection<ForumMessage> SecurityMessage { get; set; }

        [JsonProperty("Development")]
        public ObservableCollection<ForumMessage> DevelopmentMessage { get; set; }

        [JsonProperty("Testing")]
        public ObservableCollection<ForumMessage> TestingMessage { get; set; }
    }
}
