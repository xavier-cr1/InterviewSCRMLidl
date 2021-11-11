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

        [Given(@"The username '(.*)' sends a private message with the following properties")]
        public async Task GivenTheUsernameReceivesAPrivateMessageWithTheFollowingProperties(string username, Table table)
        {
            this.forumRequest = table.CreateInstance<PrivateMessage>();
            this.response = await this.messagesServiceRestApi.SendPrivateMessageAsync(username, this.forumRequest);
        }

        [When(@"The username '(.*)' and password '(.*)' sends a request to obtain its private message list")]
        public async Task WhenTheUserRequestToObtainItsPrivateMessageList(string username, string password)
        {
            this.messagesListResponse = await this.messagesServiceRestApi.GetPrivateMessageListAsync(username, password);
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

        [Then(@"The message list has the new message '(.*)'")]
        public void TheMessageListHasTheNewMessage(string newMessage)
        {
            var realPrivateMessageList = this.messagesListResponse.Result.Messages;
            realPrivateMessageList.Should().Contain(privateMessage => privateMessage.Message.Equals(newMessage));
        }
    }
}
