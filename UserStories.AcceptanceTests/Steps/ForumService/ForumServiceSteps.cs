using System;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests.Steps.ForumService
{
    [Binding]
    public class ForumServiceSteps
    {
        [Given(@"The user requests forum")]
        public void GivenTheUserRequestsForum()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
