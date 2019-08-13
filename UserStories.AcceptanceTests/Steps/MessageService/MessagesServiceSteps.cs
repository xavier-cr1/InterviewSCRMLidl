using System;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests.Steps.MessageService
{
    [Binding]
    public class MessagesServiceSteps
    {
        [Given(@"The user with username ''(.*)'' is authorised")]
        public void GivenTheUserWithUsernameIsAuthorised(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"The username '(.*)' receives a private message with the following properties")]
        public void GivenTheUsernameReceivesAPrivateMessageWithTheFollowingProperties(string p0, Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"The username '(.*)' obtains its private message list")]
        public void WhenTheUsernameObtainsItsPrivateMessageList(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"The status code for obtaining the private message list is '(.*)'")]
        public void WhenTheStatusCodeForObtainingThePrivateMessageListIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The status code for sending a private message in the forum is '(.*)'")]
        public void ThenTheStatusCodeForSendingAPrivateMessageInTheForumIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
