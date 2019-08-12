using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.UserService;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.AcceptanceTests.Steps
{
    [Binding]
    public class UsersServiceSteps : StepBase
    {
        private readonly IUsersServiceRestApi usersServiceRestApi;
        private User userRequest;
        private SwaggerResponse response;
        private SwaggerResponse<UserListResponse> userListResponse;

        public UsersServiceSteps(IUsersServiceRestApi usersServiceRestApi)
        {
            this.usersServiceRestApi = usersServiceRestApi;
        }

        [Given(@"The forum receives a request for creating a user with the following properties")]
        public async Task GivenTheForumReceivesARequestForCreatingAUserWithTheFollowingProperties(Table table)
        {
            this.userRequest = table.CreateInstance<User>();
            this.response = await this.usersServiceRestApi.PostNewUserAsync(this.userRequest);
        }

        [Given(@"The forum receives a request for obtaining the user list")]
        public async Task GivenTheForumReceivesARequestForCreatingAUserWithTheFollowingProperties()
        {
            this.userListResponse = await this.usersServiceRestApi.GetUserListAsync();
        }

        [Then(@"The status code of the users service is '(.*)'")]
        public void ThenTheStatusCodeOfTheUsersServiceIs(string expectedCode)
        {
            var realCode = this.response.StatusCode;
            realCode.Should().Be(expectedCode, $"Real code {realCode} --- Expected code {expectedCode}");
        }
    }
}
