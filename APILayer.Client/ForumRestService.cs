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
            var getForumMessageRequestUrl = string.IsNullOrEmpty(theme) ? forumServiceUrl : $"{forumServiceUrl}{themeAttribute}{theme}";

            using (var response = await this.GetAsync(getForumMessageRequestUrl))
            {
                return await this.CreateGenericSwaggerResponse<ForumMessagesResponse>(response);
            }
        }
    }
}
