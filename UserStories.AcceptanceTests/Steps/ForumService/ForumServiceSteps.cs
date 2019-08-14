using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.ForumService;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.AcceptanceTests.Steps.ForumService
{
    [Binding]
    public class ForumServiceSteps : StepBase
    {
        private readonly IForumServiceRestApi forumMessagesServiceRestApi;
        private ForumMessage forumRequest;
        private SwaggerResponse response;
        private SwaggerResponse<ForumMessagesResponse> forumListResponse;

        public ForumServiceSteps(IForumServiceRestApi forumMessagesServiceRestApi)
        {
            this.forumMessagesServiceRestApi = forumMessagesServiceRestApi;
        }

        [Given(@"The forum receives a request for creating a message with the following properties")]
        public async Task GivenTheForumReceivesARequestForCreatingAMessageWithTheFollowingProperties(Table table)
        {
            this.forumRequest = table.CreateInstance<ForumMessage>();
            this.response = await this.forumMessagesServiceRestApi.PostNewForumMessageAsync(this.forumRequest);
        }

        [When(@"The forum receives a request for obtaining the forum messages list by theme '(.*)'")]
        public async Task WhenTheForumReceivesARequestForObtainingTheForumMessagesListByTheme(string theme)
        {
            this.forumListResponse = await this.forumMessagesServiceRestApi.GetForumMessagesListByThemeAsync(theme);
        }

        [When(@"The status code for getting the forum messages list is '(.*)'")]
        public void WhenTheStatusCodeForGettingTheForumMessagesListIs(string expectedStatusCode)
        {
            var realStatusCodeForumMessage = this.forumListResponse.StatusCode;
            realStatusCodeForumMessage.Should().Be(expectedStatusCode, $"Real code {realStatusCodeForumMessage} --- Expected code {expectedStatusCode}");
        }

        [Then(@"The status code for creating a message in the forum is '(.*)'")]
        public void ThenTheStatusCodeForCreatingAMessageInTheForumIs(string expectedStatusCode)
        {
            var realStatusCodePostForum = this.response.StatusCode;
            realStatusCodePostForum.Should().Be(expectedStatusCode, $"Real code {realStatusCodePostForum} --- Expected code {expectedStatusCode}");
        }

        [Then(@"The message with the subject '(.*)' is in the theme '(.*)' list")]
        public void ThenTheMessageWithTheSubjectIsInTheThemeList(string subject, string theme)
        {
            switch (theme)
            {
                case "Automation":
                    var realAutomationMessageResult = this.forumListResponse.Result.AutomationMessage;
                    realAutomationMessageResult.Should().Contain(automationMessage => automationMessage.Subject.Equals(subject));
                    break;
                case "Development":
                    var realDevelopmentMessageResult = this.forumListResponse.Result.DevelopmentMessage;
                    realDevelopmentMessageResult.Should().Contain(developmentMessage => developmentMessage.Subject.Equals(subject));
                    break;
                case "Security":
                    var realSecurityMessageResult = this.forumListResponse.Result.SecurityMessage;
                    realSecurityMessageResult.Should().Contain(securityMessage => securityMessage.Subject.Equals(subject));
                    break;
                case "Testing":
                    var realTestingMessageResult = this.forumListResponse.Result.TestingMessage;
                    realTestingMessageResult.Should().Contain(testingMessage => testingMessage.Subject.Equals(subject));
                    break;
                default:
                    theme.Should().BeOneOf("Automation", "Development", "Security", "Testing");
                    break;
            }
        }
    }
}
