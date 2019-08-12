using System;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests.Steps.MessageService
{
    [Binding]
    public class MessagesServiceSteps
    {
        [Given(@"The user requests messages")]
        public void GivenTheUserRequestsMessages()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
