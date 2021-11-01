using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.ForumService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

        public ForumRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper, HttpClient httpClient)
            : base(configurationRoot, specFlowOutputHelper, httpClient)
        {
        }

        public async Task<SwaggerResponse> PostNewForumMessageAsync(ForumMessage forumRequest)
        {
            using (var response = await this.PostAsync(forumServiceUrl, forumRequest))
            {
                return await this.CreateSwaggerResponse(response);
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
            catch (Exception ex)
            {
                this._specFlowOutputHelper.WriteLine($"Unhandled exception: {ex.Message} when calling the endpoint: {forumServiceUrl}");
                throw;
            }
        }
    }
}
