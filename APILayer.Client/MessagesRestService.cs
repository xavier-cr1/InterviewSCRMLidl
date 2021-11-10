using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.MessageService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace APILayer.Client
{
    public class MessagesRestService : RestApiBase, IMessageServiceRestApi
    {
        private string messageServiceUrl => this.ConfigurationRoot.GetSection("AppConfiguration")["MessageAPIService"];

        public MessagesRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper, HttpClient httpClient)
            : base(configurationRoot, specFlowOutputHelper, httpClient)
        {
        }

        public async Task<SwaggerResponse> SendPrivateMessageAsync(string username, PrivateMessage privateMessageRequest)
        {
            using (var response = await this.PostAsync($"{messageServiceUrl}/{username}", privateMessageRequest))
            {
                return await this.CreateSwaggerResponse(response);
            }
        }

        public async Task<SwaggerResponse<PrivateMessageResponse>> GetPrivateMessageListAsync(string username, string password)
        {
            using (var response = await this.GetAsync($"{messageServiceUrl}/{username}"))
            {
                if(!response.IsSuccessStatusCode)
                {
                    var emptyPrivateMessageResponse = new PrivateMessageResponse { Messages = new ObservableCollection<PrivateMessage>(), Username = "" };
                    var emptyPrivateMessageSwaggerResponse = new SwaggerResponse<PrivateMessageResponse>(((int)response.StatusCode).ToString(), emptyPrivateMessageResponse) { Body = response.ReasonPhrase };
                    return emptyPrivateMessageSwaggerResponse;
                }
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
                return await this.CreateGenericSwaggerResponse<PrivateMessageResponse>(response);
            }
        }
    }
}
