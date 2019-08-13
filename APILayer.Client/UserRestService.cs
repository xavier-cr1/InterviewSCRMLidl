using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.Commom;
using APILayer.Entities.UserService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace APILayer.Client
{
    public class UserRestService : RestApiBase, IUsersServiceRestApi
    {
        private string usersServiceUrl => this.ConfigurationRoot.GetSection("AppConfiguration")["UsersAPIService"];

        public UserRestService(IConfigurationRoot configurationRoot)
            : base(configurationRoot)
        {
        }

        public async Task<SwaggerResponse> PostNewUserAsync(User userRequest)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    // Create content
                    var content = new StringContent(JsonConvert.SerializeObject(userRequest), Encoding.UTF8, this.JsonMediaType);

                    // Create request
                    var response = await client.PostAsync(usersServiceUrl, content);

                    return await this.CreateSwaggerResponse(response);
                }
            }
            catch (Exception ex)
            {
                throw new SwaggerException(ex.Message, ex);
            }
        }

        public async Task<SwaggerResponse<UserListResponse>> GetUserListAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);

                    // Response
                    var response = await client.GetAsync(usersServiceUrl);

                    return await this.CreateGenericSwaggerResponse<UserListResponse>(response);
                }
            }
            catch (Exception ex)
            {
                throw new SwaggerException(ex.Message, ex);
            }
        }
    }
}
