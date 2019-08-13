using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace APILayer.Entities.UserService
{
    public class UserListResponse
    {
        [JsonProperty("users")]
        public ObservableCollection<User> Users { get; set; }
    }
}
