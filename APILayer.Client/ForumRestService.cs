using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.ForumService;
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
                if (!response.IsSuccessStatusCode)
                {
                    var emptyGetForumMessagesResponse = new ForumMessagesResponse { AutomationMessage = new ObservableCollection<AutomationMessage>(),
                        SecurityMessage = new ObservableCollection<SecurityMessage>(), DevelopmentMessage = new ObservableCollection<DevelopmentMessage>(),
                        TestingMessage = new ObservableCollection<TestingMessage>()
                    };

                    var emptyGetForumMessagesSwaggerResponse = new SwaggerResponse<ForumMessagesResponse>(((int)response.StatusCode).ToString(), emptyGetForumMessagesResponse) { Body = response.ReasonPhrase };
                    return emptyGetForumMessagesSwaggerResponse;
                }

                return await this.CreateGenericSwaggerResponse<ForumMessagesResponse>(response);
            }
        }
    }
}
