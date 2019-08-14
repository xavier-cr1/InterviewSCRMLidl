using APILayer.Entities;
using APILayer.Entities.UserService;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IUsersServiceRestApi
    {
        /// <summary>Posts a new user asynchronous.</summary>
        /// <param name="userRequest">The user object.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse> PostNewUserAsync(User userRequest);

        /// <summary>Gets the user list asynchronous.</summary>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<SwaggerResponse<UserListResponse>> GetUserListAsync();
    }
}
