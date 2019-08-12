using System;
using System.Collections.Generic;
using System.Text;
using APILayer.Client;
using APILayer.Client.Contracts;
using BoDi;

namespace CrossLayer.Containers
{
    public class AppContainer : IAppContainer
    {
        public void RegisterAPIs(IObjectContainer objectContainer)
        {
            //Register API's
            objectContainer.RegisterTypeAs<UserRestService, IUsersServiceRestApi>();
            objectContainer.RegisterTypeAs<MessagesRestService, IMessageServiceRestApi>();
            objectContainer.RegisterTypeAs<ForumRestService, IForumServiceRestApi>();
        }
    }
}
