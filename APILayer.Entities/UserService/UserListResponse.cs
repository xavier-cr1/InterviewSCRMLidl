using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace APILayer.Entities.UserService
{
    public class UserListResponse
    {
        [JsonProperty("users")]
        public ObservableCollection<User> Users { get; set; }
    }
}
