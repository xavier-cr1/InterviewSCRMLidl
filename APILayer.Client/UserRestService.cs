using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.UserService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace APILayer.Client
{
    public class UserRestService : RestApiBase, IUsersServiceRestApi
    {
        private string usersServiceUrl => this.ConfigurationRoot.GetSection("AppConfiguration")["UsersAPIService"];

        public UserRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper, HttpClient httpClient)
            : base(configurationRoot, specFlowOutputHelper, httpClient)
        {
        }

        public async Task<SwaggerResponse> PostNewUserAsync(User userRequest)
        {
            using (var response = await this.PostAsync($"{usersServiceUrl}", userRequest))
            {
                return await this.CreateSwaggerResponse(response);
            }
        }

        public async Task<SwaggerResponse<UserListResponse>> GetUserListAsync()
        {
            using (var response = await this.GetAsync($"{usersServiceUrl}"))
            {
                if(!response.IsSuccessStatusCode)
                {
                    var emptyUserListResponse = new UserListResponse { Users = new ObservableCollection<User>() };
                    var emptyUserListSwaggerResponse = new SwaggerResponse<UserListResponse>(((int)response.StatusCode).ToString(), emptyUserListResponse) { Body = response.ReasonPhrase };

                    return emptyUserListSwaggerResponse;
                }

                return await this.CreateGenericSwaggerResponse<UserListResponse>(response);
            }
        }
    }
}
