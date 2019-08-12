using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.Commom;
using APILayer.Entities.UserService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
                using (var request = new HttpRequestMessage())
                {
                    client.Timeout = TimeSpan.FromSeconds(180);

                    // Create content
                    var content = new StringContent(JsonConvert.SerializeObject(userRequest));
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse(this.JsonMediaType);

                    // Create request
                    request.Content = content;
                    request.Method = new HttpMethod("POST");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.JsonMediaType));

                    request.RequestUri = new Uri(usersServiceUrl, UriKind.RelativeOrAbsolute);

                    // Response
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

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
                using (var request = new HttpRequestMessage())
                {
                    // Create request
                    request.Method = new HttpMethod("GET");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.JsonMediaType));

                    request.RequestUri = new Uri(usersServiceUrl, UriKind.RelativeOrAbsolute);

                    // Response
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);

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
