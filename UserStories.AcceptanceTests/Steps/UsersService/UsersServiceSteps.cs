using System;
using TechTalk.SpecFlow;

namespace UserStories.AcceptanceTests.Steps
{
    [Binding]
    public class UsersServiceSteps
    {
        [Given(@"The user requests")]
        public void GivenTheUserRequests()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
