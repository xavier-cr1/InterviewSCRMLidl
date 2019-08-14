using APILayer.Entities;
using APILayer.Entities.ForumService;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IForumServiceRestApi
    {
        /// <summary>Post a new forum message asynchronous.</summary>
        /// <param name="forumRequest">The forum message object.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse> PostNewForumMessageAsync(ForumMessage forumRequest);

        /// <summary>Gets the forum messages list by theme asynchronous. Or obtains the full list if theme is empty</summary>
        /// <param name="theme">The theme.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse<ForumMessagesResponse>> GetForumMessagesListByThemeAsync(string theme);
    }
}
