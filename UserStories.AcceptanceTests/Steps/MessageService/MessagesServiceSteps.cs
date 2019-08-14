using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.MessageService;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.AcceptanceTests.Steps.MessageService
{
    [Binding]
    public class MessagesServiceSteps : StepBase
    {
        private readonly IMessageServiceRestApi messagesServiceRestApi;
        private PrivateMessage forumRequest;
        private SwaggerResponse response;
        private SwaggerResponse<PrivateMessageResponse> messagesListResponse;

        public MessagesServiceSteps(IMessageServiceRestApi messagesServiceRestApi)
        {
            this.messagesServiceRestApi = messagesServiceRestApi;
        }

        [Given(@"The username '(.*)' receives a private message with the following properties")]
        public async Task GivenTheUsernameReceivesAPrivateMessageWithTheFollowingProperties(string username, Table table)
        {
            this.forumRequest = table.CreateInstance<PrivateMessage>();
            this.response = await this.messagesServiceRestApi.SendPrivateMessageAsync(username, this.forumRequest);
        }

        [When(@"The username '(.*)' and password '(.*)' obtains its private message list with the new message '(.*)'")]
        public async Task WhenTheUsernameObtainsItsPrivateMessageList(string username, string password, string newMessage)
        {
            this.messagesListResponse = await this.messagesServiceRestApi.GetPrivateMessageListAsync(username, password);
            var realPrivateMessageList = this.messagesListResponse.Result.Messages;
            realPrivateMessageList.Should().Contain(privateMessage => privateMessage.Message.Equals(newMessage));
        }

        [When(@"The status code for obtaining the private message list is '(.*)'")]
        public void WhenTheStatusCodeForObtainingThePrivateMessageListIs(string expectedStatusCode)
        {
            var realStatusCodePrivateMessageList = this.messagesListResponse.StatusCode;
            realStatusCodePrivateMessageList.Should().Be(expectedStatusCode, $"Real code {realStatusCodePrivateMessageList} --- Expected code {expectedStatusCode}");
        }

        [Then(@"The status code for sending a private message in the forum is '(.*)'")]
        public void ThenTheStatusCodeForSendingAPrivateMessageInTheForumIs(string expectedStatusCode)
        {
            var realStatusCodePostPrivateMessage = this.response.StatusCode;
            realStatusCodePostPrivateMessage.Should().Be(expectedStatusCode, $"Real code {realStatusCodePostPrivateMessage} --- Expected code {expectedStatusCode}");
        }
    }
}
