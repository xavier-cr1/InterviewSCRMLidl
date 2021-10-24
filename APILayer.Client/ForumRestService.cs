using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.Commom;
using APILayer.Entities.ForumService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace APILayer.Client
{
    public class ForumRestService : RestApiBase, IForumServiceRestApi
    {
        private string forumServiceUrl => this.ConfigurationRoot.GetSection("AppConfiguration")["ForumAPIService"];

        private readonly string themeAttribute = "?theme=";

        public ForumRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper)
            : base(configurationRoot, specFlowOutputHelper)
        {
        }

        public async Task<SwaggerResponse> PostNewForumMessageAsync(ForumMessage forumRequest)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    // Create content
                    var content = new StringContent(JsonConvert.SerializeObject(forumRequest), Encoding.UTF8, this.JsonMediaType);

                    // Create request
                    this._specFlowOutputHelper.WriteLine($"calling endpoint: {forumServiceUrl}");
                    var response = await client.PostAsync(forumServiceUrl, content);

                    return await this.CreateSwaggerResponse(response);
                }
            }
            catch (SwaggerException sWex)
            {
                this._specFlowOutputHelper.WriteLine($"swagger exception after calling the endpoint: {forumServiceUrl}");
                throw new HttpRequestException(sWex.Message, sWex);
            }
            catch (Exception ex)
            {
                this._specFlowOutputHelper.WriteLine($"Unhandled exception: {ex.Message} when calling the endpoint: {forumServiceUrl}");
                throw;
            }
        }

        public async Task<SwaggerResponse<ForumMessagesResponse>> GetForumMessagesListByThemeAsync(string theme)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);

                    // Response
                    this._specFlowOutputHelper.WriteLine($"calling endpoint: {forumServiceUrl}");
                    var response = string.IsNullOrEmpty(theme) ? await client.GetAsync(forumServiceUrl) : await client.GetAsync($"{forumServiceUrl}{themeAttribute}{theme}");

                    return await this.CreateGenericSwaggerResponse<ForumMessagesResponse>(response);
                }
            }
            catch (SwaggerException sWex)
            {
                this._specFlowOutputHelper.WriteLine($"swagger exception after calling the endpoint: {forumServiceUrl}");
                throw new HttpRequestException(sWex.Message, sWex);
            }
            catch (Exception ex)
            {
                this._specFlowOutputHelper.WriteLine($"Unhandled exception: {ex.Message} when calling the endpoint: {forumServiceUrl}");
                throw;
            }
        }
    }
}
