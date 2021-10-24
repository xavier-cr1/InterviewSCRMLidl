using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.Commom;
using APILayer.Entities.MessageService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

        public MessagesRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper)
            : base(configurationRoot, specFlowOutputHelper)
        {
        }

        public async Task<SwaggerResponse> SendPrivateMessageAsync(string username, PrivateMessage privateMessageRequest)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    // Create content
                    var content = new StringContent(JsonConvert.SerializeObject(privateMessageRequest), Encoding.UTF8, this.JsonMediaType);

                    // Create request
                    var response = await client.PostAsync($"{messageServiceUrl}/{username}", content);

                    this._specFlowOutputHelper.WriteLine($"calling endpoint: {messageServiceUrl}");
                    return await this.CreateSwaggerResponse(response);
                }
            }
            catch (SwaggerException sWex)
            {
                this._specFlowOutputHelper.WriteLine($"swagger exception after calling the endpoint: {messageServiceUrl}");
                throw new HttpRequestException(sWex.Message, sWex);
            }
            catch (Exception ex)
            {
                this._specFlowOutputHelper.WriteLine($"Unhandled exception: {ex.Message} when calling the endpoint: {messageServiceUrl}");
                throw;
            }
        }

        public async Task<SwaggerResponse<PrivateMessageResponse>> GetPrivateMessageListAsync(string username, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);

                    // Response
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                    var response = await client.GetAsync($"{messageServiceUrl}/{username}");

                    this._specFlowOutputHelper.WriteLine($"calling endpoint: {messageServiceUrl}");
                    return await this.CreateGenericSwaggerResponse<PrivateMessageResponse>(response);
                }
            }
            catch (SwaggerException sWex)
            {
                this._specFlowOutputHelper.WriteLine($"swagger exception after calling the endpoint: {messageServiceUrl}");
                throw new HttpRequestException(sWex.Message, sWex);
            }
            catch (Exception ex)
            {
                this._specFlowOutputHelper.WriteLine($"Unhandled exception: {ex.Message} when calling the endpoint: {messageServiceUrl}");
                throw;
            }
        }
    }
}
