using APILayer.Entities;
using APILayer.Entities.MessageService;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IMessageServiceRestApi
    {
        /// <summary>Sends the private message to a username asynchronous. No basic auth needed.</summary>
        /// <param name="username">The username.</param>
        /// <param name="privateMessageRequest">The private message object request.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse> SendPrivateMessageAsync(string username, PrivateMessage privateMessageRequest);

        /// <summary>Gets the private message list asynchronous. Basic auth is needed in the headers, username and password</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse<PrivateMessageResponse>> GetPrivateMessageListAsync(string username, string password);
    }
}
