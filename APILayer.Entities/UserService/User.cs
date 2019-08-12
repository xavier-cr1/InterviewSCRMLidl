using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APILayer.Entities.UserService
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
