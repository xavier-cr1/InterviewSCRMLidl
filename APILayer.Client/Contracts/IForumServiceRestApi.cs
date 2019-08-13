using APILayer.Entities;
using APILayer.Entities.ForumService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IForumServiceRestApi
    {
        Task<SwaggerResponse> PostNewForumMessageAsync(ForumMessage forumRequest);

        Task<SwaggerResponse<ForumMessagesResponse>> GetForumMessagesListByThemeAsync(string theme);
    }
}
