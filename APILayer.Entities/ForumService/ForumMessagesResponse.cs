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
        public ObservableCollection<AutomationMessage> AutomationMessage { get; set; }

        [JsonProperty("Security")]
        public ObservableCollection<DevelopmentMessage> SecurityMessage { get; set; }

        [JsonProperty("Development")]
        public ObservableCollection<SecurityMessage> DevelopmentMessage { get; set; }

        [JsonProperty("Testing")]
        public ObservableCollection<TestingMessage> TestingMessage { get; set; }
    }
}
